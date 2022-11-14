using AutoMapper;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Mappings
{
    public class AutomapperBusinessProfile : Profile
    {
        public AutomapperBusinessProfile()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResult>();


            CreateMap<PetRequest, PetResponse>();
            CreateMap<PetRequest, Animal>()
                .ForMember(x => x.AnimalName, o => o.MapFrom(s => s.PetsName))
                .ForMember(x => x.AnimalDescription, o => o.MapFrom(s => s.Description))
                .ForMember(x => x.AnimalType, o => o.MapFrom(s => s.AnimalType));
            CreateMap<Contacts, ContactsResponse>();
            CreateMap<Location, LocationResponse>();
            CreateMap<Photo, PhotoResponse>();
            CreateMap<Animal, PetResponse>()
                .ForMember(x => x.PetsName, o => o.MapFrom(s => s.AnimalName))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.AnimalDescription))
                .ForMember(x => x.AnimalType, o => o.MapFrom(s => s.AnimalType));
            CreateMap<PetRequest, Contacts>()
                .ForMember(x => x.Telegram, o => o.MapFrom(s => s.TelegramUrl))
                .ForMember(x => x.Instagram, o => o.MapFrom(s => s.InstagramUrl))
                .ForMember(x => x.Facebook, o => o.MapFrom(s => s.FacebookUrl))
                .ForMember(x => x.Viber, o => o.MapFrom(s => s.ViberUrl))
                .ForMember(x => x.Phone, o => o.MapFrom(s => s.Phone));
            CreateMap<PetRequest, Location>();

            CreateMap<UpdateRequest, Animal>()
                .ForMember(x => x.AnimalName, o => o.MapFrom(s => s.PetsName))
                .ForMember(x => x.AnimalDescription, o => o.MapFrom(s => s.Description))
                .ForMember(x => x.AnimalType, o => o.MapFrom(s => s.AnimalType))
                .ForMember(x => x.IsFounded, o => o.MapFrom(s => s.IsFounded));
                
            CreateMap<UpdateRequest, Contacts>()
                .ForMember(x => x.Telegram, o => o.MapFrom(s => s.TelegramUrl))
                .ForMember(x => x.Instagram, o => o.MapFrom(s => s.InstagramUrl))
                .ForMember(x => x.Facebook, o => o.MapFrom(s => s.FacebookUrl))
                .ForMember(x => x.Viber, o => o.MapFrom(s => s.ViberUrl))
                .ForMember(x => x.Phone, o => o.MapFrom(s => s.Phone));
            CreateMap<UpdateRequest, Location>();
        }
    }
}
