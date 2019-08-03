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
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double Volume { get; set; }
        public string Id { get; set; }
        public StOrder_Action Action { get; set; }
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
