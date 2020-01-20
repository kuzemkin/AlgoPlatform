using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace IEX_WebRequests
{
    class MyWebRequests
    {
        public static void CreateTransactionRequest(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            using(Stream stream=req.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    string json= "{ \"amount\":\"100\","+
                                    "\"cardCvv\":\"924\","+
                                    "\"cardExpireMonth\":\"03\","+
                                    "\"cardExpireYear\":\"21\"," +
                                    "\"cardHolderName\":\"MOMENTUM\"," +
                                    "\"cardNumber\":\"5336690075661704\","+
                                    "\"currency\":\"USD\","+
                                    "\"projectId\":\"id12312\"," +
                                    "\"transactionId\":\"2135fe13f12e112\"," +
                                    "\"userEmail\":\"kkuz@gmail.com\"," +
                                    "\"userIp\":\"46.56.82.33\"," +
                                    "\"userId\":\"eed12312\"," +
                                    "\"userRedirectUrl\":\"https://ru.wikipedia.org/wiki/\"}";
                    Console.WriteLine(req.Method +" "+ url);
                    Console.WriteLine(req.Headers);
                    Console.WriteLine(json);
                    streamWriter.Write(json);
                    Console.WriteLine("");
                }
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using(Stream stream1=res.GetResponseStream())
                {
                    using(StreamReader streamReader=new StreamReader(stream1))
                    {
                        Console.WriteLine("ContentType:"+res.ContentType);                       
                        Console.WriteLine("");
                        Console.WriteLine(streamReader.ReadToEnd());
                    }
                }
                res.Close();
            }
        }
        public static string TransactionStatusRequest(string url)
        {
            string st= "";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            using(Stream stream=req.GetRequestStream())
            {
                using(StreamWriter streamWriter=new StreamWriter(stream))
                {
                    string json = "{\"transactionId\":\"2122ffe1f12e112\"}";
                    streamWriter.Write(json);
                }
            }
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            using(Stream stream1=res.GetResponseStream())
            {
                using(StreamReader streamReader=new StreamReader(stream1))
                {
                    Console.WriteLine("_____________________________________________________________________________");
                    Console.WriteLine("\n"+req.Method+" " + url);
                    Console.WriteLine(req.Headers);                 
                    //Console.WriteLine("\n"+streamReader.ReadToEnd());
                    st = streamReader.ReadToEnd();
                }
            }
            res.Close();
            return st;
        }        
        public static void Print(string st)
        {
            string[] ar = st.Split(',');
            foreach (string s in ar)
            {
                Console.WriteLine(s);
            }

        }
    }
}
