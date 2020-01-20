using System;
using System.Collections.Generic;
using System.Text;

namespace Babilon_mobile.Model
{
     class PaymentResponse
    {
        public int result { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public PaymentResponse()
        { 

        }
        public PaymentResponse(int result, string description, string status)
        {
            this.result = result;
            this.description = description;
            this.status = status;
        }
        public void ResponseInfo()
        {
            Console.WriteLine("Результат выполнения операции: {0}", result);
            Console.WriteLine("Описание ошибки: {0}", description);
            Console.WriteLine("Статус операции: {0}", status);
        }
    }
}
