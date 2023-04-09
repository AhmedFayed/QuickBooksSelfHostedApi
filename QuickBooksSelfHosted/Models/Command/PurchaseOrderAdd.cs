using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models
{
    public class PurchaseOrderAdd
    {
        public ClassRef VendorRef { get; set; }

        public ClassRef ClassRef { get; set; }

        public string TxnDate { get; set; }

        public string RefNumber { get; set; }

        public Address VendorAddress { get; set; }

        public Address ShipAddress { get; set; }

        public string DueDate { get; set; }

        public string ExpectedDate { get; set; }

        public decimal ExchangeRate { get; set; }

        public List<PurchaseOrderLine> PurchaseOrderLineAdd { set; get; }
    }

    public class PurchaseOrderLine
    {
        public ClassRef ItemRef { get; set; }

        public string ManufacturerPartNumber { get; set; }

        public string Desc { get; set; }

        public string Quantity { get; set; }

        public string Rate { get; set; }

        public string Amount { get; set; }

        public ClassRef CustomerRef { get; set; }
    }


}