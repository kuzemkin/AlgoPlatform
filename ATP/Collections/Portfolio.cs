using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP.Collections
{
    /// <summary>
    /// Класс Portfolio содержит информацию о счете
    /// </summary>
    public class Portfolio
    {
        string Portf { get; set; }
        double Cash { get; set; }
        double Leverage { get; set; }
        double Comission { get; set; }
        double Saldo { get; set; }
        double LiquidationValue { get; set; }
        double InitialMargin { get; set; }
        double TotalAssets { get; set; }
        /// <summary>
        /// Метод-конструктор экземпляра Portfolio
        /// </summary>
        /// <param name="portfolio"></param>
        /// <param name="cash"></param>
        /// <param name="levarge"></param>
        /// <param name="comission"></param>
        /// <param name="saldo"></param>
        /// <param name="liquidationValue"></param>
        /// <param name="initialMargin"></param>
        /// <param name="totalAssets"></param>
        public Portfolio(string portfolio, double cash, double leverage, double comission, double saldo, double liquidationValue, double initialMargin, double totalAssets)
        {
            Portf = portfolio;
            Cash = cash;
            Leverage = leverage;
            Comission = comission;
            Saldo = saldo;
            LiquidationValue = liquidationValue;
            InitialMargin = initialMargin;
            TotalAssets = totalAssets;
        }
    }
}
