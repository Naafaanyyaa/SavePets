using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext.Entities;
using BaseEntity = SavePets.Data.Entities.Abstract.BaseEntity;

namespace SavePets.Data.Entities
{
    public class AnimalPhoto : BaseEntity
    {
        public string AnimalId { get; set; }
        public Animal Animals { get; set; }
        public string PhotoId { get; set; }
        public Photo Photos { get; set; }
    }
}
