using QIWILoader;
using System.Windows.Forms; // Для доступа к Form

public class UtilityClass
{
    private Form1.LogDelegate _logger;
    private Tutorial _tutorialInstance; // Для ссылки на экземпляр Tutorial

    // Конструктор, который принимает логгер и ссылку на форму Tutorial
    public UtilityClass(Form1.LogDelegate logger, Tutorial tutorialForm)
    {
        _logger = logger;
        _tutorialInstance = tutorialForm;
        _logger("UtilityClass: Инициализация.", Form1.LogType.Info);
    }

    public void PlayVideoInTutorial(string playerName, string videoPath, bool autoPlay, string tabName)
    {
        if (_tutorialInstance != null)
        {
            _logger($"UtilityClass: Прошу Tutorial запустить видео {videoPath} на плеере {playerName}.", Form1.LogType.Info);
            _tutorialInstance.LoadAndPlaySpecificVideo(playerName, videoPath, autoPlay, tabName);
        }
        else
        {
            _logger("UtilityClass: Экземпляр Tutorial не передан или равен null. Невозможно запустить видео.", Form1.LogType.Warning);
            MessageBox.Show("Окно с видеоуроками не открыто.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}