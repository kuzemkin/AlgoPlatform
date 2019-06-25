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
            Close
        }
        OrderState State { get; set; }
        double Amount { get; set; }
        int Id { get; set; }           
        public Trade(double openPrice, double closePrice, OrderType order, OrderState state, double amount, int id)
        {
            OpenPrice = openPrice;
            ClosePrice = closePrice;
            Order = order;
            State = state;
            Amount = amount;
            Id = id;
        }
    }
}
