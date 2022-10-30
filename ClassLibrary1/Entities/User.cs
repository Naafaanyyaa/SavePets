using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities
{
    public class User : BaseEntity
    {
        public bool IsBanned { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string? SubscriptionID { get; set; }
        public Subscription Subscription { get; set; }
        public string RoleID { get; set; }
        public Role Role { get; set; }
        public List<UserAnimal> UserAnimals { get; set; }
    }
}
