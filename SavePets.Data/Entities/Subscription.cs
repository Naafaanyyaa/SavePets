﻿using SavePets.Data.Entities.Abstract;
using SavePets.Data.Entities.Identity;

namespace SavePets.Data.Entities;

public class Subscription : BaseEntity
{
    public User User { get; set; }
}