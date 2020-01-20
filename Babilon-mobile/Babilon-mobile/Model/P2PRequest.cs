using System;
using System.Collections.Generic;
using System.Text;

namespace Babilon_mobile.Model
{
    class P2PRequest
    {
        public string pan { get; set; }
        public string expdate { get; set; }
        public string cvv2 { get; set; }
        public decimal amount { get; set; }
        public string approval_code { get; set; }
        public string tran_id { get; set; }
        public string operation_type { get; set; }
        public string pan2 { get; set; }
        public string account { get; set; }
        public string phone_number { get; set; }
        public string signature { get; set; }
        public P2PRequest()
        {

        }
        public P2PRequest(string pan, string expdate, string cvv2, decimal amount, string approval_code, string tran_id, string operation_type, string pan2, string signature)
        {
            this.pan = pan;
            this.expdate = expdate;
            this.cvv2 = cvv2;
            this.amount = amount;
            this.approval_code = approval_code;
            this.tran_id = tran_id;
            this.operation_type = operation_type;
            this.pan2 = pan2;            
            this.signature = signature;
        }       
    }

}
