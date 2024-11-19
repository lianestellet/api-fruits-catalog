using API.Models.Request;
using AutoMapper;
using BusinessLogic.Models.DTOs;
using BusinessLogic.Models.Request;
using Entities.Domain;

namespace BusinessLogic.Mappings
{
    public class BusinessLogicMappingProfile : Profile
    {
        public BusinessLogicMappingProfile()
        {
            // DTO to Entity
            CreateMap<SaveFruitRequest, Fruit>();
            CreateMap<UpdateFruitRequest, Fruit>();
            CreateMap<FruitDTO, Fruit>();
            

            // Entity to DTO
            CreateMap<Fruit, FruitDTO>()
                .MapFruitTypeName()
                .MapFruitTypeDescription();
        }
    }
}
