using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class Tag : BaseEntity
{
    public string TagName { get; set; }
    public virtual List<AnimalTag> AnimalTag { get; set; }
}