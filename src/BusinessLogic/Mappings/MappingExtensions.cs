using AutoMapper;
using BusinessLogic.Models.DTOs;
using Entities.Domain;

namespace BusinessLogic.Mappings
{
    static class BusinessLogicMappingExtensions
    {
        public static IMappingExpression<TSource, TDestination> MapFruitTypeName<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
            where TDestination : FruitDTO
            where TSource : Fruit
        {
            return mapping.ForMember(dest => dest.FruitTypeName, opt => opt.MapFrom(src => src.FruitType != null ? src.FruitType.Name : string.Empty));
        }

        public static IMappingExpression<TSource, TDestination> MapFruitTypeDescription<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
            where TDestination : FruitDTO
            where TSource : Fruit
        {
            return mapping.ForMember(dest => dest.FruitTypeDescription, opt => opt.MapFrom(src => src.FruitType != null ? src.FruitType.Description : string.Empty));
        }
    }
}

