using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenAPI
{
    class ExtraMethods
    {
        public static void FindRegex(string st)
        {
            string r = @"^[\""]|$[\""]|[\{\""]|[\""}]";
            Regex reg = new Regex(r);
            foreach (string s in st.Split(':'))
            {
                if (Regex.IsMatch(s, r))
                {
                    Console.WriteLine(s);
                }
                
            }
        }
    }
}
