using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Interfaces
{
    public interface IAdminService
    {
        Task<UserResponse> BanUser(string UserId);
        Task DeleteAnimal(string AnimalId);
        Task<UserResponse> ChangeRoleToAdmin(string UserId);
        Task<UserResponse> GetUserById(string id);
        Task<IEnumerable<UserResponse>> GetUserList();

    }
}
