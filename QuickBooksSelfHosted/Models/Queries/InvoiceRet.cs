using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickBooksSelfHostedApi.Interfaces;

namespace QuickBooksSelfHostedApi.Models.Queries
{
    public class InvoiceRet : IQBReturnModel
    {
        public string TxnID { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string TxnNumber { get; set; }
        public ClassRef CustomerRef { get; set; }
        public ClassRef ARAccountRef { get; set; }
        public ClassRef TemplateRef { get; set; }
        public string TxnDate { get; set; }
        public string RefNumber { get; set; }
        public Address BillAddress { get; set; }
        public Address BillAddressBlock { get; set; }
        public Address ShipAddress { get; set; }
        public Address ShipAddressBlock { get; set; }
        public bool IsPending { get; set; }
        public bool IsFinanceCharge { get; set; }
        public string DueDate { get; set; }
        public string ShipDate { get; set; }
        public string Subtotal { get; set; }
        public string SalesTaxPercentage { get; set; }
        public string SalesTaxTotal { get; set; }
        public string AppliedAmount { get; set; }
        public string BalanceRemaining { get; set; }
        public ClassRef CurrencyRef { get; set; }
        public string ExchangeRate { get; set; }
        public string BalanceRemainingInHomeCurrency { get; set; }
        public bool IsPaid { get; set; }
        public bool IsToBePrinted { get; set; }
        public bool IsToBeEmailed { get; set; }
        public List<InvoiceLineRet> InvoiceLineRet { get; set; }
    }

    public class InvoiceLineRet
    {
        public string TxnLineID { get; set; }
        public ClassRef ItemRef { get; set; }
        public string Desc { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public ClassRef SalesTaxCodeRef { get; set; }
    }
}