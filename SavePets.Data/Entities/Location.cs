using NetTopologySuite.Geometries;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class Location : BaseEntity
{
    public Geometry Point { get; set; }
    public Animal Animal { get; set; }
}