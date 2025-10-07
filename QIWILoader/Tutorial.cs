using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIWILoader
{
    public partial class Tutorial : Form
    {
        public Form1.LogDelegate _logger;

        private Dictionary<string, AxWMPLib.AxWindowsMediaPlayer> _mediaPlayers;

        public Tutorial(Form1.LogDelegate logger)
        {
            InitializeComponent(); // <-- Здесь создаются все визуальные компоненты, включая tabControl1 и axWindowsMediaPlayerX
            _logger = logger;

            _mediaPlayers = new Dictionary<string, AxWMPLib.AxWindowsMediaPlayer>();

            // --- ДОБАВЬТЕ ЭТИ СТРОКИ ЗДЕСЬ, В КОНСТРУКТОРЕ TUTORIAL ---
            // Убедитесь, что имена 'axWindowsMediaPlayer1', 'axWindowsMediaPlayer2', 'axWindowsMediaPlayer3'
            // точно соответствуют именам ваших AxWindowsMediaPlayer в свойствах в дизайнере!
            if (this.axWindowsMediaPlayer1 != null) // Проверка на null для безопасности
            {
                _mediaPlayers.Add("player1", this.axWindowsMediaPlayer1); // "player1" - ваш ключ, this.axWindowsMediaPlayer1 - объект плеера
            }
            if (this.axWindowsMediaPlayer3 != null) // Если у вас есть третий плеер (например, для QIWI)
            {
                _mediaPlayers.Add("qiwiPlayer", this.axWindowsMediaPlayer3); // Или любое другое логическое имя, например "player3"
            }
            // --- КОНЕЦ БЛОКА, КОТОРЫЙ НУЖНО ДОБАВИТЬ ---


            // Общая настройка для всех плееров и подписка на события
            foreach (var entry in _mediaPlayers)
            {
                var player = entry.Value;
                player.uiMode = "full";
                player.stretchToFit = true;
                player.settings.autoStart = false;

                player.PlayStateChange += axWindowsMediaPlayer1_PlayStateChange;
                Log($"Плеер '{entry.Key}' инициализирован и добавлен в словарь.", Form1.LogType.Debug);
            }

            if (_mediaPlayers.Count == 0)
            {
                Log("Ошибка: На форме Tutorial не найдены компоненты AxWindowsMediaPlayer. (Это критично!)", Form1.LogType.Error);
                MessageBox.Show("На форме Tutorial не найдены медиаплееры. Перезапустите приложение или обратитесь к разработчику.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Init();
        }
        public async void Init()
        {
            string tut1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "");
            string QIWI = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "QIWI.mp4");
            await Task.Delay(1000);
            axWindowsMediaPlayer1.URL = tut1;
            axWindowsMediaPlayer3.settings.autoStart = false;
            axWindowsMediaPlayer3.URL = QIWI;
        }
        private void Log(string message, Form1.LogType type)
        {
            _logger?.Invoke(message, type);
        }

        public async void LoadAndPlaySpecificVideo(string targetPlayerName, string relativeVideoPath, bool autoPlay, string targetTabName)
        {
            // Получаем нужный плеер из словаря
            if (!_mediaPlayers.TryGetValue(targetPlayerName, out AxWMPLib.AxWindowsMediaPlayer currentPlayer) || currentPlayer == null)
            {
                Log($"Ошибка: Плеер с именем '{targetPlayerName}' не найден или не инициализирован.", Form1.LogType.Error);
                MessageBox.Show($"Плеер '{targetPlayerName}' не готов. Пожалуйста, проверьте конфигурацию.", "Ошибка Плеера", MessageBoxButtons.OK, MessageBoxIcon.Error); // Исправлено
                return;
            }

            // **1. Переключаем вкладку ВНУТРИ формы Tutorial**
            if (this.tabControl1 != null && !string.IsNullOrEmpty(targetTabName))
            {
                if (this.tabControl1.TabPages.ContainsKey(targetTabName))
                {
                    this.tabControl1.SelectedTab = this.tabControl1.TabPages[targetTabName];
                    Log($"Tutorial: Переключено на вкладку '{targetTabName}'.", Form1.LogType.Info);
                }
                else
                {
                    Log($"Tutorial: Предупреждение: Вкладка с именем '{targetTabName}' не найдена в собственном TabControl формы Tutorial.", Form1.LogType.Warning);
                }
            }
            else
            {
                Log("Tutorial: Предупреждение: TabControl не найден на форме Tutorial или имя целевой вкладки пустое.", Form1.LogType.Warning);
            }

            // **2. Загружаем и запускаем видео на ВЫБРАННОМ плеере**
            string fullVideoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", relativeVideoPath);

            if (File.Exists(fullVideoPath))
            {
                // Сначала останавливаем и скрываем ВСЕ плееры, кроме текущего
                foreach (var entry in _mediaPlayers)
                {
                    if (entry.Value != currentPlayer)
                    {
                        entry.Value.Ctlcontrols.stop();
                    }
                }

                await Task.Delay(1000); // Ваша задержка

                currentPlayer.URL = fullVideoPath; // Устанавливаем URL видео
                Log($"Видео: '{Path.GetFileName(fullVideoPath)}' установлено для плеера '{targetPlayerName}'.", Form1.LogType.Info);

                if (autoPlay)
                {
                    currentPlayer.Visible = true;       // Делаем выбранный плеер видимым
                    currentPlayer.Ctlcontrols.play();   // Запускаем воспроизведение
                    Log($"Видео запущено автоматически на плеере '{targetPlayerName}'.", Form1.LogType.Info);
                }
                else
                {
                    currentPlayer.Visible = true;       // Плеер виден, но не играет
                    Log($"Видео загружено для плеера '{targetPlayerName}', ожидает команды запуска (автозапуск отключен).", Form1.LogType.Info);
                }
            }
            else
            {
                Log($"Ошибка: Видеофайл не найден по пути: {fullVideoPath}", Form1.LogType.Error);
                MessageBox.Show($"Видеофайл не найден по пути: {fullVideoPath}.\nПроверьте наличие файла в папке 'Files'.", "Ошибка Видео", MessageBoxButtons.OK, MessageBoxIcon.Error); // Исправлено
                currentPlayer.Visible = false; // Скрываем плеер, если файла нет
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Логируем текущее состояние плеера для диагностики
            switch ((WMPLib.WMPPlayState)e.newState)
            {
                case WMPLib.WMPPlayState.wmppsPlaying:
                    Log("Состояние видео: Воспроизводится.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsPaused:
                    Log("Состояние видео: Пауза.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsStopped:
                    Log("Состояние видео: Остановлено.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsMediaEnded:
                    Log("Состояние видео: Воспроизведение завершено.", Form1.LogType.Info);
                    break;
                case WMPLib.WMPPlayState.wmppsReady:
                    Log("Состояние видео: Плеер готов к воспроизведению.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsBuffering:
                    Log("Состояние видео: Буферизация...", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsUndefined:
                    Log("Состояние видео: Неопределенное состояние.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsTransitioning:
                    Log("Состояние видео: Переход к новому медиа.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsWaiting:
                    Log("Состояние видео: Ожидание данных.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsScanForward:
                    Log("Состояние видео: Перемотка вперед.", Form1.LogType.Debug);
                    break;
                case WMPLib.WMPPlayState.wmppsScanReverse:
                    Log("Состояние видео: Перемотка назад.", Form1.LogType.Debug);
                    break;
                default:
                    Log($"Состояние видео: Изменено на неизвестное ({e.newState}).", Form1.LogType.Debug);
                    break;
            }
        }
    }
}
