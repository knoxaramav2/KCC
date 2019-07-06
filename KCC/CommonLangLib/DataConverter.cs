using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    public class DataConverter
    {
        //TODO Maybe useful? Might just use modern directives
        public static string DecimalToRep(double d)
        {
            const int radix = 10;
            var integral = (int)d;
            string rep;

            int quotient;
            double product;

            #region integral portion
            var intList = new List<int>();
            quotient = integral;

            while (quotient != 0)
            {
                intList.Add(quotient%radix);
                quotient /= radix;
            }

            #endregion
            #region decimal portion
            var deciList = new List<int>();
            product = d - integral;
            int whole;
            while ((product - (int)product) != 0)
            {
                product *= radix;
                whole = (int)product;
            } 

            #endregion



            return null;
        }
    }
}
