using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooksSelfHostedApi.Utilites
{
    public static class ConfigurationReader
    {
        public static string GetValue(string key)
        {
            try
            {
                var filePath = Environment.CurrentDirectory + "\\appsettings.json";
                var fileData = System.IO.File.ReadAllText(filePath);
                var JsonSettings = JObject.Parse(fileData);
                return JsonSettings.GetValue(key).ToString();
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
}
