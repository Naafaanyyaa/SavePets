using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }
        public virtual List<AnimalTag> AnimalTag { get; set; }
    }
}
