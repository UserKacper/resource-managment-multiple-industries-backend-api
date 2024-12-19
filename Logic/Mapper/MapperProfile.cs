using AutoMapper;
using resource_manager_db.Models;
using resource_mangment.Logic.AuthModels;
using resource_mangment.Logic.DTO_s;

namespace resource_mangment.Logic.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterCompanyAndOwner, Company>();
            CreateMap<RegisterCompanyAndOwner, Employee>();
            CreateMap<RegisterCompanyAndOwner, JwtGenerationAccountModel>();
        }
    }
}
