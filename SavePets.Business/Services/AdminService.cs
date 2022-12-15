using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SavePets.Business.Exceptions;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Identity;
using SavePets.Data.Enums;
using SavePets.Data.Interfaces;

namespace SavePets.Business.Services
{
    public class AdminService : IAdminService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAnimalRepository _animalRepository;

        public AdminService(IMapper mapper, UserManager<User> user, ISubscriptionRepository subscriptionRepository, IAnimalRepository animalRepository)
        {
            _mapper = mapper;
            _userManager = user;
            _subscriptionRepository = subscriptionRepository;
            _animalRepository = animalRepository;
        }

        public async Task<UserResponse> ChangeRoleToAdmin(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var usersWithAdminRole = await _userManager.GetUsersInRoleAsync("Admin");

            if (user == null)
            {
                throw new NotFoundException(nameof(user), UserId);
            }

            if (usersWithAdminRole.Contains(user))
            {
                throw new ValidationException("User with such id has already been admin");
            }

            await _userManager.AddToRolesAsync(user, new List<string>
            {
                CustomRoles.AdminRole
            });

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public async Task<UserResponse> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), id);
            }

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public async Task<IEnumerable<UserResponse>> GetUserList()
        {
           var listOfUsers = await _userManager.GetUsersInRoleAsync("User");

           var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(listOfUsers);

           return result;
        }

        public async Task<UserResponse> BanUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            user.IsBanned = !user.IsBanned;

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public async Task DeleteAnimal(string animalId)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);

            if (animal == null)
            {
                throw new NotFoundException(nameof(animal), animalId);
            }

            await _animalRepository.DeleteAsync(animal);
        }
    }
}
