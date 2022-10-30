using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class AnimalPhoto : BaseEntity
{
    public string AnimalId { get; set; }
    public Animal Animals { get; set; }
    public string PhotoId { get; set; }
    public Photo Photos { get; set; }
}