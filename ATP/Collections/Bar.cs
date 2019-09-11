using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCOM4Lib;

namespace ATP.Collections
{   
    /// <summary>
    /// Класс бар содержит информацию о баре
    /// </summary>
    public class Bar
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; } 
        public double Median { get; set; }

        /// <summary>
        /// Метод-конструктор экземпляра бар
        /// </summary>
        /// <param name="date"></param>
        /// <param name="open"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="close"></param>
        public Bar(string symbol, SmartCOM4Lib.StBarInterval interval, DateTime date, double open, double high, double low, double close)
        {
            Symbol = symbol;
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Median = (open + close + high + low) / 4;
        }
    }
}

