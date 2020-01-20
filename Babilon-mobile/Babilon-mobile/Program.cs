using System;
using System.Text.Json;

namespace Babilon_mobile
{
    class Program
    {
        static void Main(string[] args)
        {
            //Requests.GetRate("901117077").RespInfo();
            //Requests.GetSMS("918630100", "5058270221000580").ResponseInfo();
            Requests.P2P("5058270221000580", "0322", "334", 100, "443", DateTime.Now.Ticks.ToString(), "C2C", "5058270222006644", "9802CEBC-67AA-41EA-8F1F-50DF8B31A099").ResponseInfo();
            //Requests.Payment("20218810000001022811",DateTime.Now.Ticks,100,99,1, "2021881", "9802CEBC-67AA-41EA-8F1F-50DF8B31A099").ResponseInfo();            
            Console.ReadKey();            
        }
    }
}
