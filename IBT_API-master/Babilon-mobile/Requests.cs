using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Security;

namespace Babilon_mobile
{
    class Requests
    {
        public static Model.GetSMSResponse GetSMS(string phone, string pan)
        {
            string result = "";
            WebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.7.101:22304" + $"/ibt/km_api/v1/sms?phone={phone}&pan={pan}");
            WebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream1 = response.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(stream1))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            Model.GetSMSResponse getSMSResponse = JsonSerializer.Deserialize<Model.GetSMSResponse>(result);

            return getSMSResponse;
        }
        public static Model.GetRateResponse GetRate(string pan)
        {
            string result = "";
            WebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.7.101:22304" + $"/ibt/paymentapi/v1/rate?pan={pan}");            
            WebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream1 = response.GetResponseStream())
            {
                using(StreamReader streamReader=new StreamReader(stream1))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            Model.GetRateResponse getRateResponse = JsonSerializer.Deserialize<Model.GetRateResponse>(result);

                return getRateResponse;
        }
        public static Model.PaymentResponse Payment(string pan, long tran_id, decimal amount, decimal trn_amount, decimal commission, string payment_sys, string key)
        {
            string result = "";
            Model.PaymentRequest paymentRequest = new Model.PaymentRequest(pan, tran_id, amount, trn_amount, commission, payment_sys, getHashSha256(pan + tran_id.ToString() + key));
            string json = JsonSerializer.Serialize<Model.PaymentRequest>(paymentRequest);
            WebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.7.101:22304" + "/ibt/paymentapi/v1/payment");
            request.Method = "POST";
            request.ContentType = "application/json";
            using(Stream stream=request.GetRequestStream())
            {
                using(StreamWriter streamWriter=new StreamWriter(stream))
                {
                    streamWriter.Write(json);
                }
            }
            WebResponse response = (HttpWebResponse)request.GetResponse();
            using(Stream stream1=response.GetResponseStream())
            {
                using(StreamReader streamReader=new StreamReader(stream1))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            Model.PaymentResponse paymentResponse = JsonSerializer.Deserialize<Model.PaymentResponse>(result);
            return paymentResponse;
        }  
        public static Model.P2PResponse P2P(string pan, string expdate, string cvv2, decimal amount, string approval_code, string tran_id, string operation_type, string pan2, string key)
        {
            string result = "";
            string json =JsonSerializer.Serialize<Model.P2PRequest>(new Model.P2PRequest(pan, expdate, cvv2, amount, approval_code, tran_id, operation_type, pan2, getHashSha256(pan+tran_id+key)));            
            WebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.7.101:22304" + "/ibt/km_api/v1/p2p");
            request.Method = "POST";
            request.ContentType = "application/json";
            using(Stream stream=request.GetRequestStream())
            {
                using(StreamWriter streamWriter=new StreamWriter(stream))
                {
                    streamWriter.Write(json);
                }
            }
            WebResponse response = (HttpWebResponse)request.GetResponse();
            using(Stream stream1=response.GetResponseStream())
            {
                using(StreamReader streamReader=new StreamReader(stream1))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            Model.P2PResponse p2PResponse = JsonSerializer.Deserialize<Model.P2PResponse>(result);
            return p2PResponse;
        }
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            System.Security.Cryptography.SHA256Managed hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {                
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
