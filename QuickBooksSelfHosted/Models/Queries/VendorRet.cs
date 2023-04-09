using QuickBooksSelfHostedApi.Interfaces;
using System;

namespace QuickBooksSelfHostedApi.Models
{
    public class VendorRet : IQBReturnModel
    {
        public string ListID { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public string IsVendorEligibleFor1099 { get; set; }
        public string Balance { get; set; }
        public ClassRef CurrencyRef { get; set; }
    }
}