using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using SavePets.Business.Exceptions;
using SavePets.Business.Interfaces;

namespace SavePets.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private IPayPalService _payPalService;

        public PayPalController(IPayPalService payPal)
        {
            _payPalService = payPal;
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> CreatePayment()
        {
            var result = await _payPalService.CreatePayment();

            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    return Redirect(link.href);
                }
            }

            return NotFound();
        }

        [HttpGet]
        [Route("success")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string token, string payerId)
        {
            Payment result = await _payPalService.ExecutePayment(payerId, paymentId);

            return Ok(result);
        }

        [HttpGet]
        [Route("cancel")]
        public async Task<IActionResult> CancelPayment()
        {
            throw new NotFoundException("Something is wrong with your payment.");
        }
    }
}