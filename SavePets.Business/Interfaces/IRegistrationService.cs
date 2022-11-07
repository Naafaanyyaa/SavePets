using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;

namespace SavePets.Business.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegisterResult> RegisterAsync(RegisterRequest request);
    }
}
