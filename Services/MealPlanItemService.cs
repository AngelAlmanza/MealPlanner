using AutoMapper;
using MealPlannerApi.Data;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Services
{
    public class MealPlanItemService : ICommonService<MealPlanItemDto, MealPlanItemInsertDto, MealPlanItemUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public List<string> Errors { get; set; }

        public MealPlanItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<MealPlanItemDto>> Get()
        {
            var mealPlanItems = await _unitOfWork.MealPlanItemRepository.Get();
            return mealPlanItems.Select(mpi => _mapper.Map<MealPlanItemDto>(mpi));
        }

        public async Task<MealPlanItemDto> GetById(int id)
        {
            var mealPlanItem = await _unitOfWork.MealPlanItemRepository.GetById(id);

            if (mealPlanItem == null)
            {
                Errors.Add($"Meal Plan Item with ID {id} not found.");
                return null;
            }

            return _mapper.Map<MealPlanItemDto>(mealPlanItem);
        }
        
        public async Task<MealPlanItemDto> Add(MealPlanItemInsertDto insertDto)
        {
            var mealPlanItem = _mapper.Map<MealPlanItem>(insertDto);
            await _unitOfWork.MealPlanItemRepository.Add(mealPlanItem);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanItemDto>(mealPlanItem);
        }
        
        public async Task<MealPlanItemDto> Update(int id, MealPlanItemUpdateDto updateDto)
        {
            var mealPlanItem = await _unitOfWork.MealPlanItemRepository.GetById(id);
            if (mealPlanItem == null)
            {
                Errors.Add($"Meal Plan Item with ID {id} not found.");
                return null;
            }

            mealPlanItem = _mapper.Map<MealPlanItemUpdateDto, MealPlanItem>(updateDto, mealPlanItem);
            _unitOfWork.MealPlanItemRepository.Update(mealPlanItem);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanItemDto>(mealPlanItem);
        }

        public async Task<MealPlanItemDto> Delete(int id)
        {
            var mealPlanItem = await _unitOfWork.MealPlanItemRepository.GetById(id);
            if (mealPlanItem == null)
            {
                Errors.Add($"Meal Plan Item with ID {id} not found.");
                return null;
            }

            _unitOfWork.MealPlanItemRepository.Delete(mealPlanItem);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanItemDto>(mealPlanItem);
        }

        public bool Validate(MealPlanItemInsertDto dto)
        {
            return true;
        }

        public bool Validate(MealPlanItemUpdateDto dto)
        {
            return true;
        }
    }
}