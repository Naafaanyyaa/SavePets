using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext.Entities;
using BaseEntity = SavePets.Data.Entities.Abstract.BaseEntity;

namespace SavePets.Data.Entities
{
    public class AnimalTag : BaseEntity
    {
        public string AnimalId { get; set; }
        public Animal Animals { get; set; }

        public string TagId { get; set; }
        public Tag Tags { get; set; }
    }
}
