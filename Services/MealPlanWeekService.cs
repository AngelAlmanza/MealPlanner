using AutoMapper;
using MealPlannerApi.Data;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Services
{
    public class MealPlanWeekService : ICommonService<MealPlanWeekDto, MealPlanWeekInsertDto, MealPlanWeekUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public List<string> Errors { get; private set; }
        
        public MealPlanWeekService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Errors = new List<string>();
        }
        
        public async Task<IEnumerable<MealPlanWeekDto>> Get()
        {
            var mealPlanWeeks = await _unitOfWork.MealPlanWeekRepository.Get();
            return mealPlanWeeks.Select(mp => _mapper.Map<MealPlanWeekDto>(mp));
        }
        
        public async Task<MealPlanWeekDto> GetById(int id)
        {
            var mealPlanWeek = await _unitOfWork.MealPlanWeekRepository.GetById(id);
            if (mealPlanWeek == null)
            {
                Errors.Add($"MealPlanWeek with ID {id} not found.");
                return null;
            }
            return _mapper.Map<MealPlanWeekDto>(mealPlanWeek);
        }
        
        public async Task<MealPlanWeekDto> Add(MealPlanWeekInsertDto insertDto)
        {
            var mealPlanWeek = _mapper.Map<MealPlanWeek>(insertDto);
            await _unitOfWork.MealPlanWeekRepository.Add(mealPlanWeek);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanWeekDto>(mealPlanWeek);
        }

        public async Task<MealPlanWeekDto> Update(int id, MealPlanWeekUpdateDto updateDto)
        {
            var mealPlanWeek = await _unitOfWork.MealPlanWeekRepository.GetById(id);
            if (mealPlanWeek == null)
            {
                Errors.Add($"MealPlanWeek with ID {id} not found.");
                return null;
            }
            mealPlanWeek = _mapper.Map<MealPlanWeekUpdateDto, MealPlanWeek>(updateDto, mealPlanWeek);
            _unitOfWork.MealPlanWeekRepository.Update(mealPlanWeek);
            await _unitOfWork.Save();
            mealPlanWeek = await _unitOfWork.MealPlanWeekRepository.GetById(id);
            return _mapper.Map<MealPlanWeekDto>(mealPlanWeek);
        }

        public async Task<MealPlanWeekDto> Delete(int id)
        {
            var mealPlanWeek = await _unitOfWork.MealPlanWeekRepository.GetById(id);
            if (mealPlanWeek == null)
            {
                Errors.Add($"MealPlanWeek with ID {id} not found.");
                return null;
            }
            _unitOfWork.MealPlanWeekRepository.Delete(mealPlanWeek);
            await _unitOfWork.Save();
            return _mapper.Map<MealPlanWeekDto>(mealPlanWeek);
        }

        public bool Validate(MealPlanWeekInsertDto insertDto)
        {
            return true;
        }

        public bool Validate(MealPlanWeekUpdateDto updateDto)
        {
            return true;
        }
    }
}