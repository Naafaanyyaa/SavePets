using SavePets.Data.Entities.Abstract;
using SavePets.Data.Entities.Identity;

namespace SavePets.Data.Entities;

public class Subscription : BaseEntity
{
    public DateTime? StartDateOfPayment { get; set; }
    public DateTime? EndDateOfPayment { get; set; }
    public User User { get; set; }
}