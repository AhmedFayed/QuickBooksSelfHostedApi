using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickBooksSelfHostedApi.Models
{
    public class ItemInventoryAdd
    {
        public string Name { get; set; }

        public decimal SalesPrice { get; set; }

        public ClassRef IncomeAccountRef { get; set; }

        public ClassRef COGSAccountRef { get; set; }

        public ClassRef AssetAccountRef { get; set; }
    }
}