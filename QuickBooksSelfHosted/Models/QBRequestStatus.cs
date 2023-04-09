using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace QuickBooksSelfHostedApi.Models
{
    public class QBRequestStatus
    {

        public string RequestID { get; set; }

        public string StatusCode { get; set; }

        public string StatusSeverity { get; set; }

        public string StatusMessage { get; set; }
    }
}