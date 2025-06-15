using FluentValidation;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanEntryController : ControllerBase
    {
        private readonly IValidator<MealPlanEntryInsertDto> _mealPlanEntryInsertValidator;
        private readonly IValidator<MealPlanEntryUpdateDto> _mealPlanEntryUpdateValidator;
        private readonly ICommonService<MealPlanEntryDto, MealPlanEntryInsertDto, MealPlanEntryUpdateDto> _service;
        
        public MealPlanEntryController(
            IValidator<MealPlanEntryInsertDto> mealPlanEntryInsertValidator,
            IValidator<MealPlanEntryUpdateDto> mealPlanEntryUpdateValidator,
            ICommonService<MealPlanEntryDto, MealPlanEntryInsertDto, MealPlanEntryUpdateDto> service)
        {
            _mealPlanEntryInsertValidator = mealPlanEntryInsertValidator;
            _mealPlanEntryUpdateValidator = mealPlanEntryUpdateValidator;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<MealPlanEntryDto>> Get()
            => await _service.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlanEntryDto>> GetById(int id)
        {
            var mealPlanEntry = await _service.GetById(id);
            if (mealPlanEntry == null)
            {
                return NotFound();
            }
            return Ok(mealPlanEntry);
        }

        [HttpPost]
        public async Task<ActionResult<MealPlanEntryDto>> Create(MealPlanEntryInsertDto dto)
        {
            var validationResult = await _mealPlanEntryInsertValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(dto))
            {
                return BadRequest(_service.Errors);
            }
            
            var mealPlanEntryDto = await _service.Add(dto);
            
            if (mealPlanEntryDto == null)
            {
                return BadRequest(_service.Errors);
            }

            return CreatedAtAction(nameof(GetById), new { id = mealPlanEntryDto.Id }, mealPlanEntryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MealPlanEntryDto>> Update(int id, MealPlanEntryUpdateDto dto)
        {
            var validationResult = await _mealPlanEntryUpdateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(dto))
            {
                return BadRequest(_service.Errors);
            }
            
            var mealPlanEntryDto = await _service.Update(id, dto);
            
            if (mealPlanEntryDto == null)
            {
                return NotFound(_service.Errors);
            }
            
            return Ok(mealPlanEntryDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MealPlanEntryDto>> Delete(int id)
        {
            var mealPlanEntryDto = await _service.Delete(id);

            if (mealPlanEntryDto == null)
            {
                return NotFound();
            }
            
            return Ok(mealPlanEntryDto);
        }
    }
}