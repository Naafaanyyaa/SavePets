﻿using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities;

public class Photo : BaseEntity
{
    public string AnimalId { get; set; }
    public virtual Animal Animal { get; set; }
    public string PhotoPath { get; set; }
}