using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class AnimalTag : BaseEntity
{
    public string AnimalId { get; set; }
    public Animal Animals { get; set; }

    public string TagId { get; set; }
    public Tag Tags { get; set; }
}