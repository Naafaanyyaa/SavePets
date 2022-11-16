using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities.Identity;
using SavePets.Data.Interfaces;

namespace SavePets.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountService(IMapper mapper, UserManager<User> user)
        {
            _mapper = mapper;
            _userManager = user;
        }

        public async Task DeleteAccountByUser(string userIdFromToken)
        {
            var user = await _userManager.FindByIdAsync(userIdFromToken);

            if (user == null)
            {
                throw new Exception($"User with such id {userIdFromToken} does not exist.");
            }

            await _userManager.DeleteAsync(user);
        }

        public async Task<UserResponse> UpdateAccountInfoByUser(string userIdFromToken, UpdateAccountRequest accountRequest)
        {
            var user = await _userManager.FindByIdAsync(userIdFromToken);

            if (user == null)
            {
                throw new Exception($"User with such id {userIdFromToken} does not exist.");
            }

            user = _mapper.Map<UpdateAccountRequest, User>(accountRequest, user);

            var emailCheck = await _userManager.FindByEmailAsync(user.Email);

            if (emailCheck != null && emailCheck.Id != userIdFromToken)
            {
                throw new Exception($"User with such email {user.Email} exists.");
            }

            var userNameCheck = await _userManager.FindByNameAsync(user.UserName);

            if (userNameCheck != null && userNameCheck.Id != userIdFromToken)
            {
                throw new Exception($"User with such name {user.UserName} exists.");
            }

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public async Task<UserResponse> ProfileInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"User with such id {userId} does not exist.");
            }

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }
    }
}
