using API.Models.Request;
using AutoMapper;
using BusinessLogic.Models.Request;
using Entities.Domain;

namespace BusinessLogic.UnitTests.Mappings
{
    public class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            CreateMap<Fruit, UpdateFruitRequest>();
            CreateMap<Fruit, SaveFruitRequest>();
        }
    }
}
