using FluentValidation;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ICommonService<RecipeDto, RecipeInsertDto, RecipeUpdateDto> _service;
        private readonly IValidator<RecipeInsertDto> _recipeInsertValidator;
        private readonly IValidator<RecipeUpdateDto> _recipeUpdateValidator;
        
        public RecipeController(
                ICommonService<RecipeDto, RecipeInsertDto, RecipeUpdateDto> service,
                IValidator<RecipeInsertDto> recipeInsertValidator,
                IValidator<RecipeUpdateDto> recipeUpdateValidator
            )
        {
            _service = service;
            _recipeInsertValidator = recipeInsertValidator;
            _recipeUpdateValidator = recipeUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeDto>> Get()
            => await _service.Get();
        
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetById(int id)
        {
            var recipe = await _service.GetById(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Create(RecipeInsertDto recipeInsertDto)
        {
            var validationResult = await _recipeInsertValidator.ValidateAsync(recipeInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if (!_service.Validate(recipeInsertDto))
            {
                return BadRequest(_service.Errors);
            }
            
            var recipeDto = await _service.Add(recipeInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = recipeDto.Id }, recipeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeDto>> Update(int id, RecipeUpdateDto recipeUpdateDto)
        {
            var validationResult = await _recipeUpdateValidator.ValidateAsync(recipeUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if (!_service.Validate(recipeUpdateDto))
            {
                return BadRequest(_service.Errors);
            }
            
            var recipeDto = await _service.Update(id, recipeUpdateDto);
            return Ok(recipeDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeDto>> Delete(int id)
        {
            var recipe = await _service.Delete(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }
    }
}
