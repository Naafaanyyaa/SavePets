using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class Contacts : BaseEntity
{
    public string Telegram { get; set; }
    public string Instagram { get; set; }
    public string Facebook { get; set; }
    public string Viber { get; set; }
    public string Phone { get; set; }
    public Animal Animal { get; set; }
}