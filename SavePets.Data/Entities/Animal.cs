using SavePets.Data.Entities.Abstract;
using SavePets.Data.Entities.Identity;
using SavePets.Data.Enums;

namespace SavePets.Data.Entities;

public class Animal : BaseEntity
{
    public string AnimalName { get; set; }
    public string AnimalDescription { get; set; }
    public bool IsFounded { get; set; }
    public string ContactsId { get; set; }
    public Contacts Contacts { get; set; }
    public string LocationId { get; set; }
    public Location Location { get; set; }
    public AnimalType AnimalType { get; set; }
    public virtual List<Photo> Photos { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}