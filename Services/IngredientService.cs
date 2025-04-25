using AutoMapper;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Services
{
    public class IngredientService : ICommonService<IngredientDto, IngredientInsertDto, IngredientUpdateDto>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public List<string> Errors { get; private set; }

        public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<IngredientDto>> Get()
        {
            var ingredients = await _unitOfWork.IngredientRepository.Get();
            return ingredients.Select(i => _mapper.Map<IngredientDto>(i));
        }

        public async Task<IngredientDto> GetById(int id)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetById(id);
            if (ingredient == null)
            {
                Errors.Add($"Ingredient with ID {id} not found.");
                return null;
            }
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto> Add(IngredientInsertDto insertDto)
        {
            var ingredient = _mapper.Map<Ingredient>(insertDto);
            await _unitOfWork.IngredientRepository.Add(ingredient);
            await _unitOfWork.Save();
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto> Update(int id, IngredientUpdateDto updateDto)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetById(id);
            if (ingredient == null)
            {
                Errors.Add($"Ingredient with ID {id} not found.");
                return null;
            }
            ingredient = _mapper.Map<IngredientUpdateDto, Ingredient>(updateDto, ingredient);
            _unitOfWork.IngredientRepository.Update(ingredient);
            await _unitOfWork.Save();
            ingredient = await _unitOfWork.IngredientRepository.GetById(id);
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto> Delete(int id)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetById(id);
            if (ingredient == null)
            {
                Errors.Add($"Ingredient with ID {id} not found.");
                return null;
            }
            var ingredientDto = _mapper.Map<IngredientDto>(ingredient);
            _unitOfWork.IngredientRepository.Delete(ingredient);
            await _unitOfWork.Save();
            return ingredientDto;
        }

        public bool Validate(IngredientInsertDto insertDto)
        {
            if (_unitOfWork.IngredientRepository.Search(i => i.Name == insertDto.Name).Any())
            {
                Errors.Add($"Ingredient with name {insertDto.Name} already exists.");
                return false;
            }

            return true;
        }

        public bool Validate(IngredientUpdateDto updateDto)
        {
            if (_unitOfWork.IngredientRepository.Search(i => i.Name == updateDto.Name && i.Id != updateDto.Id).Any())
            {
                Errors.Add($"Ingredient with name {updateDto.Name} already exists.");
                return false;
            }

            return true;
        }
    }
}
