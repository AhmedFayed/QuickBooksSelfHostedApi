using System;
using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models.Queries
{
    public class ItemReceiptRet
    {
        public string TxnID { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string TxnNumber { get; set; }
        public ClassRef VendorRef { get; set; }
        public ClassRef APAccountRef { get; set; }
        public string TxnDate { get; set; }
        public string TotalAmount { get; set; }
        public ClassRef CurrencyRef { get; set; }
        public string ExchangeRate { get; set; }
        public string TotalAmountInHomeCurrency { get; set; }
        public string RefNumber { get; set; }
        public string Memo { get; set; }
        public List<ItemLineRet> ItemLineRet { get; set; }
    }

    public class ItemLineRet
    {
        public string TxnLineID { get; set; }
        public ClassRef ItemRef { get; set; }
        public string Desc { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Amount { get; set; }
        public ClassRef CustomerRef { get; set; }
        public string BillableStatus { get; set; }
    }
}