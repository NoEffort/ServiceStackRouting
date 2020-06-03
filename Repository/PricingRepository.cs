using Api.Core;
using Api.Repository.Interface;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Repository
{
    public class PricingRepository : RepositoryBase, IPricingRepository
    {
        public IPricingClient PricingClient { get; set; }

        public List<Price> GetPrices(GetPrices request)
        {
            return PricingClient.GetPrices(request);
        }
    }
}
