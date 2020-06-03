using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Core
{
    [Route("/price", "GET")]
    public class GetPrices : IReturn<List<Price>>, IGet
    {

    }

    public class Price
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
