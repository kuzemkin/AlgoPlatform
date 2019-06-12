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
        public List<ATP.Collections.Bar> BarsList;
        public List<ATP.Collections.Portfolio> Portf;
        /// <summary>
        /// Инициалезация компонентов
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод инициалезации переменной Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus1_Method(object sender, EventArgs e)
        {
            Login = textBox1.Text;
        }
        /// <summary>
        /// Метод инициализации переменной Password
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
                    SmartCom.ListenPortfolio("BP15102-MO-01");
                    SmartCom.SetPortfolio += AddPortfolio;
                }
                catch { label3.Text = $"[{DateTime.Now}]: Ошибка подключения"; }
            }
                   
        }
        /// <summary>
        /// Метод отображает статус соединения с сервером
        /// </summary>
        private void ConStatus()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate { label3.Text = $"[{DateTime.Now}]: Соединение установлено"; button1.Text = "Отключиться"; }));
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
        private void AddPortfolio(string portfolio, double cash, double levarge, double comission, double saldo, double liquidationValue, double initialMargin, double totalAssets)
        {
            Portf.Add(new Collections.Portfolio(string portfolio, double cash, double leverage, double comission, double saldo, double liquidationValue, double initialMargin, double totalAssets));
        }
    }
}
