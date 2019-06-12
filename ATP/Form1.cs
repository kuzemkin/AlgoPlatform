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
        /// Подключаем com-модуль
        /// </summary>
        StServerClass SmartCom = new StServerClass();
        

        /// <summary>
        /// Инициалезируем компоненты
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Подключаемся к серверу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(SmartCom.IsConnected())
            {
                try
                {
                    label3.Text = $"[{DateTime.Now}]: Выполняется отключение.....";
                    SmartCom.disconnect();
                    SmartCom.Disconnected += DisConStatus;
                }
                catch { label3.Text = $"[{DateTime.Now}]: Возникла ошибка!"; }
            }
            else
            {
                try
                {

                    label3.Text = $"[{DateTime.Now}]: Выполняется подключение.....";
                    SmartCom.connect("mx2.ittrade.ru", 8443, "kuziomkin", "QJHAWU");
                    SmartCom.Connected += ConStatus;
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
    }
}
