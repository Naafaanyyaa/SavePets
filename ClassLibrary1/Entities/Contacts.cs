using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Abstract;

namespace DBContext.Entities
{
    public class Contacts : BaseEntity
    {
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Viber { get; set; }
        public string Phone { get; set; }
        public Animal Animal { get; set; }
    }
}
