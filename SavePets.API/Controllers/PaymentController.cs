using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavePets.Business.Models.PayModels;
using SavePets.Business.Services;
using PayPal.Api;
using SavePets.Business.Infrastructure;

namespace SavePets.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PayService _payService;
        private IConfiguration _configuration;
        private IHttpContextAccessor httpContextAccessor;
        public PaymentController(PayService payService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _payService = payService;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDonate(int amount)
        {
            var ClientID = _configuration.GetValue<string>("PayPal:Key");
            var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
            var mode = _configuration.GetValue<string>("PayPal:mode");

            APIContext apiContext = PaypalConfiguration.GetApiContext(ClientID, ClientSecret, mode);

            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return null;
        }
    }
}
