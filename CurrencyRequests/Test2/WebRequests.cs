using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace Test2
{
    class WebRequests
    {
        public static string WebRequestMethod(string url)
        {
            string st = "";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (Stream stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        st = reader.ReadToEnd();
                    }
                }
                res.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return st;
        }
        public static string WebRequestMethod(string url, string key, string id, string signature)
        {
            string st = "";
            string data = "key=" + key + "&id=" + id + "&nonce=" + DateTime.Now.Millisecond.ToString("fffffffff") + "&signature=" + signature;
            byte[] bateArray = System.Text.Encoding.UTF8.GetBytes(data);
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";
                req.Accept = "application/json";
                req.ContentType = ("application/x-www-form-urlencoded");
                req.Headers.Add("authority", "http://api.intervale.bitstamp.currency.com");
                req.Headers.Add("cache-control", "max-age=0");
                req.Headers.Add("upgrade-insecure-requests", "1");
                using (Stream dataStream= req.GetRequestStream())
                {
                    dataStream.Write(bateArray, 0, bateArray.Length);
                }
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (Stream stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        st = reader.ReadToEnd();
                    }
                }
                res.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return st;
        }
        public static string GenHMAC(string message)
        {
            byte[] APISecret_Bytes = System.Text.Encoding.UTF8.GetBytes("market");
            byte[] MESSAGE_Bytes = System.Text.Encoding.UTF8.GetBytes(message);

            var hmac = new HMACSHA512(APISecret_Bytes);
            var hashmessage = hmac.ComputeHash(MESSAGE_Bytes);

            var sign = BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
            return sign;
        }
    }
}
