using System;
using System.Collections.Generic;
using System.Text;

namespace Babilon_mobile.Model
{
    class GetSMSResponse
    {
        public int result { get; set; }
        public string description { get; set; }
        public GetSMSResponse()
        {

        }
        public GetSMSResponse(int result, string description)
        {
            this.result = result;
            this.description = description;
        }
        public void ResponseInfo()
        {
            Console.WriteLine("Результат выполнения операции: {0}", result);
            Console.WriteLine("Описание ошибки: {0}", description);
        }
    }
}
