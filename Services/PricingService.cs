using Api.Core;
using Api.Repository.Interface;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Services
{
    public class PricingService : Service
    {
        public IPricingRepository PricingRepository { get; set; }

        public object Get(GetPrices request)
        {
            return PricingRepository.GetPrices(request);
        }
    }
}
