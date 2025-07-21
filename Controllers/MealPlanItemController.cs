using FluentValidation;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanItemController : ControllerBase
    {
        private readonly ICommonService<MealPlanItemDto, MealPlanItemInsertDto, MealPlanItemUpdateDto> _service;
        private readonly IValidator<MealPlanItemInsertDto> _mealPlanItemInsertValidator;
        private readonly IValidator<MealPlanItemUpdateDto> _mealPlanItemUpdateDtoValidator;
        
        public MealPlanItemController(
            ICommonService<MealPlanItemDto, MealPlanItemInsertDto, MealPlanItemUpdateDto> service,
            IValidator<MealPlanItemInsertDto> mealPlanItemInsertValidator,
            IValidator<MealPlanItemUpdateDto> mealPlanItemUpdateDtoValidator
        )
        {
            _service = service;
            _mealPlanItemInsertValidator = mealPlanItemInsertValidator;
            _mealPlanItemUpdateDtoValidator = mealPlanItemUpdateDtoValidator;
        }
        
        [HttpGet]
        public async Task<IEnumerable<MealPlanItemDto>> Get()
            => await _service.Get();
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlanItemDto>> Get(int id)
        {
            var mealPlanItem = await _service.GetById(id);
            if (mealPlanItem == null)
            {
                return NotFound();
            }
            return Ok(mealPlanItem);
        }

        [HttpPost]
        public async Task<ActionResult<MealPlanItemDto>> Create(MealPlanItemInsertDto dto)
        {
            var validationResult = await _mealPlanItemInsertValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if (!_service.Validate(dto))
            {
                return BadRequest(_service.Errors);
            }
            
            var mealPlanItemDto = await _service.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = mealPlanItemDto.Id }, mealPlanItemDto);
        }

        public async Task<ActionResult<MealPlanItemDto>> Update(int id, MealPlanItemUpdateDto dto)
        {
            var validationResult = await _mealPlanItemUpdateDtoValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if (!_service.Validate(dto))
            {
                return BadRequest(_service.Errors);
            }
            
            var mealPlanItemDto = await _service.Update(id, dto);
            if (mealPlanItemDto == null)
            {
                return NotFound();
            }
            return Ok(mealPlanItemDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MealPlanItemDto>> Delete(int id)
        {
            var mealPlanItemDto = await _service.Delete(id);
            if (mealPlanItemDto == null)
            {
                return NotFound();
            }

            return Ok(mealPlanItemDto);
        }
    }
}