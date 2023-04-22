using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using SavePets.Business.Infrastructure;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Services
{
    public class PayPalService : IPayPalService
    {
        private Payment _createdPayment = null!;
        private readonly IConfiguration _configuration;

        public PayPalService(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("PayPal");
        }

        public async Task<Payment> CreatePayment()
        {
            var apiContext = GetApiContext();

            try
            {
                Payment payment = new Payment()
                {
                    intent = "sale",
                    payer = new Payer() { payment_method = "paypal" },
                    transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            amount = new Amount()
                            {
                                currency = "EUR",
                                total = "1"
                            },
                            description = "Test product"
                        }
                    },
                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = "https://192.168.1.6:7140/api/paypal/cancel",
                        return_url = "https://192.168.1.6:7140/api/paypal/success"
                    }
                };

                _createdPayment = await Task.Run(() => payment.Create(apiContext));

                
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot create payment.");
            }
            return _createdPayment;
        }

        public async Task<Payment> ExecutePayment(string payerId, string paymentId)
        {
            var apiContext = GetApiContext();

            PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerId };

            Payment payment = new Payment() { id = paymentId };

            Payment executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));

            return executedPayment;
        }

        private APIContext? GetApiContext()
        {
            var clientId = _configuration["Key"];

            var clientSecret = _configuration["Secret"];

            var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();

            var apiContext = new APIContext(accessToken)
            {
                Config = ConfigManager.Instance.GetProperties()
            };

            return apiContext;
        }
    }
}