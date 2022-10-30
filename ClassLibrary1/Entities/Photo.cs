using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities
{
    public class Photo : BaseEntity
    {
        public string PhotoUri { get; set; }
        public virtual List<AnimalPhoto> AnimalPhotos { get; set; }
    }
}
