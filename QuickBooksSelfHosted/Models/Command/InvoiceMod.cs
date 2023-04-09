using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models
{
    public class InvoiceMod
    {
        public string TxnID { get; set; }

        public string EditSequence { get; set; }

        public ClassRef CustomerRef { get; set; }

        public ClassRef ClassRef { get; set; }

        public string TxnDate { get; set; }

        public string RefNumber { get; set; }

        public Address BillAddress { get; set; }

        public Address ShipAddress { get; set; }

        public decimal ExchangeRate { get; set; }

        public List<InvoiceLineMod> InvoiceLineMod { set; get; }
    }


    public class InvoiceLineMod
    {
        public string TxnLineID { get; set; }

        public ClassRef ItemRef { get; set; }

        public string Desc { get; set; }

        public string Quantity { get; set; }

        public string Rate { get; set; }

        public string Amount { get; set; }
    }
}