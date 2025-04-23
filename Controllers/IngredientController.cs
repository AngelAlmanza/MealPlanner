using FluentValidation;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using MealPlannerApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly ICommonService<IngredientDto, IngredientInsertDto, IngredientUpdateDto> _service;
        private readonly IValidator<IngredientInsertDto> _ingredientInsertValidator;
        private readonly IValidator<IngredientUpdateDto> _ingredientUpdateValidator;

        public IngredientController(
                ICommonService<IngredientDto, IngredientInsertDto, IngredientUpdateDto> service,
                IValidator<IngredientInsertDto> ingredientInsertValidator,
                IValidator<IngredientUpdateDto> ingredientUpdateValidator
            )
        {
            _service = service;
            _ingredientInsertValidator = ingredientInsertValidator;
            _ingredientUpdateValidator = ingredientUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<IngredientDto>> Get()
            => await _service.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDto>> GetById(int id)
        {
            var ingredient = await _service.GetById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> Create(IngredientInsertDto ingredientInsertDto)
        {
            var validationResult = await _ingredientInsertValidator.ValidateAsync(ingredientInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(ingredientInsertDto))
            {
                return BadRequest(_service.Errors);
            }

            var ingredientDto = await _service.Add(ingredientInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = ingredientDto.Id }, ingredientDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IngredientDto>> Update(int id, IngredientUpdateDto ingredientUpdateDto)
        {
            var validationResult = await _ingredientUpdateValidator.ValidateAsync(ingredientUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(ingredientUpdateDto))
            {
                return BadRequest(_service.Errors);
            }
            var ingredientDto = await _service.Update(id, ingredientUpdateDto);
            return Ok(ingredientDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IngredientDto>> Delete(int id)
        {
            var ingredient = await _service.Delete(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }
    }
}
