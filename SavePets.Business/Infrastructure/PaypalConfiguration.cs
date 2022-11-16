using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;

namespace SavePets.Business.Infrastructure
{
    public static class PaypalConfiguration
    {
        static PaypalConfiguration()
        {

        }

        public static Dictionary<string, string> GetConfig(string mode)
        {
            return new Dictionary<string, string>()
            {
                { "mode", mode }
            };
        }

        private static string GetAccessTocken(string ClientId, string ClientSecret, string mode)
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, new Dictionary<string, string>()
            {
                { "mode", mode }
            }).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetApiContext(string ClientId, string ClientSecret, string mode)
        {
            APIContext apiContext = new APIContext(GetAccessTocken(ClientId, ClientSecret,mode));
            apiContext.Config = GetConfig(mode);
            return apiContext;
        }
    }
}
