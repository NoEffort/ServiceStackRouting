using Api.Core;
using Api.Repository.Interface;
using ServiceStack;
using ServiceStack.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Api.Repository.Wrapper
{
    public class PricingClientWrapper : IPricingClient
    {
        private string Username => "username";
        private string Password => "password";
        private string ApiUrl => "url";

        public ICacheClient Cache { get; set; }

        private string GetAccessToken()
        {
            const string key = "AccessToken";
            var token = Cache.Get<string>(key);

            if(token == null)
            {
                using(var client = new JsonServiceClient(ApiUrl))
                {
                    var request = new LoginRequest
                    {
                        Username = Username,
                        Password = Password
                    };
                    token = client.Post(request).AccessToken;
                }
                Cache.Add(key, token, TimeSpan.FromMinutes(5));
            }
            return token;
        }

        internal JsonServiceClient Api => new JsonServiceClient(ApiUrl)
        {
            BearerToken = $"Bearer {GetAccessToken()}"
        };

        public List<Price> GetPrices(GetPrices request)
        {
            /*
             * This does not work
             * 
            var req = Api.Get(new LatestPricesRequest());
            var response = req.ConvertAll(p => new Price
            {
                ProductName = p.Description,
                ProductPrice = p.CashPrice.ConvertTo<decimal>()
            });
            return response;
            */

            // I can manually return things with no problem.
            // Issues seem to happen when I call 'Api' from here.
            return new List<Price>
            {
                new Price { ProductName = "A", ProductPrice = 1.0M }
            };
        }
    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }

    [DataContract]
    public class LatestPricesResponse
    {
        [DataMember(Name = "StoreID")]
        public int StoreId { get; set; }
        [DataMember]
        public int StoreNumber { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime PriceDate { get; set; }
        [DataMember]
        public float CashPrice { get; set; }
        [DataMember]
        public float CreditPrice { get; set; }
    }

    [Route("/login", "POST")]
    [DataContract]
    public class LoginRequest : IReturn<LoginResponse>, IPost
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }

    [Route("/store_latest_prices", "GET")]
    [DataContract]
    public class LatestPricesRequest : IReturn<List<LatestPricesResponse>>, IGet
    {
        /*
        [DataMember(Name = "show_details")]
        public string ShowDetails => "no";
        */
    }
}
