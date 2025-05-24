using AutoMapper;
using MealPlannerApi.Data;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Services
{
    public class RecipeInstanceService : ICommonService<RecipeInstanceDto, RecipeInstanceInsertDto, RecipeInstanceUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public List<string> Errors { get; private set; }
        
        public RecipeInstanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<RecipeInstanceDto>> Get()
        {
            var recipeInstances = await _unitOfWork.RecipeInstanceRepository.Get();
            return recipeInstances.Select(ri => _mapper.Map<RecipeInstanceDto>(ri));
        }

        public async Task<RecipeInstanceDto> GetById(int id)
        {
            var recipeInstance = await _unitOfWork.RecipeInstanceRepository.GetById(id);
            if (recipeInstance == null)
            {
                Errors.Add($"RecipeInstance with ID {id} not found.");
                return null;
            }
            return _mapper.Map<RecipeInstanceDto>(recipeInstance);
        }

        public async Task<RecipeInstanceDto> Add(RecipeInstanceInsertDto insertDto)
        {
            var recipeInstance = _mapper.Map<RecipeInstance>(insertDto);
            await _unitOfWork.RecipeInstanceRepository.Add(recipeInstance);
            await _unitOfWork.Save();
            return _mapper.Map<RecipeInstanceDto>(recipeInstance);
        }

        public async Task<RecipeInstanceDto> Update(int id, RecipeInstanceUpdateDto updateDto)
        {
            var recipeInstance = await _unitOfWork.RecipeInstanceRepository.GetById(id);
            if (recipeInstance == null)
            {
                Errors.Add($"RecipeInstance with ID {id} not found.");
                return null;
            }
            recipeInstance = _mapper.Map<RecipeInstanceUpdateDto, RecipeInstance>(updateDto, recipeInstance);
            _unitOfWork.RecipeInstanceRepository.Update(recipeInstance);
            await _unitOfWork.Save();
            return _mapper.Map<RecipeInstanceDto>(recipeInstance);
        }

        public async Task<RecipeInstanceDto> Delete(int id)
        {
            var recipeInstance = await _unitOfWork.RecipeInstanceRepository.GetById(id);
            if (recipeInstance == null)
            {
                Errors.Add($"RecipeInstance with ID {id} not found.");
                return null;
            }
            var recipeInstanceDto = _mapper.Map<RecipeInstanceDto>(recipeInstance);
            _unitOfWork.RecipeInstanceRepository.Delete(recipeInstance);
            await _unitOfWork.Save();
            return recipeInstanceDto;
        }

        public bool Validate(RecipeInstanceInsertDto insertDto)
        {
            return true;
        }

        public bool Validate(RecipeInstanceUpdateDto updateDto)
        {
            return true;
        }
    }
}