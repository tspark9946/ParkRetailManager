using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.LIbrary
{
    public class ConfigHelper 
    {
        public static decimal GetTaxRate()
        {
            decimal ret = 0;

            string rateTax = ConfigurationManager.AppSettings["taxRate"];

            bool IsValidTaxRate = Decimal.TryParse(rateTax, out ret);

            if (IsValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up proper");
            }

            return ret;
        }
    }
}
