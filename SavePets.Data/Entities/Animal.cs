using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class Animal : BaseEntity
{
    public string AnimalName { get; set; }
    public string AnimalDescription { get; set; }
    public string Region { get; set; }
    public bool IsFounded { get; set; }
    public string LocationId { get; set; }
    public string ContactsId { get; set; }
    public Contacts Contacts { get; set; }
    public Location Location { get; set; }
    public virtual List<AnimalTag> AnimalTags { get; set; }
    public virtual List<AnimalPhoto> AnimalPhotos { get; set; }
    public List<UserAnimal> UserAnimals { get; set; }
}