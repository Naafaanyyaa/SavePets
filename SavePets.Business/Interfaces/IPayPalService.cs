using PayPal.Api;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;

namespace SavePets.Business.Interfaces
{
    public interface IPayPalService
    {
        Task<Payment> CreatePayment();

        Task<Payment> ExecutePayment(string payerId, string paymentId);
    }
}