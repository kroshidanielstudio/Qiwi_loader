using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QIWILoader
{
    public partial class SettingsForm : Form
    {
        // Приватные поля для хранения текущих выбранных значений
        private Color _formBackgroundColor;
        private Color _formShadowColor;
        private Color _textColor;
        private Color _buttonColor;
        private Color _buttonTextColor;
        private string _backgroundImagePath;
        private FormBorderStyle _formBorderStyle; // <-- Добавлено: Для стиля границы окна

        // Публичные свойства ТОЛЬКО для чтения, которые Form1 будет использовать для получения окончательных значений
        public Color SelectedFormBackgroundColor => _formBackgroundColor;
        public Color SelectedFormShadowColor => _formShadowColor;
        public Color SelectedTextColor => _textColor;
        public Color SelectedButtonColor => _buttonColor;
        public Color SelectedButtonTextColor => _buttonTextColor;
        public string SelectedBackgroundImagePath => _backgroundImagePath;
        public FormBorderStyle SelectedFormBorderStyle => _formBorderStyle; // <-- Добавлено: Свойство для стиля окна

        // Изменен конструктор, чтобы принимать текущий FormBorderStyle от Form1
        public SettingsForm(AppSettings currentSettings, FormBorderStyle initialBorderStyle)
        {
            InitializeComponent();

            // Инициализируем внутренние поля текущими значениями из AppSettings
            _formBackgroundColor = currentSettings.GetFormBackgroundColor();
            _formShadowColor = currentSettings.GetFormShadowColor();
            _textColor = currentSettings.GetTextColor();
            _buttonColor = currentSettings.GetButtonColor();
            _buttonTextColor = currentSettings.GetButtonTextColor();
            _backgroundImagePath = currentSettings.BackgroundImagePath;
            _formBorderStyle = initialBorderStyle; // <-- Инициализируем стилем, который передала Form1

            // Применяем начальные значения к элементам управления
            ApplyCurrentSettingsToControls();
        }

        // Метод для применения текущих настроек к элементам управления формы
        private void ApplyCurrentSettingsToControls()
        {
            // Обновляем цвета кнопок выбора цвета
            if (btnSelectFormBgColor != null) btnSelectFormBgColor.FillColor = _formBackgroundColor;
            if (btnSelectFormShadowColor != null) btnSelectFormShadowColor.FillColor = _formShadowColor;
            if (btnSelectTextColor != null) btnSelectTextColor.FillColor = _textColor;
            if (btnSelectButtonColor != null) btnSelectButtonColor.FillColor = _buttonColor;
            if (btnSelectButtonTextColor != null) btnSelectButtonTextColor.FillColor = _buttonTextColor;

            // Обновляем текстбокс с путем к изображению
            if (txtBackgroundImagePath != null) txtBackgroundImagePath.Text = _backgroundImagePath;

            // Можно также обновить цвет фона самой формы настроек, чтобы видеть изменения
            this.BackColor = _formBackgroundColor;

            // --- Настройка ComboBox для FormBorderStyle ---
            if (cmbFormBorderStyle != null) // Убедитесь, что ваш ComboBox в дизайнере назван cmbFormBorderStyle
            {
                // Заполняем ComboBox всеми возможными значениями FormBorderStyle
                cmbFormBorderStyle.Items.Clear();
                foreach (FormBorderStyle style in Enum.GetValues(typeof(FormBorderStyle)))
                {
                    cmbFormBorderStyle.Items.Add(style.ToString());
                }

                // Устанавливаем выбранный элемент по текущему значению
                cmbFormBorderStyle.SelectedItem = _formBorderStyle.ToString();

                // Подпишемся на событие изменения выбранного элемента
                // Важно: Отпишитесь от события в дизайнере, чтобы избежать двойной подписки, если вы делали это там.
                cmbFormBorderStyle.SelectedIndexChanged -= cmbFormBorderStyle_SelectedIndexChanged; // Отписка на случай повторной инициализации
                cmbFormBorderStyle.SelectedIndexChanged += cmbFormBorderStyle_SelectedIndexChanged;
            }
        }

        // --- Обработчики кнопок выбора цвета ---
        private void btnSelectFormBgColor_Click(object sender, EventArgs e) => SelectColor(sender, ref _formBackgroundColor);
        private void btnSelectFormShadowColor_Click(object sender, EventArgs e) => SelectColor(sender, ref _formShadowColor);
        private void btnSelectTextColor_Click(object sender, EventArgs e) => SelectColor(sender, ref _textColor);
        private void btnSelectButtonColor_Click(object sender, EventArgs e) => SelectColor(sender, ref _buttonColor);
        private void btnSelectButtonTextColor_Click(object sender, EventArgs e) => SelectColor(sender, ref _buttonTextColor);

        // Вспомогательный метод для открытия ColorDialog и обновления цвета
        private void SelectColor(object sender, ref Color targetColorField)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = targetColorField; // Устанавливаем текущий цвет
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    targetColorField = colorDialog.Color; // Обновляем приватное поле
                    // Обновляем цвет кнопки, которая вызвала диалог
                    if (sender is Guna2Button button)
                    {
                        button.FillColor = targetColorField;
                    }
                    else if (sender is Button stdButton)
                    {
                        stdButton.BackColor = targetColorField;
                    }
                    // Если фон формы меняется, обновляем её сразу
                    if (sender == btnSelectFormBgColor)
                    {
                        this.BackColor = targetColorField;
                    }
                }
            }
        }

        // --- Обработчик кнопки выбора фонового изображения ---
        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            openFileDialog.Title = "Выберите фоновое изображение";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _backgroundImagePath = openFileDialog.FileName;
                if (txtBackgroundImagePath != null)
                {
                    txtBackgroundImagePath.Text = _backgroundImagePath;
                }
            }
        }

        // --- Обработчик кнопки "Очистить изображение" ---
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            _backgroundImagePath = string.Empty; // Очищаем путь
            if (txtBackgroundImagePath != null)
            {
                txtBackgroundImagePath.Text = string.Empty; // Очищаем текстбокс
            }
        }

        // --- Новый обработчик для ComboBox стиля окна ---
        private void cmbFormBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStyleString = cmbFormBorderStyle.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedStyleString))
            {
                try
                {
                    // Преобразуем строку в enum FormBorderStyle
                    _formBorderStyle = (FormBorderStyle)Enum.Parse(typeof(FormBorderStyle), selectedStyleString);
                }
                catch (ArgumentException ex)
                {
                    // Логируем ошибку, если строка не соответствует enum (хотя такого быть не должно, если ComboBox заполнен правильно)
                    Console.WriteLine($"Ошибка преобразования стиля окна: {ex.Message}");
                }
            }
        }

        // --- Обработчик кнопки "Применить" (OK) ---
        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // --- Обработчик кнопки "Отмена" ---
        private void btnCancelChanges_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Этот метод, вероятно, является дубликатом кнопки закрытия. 
        // Если у вас есть кнопка закрытия на SettingsForm, убедитесь, что она назначена на btnCancelChanges_Click или новый обработчик.
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}