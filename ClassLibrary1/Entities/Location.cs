using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities
{
    public class Location : BaseEntity
    {
        public Geometry Point { get; set; }
        public Animal Animal { get; set; }
    }
}
