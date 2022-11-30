using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;

namespace SavePets.Business.Interfaces
{
    public interface IGeoLocation
    {
        Task<PetResponse> UpdateGeolocation(GeoLocationRequest request);
    }
}
