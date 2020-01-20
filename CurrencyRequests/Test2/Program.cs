using System;

namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выполняется запрос в систему.....");
            Console.WriteLine(WebRequests.WebRequestMethod("https://api.intervale.bitstamp.currency.com/api/v2/order_status/", "3381769202835987", "00000000-5972-1b7b-0000-00000001b1ed", "market"));           
            Console.ReadKey();
        }
    }
}
