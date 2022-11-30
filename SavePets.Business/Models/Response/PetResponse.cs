using System.ComponentModel.DataAnnotations;
using SavePets.Business.Models.Abstract;
using SavePets.Data.Entities.Identity;
using SavePets.Data.Enums;

namespace SavePets.Business.Models.Response
{
    public class PetResponse : BaseResult
    {
        public string PetsName { set; get; } = string.Empty;
        public string Description { set; get; } = string.Empty;
        public string LocationId { set; get; }
        public string ContactsId { set; get; }
        public List<PhotoResponse> Photos { get; set; }
        public ContactsResponse Contacts { get; set; }
        public LocationResponse Location { get; set; }
        public AnimalType AnimalType { set; get; }
    }
}
