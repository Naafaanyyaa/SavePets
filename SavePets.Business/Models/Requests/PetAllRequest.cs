using SavePets.Data.Enums;

namespace SavePets.Business.Models.Requests
{
    public class PetAllRequest
    {
        public string? SearchString { get; set; }
        public AnimalType? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
