using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models
{
    public class ItemReceiptAdd
    {
        public ClassRef VendorRef { get; set; }

        public ClassRef APAccountRef { get; set; }

        public string TxnDate { get; set; }

        public string RefNumber { get; set; }

        public decimal ExchangeRate { get; set; }

        public List<ItemLine> ItemLineAdd { set; get; }
    }

    public class ItemLine
    {

        public string Desc { get; set; }

        public string Quantity { get; set; }

        public string Cost { get; set; }

        public string Amount { get; set; }

        public ClassRef CustomerRef { get; set; }

        public LinkToTxn LinkToTxn { get; set; }
    }

    public class LinkToTxn
    {
        public string TxnID { get; set; }

        public string TxnLineID { get; set; }
    }
}