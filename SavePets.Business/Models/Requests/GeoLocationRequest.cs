using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePets.Business.Models.Requests
{
    public class GeoLocationRequest
    {
        public string AnimalId { get; set; }
        public int Count { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Alt { get; set; }
    }
}
