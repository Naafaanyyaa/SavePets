using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;

namespace SavePets.Business.Interfaces
{
    public interface ILoginService
    {
        Task<AuthorizeResponse> SignInAsync(AuthenticateRequest request);
    }
}
