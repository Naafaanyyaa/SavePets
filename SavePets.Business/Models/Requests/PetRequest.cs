using System.ComponentModel.DataAnnotations;
using SavePets.Data.Enums;

namespace SavePets.Business.Models.Requests
{
    public class PetRequest
    {
        [Required]
        public string UserId { set; get; }
        [Required]
        public string PetsName { set; get; }
        [Required]
        public string Description { set; get; }
        public string? TelegramUrl { set; get; }
        public string? InstagramUrl { set; get; } 
        public string? FacebookUrl { set; get; }
        public string? ViberUrl { set; get; }
        public string? Phone { set; get; }
        [Required]
        public AnimalType AnimalType { set; get; }
    
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }

    }
}
