using System;
using QuickBooksSelfHostedApi.Interfaces;

namespace QuickBooksSelfHostedApi.Models
{
    public class ItemInventoryRet : IQBReturnModel
    {
        public string ListID { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string IsActive { get; set; }
        public string Sublevel { get; set; }
    }
}