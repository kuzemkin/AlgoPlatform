using System;

namespace OpenAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выполнение запроса....");
            //Console.WriteLine("\n"+WebRequests.GetToken("https://openapi-entry-api2.intervale.ru/api/v4/KUZEMKIN000000000000000000000001/token"));
            ExtraMethods.FindRegex("{\"amount\":\"100\"," +
                                    "\"cardCvv\":\"924\"," +
                                    "\"cardExpireMonth\":\"03\"," +
                                    "\"cardExpireYear\":\"21\"," +
                                    "\"cardHolderName\": \"MOMENTUM\"," +
                                    "\"cardNumber\":\"5336690075661704\"," +
                                    "\"currency\":\"USD\"," +
                                    "\"projectId\":\"id12312\"," +
                                    "\"transactionId\":\"2125fe13f12e112\"," +
                                    "\"userEmail\":\"kkuz@gmail.com\"," +
                                    "\"userIp\":\"46.56.82.33\"," +
                                    "\"userId\":\"eed12312\"," +
                                    "\"userRedirectUrl\": \"https://ru.wikipedia.org/wiki/\"}");
        }
    }
}
