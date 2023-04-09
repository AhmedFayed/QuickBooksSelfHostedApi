using QuickBooksSelfHostedApi.Interfaces;
using System;

namespace QuickBooksSelfHostedApi.Models
{
    public class CustomerRet : IQBReturnModel
    {
        public string ListID { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string Sublevel { get; set; }
        public string CompanyName { get; set; }
        public Address BillAddress { get; set; }
        public Address BillAddressBlock { get; set; }
        public Address ShipAddress { get; set; }
        public Address ShipAddressBlock { get; set; }
        public Address ShipToAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Balance { get; set; }
        public string TotalBalance { get; set; }
        public string JobStatus { get; set; }
        public string PreferredDeliveryMethod { get; set; }
        public ClassRef CurrencyRef { get; set; }
    }
}