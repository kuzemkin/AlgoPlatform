using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace HTTPWebRequest
{
     class WebRequest
    {        
        public static void RequestMethod(string url)
        {
            Console.WriteLine("Try to make webRequest....\n\nResult:");
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url); 
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();                
                using (Stream stream = resp.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                        resp.Close();
                        Console.WriteLine("\nThe request is complete!");                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }            
        }
        public static void RequestMethod(string url, string key, string data)
        {
            Console.WriteLine("Try to make webRequest....\n\nResult:");
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);                
                req.Method = "POST";
                req.Headers.Add("Key", key);
                req.Headers.Add("Sign", GenHMAC(data));
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = byteArray.Length;
                using (Stream dataStream = req.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                using (Stream stream = resp.GetResponseStream())
                {
                    using (StreamReader reader=new StreamReader(stream))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
                resp.Close();
                Console.WriteLine("\nThe request is complete!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public static string GenHMAC(string message)
        {
            byte[] APISecret_Bytes = System.Text.Encoding.UTF8.GetBytes("45695213264253db2dc02e1c736ee1d1a8e4decc3683393db7a7f412b5a18d61e8e1acee3d2b8c2a12dd72a196732dec24a69797b84026f00353c6822deccd87");
            byte[] MESSAGE_Bytes = System.Text.Encoding.UTF8.GetBytes(message);

            var hmac = new HMACSHA512(APISecret_Bytes);
            var hashmessage = hmac.ComputeHash(MESSAGE_Bytes);

            var sign = BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
            return sign;
        }

    }
}
 