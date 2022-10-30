using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class Photo : BaseEntity
{
    public string PhotoUri { get; set; }
    public virtual List<AnimalPhoto> AnimalPhotos { get; set; }
}