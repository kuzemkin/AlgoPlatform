using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartCOM4Lib;
using System.Threading;


namespace ATP
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Подключение com-модуля
        /// </summary>
        StServerClass SmartCom = new StServerClass();
        //Объявление полей        
        public SmartCOM4Lib.StBarInterval Interval;
        private string Login {get; set;}
        private string Password {get; set;}
        public string symbol = "SBER";
        public string Portf = "BP15102-MO-01";
        public StBarInterval interval = StBarInterval.StBarInterval_1Min;
        public List<Collections.Bar> BarsList= new List<Collections.Bar>();
        public List<Collections.Portfolio> PortfList = new List<Collections.Portfolio>();
        public List<Collections.Trade> TradesList = new List<Collections.Trade>();
        public List<Collections.Tick> TicksList = new List<Collections.Tick>();
        public int n = 100;                  //количество запрашиваемых баров 
        public static int nBars = 30;        //количество баров для отрезка экстремумов
        public int ind = nBars;              //начальный индекс  
        public int sma;                //количество баров для скользящей средней  
        public double money;
        /// <summary>
        /// Инициалезация компонентов
        /// </summary>
        public Form1()
        {
            InitializeComponent();           
        }
        /// <summary>
        /// Метод инициалезации поля Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus1_Method(object sender, EventArgs e)
        {
            Login = textBox1.Text;
            
        }
        /// <summary>
        /// Метод инициализации поля Password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus2_Method(object sender, EventArgs e)
        {
            Password = textBox2.Text;
            
        }
        /// <summary>
        /// Метод подключения к серверу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if(SmartCom.IsConnected())
            {
                try
                {                    
                    label3.Text = $"[{DateTime.Now}]: Выполняется отключение.....";
                    SmartCom.disconnect();                    
                }
                catch { label3.Text = $"[{DateTime.Now}]: Возникла ошибка!"; }
            }
            else
            {
                try
                {                   
                    label3.Text = $"[{DateTime.Now}]: Выполняется подключение.....";
                    SmartCom.connect("mx2.ittrade.ru", 8443, Login, Password);                    
                    SmartCom.Connected += ConStatus;
                    SmartCom.Disconnected += DisConStatus;                   
                }
                catch (Exception ex)
                {
                    label3.Text = $"[{DateTime.Now}]: {ex.Message}!";
                }                
            }        
        }
        /// <summary>
        /// Метод отображает статус соединения с сервером
        /// </summary>
        private void ConStatus()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    label3.Text = $"[{DateTime.Now}]: Соединение установлено";
                    button1.Text = "Отключиться";
                    try
                    {
                        SmartCom.ListenPortfolio(Portf);
                        SmartCom.SetPortfolio += AddPortfolio;
                    }
                    catch (Exception ex)
                    {
                        label3.Text = $"[{DateTime.Now}]: {ex.Message}!";
                    }
                }));
            }                   
        }
        /// <summary>
        /// Метод отображает статус соединения с сервером
        /// </summary>
        /// <param name="st"></param>
        private void DisConStatus(string st= "Отключение по инициативе клиента")
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate { label3.Text = $"[{DateTime.Now}]: Соединение разорвано"; button1.Text = "Подключиться"; }));
            }
        }
        /// <summary>
        /// Медот добавляет информацию Portfolio в список
        /// </summary>
        /// <param name="portfolio"></param>
        /// <param name="cash"></param>
        /// <param name="leverage"></param>
        /// <param name="comission"></param>
        /// <param name="saldo"></param>
        /// <param name="liquidationValue"></param>
        /// <param name="initialMargin"></param>
        /// <param name="totalAssets"></param>
        private void AddPortfolio(string portfolio, double cash, double leverage, double comission, double saldo, double liquidationValue, double initialMargin, double totalAssets)
        {
            if(InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate 
                {
                    PortfList.Add(new Collections.Portfolio(portfolio, cash, leverage, comission, saldo, liquidationValue, initialMargin, totalAssets));
                    label5.Text = cash.ToString("# ###.#")+"  руб.";
                    money = cash;
                    label8.Text = saldo.ToString("# ###.#") + "  руб.";
                }));
            }
        }
        /// <summary>
        /// Метод очистки поля ввода инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_Click(object sender, EventArgs e) 
        {
            textBox3.Clear();
        }
        /// <summary>
        /// Метод инициализации поля symbol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus3_Method(object sender, EventArgs e)
        {
            symbol = textBox3.Text;
        }
        /// <summary>
        /// Метод очистки поля при фокусен на поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox4GotFocus(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }
        /// <summary>
        /// Метод считывает количество запрашиваемых баров с поля ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox4LostFocus(object sender, EventArgs e)
        {
            int.TryParse(textBox4.Text, out n);
        }
        /// <summary>
        /// Метод инициалезирует поле ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_Change(object sender, EventArgs e)
        {
            switch(comboBox1.Text)
            {
                case "1 мин.":
                    {
                        interval = StBarInterval.StBarInterval_1Min;
                        break;
                    }
                case "5 мин.":
                    {
                        interval = StBarInterval.StBarInterval_5Min;
                        break;
                    }
                case "10 мин.":
                    {
                        interval = StBarInterval.StBarInterval_10Min;
                        break;
                    }
                case "15 мин.":
                    {
                        interval = StBarInterval.StBarInterval_15Min;
                        break;
                    }
                case "30 мин.":
                    {
                        interval = StBarInterval.StBarInterval_30Min;
                        break;
                    }
                case "60 мин.":
                    {
                        interval = StBarInterval.StBarInterval_60Min;
                        break;
                    }
                case "2 часа":
                    {
                        interval = StBarInterval.StBarInterval_2Hour;
                        break;
                    }
                case "4 часа":
                    {
                        interval = StBarInterval.StBarInterval_4Hour;
                        break;
                    }
                case "1 день":
                    {
                        interval = StBarInterval.StBarInterval_Day;
                        break;
                    }
                case "1 неделя":
                    {
                        interval = StBarInterval.StBarInterval_Week;
                        break;
                    }
            }
        }
        /// <summary>
        /// Метод получает рыночные данные по инструменту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClearSeriesMethod(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            for (int i = 3; chart.Series.Count > i; )
            {
                chart.Series.RemoveAt(i);
            }
        }
        /// <summary>
        /// Метод очищает колекцию точек
        /// </summary>
        /// <param name="ser"></param>
        public void ClearMethod(System.Windows.Forms.DataVisualization.Charting.SeriesCollection ser)
        {
            foreach (System.Windows.Forms.DataVisualization.Charting.Series s in ser)
            {
                s.Points.Clear();
            }
        }
        /// <summary>
        /// Метод получает данные с рынка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //сбрасываем коллекции к начальному значению
            ind = nBars;
            SmartCom.AddBar -= AddBars;
            SmartCom.CancelTicks(symbol);
            BarsList.Clear();
            TradesList.Clear();
            ClearSeriesMethod(chart1);
            ClearMethod(chart1.Series);
            ClearMethod(chart2.Series);
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            //получаем бары
            try
            {
                SmartCom.AddBar += AddBars;
                SmartCom.GetBars(symbol, interval, DateTime.Today,
                    //new DateTime(SetDateTime(interval,n).Year, SetDateTime(interval, n).Month, SetDateTime(interval, n).Day, SetDateTime(interval, n).Hour, SetDateTime(interval, n).Minute, SetDateTime(interval, n).Second), 
                    -n);
                Thread.Sleep(10000);
                SmartCom.AddTick += AddTicks;
                SmartCom.ListenTicks(symbol);
            }
            catch { label3.Text = $"[{DateTime.Now}]: Возникла ошибка!"; }
        }
        /// <summary>
        /// Метод установки даты отсчета
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static DateTime SetDateTime(StBarInterval interval, int n)
        {
            DateTime setdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            switch (interval)
            {
                case StBarInterval.StBarInterval_1Min:
                    setdate = setdate.AddMinutes(-n);                   
                    break;
                case StBarInterval.StBarInterval_5Min:
                    setdate = setdate.AddMinutes(-n * 5);
                    break;
                case StBarInterval.StBarInterval_10Min:
                    setdate = setdate.AddMinutes(-n*10);
                    break;
                case StBarInterval.StBarInterval_15Min:
                    setdate = setdate.AddMinutes(-n * 15);
                    break;
                case StBarInterval.StBarInterval_30Min:
                    setdate = setdate.AddMinutes(-n * 30);
                    break;
                case StBarInterval.StBarInterval_60Min:
                    setdate = setdate.AddMinutes(-n * 60);
                    break;
                case StBarInterval.StBarInterval_2Hour:
                    setdate = setdate.AddHours(-n * 2);
                    break;
                case StBarInterval.StBarInterval_4Hour:
                    setdate = setdate.AddHours(-n * 4);
                    break;
                case StBarInterval.StBarInterval_Day:
                    setdate = setdate.AddDays(-n);
                    break;
                case StBarInterval.StBarInterval_Week:
                    setdate = setdate.AddDays(-n * 7);
                    break;
            }
            return setdate;
        }
        /// <summary>
        /// Метод добавления бара в список баров
        /// </summary>
        /// <param name="row"></param>
        /// <param name="nrows"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="date"></param>
        /// <param name="open"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="close"></param>
        /// <param name="volume"></param>
        /// <param name="open_int"></param>
        private void AddBars(int row, int nrows, string symbol, StBarInterval interval, DateTime date, double open, double high, double low, double close, double volume, double open_int)
        {
            BarsList.Add(new Collections.Bar(date, open, high, low, close)); 
            //
            //добавляем данные на график
            //
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {                    
                    chart1.Series[0].Points.AddXY(date, high, low, open, close);                    
                    //
                    //ниже представлена инстуркция, регулирующая масштаб графика
                    //
                    if (BarsList.Count>nBars)
                    {                        
                       chart1.ChartAreas[0].AxisY.Minimum = chart1.Series[0].Points.Where(p => p.YValues[1] > 0).Min(p => p.YValues[1])-(Math.Abs(chart1.Series[0].Points.Where(p => p.YValues[0] > 0).Min(p => p.YValues[0])- chart1.Series[0].Points.Where(p => p.YValues[1] > 0).Min(p => p.YValues[1])));
                    }
                    //
                    //добавляем скользящую среднею на график
                    //
                    if (BarsList.Count() > nBars)
                    {
                        SMACalculation();                        
                        if (BarsList.Count > sma)
                        {
                            chart1.Series[2].Points.AddXY(BarsList.Last().Date, ((BarsList.GetRange(BarsList.Count() - sma, sma).Select(p => p.Median).Sum()) / sma));
                        }
                    }                    
                }));                 
            }
            else
            {
                chart1.Series[0].Points.AddXY(date, high, low, open, close);
            }
            Strategy1(BarsList, TradesList);
        }
        /// <summary>
        /// Метод добавления Тиков
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <param name="volume"></param>
        /// <param name="id"></param>
        /// <param name="action"></param>
        private void AddTicks(string symbol, DateTime date, double price, double volume, string id, StOrder_Action action)
        {
            TicksList.Add(new Collections.Tick(symbol, date, price, volume, id, action));
            
            if(TicksList.Last().Date.Second==00)
            {
                AddBars(BarsList.Count - 1, BarsList.Count, symbol, interval, TicksList.Last().Date, TicksList[0].Price, TicksList.Select(n => n.Price).Max(), TicksList.Select(n => n.Price).Min(), TicksList.Last().Price, TicksList.Select(n => n.Volume).Sum(), 0);
                TicksList.Clear();
            }
        }
        /// <summary>
        /// Метод создания трейда при соблюдении условий
        /// </summary>
        /// <param name="b"></param>
        public void Strategy1(List<Collections.Bar> b, List<Collections.Trade> t)
        {            
            //проверяем есть ли открытые позиции
            if (t.Count>0 && t.Last().State==Collections.Trade.OrderState.Active)
            {
                //условия выхода
                for (int l=ind-nBars/4, i = ind+1; i+1 < b.Count(); i++, l++)
                {
                    if (b[i].Close < b.GetRange(l, nBars/4).Select(p => p.Low).Min())
                    {
                        StopBuy(b, t, i);
                    }
                }
            }
            else
            {
                if (b.Count > sma)
                {
                    //условия входа
                    for (int l = ind - nBars, i = ind + 1; i + 1 < b.Count(); i++, l++)
                    {
                        if (i > sma)
                        {
                            if (b[i].Close> b.GetRange(l, nBars).Select(p => p.High).Max() & b[i].Median> SMA(b.GetRange(i - sma, sma), sma))
                            {
                                BuyOrder(b, t, i);              
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Метод вычисляет SMA
        /// </summary>
        /// <param name="bar"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private double SMA(List<Collections.Bar> bar, int n)
        {           
            return ((bar.GetRange(bar.Count - n, n).Select(p => p.Median).Sum()) / n);
        }
        /// <summary>
        /// Метод выставляет ордер на покупку
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="i"></param>
        public void BuyOrder(List<Collections.Bar> b, List<Collections.Trade> t, int i)
        {
            if (t.Count() > 0 && t.Last().State != Collections.Trade.OrderState.Active || t.Count() == 0)
            {
                t.Add(new Collections.Trade(b[i + 1].Date, b[i + 1].Open, Collections.Trade.OrderType.Buy, AmountCalculation(money, BarsList)));                
                //выставляем ордер на биржу
                if (b[i].Date==DateTime.Today)
                {
                    SmartCom.PlaceOrder(Portf, symbol, StOrder_Action.StOrder_Action_Buy, StOrder_Type.StOrder_Type_Market, StOrder_Validity.StOrder_Validity_Day, 0, t.Last().Amount , 0, Convert.ToInt32(DateTime.Now.ToBinary()));
                }
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        label10.Text = t.Count().ToString();
                        chart1.Series[1].Points.AddXY(b[i + 1].Date, b[i + 1].Open);
                        chart1.Series.Add(b[i].Date.ToString());
                        chart1.Series.Last().ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        chart1.Series.Last().Color = System.Drawing.Color.Blue;
                        chart1.Series.Last().Points.AddXY(b[i + 1].Date, b[i + 1].Open);
                    }));
                }
                ind = b.FindLastIndex(p => p.Date == t.Last().OpenDate);
            }
        }
        /// <summary>
        /// Метод закрывает открытую позицию в покупке
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="i"></param>
        public void StopBuy(List<Collections.Bar> b, List<Collections.Trade> t, int i)
        {
            if (t.Last().State != Collections.Trade.OrderState.Close)
            {                
                //выставляем ордер на биржу
                if (b[i].Date == DateTime.Today)
                {
                    SmartCom.PlaceOrder(Portf, symbol, StOrder_Action.StOrder_Action_Sell, StOrder_Type.StOrder_Type_Market, StOrder_Validity.StOrder_Validity_Day, 0, t.Last().Amount, 0, Convert.ToInt32(DateTime.Now.ToBinary()));
                }
                t.Last().ClosePrice = b[i + 1].Open;
                t.Last().CloseDate = b[i + 1].Date;
                t.Last().Result = t.Last().ClosePrice - t.Last().OpenPrice;
                t.Last().State = Collections.Trade.OrderState.Close;
                t.Last().Span = t.Last().CloseDate - t.Last().OpenDate;
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        label12.Text = t.Where(n => n.Result > 0).Select(m => m).Count().ToString();
                        label14.Text = t.Where(n => n.Result < 0).Select(m => m).Count().ToString();
                        label16.Text = Math.Round(t.Where(v => v.State == Collections.Trade.OrderState.Close).Select(n => n.Result).Sum(), 1).ToString();
                        label19.Text = Math.Round((t.Where(r => r.Result > 0).Select(s => s.Result).Sum()) / Math.Abs(t.Where(r => r.Result < 0).Select(s => s.Result).Sum()), 2).ToString();
                        label21.Text = Math.Round((b.Last().Close - b[0].Open), 2).ToString();
                        switch (interval)
                        {
                            case StBarInterval.StBarInterval_Day:
                                label23.Text = Math.Round((t.Select(s => s.Span).Sum(m => m.TotalDays) / t.Count())).ToString();
                                break;
                            case StBarInterval.StBarInterval_Week:
                                label23.Text = Math.Round((t.Select(s => s.Span).Sum(m => m.TotalDays) / t.Count())).ToString();
                                break;
                            case StBarInterval.StBarInterval_60Min:
                                label23.Text = Math.Round((t.Select(s => s.Span).Sum(m => m.TotalHours) / t.Count())).ToString();
                                break;
                            case StBarInterval.StBarInterval_2Hour:
                                label23.Text = Math.Round((t.Select(s => s.Span).Sum(m => m.TotalHours) / t.Count())).ToString();
                                break;
                            case StBarInterval.StBarInterval_4Hour:
                                label23.Text = Math.Round((t.Select(s => s.Span).Sum(m => m.TotalHours) / t.Count())).ToString();
                                break;
                            default:
                                label23.Text = Math.Round((t.Select(s => s.Span).Sum(m => m.TotalMinutes) / t.Count())).ToString();
                                break;
                        }
                        chart1.Series[1].Points.AddXY(b[i + 1].Date, b[i + 1].Open);
                        chart1.Series.Last().Points.AddXY(b[i + 1].Date, b[i + 1].Open);
                        chart2.Series[0].Points.AddXY(t.Last().CloseDate, t.Where(v => v.State == Collections.Trade.OrderState.Close).Select(r => r.Result).Sum());
                    }));
                }
                ind = b.FindLastIndex(p => p.Date == t.Last().CloseDate);
            }
        }
        /// <summary>
        /// Метод рассчитывает скользящую среднею от волатильности
        /// </summary>
        public void SMACalculation()
        {
            switch (interval)
            {
                case StBarInterval.StBarInterval_1Min:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)) / 4);
                    nBars = sma ;
                    break;
                case StBarInterval.StBarInterval_5Min:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)) / 2);
                    nBars = sma / 1;
                    break;
                case StBarInterval.StBarInterval_10Min:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)) / 2);
                    nBars = sma / 1;
                    break;
                case StBarInterval.StBarInterval_15Min:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)) / 1);
                    nBars = sma / 1; 
                    break;
                case StBarInterval.StBarInterval_30Min:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)) / 1);
                    nBars = sma / 1;
                    break;
                case StBarInterval.StBarInterval_60Min:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)) / 1);
                    nBars = sma / 1;
                    break;
                case StBarInterval.StBarInterval_2Hour:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)) / 1);
                    nBars = sma/2;
                    break;
                case StBarInterval.StBarInterval_4Hour:
                    sma = (int)((BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars)));
                    break;
                default:
                    sma = (int)(BarsList.Last().Close / ((BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.High).Sum() - (BarsList.GetRange(BarsList.Count() - nBars, nBars).Select(m => m.Low).Sum())) / nBars));
                    break;
            }
        }
        /// <summary>
        /// Метод расчета количество лот
        /// </summary>
        /// <param name="cash"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double AmountCalculation(double cash, List<Collections.Bar> b)
        {
            switch(symbol)
            {
                case "SBER":
                    return Math.Round(cash / b.Last().Close * 10/2);
                case "GAZP":
                    return Math.Round(cash / b.Last().Close * 10 / 2);
                case "LKOH":
                    return Math.Round(cash / b.Last().Close * 1 / 2);
                case "NLMK":
                    return Math.Round(cash / b.Last().Close * 10 / 2);
                case "VTBR":
                    return Math.Round(cash / b.Last().Close * 10000 / 2);
                case "SNGSP":
                    return Math.Round(cash / b.Last().Close * 100 / 2);
                case "MGNT":
                    return Math.Round(cash / b.Last().Close * 1 / 2);
                case "Si-9.19_FT":
                    return 1;
                default:
                    return Math.Round(cash / b.Last().Close * 10 / 2);
            }
        }
    }
}
