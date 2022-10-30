using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Data.Entities.Abstract;
namespace SavePets.Data.Entities
{
    public class Role : BaseEntity

    {
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public User User { get; set; }
    }
}
