using System;
using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models.Queries
{
    public class BillRet
    {
        public string TxnID { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string TxnNumber { get; set; }
        public ClassRef VendorRef { get; set; }
        public ClassRef VendorAddress { get; set; }
        public ClassRef APAccountRef { get; set; }
        public string TxnDate { get; set; }
        public string DueDate { get; set; }
        public string AmountDue { get; set; }
        public ClassRef CurrencyRef { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal AmountDueInHomeCurrency { get; set; }
        public bool IsPending { get; set; }
        public bool IsPaid { get; set; }
        public List<ItemLineRet> ItemLineRet { get; set; }
        public decimal OpenAmount { get; set; }
    }
}