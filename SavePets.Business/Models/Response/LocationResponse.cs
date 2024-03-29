﻿using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SavePets.Business.Models.Abstract;

namespace SavePets.Business.Models.Response
{
    public class LocationResponse : BaseResult
    {
        public double Lontitude { get; set; }

        public double Latitude { get; set; }
    }
}
