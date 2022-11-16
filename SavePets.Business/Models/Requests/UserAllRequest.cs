using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Data.Enums;

namespace SavePets.Business.Models.Requests
{
    public class UserAllRequest
    {
        public string? SearchString { get; set; }
        public RoleEnum? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
