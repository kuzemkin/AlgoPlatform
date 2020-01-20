using System;
using System.Collections.Generic;
using System.Text;

namespace Babilon_mobile.Model
{
    class PaymentRequest
    {
        public string pan { get; set; }
        public long tran_id { get; set; }
        public decimal amount { get; set; }
        public decimal trn_amount { get; set; }
        public decimal commission { get; set; }
        public string payment_sys { get; set; }
        public string signature { get; set; }

        public PaymentRequest(string pan, long tran_id, decimal amount, decimal trn_amount, decimal commission, string payment_sys, string signature)
        {
            this.pan = pan;
            this.tran_id = tran_id;
            this.amount = amount;
            this.trn_amount = trn_amount;
            this.commission = commission;
            this.payment_sys = payment_sys;
            this.signature = signature;
        }
    }
}
