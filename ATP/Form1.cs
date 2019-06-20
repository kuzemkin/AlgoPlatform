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
        public StBarInterval interval = StBarInterval.StBarInterval_1Min;
        public List<ATP.Collections.Bar> BarsList= new List<Collections.Bar>();
        public List<ATP.Collections.Portfolio> Portf = new List<Collections.Portfolio>();
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
                        SmartCom.ListenPortfolio("BP15102-MO-01");
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
                    Portf.Add(new Collections.Portfolio(portfolio, cash, leverage, comission, saldo, liquidationValue, initialMargin, totalAssets));
                    label5.Text = cash.ToString("# ###.#")+"  руб.";
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
                        interval = StBarInterval.StBarInterval_Month;
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SmartCom.GetBars(symbol, interval, DateTime.Today, 100);
            }
            catch { label3.Text = $"[{DateTime.Now}]: Возникла ошибка!"; }
        }
    }
}
