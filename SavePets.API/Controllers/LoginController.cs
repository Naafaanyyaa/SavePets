using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;


namespace SavePets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;
        public LoginController(IRegistrationService registrationService, ILoginService loginService)
        {
            _registrationService = registrationService;
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest userModel)
        {
            var result = await _registrationService.RegisterAsync(userModel);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(AuthorizeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AuthorizeResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest userLogin)
        {
            var result = await _loginService.SignInAsync(userLogin);
            return result.IsAuthSuccessful ? Ok(result) : Unauthorized(result);
        }
    }
}
