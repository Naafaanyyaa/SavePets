﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePets.Business.Models.Requests
{
    public class GeoLocationRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Alt { get; set; }
    }
}
