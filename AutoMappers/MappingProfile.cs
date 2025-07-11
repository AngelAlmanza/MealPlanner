﻿using AutoMapper;
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

            CreateMap<MealPlanEntry, MealPlanEntryDto>()
                .ForMember(dest => dest.MealPlanWeek, opt => opt.MapFrom(src => src.MealPlanWeek))
                .ForMember(dest => dest.RecipeInstance, opt => opt.MapFrom(src => src.RecipeInstance));
            CreateMap<MealPlanEntryInsertDto, MealPlanEntry>();
            CreateMap<MealPlanEntryUpdateDto, MealPlanEntry>();

            CreateMap<MealPlanWeek, MealPlanWeekDto>();
            CreateMap<MealPlanWeekInsertDto, MealPlanWeek>();
            CreateMap<MealPlanWeekUpdateDto, MealPlanWeek>();
            
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients));
            CreateMap<RecipeInsertDto, Recipe>();
            CreateMap<RecipeUpdateDto, Recipe>();

            CreateMap<RecipeIngredient, RecipeIngredientDto>();
            CreateMap<RecipeIngredientInsertDto, RecipeIngredient>();
            CreateMap<RecipeIngredientUpdateDto, RecipeIngredient>();

            CreateMap<RecipeInstance, RecipeInstanceDto>()
                .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => src.Recipe));
            CreateMap<RecipeInstanceInsertDto, RecipeInstance>();
            CreateMap<RecipeInstanceUpdateDto, RecipeInstance>();
            
            CreateMap<UnitMeasure, UnitMeasureDto>();
            CreateMap<UnitMeasureInsertDto, UnitMeasure>();
            CreateMap<UnitMeasureUpdateDto, UnitMeasure>();
        }
    }
}
