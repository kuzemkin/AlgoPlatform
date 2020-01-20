using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace OpenAPI
{
    class WebRequests
    {
        public static string GetToken(string url)
        {
            string st = "";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            using(Stream stream=res.GetResponseStream())
            {
                using(StreamReader streamReader=new StreamReader(stream))
                {
                    st = streamReader.ReadToEnd();
                }
            }
            res.Close();
            return st;
        }
    }
}
