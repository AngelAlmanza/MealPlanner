using FluentValidation;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitMeasureController : ControllerBase
    {
        private readonly ICommonService<UnitMeasureDto, UnitMeasureInsertDto, UnitMeasureUpdateDto> _service;
        private readonly IValidator<UnitMeasureInsertDto> _unitMeasureInsertValidator;
        private readonly IValidator<UnitMeasureUpdateDto> _unitMeasureUpdateValidator;

        public UnitMeasureController(
                ICommonService<UnitMeasureDto, UnitMeasureInsertDto, UnitMeasureUpdateDto> service,
                IValidator<UnitMeasureInsertDto> unitMeasureInsertValidator,
                IValidator<UnitMeasureUpdateDto> unitMeasureUpdateValidator
            )
        {
            _service = service;
            _unitMeasureInsertValidator = unitMeasureInsertValidator;
            _unitMeasureUpdateValidator = unitMeasureUpdateValidator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<UnitMeasureDto>> Get()
            => await _service.Get();

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitMeasureDto>> GetById(int id)
        {
            var unitMeasure = await _service.GetById(id);
            if (unitMeasure == null)
            {
                return NotFound();
            }
            return Ok(unitMeasure);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UnitMeasureDto>> Create(UnitMeasureInsertDto unitMeasureInsertDto)
        {
            var validationResult = await _unitMeasureInsertValidator.ValidateAsync(unitMeasureInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(unitMeasureInsertDto))
            {
                return BadRequest(_service.Errors);
            }
            var unitMeasureDto = await _service.Add(unitMeasureInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = unitMeasureDto.Id }, unitMeasureDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UnitMeasureDto>> Update(int id, UnitMeasureUpdateDto unitMeasureUpdateDto)
        {
            var validationResult = await _unitMeasureUpdateValidator.ValidateAsync(unitMeasureUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(unitMeasureUpdateDto))
            {
                return BadRequest(_service.Errors);
            }
            var unitMeasureDto = await _service.Update(id, unitMeasureUpdateDto);
            return Ok(unitMeasureDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UnitMeasureDto>> Delete(int id)
        {
            var unitMeasure = await _service.Delete(id);
            if (unitMeasure == null)
            {
                return NotFound();
            }
            return Ok(unitMeasure);
        }
    }
}
