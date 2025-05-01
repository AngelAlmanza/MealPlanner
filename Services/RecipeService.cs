using AutoMapper;
using MealPlannerApi.Data;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Services
{
    public class RecipeService : ICommonService<RecipeDto, RecipeInsertDto, RecipeUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public List<string> Errors { get; private set; }

        public RecipeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Errors = new List<string>();
        }
        
        public async Task<IEnumerable<RecipeDto>> Get()
        {
            var recipes = await _unitOfWork.RecipeRepository.Get();
            return recipes.Select(r => _mapper.Map<RecipeDto>(r));
        }
        
        public async Task<RecipeDto> GetById(int id)
        {
            var recipe = await _unitOfWork.RecipeRepository.GetById(id);
            if (recipe == null)
            {
                Errors.Add($"Recipe with ID {id} not found.");
                return null;
            }
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<RecipeDto> Add(RecipeInsertDto insertDto)
        {
            var recipe = _mapper.Map<Recipe>(insertDto);
            await _unitOfWork.RecipeRepository.Add(recipe);
            await _unitOfWork.Save();
            var ingredients = insertDto.Ingredients.Select(dto =>
            {
                var ingredient = _mapper.Map<RecipeIngredient>(dto);
                ingredient.RecipeId = recipe.Id;
                return ingredient;
            }).ToList();
            await _unitOfWork.RecipeIngredientRepository.AddRange(ingredients);
            await _unitOfWork.Save();
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<RecipeDto> Update(int id, RecipeUpdateDto updateDto)
        {
            var recipe = await _unitOfWork.RecipeRepository.GetById(id);
            if (recipe == null)
            {
                Errors.Add($"Recipe with ID {id} not found.");
                return null;
            }
            recipe = _mapper.Map<RecipeUpdateDto, Recipe>(updateDto, recipe);
            _unitOfWork.RecipeRepository.Update(recipe);
            await _unitOfWork.Save();
            var ingredients = updateDto.Ingredients.Select(dto =>
            {
                var ingredient = _mapper.Map<RecipeIngredient>(dto);
                ingredient.RecipeId = recipe.Id;
                return ingredient;
            }).ToList();
            await _unitOfWork.RecipeIngredientRepository.DeleteRange(id);
            await _unitOfWork.RecipeIngredientRepository.AddRange(ingredients);
            await _unitOfWork.Save();
            return _mapper.Map<RecipeDto>(recipe);
        }
        
        public async Task<RecipeDto> Delete(int id)
        {
            var recipe = await _unitOfWork.RecipeRepository.GetById(id);
            if (recipe == null)
            {
                Errors.Add($"Recipe with ID {id} not found.");
                return null;
            }
            var recipeDto = _mapper.Map<RecipeDto>(recipe);
            _unitOfWork.RecipeRepository.Delete(recipe);
            await _unitOfWork.Save();
            return recipeDto;
        }

        public bool Validate(RecipeInsertDto insertDto)
        {
            return true;
        }

        public bool Validate(RecipeUpdateDto updateDto)
        {
            return true;
        }
    }
}