using AutoMapper;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Mappings
{
    public class AutomapperBusinessProfile : Profile
    {
        public AutomapperBusinessProfile()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResult>();
        }
    }
}
