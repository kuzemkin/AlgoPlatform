using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP.Collections
{   
    /// <summary>
    /// Класс бар содержит информацию о баре
    /// </summary>
    public class Bar
    {
        DateTime Date { get; set; }
        double Open { get; set; }
        double High { get; set; }
        double Low { get; set; }
        double Close { get; set; }       

        /// <summary>
        /// Метод-конструктор экземпляра бар
        /// </summary>
        /// <param name="date"></param>
        /// <param name="open"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="close"></param>
        public Bar(DateTime date, double open, double high, double low, double close)
        {
            Date = Date;
            Open = Open;
            High = High;
            Low = Low;
            Close = Close;
        }
    }
}

