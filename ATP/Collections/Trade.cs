using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP.Collections
{    
    public class Trade
    {
        double OpenPrice { get; set; }
        double ClosePrice { get; set; }
        public enum OrderType
        {
            Buy=1,
            Sell=2
        }
        OrderType Order { get; set; }
        public enum OrderState
        {
            InProgress=0,
            Active=1,
            Close=2
        }
        OrderState State { get; set; }
        double Amount { get; set; }
        double Result
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
              
        public Trade(double openPrice, OrderType order, double amount)
        {
            OpenPrice = openPrice;
            Order = order;
            Amount = amount;
            ClosePrice = 0;
            State = OrderState.Active;
        }
    }
}
