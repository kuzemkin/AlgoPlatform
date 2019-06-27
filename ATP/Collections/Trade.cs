using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP.Collections
{    
    public class Trade
    {
        public DateTime Date { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public enum OrderType
        {
            Buy=1,
            Sell=2
        }
        public OrderType Order { get; set; }
        public enum OrderState
        {
            InProgress=0,
            Active=1,
            Close=2
        }
        public OrderState State { get; set; }
        public double Amount { get; set; }
        public double Result
        {            
            set
            {
                if (Order == OrderType.Buy)
                {
                    Result = ClosePrice - OpenPrice;
                }
                else Result = OpenPrice - ClosePrice;
            }
            get { return Result; }
        }
              
        public Trade(DateTime date, double openPrice, OrderType order, double amount)
        {
            Date = date;
            OpenPrice = openPrice;
            Order = order;
            Amount = amount;            
            ClosePrice = 0;
            State = OrderState.Active;
        }
    }
}
