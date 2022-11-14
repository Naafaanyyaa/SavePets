using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SavePets.Business.Models.Abstract;

namespace SavePets.Business.Models.Response
{
    public class LocationResponse : BaseResult
    {
        public Geometry Point { get; set; }
    }
}
