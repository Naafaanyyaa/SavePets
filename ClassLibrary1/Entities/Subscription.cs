using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext.Entities;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities
{
    public class Subscription : BaseEntity

    {
    public DateTime? StartDateOfPayment { get; set; }
    public DateTime? EndDateOfPayment { get; set; }
    public User User { get; set; }
    }
}
