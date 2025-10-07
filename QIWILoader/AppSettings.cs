using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms; // <-- ДОБАВЬТЕ ЭТУ ДИРЕКТИВУ USING для FormBorderStyle
using Newtonsoft.Json;

namespace QIWILoader
{
    public class AppSettings
    {
        // Свойства для хранения ARGB-значений цветов и пути к фоновому изображению
        // Значения по умолчанию заданы здесь.
        public string FormBackgroundColorArgb { get; set; } = Color.FromArgb(28, 28, 28).ToArgb().ToString();
        public string FormShadowColorArgb { get; set; } = Color.Black.ToArgb().ToString();
        public string TextColorArgb { get; set; } = Color.White.ToArgb().ToString();
        public string ButtonColorArgb { get; set; } = Color.FromArgb(0, 150, 136).ToArgb().ToString();
        public string ButtonTextColorArgb { get; set; } = Color.White.ToArgb().ToString();
        public string BackgroundImagePath { get; set; } = string.Empty;

        // <-- НОВОЕ СВОЙСТВО: Для сохранения стиля границы окна как строки -->
        public string FormBorderStyleString { get; set; } = FormBorderStyle.Sizable.ToString(); // Дефолтный стиль - изменяемый (Sizable)

        /// <summary>
        /// Загружает настройки приложения из файла settings.json.
        /// Если файл не найден или произошла ошибка, возвращает настройки по умолчанию.
        /// </summary>
        public static AppSettings Load()
        {
            string settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
            AppSettings loadedSettings;

            if (File.Exists(settingsFilePath))
            {
                try
                {
                    string json = File.ReadAllText(settingsFilePath);
                    loadedSettings = JsonConvert.DeserializeObject<AppSettings>(json);
                }
                catch (JsonException ex)
                {
                    // Логируем ошибку, но не прерываем работу.
                    Console.WriteLine($"Ошибка при чтении settings.json: {ex.Message}");
                    loadedSettings = new AppSettings(); // Вернуть дефолтные при ошибке десериализации
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Неизвестная ошибка при загрузке settings.json: {ex.Message}");
                    loadedSettings = new AppSettings(); // Вернуть дефолтные при любой другой ошибке
                }
            }
            else
            {
                loadedSettings = new AppSettings(); // Если файл не существует, создать новые настройки по умолчанию
            }

            // Убедимся, что все свойства инициализированы после десериализации или создания.
            // Это особенно важно для обратной совместимости, если в старых файлах settings.json
            // не было каких-то новых полей.
            loadedSettings.FormBackgroundColorArgb = loadedSettings.FormBackgroundColorArgb ?? Color.FromArgb(28, 28, 28).ToArgb().ToString();
            loadedSettings.FormShadowColorArgb = loadedSettings.FormShadowColorArgb ?? Color.Black.ToArgb().ToString();
            loadedSettings.TextColorArgb = loadedSettings.TextColorArgb ?? Color.White.ToArgb().ToString();
            loadedSettings.ButtonColorArgb = loadedSettings.ButtonColorArgb ?? Color.FromArgb(0, 150, 136).ToArgb().ToString();
            loadedSettings.ButtonTextColorArgb = loadedSettings.ButtonTextColorArgb ?? Color.White.ToArgb().ToString();
            loadedSettings.BackgroundImagePath = loadedSettings.BackgroundImagePath ?? string.Empty;
            loadedSettings.FormBorderStyleString = loadedSettings.FormBorderStyleString ?? FormBorderStyle.Sizable.ToString(); // Инициализация нового свойства

            return loadedSettings;
        }

        /// <summary>
        /// Сохраняет текущие настройки приложения в файл settings.json.
        /// </summary>
        public void Save()
        {
            string settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented); // Formatting.Indented для читабельности
                File.WriteAllText(settingsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении settings.json: {ex.Message}");
                // Здесь можно добавить более заметное уведомление пользователю, если это критично
            }
        }

        // Вспомогательные методы для получения объектов Color из ARGB-строк
        public Color GetFormBackgroundColor() => Color.FromArgb(int.Parse(FormBackgroundColorArgb));
        public Color GetFormShadowColor() => Color.FromArgb(int.Parse(FormShadowColorArgb));
        public Color GetTextColor() => Color.FromArgb(int.Parse(TextColorArgb));
        public Color GetButtonColor() => Color.FromArgb(int.Parse(ButtonColorArgb));
        public Color GetButtonTextColor() => Color.FromArgb(int.Parse(ButtonTextColorArgb));

        // <-- НОВЫЙ МЕТОД: Для получения FormBorderStyle из строки -->
        public FormBorderStyle GetFormBorderStyle()
        {
            if (Enum.TryParse(FormBorderStyleString, out FormBorderStyle style))
            {
                return style;
            }
            return FormBorderStyle.Sizable; // Возвращаем Sizable по умолчанию, если строка недействительна
        }
    }
}