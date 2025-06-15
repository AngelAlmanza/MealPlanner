using AutoMapper;
using MealPlannerApi.Data;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Services
{
    public class MealPlanEntryService : ICommonService<MealPlanEntryDto, MealPlanEntryInsertDto, MealPlanEntryUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public List<string> Errors { get; private set; }
        
        public MealPlanEntryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<MealPlanEntryDto>> Get()
        {
            var mealPlanEntries = await _unitOfWork.MealPlanEntryRepository.Get();
            return mealPlanEntries.Select(mpe => _mapper.Map<MealPlanEntryDto>(mpe));
        }

        public async Task<MealPlanEntryDto> GetById(int id)
        {
            var mealPlanEntry = await _unitOfWork.MealPlanEntryRepository.GetById(id);
            if (mealPlanEntry == null)
            {
                Errors.Add($"MealPlanEntry with ID {id} not found.");
                return null;
            }
            return _mapper.Map<MealPlanEntryDto>(mealPlanEntry);
        }

        public async Task<MealPlanEntryDto> Add(MealPlanEntryInsertDto insertDto)
        {
            // First validate RecipeInstance exists and has enough servings
            var recipeInstance = await _unitOfWork.RecipeInstanceRepository.GetById(insertDto.RecipeInstanceId);
            if (recipeInstance == null)
            {
                Errors.Add($"RecipeInstance with ID {insertDto.RecipeInstanceId} not found.");
                return null;
            }

            if (recipeInstance.TotalServings < insertDto.ServingsUsed)
            {
                Errors.Add($"Not enough servings available in RecipeInstance {insertDto.RecipeInstanceId}. " +
                           $"Available: {recipeInstance.TotalServings}, Required: {insertDto.ServingsUsed}");
                return null;
            }
            
            // Validate if some servings are already used
            var existingEntries = _unitOfWork.MealPlanEntryRepository.Search(mpe => mpe.RecipeInstanceId == insertDto.RecipeInstanceId);
            var totalUsedServings = existingEntries.Sum(mpe => mpe.ServingsUsed);

            if ((totalUsedServings + insertDto.ServingsUsed) > recipeInstance.TotalServings)
            {
                Errors.Add($"Total servings used ({totalUsedServings + insertDto.ServingsUsed}) exceeds available servings " +
                           $"in RecipeInstance {insertDto.RecipeInstanceId} ({recipeInstance.TotalServings}).");
                return null;
            }
            
            var mealPlanEntry = _mapper.Map<MealPlanEntry>(insertDto);
            await _unitOfWork.MealPlanEntryRepository.Add(mealPlanEntry);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanEntryDto>(mealPlanEntry);
        }

        public async Task<MealPlanEntryDto> Update(int id, MealPlanEntryUpdateDto updateDto)
        {
            var mealPlanEntry = await _unitOfWork.MealPlanEntryRepository.GetById(id);
            
            if (mealPlanEntry == null)
            {
                Errors.Add($"MealPlanEntry with ID {id} not found.");
                return null;
            }
            
            // First validate RecipeInstance exists and has enough servings
            var recipeInstance = await _unitOfWork.RecipeInstanceRepository.GetById(updateDto.RecipeInstanceId);
            if (recipeInstance == null)
            {
                Errors.Add($"RecipeInstance with ID {updateDto.RecipeInstanceId} not found.");
                return null;
            }
            
            if (recipeInstance.TotalServings < updateDto.ServingsUsed)
            {
                Errors.Add($"Not enough servings available in RecipeInstance {updateDto.RecipeInstanceId}. " +
                           $"Available: {recipeInstance.TotalServings}, Required: {updateDto.ServingsUsed}");
                return null;
            }
            
            // Validate if some servings are already used
            var existingEntries = _unitOfWork.MealPlanEntryRepository.Search(mpe => mpe.RecipeInstanceId == updateDto.RecipeInstanceId && mpe.Id != id);
            var totalUsedServings = existingEntries.Sum(mpe => mpe.ServingsUsed);

            if ((totalUsedServings + updateDto.ServingsUsed) > recipeInstance.TotalServings)
            {
                Errors.Add($"Total servings used ({totalUsedServings + updateDto.ServingsUsed}) exceeds available servings " +
                           $"in RecipeInstance {updateDto.RecipeInstanceId} ({recipeInstance.TotalServings}).");
                return null;
            }
            
            mealPlanEntry = _mapper.Map<MealPlanEntryUpdateDto, MealPlanEntry>(updateDto, mealPlanEntry);
            _unitOfWork.MealPlanEntryRepository.Update(mealPlanEntry);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanEntryDto>(mealPlanEntry);
        }

        public async Task<MealPlanEntryDto> Delete(int id)
        {
            var mealPlanEntry = await _unitOfWork.MealPlanEntryRepository.GetById(id);
            if (mealPlanEntry == null)
            {
                Errors.Add($"MealPlanEntry with ID {id} not found.");
                return null;
            }

            _unitOfWork.MealPlanEntryRepository.Delete(mealPlanEntry);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanEntryDto>(mealPlanEntry);
        }

        public bool Validate(MealPlanEntryInsertDto dto)
        {
            return true;
        }

        public bool Validate(MealPlanEntryUpdateDto dto)
        {
            return true;
        }
    }
}