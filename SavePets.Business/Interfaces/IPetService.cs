using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;

namespace SavePets.Business.Interfaces
{
    public interface IPetService
    {
        Task<List<PetResponse>> GetAllPetsByRequest(PetAllRequest request);
        Task<PetResponse> GetPetById(string requestId);
        Task<PetResponse> CreateAsync(PetRequest request, string UserId, IFormFileCollection files, string directoryToSave);
        Task DeleteByIdAsync(string userId, string animalId);
        Task<PetResponse> UpdateByIdAsync(string Userid, string AnimalId, UpdateRequest request);
    }
}
