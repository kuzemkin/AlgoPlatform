using System;
using System.Collections.Generic;
using System.Text;

namespace Babilon_mobile.Model
{
    class GetRateResponse
    {
        public int result { get; set; }
        public decimal rublRate { get; set; }                      
        public string currency { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public GetRateResponse()
        {

        }
        public GetRateResponse(int result, decimal rublRate, string currency, string description, string name)
        {
            this.result = result;
            this.description = description;
            this.rublRate = rublRate;
            this.currency = currency;
            this.name = name;
        }
        public void RespInfo()
        {
            Console.WriteLine($"Результат выполнения операции: {result}");
            Console.WriteLine($"Курс российского рубля на момент запроса: {rublRate}");
            Console.WriteLine($"Валюта счета: {currency}");
            Console.WriteLine($"Описание ошибки: {description}");
            Console.WriteLine($"Имя клиента: {name }");
        }
    }
}
