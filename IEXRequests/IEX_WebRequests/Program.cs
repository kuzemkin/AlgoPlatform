using System;

namespace IEX_WebRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Инициализация запроса.....");
            MyWebRequests.CreateTransactionRequest("http://cpi.iexcoin.info/api/v1/coinbroker/transaction");
            MyWebRequests.Print(MyWebRequests.TransactionStatusRequest("http://cpi.iexcoin.info/api/v1/coinbroker/status"));            
            Console.ReadKey();
        }
    }
}
