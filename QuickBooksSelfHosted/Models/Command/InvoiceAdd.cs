using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickBooksSelfHostedApi.Models
{
    public class InvoiceAdd
    {
        public ClassRef CustomerRef { get; set; }

        public ClassRef ClassRef { get; set; }
        public string TxnDate { get; set; }

        public string RefNumber { get; set; }

        public Address BillAddress { get; set; }

        public Address ShipAddress { get; set; }

        public decimal ExchangeRate { get; set; }

        public List<InvoiceLine> InvoiceLineAdd { set; get; }
    }

    public class InvoiceLine
    {
        public ClassRef ItemRef { get; set; }

        public string Desc { get; set; }
        
        public string Quantity { get; set; }

        public string Rate { get; set; }

        public string Amount { get; set; }
        

    }
    
    public class ClassRef
    {
        public string FullName { get; set; }
    }


}