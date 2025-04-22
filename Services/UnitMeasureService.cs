using AutoMapper;
using MealPlannerApi.Data;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Services
{
    public class UnitMeasureService : ICommonService<UnitMeasureDto, UnitMeasureInsertDto, UnitMeasureUpdateDto>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public List<string> Errors { get; private set; }

        public UnitMeasureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<UnitMeasureDto>> Get()
        {
            var unitMeasures = await _unitOfWork.UnitMeasureRepository.Get();
            return unitMeasures.Select(um => _mapper.Map<UnitMeasureDto>(um));
        }

        public async Task<UnitMeasureDto> GetById(int id)
        {
            var unitMeasure = await _unitOfWork.UnitMeasureRepository.GetById(id);
            if (unitMeasure == null)
            {
                Errors.Add($"Unit measure with ID {id} not found.");
                return null;
            }
            return _mapper.Map<UnitMeasureDto>(unitMeasure);
        }

        public async Task<UnitMeasureDto> Add(UnitMeasureInsertDto insertDto)
        {
            var unitMeasure = _mapper.Map<UnitMeasure>(insertDto);
            await _unitOfWork.UnitMeasureRepository.Add(unitMeasure);
            await _unitOfWork.Save();
            return _mapper.Map<UnitMeasureDto>(unitMeasure);
        }

        public async Task<UnitMeasureDto> Update(int id, UnitMeasureUpdateDto updateDto)
        {
            var unitMeasure = await _unitOfWork.UnitMeasureRepository.GetById(id);
            if (unitMeasure == null)
            {
                Errors.Add($"Unit measure with ID {id} not found.");
                return null;
            }
            unitMeasure = _mapper.Map<UnitMeasureUpdateDto, UnitMeasure>(updateDto, unitMeasure);
            _unitOfWork.UnitMeasureRepository.Update(unitMeasure);
            await _unitOfWork.Save();
            return _mapper.Map<UnitMeasureDto>(unitMeasure);
        }

        public async Task<UnitMeasureDto> Delete(int id)
        {
            var unitMeasure = await _unitOfWork.UnitMeasureRepository.GetById(id);
            if (unitMeasure == null)
            {
                Errors.Add($"Unit measure with ID {id} not found.");
                return null;
            }
            var unitMeasureDto = _mapper.Map<UnitMeasureDto>(unitMeasure);
            _unitOfWork.UnitMeasureRepository.Delete(unitMeasure);
            await _unitOfWork.Save();
            return unitMeasureDto;
        }

        public bool Validate(UnitMeasureInsertDto insertDto)
        {
            if (_unitOfWork.UnitMeasureRepository.Search(um => um.Name == insertDto.Name).Any())
            {
                Errors.Add($"Unit measure with name {insertDto.Name} already exists.");
                return false;
            }
            return true;
        }

        public bool Validate(UnitMeasureUpdateDto updateDto)
        {
            if (_unitOfWork.UnitMeasureRepository.Search(um => um.Id == updateDto.Id).Any() == false)
            {
                Errors.Add($"Unit measure with ID {updateDto.Id} not found.");
                return false;
            }

            if (_unitOfWork.UnitMeasureRepository.Search(um => um.Name == updateDto.Name && um.Id != updateDto.Id).Any())
            {
                Errors.Add($"Unit measure with name {updateDto.Name} already exists.");
                return false;
            }

            return true;
        }
    }
}
