using System;
using System.Collections.Generic;
using QuickBooksSelfHostedApi.Interfaces;

namespace QuickBooksSelfHostedApi.Models.Queries
{
    public class PurchaseOrderRet : IQBReturnModel
    {
        public string TxnID { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string TxnNumber { get; set; }
        public ClassRef VendorRef { get; set; }
        public string TxnDate { get; set; }
        public string RefNumber { get; set; }
        public Address VendorAddress { get; set; }
        public Address VendorAddressBlock { get; set; }
        public Address ShipAddress { get; set; }
        public Address ShipAddressBlock { get; set; }
        public string DueDate { get; set; }
        public string ExpectedDate { get; set; }
        public string TotalAmount { get; set; }
        public ClassRef CurrencyRef { get; set; }
        public string ExchangeRate { get; set; }
        public string TotalAmountInHomeCurrency { get; set; }
        public string IsManuallyClosed { get; set; }
        public string IsFullyReceived { get; set; }
        public string IsToBePrinted { get; set; }
        public string IsToBeEmailed { get; set; }
        public List<PurchaseOrderLineRet> PurchaseOrderLineRet { get; set; }
    }

    public class PurchaseOrderLineRet
    {
        public string TxnLineID { get; set; }
        public ClassRef ItemRef { get; set; }
        public string Desc { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public ClassRef CustomerRef { get; set; }
        public string ReceivedQuantity { get; set; }
        public string IsBilled { get; set; }
        public string IsManuallyClosed { get; set; }
    }

}