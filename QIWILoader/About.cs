using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIWILoader
{
    public partial class About : Form
    {
        private UtilityClass _utilityHelper;
        private readonly string _logFilePath;
        // Мы используем LogType из Form1, потому что он публичный.
        // Удалите enum LogType из About, если он был здесь.
        // public enum LogType { Info, Warning, Error, Debug } <--- УДАЛИТЬ ЭТОТ ENUM!

        public About()
        {
            InitializeComponent();
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Loader.log");
        }

        // Ваш метод Log теперь должен принимать LogType из Form1
        public void Log(string message, Form1.LogType type) // <--- ИЗМЕНИТЕ ТИП ДЕЛЕГАТА
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{type.ToString().ToUpper()}]: {message}";
            Console.WriteLine(logEntry);

            try
            {
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог-файл: {ex.Message}");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://t.me/osmpqiwi");
            Process.Start(sInfo);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Создаем и показываем Tutorial
            string tut1 = "QIWI.mp4"; // Убедитесь, что это реальное имя файла в папке Files
            Tutorial tutorialForm = new Tutorial(this.Log);
            tutorialForm.Show();
            Log("Открыто окно 'Туториал'.", Form1.LogType.Info);

            // Теперь создаем UtilityClass и передаем ему ссылку на только что созданный tutorialForm
            _utilityHelper = new UtilityClass(this.Log, tutorialForm);

            // Запускаем видео через UtilityClass, который затем обращается к tutorialForm
            _utilityHelper.PlayVideoInTutorial("qiwiPlayer", tut1, true, "tabPage3");
        }
    }
}