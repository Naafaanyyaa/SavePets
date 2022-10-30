using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities
{
    public class UserAnimal : BaseEntity
    {
        public string UserId { get; set; }
        public string AnimalId { get; set; }
        public User Users { get; set; }
        public Animal Animals { get; set; }
    }
}
