using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCOM4Lib;

namespace ATP.Collections
{
    public class Tick
    {
        string Symbol { get; set; }
        DateTime Date { get; set; }
        double Price { get; set; }
        double Volume { get; set; }
        string Id { get; set; }
        StOrder_Action Action { get; set; }
        /// <summary>
        /// Конструктор объекта Тик
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <param name="volume"></param>
        /// <param name="id"></param>
        /// <param name="action"></param>
        public Tick(string symbol, DateTime date, double price, double volume, string id, StOrder_Action action)
        {
            Symbol = symbol;
            Date = date;
            Price = price;
            Volume = volume;
            Id = id;
            Action = action;
        }
    }
}
