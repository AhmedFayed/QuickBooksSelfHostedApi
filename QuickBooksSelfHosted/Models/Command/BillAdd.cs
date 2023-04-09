using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models
{
    public class BillAdd
    {
        public ClassRef VendorRef { get; set; }

        public ClassRef APAccountRef { get; set; }

        public string TxnDate { get; set; }

        public string RefNumber { get; set; }

        public decimal ExchangeRate { get; set; }

        public List<ItemLine> ItemLineAdd { set; get; }
    }

}