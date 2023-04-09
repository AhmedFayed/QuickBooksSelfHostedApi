using Newtonsoft.Json;
using QuickBooksSelfHostedApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace QuickBooksSelfHostedApi.Helper
{
    public class QBRequestBuilder
    {
        private static readonly string maxVersion = "16.0";

        public static string BuildAddRequest<T>(T model)
        {
            var requestName = model.GetType().Name;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            XmlElement requestElement = xmlDoc.CreateElement(requestName + "Rq");
            qbXMLMsgsRq.AppendChild(requestElement);

            var modeldata = JsonConvert.SerializeObject(model, QBParser.GetDefaultSerializer());

            var requestBody = JsonConvert.DeserializeXmlNode(modeldata, requestName, false, false);
            requestElement.InnerXml = requestBody.InnerXml;

            requestElement.SetAttribute("requestID",Guid.NewGuid().ToString());
            return xmlDoc.OuterXml;
        }

        static XmlElement BuildRqEnvelope(XmlDocument doc)
        {
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
            doc.AppendChild(doc.CreateProcessingInstruction("qbxml", "version=\"" + maxVersion + "\""));
            XmlElement qbXML = doc.CreateElement("QBXML");
            doc.AppendChild(qbXML);
            XmlElement qbXMLMsgsRq = doc.CreateElement("QBXMLMsgsRq");
            qbXML.AppendChild(qbXMLMsgsRq);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            return qbXMLMsgsRq;
        }

        public static string buildQueryRequestXML(string queryType, string identefir, string identefirName, string[] includeRetElement = null)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            XmlElement ItemQueryRq = xmlDoc.CreateElement(queryType);
            qbXMLMsgsRq.AppendChild(ItemQueryRq);
            if (identefir != null)
            {
                XmlElement identefirNameElement = xmlDoc.CreateElement(identefirName);
                ItemQueryRq.AppendChild(identefirNameElement).InnerText = identefir;
            }

            if(includeRetElement != null){
                foreach (var item in includeRetElement)
                {
                    XmlElement includeRet = xmlDoc.CreateElement("IncludeRetElement");
                    ItemQueryRq.AppendChild(includeRet).InnerText = item;
                }
            }

            ItemQueryRq.SetAttribute("requestID", "2");
            xml = xmlDoc.OuterXml;
            return xml;
        }

        public static string buildQueryRequestXML(string queryType, string[] includeRetElement = null)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            XmlElement ItemQueryRq = xmlDoc.CreateElement(queryType);
            qbXMLMsgsRq.AppendChild(ItemQueryRq);

            if (includeRetElement != null)
            {
                foreach (var item in includeRetElement)
                {
                    XmlElement includeRet = xmlDoc.CreateElement("IncludeRetElement");
                    ItemQueryRq.AppendChild(includeRet).InnerText = item;
                }
            }

            ItemQueryRq.SetAttribute("requestID", "2");
            xml = xmlDoc.OuterXml;
            return xml;
        }

        public static string buildQueryRequestXML(string queryType, IReadOnlyCollection<string> identefirs, string identefirName, string[] includeRetElement = null)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            XmlElement ItemQueryRq = xmlDoc.CreateElement(queryType);
            qbXMLMsgsRq.AppendChild(ItemQueryRq);

            if (identefirs != null)
            {
                foreach (var identefir in identefirs)
                {
                    XmlElement identefirNameElement = xmlDoc.CreateElement(identefirName);
                    ItemQueryRq.AppendChild(identefirNameElement).InnerText = identefir;
                }
               
            }

            if (includeRetElement != null)
            {
                foreach (var item in includeRetElement)
                {
                    XmlElement includeRet = xmlDoc.CreateElement("IncludeRetElement");
                    ItemQueryRq.AppendChild(includeRet).InnerText = item;
                }
            }

            ItemQueryRq.SetAttribute("requestID", "2");
            xml = xmlDoc.OuterXml;
            return xml;
        }
    }
}