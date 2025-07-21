using AutoMapper;
using MealPlannerApi.Data.Entities;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ingredient, IngredientDto>()
                .ForMember(dest => dest.UnitMeasure, opt => opt.MapFrom(src => src.UnitMeasure));
            CreateMap<IngredientInsertDto, Ingredient>();
            CreateMap<IngredientUpdateDto, Ingredient>();
            
            CreateMap<MealPlanItem, MealPlanItemDto>()
                .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => src.Recipe));
            CreateMap<MealPlanItemInsertDto, MealPlanItem>();
            CreateMap<MealPlanItemUpdateDto, MealPlanItem>();
            
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients));
            CreateMap<RecipeInsertDto, Recipe>();
            CreateMap<RecipeUpdateDto, Recipe>();

            CreateMap<RecipeIngredient, RecipeIngredientDto>();
            CreateMap<RecipeIngredientInsertDto, RecipeIngredient>();
            CreateMap<RecipeIngredientUpdateDto, RecipeIngredient>();
            
            CreateMap<UnitMeasure, UnitMeasureDto>();
            CreateMap<UnitMeasureInsertDto, UnitMeasure>();
            CreateMap<UnitMeasureUpdateDto, UnitMeasure>();
        }
    }
}
