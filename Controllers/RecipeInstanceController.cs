using FluentValidation;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeInstanceController : ControllerBase
    {
        private readonly ICommonService<RecipeInstanceDto, RecipeInstanceInsertDto, RecipeInstanceUpdateDto> _service;
        private readonly IValidator<RecipeInstanceInsertDto> _insertValidator;
        private readonly IValidator<RecipeInstanceUpdateDto> _updateValidator;

        public RecipeInstanceController(
            ICommonService<RecipeInstanceDto, RecipeInstanceInsertDto, RecipeInstanceUpdateDto> service,
            IValidator<RecipeInstanceInsertDto> insertValidator,
            IValidator<RecipeInstanceUpdateDto> updateValidator
        )
        {
            _service = service;
            _insertValidator = insertValidator;
            _updateValidator = updateValidator;
        }
        
        [HttpGet]
        public async Task<IEnumerable<RecipeInstanceDto>> Get()
            => await _service.Get();
        
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeInstanceDto>> GetById(int id)
        {
            var recipeInstance = await _service.GetById(id);
            if (recipeInstance == null)
            {
                return NotFound();
            }
            return Ok(recipeInstance);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeInstanceDto>> Create(RecipeInstanceInsertDto recipeInstanceInsertDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(recipeInstanceInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(recipeInstanceInsertDto))
            {
                return BadRequest(_service.Errors);
            }
            var recipeInstanceDto = await _service.Add(recipeInstanceInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = recipeInstanceDto.Id }, recipeInstanceDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeInstanceDto>> Update(int id, RecipeInstanceUpdateDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            if (!_service.Validate(updateDto))
            {
                return BadRequest(_service.Errors);
            }
            var recipeInstanceDto = await _service.Update(id, updateDto);
            return Ok(recipeInstanceDto);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeInstanceDto>> Delete(int id)
        {
            var recipeInstance = await _service.Delete(id);
            if (recipeInstance == null)
            {
                return NotFound();
            }
            return Ok(recipeInstance);
        }
    }
}