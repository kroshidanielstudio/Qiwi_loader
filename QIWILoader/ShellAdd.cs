using Guna.UI2.WinForms;
using Newtonsoft.Json;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar; // Для работы с RAR
using SharpCompress.Readers; // Для класса ExtractionOptions
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq; // Для метода .Where()
using System.Windows.Forms;

namespace QIWILoader
{
    public partial class ShellAdd : Form
    {
        // Добавляем Logger для отладки
        public Form1.LogDelegate Logger { get; set; }

        public ShellAdd()
        {
            InitializeComponent();
            // Привязываем обработчики событий
            this.guna2Button5.Click += new System.EventHandler(this.guna2Button5_Click);
            this.guna2Button4.Click += new System.EventHandler(this.guna2Button4_Click);
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
        }

        // Этот метод будет вызываться при нажатии кнопки "X" в правом верхнем углу
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Обработчик для кнопки "Выбрать"
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Архивы QIWI (*.zip;*.rar)|*.zip;*.rar";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                guna2TextBox1.Text = openFileDialog1.FileName;
            }
        }

        // Обработчик для кнопки "Добавить"
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Проверка пользовательского ввода
            if (string.IsNullOrEmpty(guna2TextBox1.Text) || string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поля 'Путь до архива' и 'Имя оболочки'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Инициализация прогресса
            var progress = new Progress<int>(value => progressBar1.Value = value);
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            try
            {
                // Вызываем метод, который выполнит всю логику
                AddShell(progress);

                MessageBox.Show("Оболочка успешно добавлена!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger?.Invoke($"Критическая ошибка: {ex.Message}", Form1.LogType.Error);
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Очищаем прогресс-бар и поля в конце
                progressBar1.Value = 0;
                guna2TextBox1.Text = "";
                guna2TextBox2.Text = "";
                guna2TextBox3.Text = "";
                guna2TextBox4.Text = "";
                guna2TextBox5.Text = "";
            }
        }

        // Синхронный метод, выполняющий всю работу
        private void AddShell(IProgress<int> progress)
        {
            string archivePath = guna2TextBox1.Text;
            string contentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");
            string jsonFilePath = Path.Combine(contentDirectory, "Loader.json");

            // --- Шаг 1: Распаковка архива (прогресс 10-25%) ---
            progress.Report(10);

            // Проверяем, существует ли папка Content
            

            if (archivePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                ZipFile.ExtractToDirectory(archivePath, contentDirectory);
                progress.Report(25);
                Logger?.Invoke("ZIP-архив успешно распакован.", Form1.LogType.Info);
            }
            else if (archivePath.EndsWith(".rar", StringComparison.OrdinalIgnoreCase))
            {
                using (var archive = SharpCompress.Archives.Rar.RarArchive.Open(archivePath))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory && entry.Size > 0))
                    {
                        // Получаем полный путь, куда будет извлечен файл
                        string extractionPath = Path.Combine(contentDirectory, entry.Key);

                        // Создаем все необходимые поддиректории
                        Directory.CreateDirectory(Path.GetDirectoryName(extractionPath));

                        // Извлекаем файл с опцией перезаписи
                        using (var stream = File.OpenWrite(extractionPath))
                        {
                            entry.WriteTo(stream);
                        }
                    }
                }
                progress.Report(25);
                Logger?.Invoke("RAR-архив успешно распакован.", Form1.LogType.Info);
            }
            else
            {
                Logger?.Invoke("Неподдерживаемый формат архива. Используйте .zip или .rar.", Form1.LogType.Warning);
                throw new NotSupportedException("Неподдерживаемый формат файла.");
            }

            // --- Шаг 2: Чтение и обновление JSON ---
            progress.Report(50);
            List<LoaderEntry> shells = new List<LoaderEntry>();

            // Проверяем, существует ли файл Loader.json
            if (File.Exists(jsonFilePath))
            {
                // Если файл существует, считываем его содержимое
                string json = File.ReadAllText(jsonFilePath);
                // Десериализуем JSON в список объектов
                shells = JsonConvert.DeserializeObject<List<LoaderEntry>>(json) ?? new List<LoaderEntry>();
            }

            // Создаём новый объект с данными из текстовых полей вашей формы
            var newShell = new LoaderEntry
            {
                Name = guna2TextBox2.Text,
                // Если поле с картинкой пустое, используем "notprev.png"
                PngPreview = string.IsNullOrEmpty(guna2TextBox3.Text) ? "notprev.png" : guna2TextBox3.Text,
                ExeFile = guna2TextBox4.Text,
                // Если поле автора пустое, используем "t.me/osmpqiwi"
                Author = string.IsNullOrEmpty(guna2TextBox5.Text) ? "t.me/osmpqiwi" : guna2TextBox5.Text
            };

            // Добавляем новый объект в список
            shells.Add(newShell);
            progress.Report(75);

            // --- Шаг 3: Сохранение JSON-файла ---
            string updatedJson = JsonConvert.SerializeObject(shells, Formatting.Indented);
            File.WriteAllText(jsonFilePath, updatedJson);
            progress.Report(100);

            Logger?.Invoke("Файл Loader.json успешно обновлён.", Form1.LogType.Info);
        }
    }
}