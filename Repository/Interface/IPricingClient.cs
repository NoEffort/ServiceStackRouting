using Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.Interface
{
    public interface IPricingClient
    {
        List<Price> GetPrices(GetPrices request);
    }
}
