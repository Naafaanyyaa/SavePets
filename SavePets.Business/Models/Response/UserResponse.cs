using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePets.Business.Models.Response
{
    public class UserResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsBanned { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SubscriptionResponse? Subscription { get; set; }
    }
}
