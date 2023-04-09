using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickBooksSelfHostedApi.Utilites
{
    public class Utility
    {
        public static bool IsDecimal(string theValue)
        {
            bool returnVal = false;
            try
            {
                Convert.ToDouble(theValue, System.Globalization.CultureInfo.CurrentCulture);
                returnVal = true;
            }
            catch
            {
                returnVal = false;
            }
            finally
            {
            }

            return returnVal;

        }
    }
}