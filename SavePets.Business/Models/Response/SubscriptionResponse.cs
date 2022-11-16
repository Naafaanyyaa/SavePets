using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePets.Business.Models.Response
{
    public class SubscriptionResponse
    {
        public DateTime? StartDateOfPayment { get; set; }
        public DateTime? EndDateOfPayment { get; set; }
    }
}
