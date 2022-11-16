using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Interfaces
{
    public interface IAccountService
    {
        Task DeleteAccountByUser(string userIdFromToken);
        Task<UserResponse> UpdateAccountInfoByUser(string userIdFromToken, UpdateAccountRequest accountRequest);
        Task<UserResponse> ProfileInfo(string userIdFromToken);
    }
}
