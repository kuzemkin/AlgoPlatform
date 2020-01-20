using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;



namespace HTTPWebRequest
{
    class Program
    {
              
        static void Main(string[] args)
        {
            mark1:
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------" +
                "\nВыберите операцию:\n\t 1-Вернуть книгу заявок;\n\t 2-Вернуть информацию по инструментам;\n\t 3-Вернуть проторгованные по инструментам объем;\n\t 4-Вернуть баланс счета;");
            switch (Console.ReadLine())
            {
                case "1":
                    ReturnOrderBook();
                    break;
                case "2":
                    ReturnTicker();
                    break;
                case "3":
                    ReturnVolume();
                    break;
                case "4":
                    ReturnBalance();
                    break;
                default:
                    Console.WriteLine("Выберите операцию");
                    break;                   
            }           
            goto mark1;
        }
        static void ReturnOrderBook()
        {
            WebRequest.RequestMethod("https://poloniex.com/public?command=returnOrderBook&currencyPair=USDT_BTC&depth=10");
        }
        static void ReturnTicker()
        {
            WebRequest.RequestMethod("https://poloniex.com/public?command=returnTicker");
        }
        static void ReturnVolume()
        {
            WebRequest.RequestMethod("https://poloniex.com/public?command=return24hVolume");
        }
        static void ReturnBalance()
        {
            string data = "command=returnBalances&nonce=" + DateTime.Now.ToString("fffffff");
            WebRequest.RequestMethod("https://poloniex.com/tradingApi", "U1QH9P9M-WUG20ICE-BQE7AO11-WNON627Q", data);
        }
    }
}
