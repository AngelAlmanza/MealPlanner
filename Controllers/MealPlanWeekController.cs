using FluentValidation;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanWeekController : ControllerBase
    {
        private readonly ICommonService<MealPlanWeekDto, MealPlanWeekInsertDto, MealPlanWeekUpdateDto> _service;
        private readonly IValidator<MealPlanWeekInsertDto> _mealPlanWeekInsertDtoValidator;
        private readonly IValidator<MealPlanWeekUpdateDto> _mealPlanWeekUpdateDtoValidator;
        
        public MealPlanWeekController(
            ICommonService<MealPlanWeekDto, MealPlanWeekInsertDto, MealPlanWeekUpdateDto> service,
            IValidator<MealPlanWeekInsertDto> mealPlanWeekInsertDtoValidator,
            IValidator<MealPlanWeekUpdateDto> mealPlanWeekUpdateDtoValidator
        )
        {
            _service = service;
            _mealPlanWeekInsertDtoValidator = mealPlanWeekInsertDtoValidator;
            _mealPlanWeekUpdateDtoValidator = mealPlanWeekUpdateDtoValidator;
        }
        
        [HttpGet]
        public async Task<IEnumerable<MealPlanWeekDto>> Get()
            => await _service.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlanWeekDto>> GetById(int id)
        {
            var mealPlanWeek = await _service.GetById(id);
            if (mealPlanWeek == null)
            {
                return NotFound();
            }

            return Ok(mealPlanWeek);
        }

        [HttpPost]
        public async Task<ActionResult<MealPlanWeekDto>> Create(MealPlanWeekInsertDto mealPlanWeekInsertDto)
        {
            var validationResult = await _mealPlanWeekInsertDtoValidator.ValidateAsync(mealPlanWeekInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if (!_service.Validate(mealPlanWeekInsertDto))
            {
                return BadRequest(_service.Errors);
            }
            
            var mealPlanWeekDto = await _service.Add(mealPlanWeekInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = mealPlanWeekDto.Id }, mealPlanWeekDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MealPlanWeekDto>> Update(int id, MealPlanWeekUpdateDto mealPlanWeekUpdateDto)
        {
            var mealPlanWeek = await _service.GetById(id);
            if (mealPlanWeek == null)
            {
                return NotFound();
            }
            var validationResult = await _mealPlanWeekUpdateDtoValidator.ValidateAsync(mealPlanWeekUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if (!_service.Validate(mealPlanWeekUpdateDto))
            {
                return BadRequest(_service.Errors);
            }
            var mealPlanWeekDto = await _service.Update(id, mealPlanWeekUpdateDto);
            return Ok(mealPlanWeekDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var mealPlanWeek = await _service.Delete(id);
            if (mealPlanWeek == null)
            {
                return NotFound();
            }
            return Ok(mealPlanWeek);
        }
    }
}