using Guna.UI2.WinForms; // For Guna UI elements
using Newtonsoft.Json;   // For working with JSON (Loader.json, settings.json)
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics; // For Process.Start
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QIWILoader
{
    public partial class Form1 : Form
    {
        private readonly string _logFilePath;
        private readonly string _contentFolderPath; // Путь к папке Content
        private readonly string _loaderJsonPath;    // Путь к файлу Loader.json
        private AppSettings _currentSettings;       // Для хранения текущих настроек темы
        private string _lastLaunchedShellDirectory; // Для хранения пути к последней запущенной оболочке
        public delegate void LogDelegate(string message, LogType type);
        private UtilityClass _utilityHelper;
        private readonly string versionsUrl = "http://k90052gj.beget.tech/API/versions.json";
        private readonly string currentVersion = "1.0.0";

        public Form1()
        {
            InitializeComponent();

            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Loader.log");
            _contentFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");
            _loaderJsonPath = Path.Combine(_contentFolderPath, "Loader.json");

            Log("Инициализация успешна.", LogType.Info);

            // --- Инициализация настроек темы ---
            _currentSettings = AppSettings.Load();
            ApplySettings(_currentSettings); // Применяем настройки сразу при старте

            // --- Проверка папки Content и Loader.json (для оболочек) ---
            if (!Directory.Exists(_contentFolderPath))
            {
                Log($"Папка Content не найдена: {_contentFolderPath}", LogType.Error);
                MessageBox.Show($"Папка 'Content' не найдена по пути: {_contentFolderPath}. Приложение не может загрузить оболочки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!File.Exists(_loaderJsonPath))
            {
                Log($"Файл Loader.json не найден: {_loaderJsonPath}", LogType.Error);
                MessageBox.Show($"Файл 'Loader.json' не найден по пути: {_loaderJsonPath}. Приложение не может загрузить оболочки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoadShells(); // Загружаем оболочки, если все пути корректны
            }

            // Скрываем боковую панель при запуске, если она изначально не нужна
            if (guna2Panel1 != null)
            {
                guna2Panel1.Visible = false;
            }
            _ = CheckForUpdatesAsync();
        }
        private async Task CheckForUpdatesAsync()
        {
            Log("Начата проверка обновлений...", LogType.Info);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                    string json = await client.GetStringAsync(versionsUrl);
                    Log($"Получен JSON с информацией о версиях", LogType.Debug);

                    // --- ИЗМЕНЕНИЯ ЗДЕСЬ: Парсим JSON как JObject и ищем нужный ключ ---
                    JObject allVersions = JObject.Parse(json); // Парсим весь JSON как объект

                    // Ищем объект "QL" внутри JSON
                    JToken qlInfoToken = allVersions["QL"];

                    string latestVersion = null; // Переменная для хранения версии QL

                    if (qlInfoToken != null && qlInfoToken["version"] != null)
                    {
                        latestVersion = qlInfoToken["version"].ToString();
                        Log($"Версия 'QIWI Loader' на сервере: {latestVersion}", LogType.Debug);
                    }
                    else
                    {
                        Log("Ошибка: Информация о версии 'QIWI Loader' не найдена в JSON или имеет неверный формат.", LogType.Error);
                        // Можно показать ошибку пользователю, если это критично
                        return; // Выходим, так как не можем определить версию
                    }
                    // --- КОНЕЦ ИЗМЕНЕНИЙ ---

                    Log($"Текущая версия приложения: {currentVersion}, Последняя версия 'QIWI Loader' на сервере: {latestVersion}", LogType.Info);

                    // Сравниваем версии
                    if (latestVersion != currentVersion)
                    {
                        Log("Доступно новое обновление для 'QIWI Loader'!", LogType.Info);
                        guna2Button9.Visible = true;
                        new Update().ShowDialog();
                    }
                    else
                    {
                        Log("У вас установлена последняя версия 'QIWI Loader'.", LogType.Info);
                        guna2Button9.Visible = false;
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    Log($"Ошибка при загрузке информации о версиях: {httpEx.Message}", LogType.Error);
                    MessageBox.Show($"Не удалось получить информацию об обновлениях. Проверьте подключение к интернету или попробуйте позже.\nОшибка: {httpEx.Message}", "Ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (JsonException jsonEx) // Используем общий JsonException, так как Parse() и другие могут его бросить
                {
                    Log($"Ошибка парсинга JSON или доступа к данным: {jsonEx.Message}", LogType.Error);
                    MessageBox.Show($"Ошибка в формате информации об обновлении. Сообщите разработчику.\nОшибка: {jsonEx.Message}", "Ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    Log($"Произошла непредвиденная ошибка при проверке обновлений: {ex.Message}", LogType.Error);
                    MessageBox.Show($"Произошла непредвиденная ошибка при проверке обновлений.\nОшибка: {ex.Message}", "Ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Метод для логирования сообщений в консоль и файл
        public void Log(string message, LogType type)
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

        // Перечисление для типов логов
        public enum LogType
        {
            Info,
            Warning,
            Error,
            Debug
        }

        /// <summary>
        /// Применяет текущие настройки внешнего вида к форме и всем ее элементам.
        /// </summary>
        private void ApplySettings(AppSettings settings)
        {
            // Установка фонового изображения или цвета
            if (!string.IsNullOrEmpty(settings.BackgroundImagePath) && File.Exists(settings.BackgroundImagePath))
            {
                try
                {
                    this.BackgroundImage = Image.FromFile(settings.BackgroundImagePath);
                    this.BackgroundImageLayout = ImageLayout.Stretch; // Можно использовать Zoom, Center, Tile и т.д.
                    Log($"Применено фоновое изображение: {settings.BackgroundImagePath}", LogType.Debug);
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при загрузке фонового изображения {settings.BackgroundImagePath}: {ex.Message}", LogType.Error);
                    this.BackgroundImage = null; // Сброс изображения
                    this.BackColor = settings.GetFormBackgroundColor(); // Применение цвета
                }
            }
            else
            {
                this.BackgroundImage = null; // Сброс изображения, если путь пуст или файл не существует
                this.BackColor = settings.GetFormBackgroundColor(); // Применение цвета
                Log($"Фоновое изображение не установлено. Применен цвет фона формы: {settings.GetFormBackgroundColor().Name}", LogType.Debug);
            }

            // Применение цвета тени для Guna2BorderlessForm
            if (this.guna2BorderlessForm1 != null) // Предполагается, что у вас есть этот компонент
            {
                this.guna2BorderlessForm1.ShadowColor = settings.GetFormShadowColor();
                Log($"Применен цвет тени формы: {settings.GetFormShadowColor().Name}", LogType.Debug);
            }
            else
            {
                Log("Компонент Guna2BorderlessForm не найден на форме. Цвет тени не применен.", LogType.Warning);
            }

            // Применяем цвета к основным элементам формы (панелям, кнопкам и т.д.)
            // Предполагается, что у вас есть Guna2Panel с именем guna2Panel1 для бокового меню
            if (guna2Panel1 != null) // Предполагается, что у вас есть эта панель
            {
                guna2Panel1.FillColor = settings.GetButtonColor(); // Цвет фона панели меню
                guna2Panel1.ForeColor = settings.GetButtonTextColor(); // Цвет текста на панели (если есть лейблы)
            }

            // Обновляем цвета кнопок (предполагаются существующие кнопки)
            UpdateGunaButtonColors(guna2Button1, settings); // Кнопка закрытия
            UpdateGunaButtonColors(guna2Button2, settings); // Кнопка максимизации
            UpdateGunaButtonColors(guna2Button3, settings); // Кнопка минимизации
            UpdateGunaButtonColors(guna2Button4, settings); // Кнопка меню
            UpdateGunaButtonColors(guna2Button5, settings); // Кнопка "Кастомизация"
            UpdateGunaButtonColors(guna2Button6, settings); // Кнопка "О программе"
            UpdateGunaButtonColors(guna2Button7, settings); // Кнопка "Локальные файлы"
            UpdateGunaButtonColors(guna2Button8, settings); // Кнопка "Загрузка оболочек"
            UpdateGunaButtonColors(guna2Button9, settings);
            UpdateGunaButtonColors(guna2Button10, settings);

            // --- Применение стиля границы окна ---
            this.FormBorderStyle = settings.GetFormBorderStyle();
            if (this.FormBorderStyle != FormBorderStyle.None)
            {
                if (guna2BorderlessForm1 != null) guna2BorderlessForm1.HasFormShadow = false;
                Log($"Стиль окна установлен на {this.FormBorderStyle}. Guna2BorderlessForm включен.", LogType.Info);
            }
            else
            {
                if (guna2BorderlessForm1 != null) guna2BorderlessForm1.HasFormShadow = true;
                Log("Стиль окна установлен на None. Guna2BorderlessForm отключен.", LogType.Info);
            }
            // --- Конец применения стиля границы окна ---

            Log("Настройки применены к существующим элементам.", LogType.Debug);
        }

        // Вспомогательный метод для обновления цвета кнопок Guna
        private void UpdateGunaButtonColors(Guna2Button button, AppSettings settings)
        {
            if (button != null)
            {
                button.FillColor = settings.GetButtonColor();
                button.ForeColor = settings.GetButtonTextColor();
            }
        }

        // --- Загрузка и отображение оболочек ---
        private void LoadShells()
        {
            try
            {
                string jsonContent = File.ReadAllText(_loaderJsonPath);
                List<LoaderEntry> shells = JsonConvert.DeserializeObject<List<LoaderEntry>>(jsonContent);

                if (shells == null || !shells.Any())
                {
                    Log("Loader.json пуст или содержит некорректные данные. Оболочки не найдены.", LogType.Warning);
                    return;
                }

                // Очищаем существующие панели перед добавлением новых
                if (flowLayoutPanel1 != null) // Предполагается, что у вас есть flowLayoutPanel1 для оболочек
                {
                    flowLayoutPanel1.Controls.Clear();
                }

                foreach (var shell in shells)
                {
                    CreateShellPanel(shell);
                }
                Log($"Загружено {shells.Count} оболочек из Loader.json.", LogType.Info);
            }
            catch (JsonException jEx)
            {
                Log($"Ошибка десериализации Loader.json: {jEx.Message}", LogType.Error);
                MessageBox.Show($"Ошибка при чтении файла Loader.json: {jEx.Message}", "Ошибка JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Log($"Неизвестная ошибка при загрузке оболочек: {ex.Message}", LogType.Error);
                MessageBox.Show($"Произошла ошибка при загрузке оболочек: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateShellPanel(LoaderEntry shell)
        {
            // Здесь предполагается, что flowLayoutPanel1 существует на вашей форме
            if (flowLayoutPanel1 == null)
            {
                Log("flowLayoutPanel1 не найден на форме. Невозможно создать панели оболочек.", LogType.Error);
                return;
            }

            // Проверяем критически важные поля перед использованием
            if (string.IsNullOrEmpty(shell.PngPreview) || string.IsNullOrEmpty(shell.ExeFile))
            {
                Log($"Пропущена оболочка '{shell.Name ?? "Неизвестная"}' из-за отсутствия PngPreview или ExeFile.", LogType.Warning);
                return; // Пропускаем создание панели для этой некорректной оболочки
            }

            Panel panel = new Panel();
            panel.Size = new Size(142, 151);
            panel.Margin = new Padding(3);
            panel.BackColor = _currentSettings.GetButtonColor(); // Применяем цвет кнопок к фону панели оболочки
            panel.ForeColor = _currentSettings.GetButtonTextColor(); // Применяем цвет текста кнопок к тексту панели

            // Guna2PictureBox для предпросмотра
            Guna.UI2.WinForms.Guna2PictureBox pictureBox = new Guna.UI2.WinForms.Guna2PictureBox();
            pictureBox.ImageRotate = 0F;
            pictureBox.Location = new Point(3, 3);
            pictureBox.Size = new Size(136, 76); // Немного уменьшил, чтобы поместилось
            pictureBox.TabStop = false;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BorderRadius = 5; // Скругление углов

            string imagePath = Path.Combine(_contentFolderPath, shell.PngPreview);
            if (File.Exists(imagePath))
            {
                try
                {
                    // Создаем копию изображения, чтобы избежать блокировки файла
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(fs);
                    }
                }
                catch (OutOfMemoryException)
                {
                    Log($"Не удалось загрузить изображение: {imagePath}. Возможно, файл поврежден или имеет неподдерживаемый формат.", LogType.Error);
                    pictureBox.Image = null;
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при загрузке изображения {imagePath}: {ex.Message}", LogType.Error);
                    pictureBox.Image = null;
                }
            }
            else
            {
                Log($"Файл изображения не найден: {imagePath}", LogType.Warning);
            }
            panel.Controls.Add(pictureBox);

            // Label для названия оболочки
            Label nameLabel = new Label();
            nameLabel.AutoSize = false; // Отключаем AutoSize, чтобы управлять размером
            nameLabel.Size = new Size(130, 18); // Задаем фиксированный размер
            nameLabel.Font = new Font("Microsoft Sans Serif", 6.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204))); // Жирный шрифт
            nameLabel.ForeColor = _currentSettings.GetTextColor(); // Применяем цвет текста
            nameLabel.Location = new Point(5, 82); // Позиция
            nameLabel.Text = shell.Name;
            nameLabel.TextAlign = ContentAlignment.MiddleCenter; // Выравнивание текста
            FitTextToLabel(nameLabel, nameLabel.Width); // Подгонка текста
            panel.Controls.Add(nameLabel);

            // Label для автора оболочки
            Label authorLabel = new Label();
            authorLabel.AutoSize = false; // Отключаем AutoSize
            authorLabel.Size = new Size(130, 18); // Задаем фиксированный размер
            authorLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            authorLabel.ForeColor = _currentSettings.GetTextColor(); // Применяем цвет текста
            authorLabel.Location = new Point(5, 98); // Позиция
            authorLabel.Text = "Автор: " + shell.Author; // Добавляем "Автор: "
            authorLabel.TextAlign = ContentAlignment.MiddleCenter; // Выравнивание текста
            FitTextToLabel(authorLabel, authorLabel.Width); // Подгонка текста
            panel.Controls.Add(authorLabel);

            // Guna2Button для запуска
            Guna.UI2.WinForms.Guna2Button runButton = new Guna.UI2.WinForms.Guna2Button();
            // Стили Guna2Button (можно настроить по своему вкусу)
            runButton.DisabledState.BorderColor = Color.DarkGray;
            runButton.DisabledState.CustomBorderColor = Color.DarkGray;
            runButton.DisabledState.FillColor = Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            runButton.DisabledState.ForeColor = Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            runButton.Font = new Font("Segoe UI", 9F);
            runButton.ForeColor = _currentSettings.GetButtonTextColor(); // Цвет текста кнопки
            runButton.FillColor = _currentSettings.GetButtonColor(); // Цвет кнопки
            runButton.Location = new Point(3, 117);
            runButton.Size = new Size(136, 31); // Немного уменьшил, чтобы поместилось
            runButton.Text = "Запуск";
            runButton.BorderRadius = 5; // Скругление углов

            string shellExeRelativePath = shell.ExeFile;
            string fullShellExePath = Path.Combine(_contentFolderPath, shellExeRelativePath);
            string shellExeDirectory = Path.GetDirectoryName(fullShellExePath);

            // Проверяем, существует ли целевой EXE-файл, прежде чем пытаться создать BAT
            if (!File.Exists(fullShellExePath))
            {
                Log($"Исполняемый файл оболочки не найден: {fullShellExePath}. Кнопка запуска будет отключена.", LogType.Warning);
                runButton.Enabled = false;
                runButton.Text = "Не найдено"; // Оповещаем пользователя
            }
            else
            {
                string batFileName = Path.GetFileNameWithoutExtension(shellExeRelativePath) + "_launcher.bat";
                string fullBatPath = Path.Combine(shellExeDirectory, batFileName);

                // Генерируем содержимое BAT-файла
                string batContent = $"@echo off\nstart \"\" \"{Path.GetFileName(fullShellExePath)}\"\nexit";

                try
                {
                    // Убедимся, что директория для BAT-файла существует
                    if (!Directory.Exists(shellExeDirectory))
                    {
                        Directory.CreateDirectory(shellExeDirectory);
                        Log($"Создана директория для BAT-файла: {shellExeDirectory}", LogType.Info);
                    }
                    File.WriteAllText(fullBatPath, batContent);
                    Log($"Создан BAT-файл: {fullBatPath}", LogType.Info);
                    runButton.Tag = fullBatPath; // Сохраняем путь к BAT-файлу
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при создании BAT-файла для {shell.Name}: {ex.Message}", LogType.Error);
                    MessageBox.Show($"Не удалось создать BAT-файл для '{shell.Name}'. Возможно, не хватает прав доступа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    runButton.Enabled = false;
                    runButton.Text = "Ошибка BAT";
                }
            }

            runButton.Click += RunButton_Click;
            panel.Controls.Add(runButton);

            flowLayoutPanel1.Controls.Add(panel); // Добавляем панель в FlowLayoutPanel
            Log($"Создана панель для оболочки: {shell.Name}", LogType.Debug);
        }

        private void FitTextToLabel(Label label, int maxWidth)
        {
            if (label == null || string.IsNullOrEmpty(label.Text) || maxWidth <= 0)
            {
                return;
            }

            string originalText = label.Text;
            float currentFontSize = label.Font.Size;
            Font currentFont = label.Font;

            const float minFontSize = 1.0f; // Попробуйте 6.0f или 5.0f, если нужно
            const float step = 0.5f;

            using (Graphics g = label.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(originalText, currentFont);

                // Сначала пробуем уменьшить шрифт
                while (textSize.Width > maxWidth && currentFontSize > minFontSize)
                {
                    currentFontSize -= step;
                    currentFont = new Font(label.Font.FontFamily, currentFontSize, label.Font.Style);
                    textSize = g.MeasureString(originalText, currentFont);
                }

                // Если даже с минимальным или уменьшенным шрифтом текст все еще слишком длинный, обрезаем и добавляем многоточие
                if (textSize.Width > maxWidth && originalText.Length > 3) // Проверяем, есть ли что обрезать
                {
                    string tempText = originalText;
                    int charsToKeep = tempText.Length;

                    // Обрезаем по одному символу, пока текст с многоточием не поместится
                    while (g.MeasureString(tempText + "...", currentFont).Width > maxWidth && charsToKeep > 3)
                    {
                        charsToKeep--;
                        tempText = originalText.Substring(0, charsToKeep);
                    }
                    label.Text = tempText + "...";
                }
                else // Если поместилось или нет смысла обрезать
                {
                    label.Text = originalText; // Возвращаем оригинальный текст, если он уже подогнан
                }

                // Применяем финальный размер шрифта
                label.Font = currentFont;
            }
        }


        // --- Обработчик кнопки "Запуск" для оболочек ---
        private void RunButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = sender as Guna.UI2.WinForms.Guna2Button;
            if (clickedButton != null && clickedButton.Tag is string batFilePath)
            {
                if (File.Exists(batFilePath))
                {
                    try
                    {
                        string batFileDirectory = Path.GetDirectoryName(batFilePath);

                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = batFilePath;
                        startInfo.WorkingDirectory = batFileDirectory;
                        startInfo.UseShellExecute = true;

                        System.Diagnostics.Process.Start(startInfo);
                        Log($"Запуск BAT-файла: {batFilePath} с рабочим каталогом: {batFileDirectory}", LogType.Info);
                        _lastLaunchedShellDirectory = batFileDirectory; // Сохраняем директорию запущенной оболочки
                    }
                    catch (Win32Exception w32Ex)
                    {
                        Log($"Ошибка Win32 при запуске {batFilePath}: {w32Ex.Message}. Возможно, файл поврежден, отсутствуют права или он не является исполняемым.", LogType.Error);
                        MessageBox.Show($"Ошибка при запуске файла '{Path.GetFileName(batFilePath)}': {w32Ex.Message}", "Ошибка запуска", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        Log($"Неизвестная ошибка при запуске {batFilePath}: {ex.Message}", LogType.Error);
                        MessageBox.Show($"Неизвестная ошибка при запуске файла '{Path.GetFileName(batFilePath)}': {ex.Message}", "Ошибка запуска", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Log($"BAT-файл не найден: {batFilePath}", LogType.Error);
                    MessageBox.Show($"BAT-файл '{Path.GetFileName(batFilePath)}' не найден. Возможно, он не был создан при загрузке.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- Обработчики кнопок окна ---
        private void guna2Button1_Click(object sender, EventArgs e) => Application.Exit(); // Кнопка закрытия
        private void guna2Button2_Click(object sender, EventArgs e) => this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized; // Кнопка максимизации/восстановления
        private void guna2Button3_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized; // Кнопка минимизации

        // --- Обработчик кнопки "Меню" (guna2Button4) ---
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2Panel1 != null)
            {
                guna2Panel1.Visible = !guna2Panel1.Visible; // Переключаем видимость
                Log($"Видимость панели меню (guna2Panel1) переключена на: {guna2Panel1.Visible}", LogType.Debug);
            }
        }

        // --- Обработчик кнопки "Кастомизация" (guna2Button5) ---
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр формы настроек, передавая текущий объект AppSettings
            // и текущий FormBorderStyle главной формы
            SettingsForm settingsForm = new SettingsForm(_currentSettings, this.FormBorderStyle);

            // Показываем форму настроек как диалоговое окно
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // Если пользователь нажал "Применить" (ОК), обновляем настройки _currentSettings
                // используя значения из публичных свойств Selected... формы настроек.
                _currentSettings.FormBackgroundColorArgb = settingsForm.SelectedFormBackgroundColor.ToArgb().ToString();
                _currentSettings.FormShadowColorArgb = settingsForm.SelectedFormShadowColor.ToArgb().ToString();
                _currentSettings.TextColorArgb = settingsForm.SelectedTextColor.ToArgb().ToString();
                _currentSettings.ButtonColorArgb = settingsForm.SelectedButtonColor.ToArgb().ToString();
                _currentSettings.ButtonTextColorArgb = settingsForm.SelectedButtonTextColor.ToArgb().ToString();
                _currentSettings.BackgroundImagePath = settingsForm.SelectedBackgroundImagePath;
                _currentSettings.FormBorderStyleString = settingsForm.SelectedFormBorderStyle.ToString(); // Обновляем стиль окна

                // Сохраняем обновленные настройки и применяем их к текущей форме
                _currentSettings.Save();
                ApplySettings(_currentSettings); // Применяем настройки к главной форме
                Log("Настройки приложения обновлены и сохранены.", LogType.Info);

                // ! Важно !: Если настройки влияют на отображение оболочек,
                // их нужно перерисовать.
                LoadShells(); // Перезагрузка оболочек, чтобы применить новые цвета к их панелям
            }
            // Если DialogResult не OK (например, Cancel), ничего не делаем, настройки не меняются.
        }

        // --- Обработчик кнопки "О программе" (guna2Button6) ---
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            new About().Show();
            Log("Открыто окно 'О программе'.", LogType.Info);
        }

        // --- Обработчик кнопки "Локальные файлы" (guna2Button7) ---
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            string targetDirectory = _lastLaunchedShellDirectory;

            // Если ни одна оболочка ещё не была запущена, открываем папку приложения
            if (string.IsNullOrEmpty(targetDirectory) || !Directory.Exists(targetDirectory))
            {
                targetDirectory = AppDomain.CurrentDomain.BaseDirectory;
                Log("Путь к последней запущенной оболочке не определен или не существует. Открытие папки приложения по умолчанию.", LogType.Info);
            }

            try
            {
                Process.Start("explorer.exe", targetDirectory);
                Log($"Открыта папка: {targetDirectory}", LogType.Info);
            }
            catch (Exception ex)
            {
                Log($"Ошибка при открытии папки '{targetDirectory}': {ex.Message}", LogType.Error);
                MessageBox.Show($"Не удалось открыть папку '{targetDirectory}': {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Обработчик кнопки "Загрузка оболочек" (guna2Button8) ---
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // Создаем и показываем Tutorial
            string tut1 = "notprev.png"; // Убедитесь, что это реальное имя файла в папке Files
            Tutorial tutorialForm = new Tutorial(this.Log);
            tutorialForm.Show();
            Log("Открыто окно 'Туториал'.", Form1.LogType.Info);

            // Теперь создаем UtilityClass и передаем ему ссылку на только что созданный tutorialForm
            _utilityHelper = new UtilityClass(this.Log, tutorialForm);

            // Запускаем видео через UtilityClass, который затем обращается к tutorialForm
            _utilityHelper.PlayVideoInTutorial("player1", tut1, true, "tabPage1");
        }

        // --- Класс для десериализации Loader.json ---
        public class LoaderEntry
        {
            // Использование атрибута JsonProperty для точного сопоставления имен полей в JSON
            // с именами свойств в C# классе. Это решает проблему "pngprev" и "exefile".
            [JsonProperty("pngprev")]
            public string PngPreview { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("author")]
            public string Author { get; set; }

            [JsonProperty("exefile")]
            public string ExeFile { get; set; }

            public List<string> Tags { get; set; } // Если есть теги, оставьте
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            guna2Panel1.Visible = false;
            ProcessStartInfo sInfo = new ProcessStartInfo("https://t.me/osmpqiwi");
            Process.Start(sInfo);
        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            ShellAdd shellAddForm = new ShellAdd();
            shellAddForm.Logger = this.Log; // Передаем ссылку на метод логирования
            shellAddForm.ShowDialog();
        }
    }
}