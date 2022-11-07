using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Enums;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Services
{
    public class RegisterService : IRegistrationService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private ApplicationDbContext _context;
        private readonly ILogger<RegisterService> _logger;

        public RegisterService(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context, ILogger<RegisterService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                throw new Exception($"User with such email: {request.Email} is already exists.");
            }

            user = await _userManager.FindByNameAsync(request.UserName);


            if (user != null)
            {
                throw new Exception($"User with such email: {request.UserName} is already exists.");
            }

            user = _mapper.Map<RegisterRequest, User>(request);

            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());

            identityResult = await _userManager.AddToRolesAsync(user, new List<string>
            {
                CustomRoles.UserRole, 
                request.Role switch
                {
                    RoleEnum.Admin => CustomRoles.AdminRole,
                    RoleEnum.User => CustomRoles.UserRole
                }
            });

            if (identityResult.Errors.Any())
                throw new Exception(identityResult.Errors.ToArray().ToString());

            await _context.Users.AddAsync(user);

            //await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} has been successfully registered", user.Id);

            var result = _mapper.Map<User, RegisterResult>(user);
            return result;
        }
    }
}
