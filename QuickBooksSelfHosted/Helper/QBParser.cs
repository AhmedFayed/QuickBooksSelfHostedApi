using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Utilites;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace QuickBooksSelfHostedApi.Helper
{
    public class QBParser
    {
        public static QBRequestStatus ParseRequestRs(string xml, string requestName)
        {
            
            QBRequestStatus RequestRs = new QBRequestStatus();
            try
            {
                XmlNodeList RsNodeList = null;
                XmlDocument Doc = new XmlDocument();
                Doc.LoadXml(xml);
                RsNodeList = Doc.GetElementsByTagName(requestName);
                XmlAttributeCollection rsAttributes = RsNodeList.Item(0).Attributes;
                XmlNode statusCode = rsAttributes.GetNamedItem("statusCode");
                RequestRs.StatusCode = statusCode.Value;
                XmlNode statusSeverity = rsAttributes.GetNamedItem("statusSeverity");
                RequestRs.StatusSeverity = Convert.ToString(statusSeverity.Value);
                XmlNode statusMessage = rsAttributes.GetNamedItem("statusMessage");
                RequestRs.StatusMessage = Convert.ToString(statusMessage.Value);
            }
            catch (Exception e)
            {
                RequestRs.StatusCode = "-1";
                RequestRs.StatusSeverity = "Exception";
                RequestRs.StatusMessage = String.Concat("Error encountered when parsing Invoice info returned from QuickBooks: " + e.Message);
            }
            return RequestRs;
        }
        public static T ParseQBResponse<T>(string xml, string dataNode, string childItemNode = null) 
        {
            if (xml is null)
                return default;
            if (childItemNode != null)
            {
                var elementName = $"</{childItemNode}>";
                var elementToReplace = $"</{childItemNode}><{childItemNode}></{childItemNode}>";
                xml = xml.Replace(elementName, elementToReplace);
            }
            XmlNodeList RsNodeList = null;
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(xml);
            RsNodeList = Doc.GetElementsByTagName(dataNode);
            if (RsNodeList.Count < 1)
            {
                return default;
            }
            var jsonNodeData = JsonConvert.SerializeXmlNode(RsNodeList[0]);
            var jsonData = JObject.Parse(jsonNodeData).GetValue(dataNode).ToString();
            return JsonConvert.DeserializeObject<T>(jsonData, GetDefaultSerializer() );
        }
        public static T ParseQBListResponse<T>(string xml, string QuaryName, string dataNode, string childItemNode = null, bool isReturnValueCollection = false) 
        {
            if (xml is null)
                return default;

            //force xml to make childItemNode to be List
            if (childItemNode != null)
            {
                var elementName = $"</{childItemNode}>";
                var elementToReplace = $"</{childItemNode}><{childItemNode}></{childItemNode}>";
                xml = xml.Replace(elementName, elementToReplace);
            }

            //force xml to make dataNode to be List
            if (isReturnValueCollection)
            {
                var elementName = $"</{dataNode}>";
                var elementToReplace = $"</{dataNode}><{dataNode}></{dataNode}>";
                xml = xml.Replace(elementName, elementToReplace);
            }

            XmlNodeList RsNodeList = null;
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(xml);
            RsNodeList = Doc.GetElementsByTagName(QuaryName);
            if (RsNodeList.Count < 1)
            {
                return default;
            }
            var jsonNodeData = JsonConvert.SerializeXmlNode(RsNodeList[0]);
            var d = JObject.Parse(jsonNodeData);
            var jsonData = d.GetValue(QuaryName).ToString();
            var d2 = JObject.Parse(jsonData).GetValue(dataNode);
            if(d2 is null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(d2.ToString(), GetDefaultSerializer());
        }

        public static string[] PropertiesFromType<T>() where T : class
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            List<string> propNames = new List<string>();
            foreach (PropertyInfo prp in props)
            {
                propNames.Add(prp.Name);
            }
            return propNames.ToArray();
        }

        public static JsonSerializerSettings GetDefaultSerializer()
        {

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.Default,
                NullValueHandling = NullValueHandling.Ignore,
            };

            return jsonSerializerSettings;
        }

    }
}