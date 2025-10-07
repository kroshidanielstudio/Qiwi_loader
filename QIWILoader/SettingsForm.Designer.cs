namespace QIWILoader
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnSelectFormBgColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnSelectFormShadowColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnSelectTextColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnSelectButtonColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnSelectButtonTextColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnSaveSettings = new Guna.UI2.WinForms.Guna2Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnChooseBackgroundImage = new Guna.UI2.WinForms.Guna2Button();
            this.btnClearBackgroundImage = new Guna.UI2.WinForms.Guna2Button();
            this.txtBackgroundImagePath = new System.Windows.Forms.Label();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFormBorderStyle = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 10;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.ResizeForm = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Qiwi Loader | Кастомизация";
            // 
            // guna2Button1
            // 
            this.guna2Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Black;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(544, 8);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(31, 26);
            this.guna2Button1.TabIndex = 5;
            this.guna2Button1.Text = "X";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // btnSelectFormBgColor
            // 
            this.btnSelectFormBgColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectFormBgColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectFormBgColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectFormBgColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectFormBgColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectFormBgColor.ForeColor = System.Drawing.Color.White;
            this.btnSelectFormBgColor.Location = new System.Drawing.Point(23, 78);
            this.btnSelectFormBgColor.Name = "btnSelectFormBgColor";
            this.btnSelectFormBgColor.Size = new System.Drawing.Size(180, 45);
            this.btnSelectFormBgColor.TabIndex = 8;
            this.btnSelectFormBgColor.Click += new System.EventHandler(this.btnSelectFormBgColor_Click);
            // 
            // btnSelectFormShadowColor
            // 
            this.btnSelectFormShadowColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectFormShadowColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectFormShadowColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectFormShadowColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectFormShadowColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectFormShadowColor.ForeColor = System.Drawing.Color.White;
            this.btnSelectFormShadowColor.Location = new System.Drawing.Point(209, 78);
            this.btnSelectFormShadowColor.Name = "btnSelectFormShadowColor";
            this.btnSelectFormShadowColor.Size = new System.Drawing.Size(180, 45);
            this.btnSelectFormShadowColor.TabIndex = 9;
            this.btnSelectFormShadowColor.Click += new System.EventHandler(this.btnSelectFormShadowColor_Click);
            // 
            // btnSelectTextColor
            // 
            this.btnSelectTextColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectTextColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectTextColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectTextColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectTextColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectTextColor.ForeColor = System.Drawing.Color.White;
            this.btnSelectTextColor.Location = new System.Drawing.Point(395, 78);
            this.btnSelectTextColor.Name = "btnSelectTextColor";
            this.btnSelectTextColor.Size = new System.Drawing.Size(180, 45);
            this.btnSelectTextColor.TabIndex = 10;
            this.btnSelectTextColor.Click += new System.EventHandler(this.btnSelectTextColor_Click);
            // 
            // btnSelectButtonColor
            // 
            this.btnSelectButtonColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectButtonColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectButtonColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectButtonColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectButtonColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectButtonColor.ForeColor = System.Drawing.Color.White;
            this.btnSelectButtonColor.Location = new System.Drawing.Point(24, 141);
            this.btnSelectButtonColor.Name = "btnSelectButtonColor";
            this.btnSelectButtonColor.Size = new System.Drawing.Size(180, 45);
            this.btnSelectButtonColor.TabIndex = 11;
            this.btnSelectButtonColor.Click += new System.EventHandler(this.btnSelectButtonColor_Click);
            // 
            // btnSelectButtonTextColor
            // 
            this.btnSelectButtonTextColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectButtonTextColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelectButtonTextColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSelectButtonTextColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSelectButtonTextColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectButtonTextColor.ForeColor = System.Drawing.Color.White;
            this.btnSelectButtonTextColor.Location = new System.Drawing.Point(210, 140);
            this.btnSelectButtonTextColor.Name = "btnSelectButtonTextColor";
            this.btnSelectButtonTextColor.Size = new System.Drawing.Size(180, 45);
            this.btnSelectButtonTextColor.TabIndex = 12;
            this.btnSelectButtonTextColor.Click += new System.EventHandler(this.btnSelectButtonTextColor_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveSettings.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveSettings.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSaveSettings.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this.btnSaveSettings.Location = new System.Drawing.Point(112, 260);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(180, 45);
            this.btnSaveSettings.TabIndex = 13;
            this.btnSaveSettings.Text = "Сохранить";
            this.btnSaveSettings.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnChooseBackgroundImage
            // 
            this.btnChooseBackgroundImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChooseBackgroundImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChooseBackgroundImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChooseBackgroundImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChooseBackgroundImage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChooseBackgroundImage.ForeColor = System.Drawing.Color.White;
            this.btnChooseBackgroundImage.Location = new System.Drawing.Point(396, 140);
            this.btnChooseBackgroundImage.Name = "btnChooseBackgroundImage";
            this.btnChooseBackgroundImage.Size = new System.Drawing.Size(180, 45);
            this.btnChooseBackgroundImage.TabIndex = 14;
            this.btnChooseBackgroundImage.Text = "Выбрать фото фона";
            this.btnChooseBackgroundImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // btnClearBackgroundImage
            // 
            this.btnClearBackgroundImage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClearBackgroundImage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClearBackgroundImage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClearBackgroundImage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClearBackgroundImage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearBackgroundImage.ForeColor = System.Drawing.Color.White;
            this.btnClearBackgroundImage.Location = new System.Drawing.Point(24, 192);
            this.btnClearBackgroundImage.Name = "btnClearBackgroundImage";
            this.btnClearBackgroundImage.Size = new System.Drawing.Size(180, 45);
            this.btnClearBackgroundImage.TabIndex = 15;
            this.btnClearBackgroundImage.Text = "Удалить фото фона";
            this.btnClearBackgroundImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // txtBackgroundImagePath
            // 
            this.txtBackgroundImagePath.AutoSize = true;
            this.txtBackgroundImagePath.ForeColor = System.Drawing.Color.White;
            this.txtBackgroundImagePath.Location = new System.Drawing.Point(24, 244);
            this.txtBackgroundImagePath.Name = "txtBackgroundImagePath";
            this.txtBackgroundImagePath.Size = new System.Drawing.Size(102, 13);
            this.txtBackgroundImagePath.TabIndex = 16;
            this.txtBackgroundImagePath.Text = "Нету изображения";
            // 
            // btnCancel
            // 
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(298, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 45);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancelChanges_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(58, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Выбрать цвет фона";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(247, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Выбрать цвет тени";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(435, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Выбрать цвет текста";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(58, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Выбрать цвет кнопок";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(226, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Выбрать цвет текста кнопок";
            // 
            // cmbFormBorderStyle
            // 
            this.cmbFormBorderStyle.FormattingEnabled = true;
            this.cmbFormBorderStyle.Items.AddRange(new object[] {
            "None",
            "FixedSingle",
            "Fixed3D",
            "FixedDialog",
            "Sizable",
            "SizableToolWindow",
            "FixedToolWindow"});
            this.cmbFormBorderStyle.Location = new System.Drawing.Point(314, 192);
            this.cmbFormBorderStyle.Name = "cmbFormBorderStyle";
            this.cmbFormBorderStyle.Size = new System.Drawing.Size(121, 21);
            this.cmbFormBorderStyle.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(226, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Стиль обводки";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(587, 315);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbFormBorderStyle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtBackgroundImagePath);
            this.Controls.Add(this.btnClearBackgroundImage);
            this.Controls.Add(this.btnChooseBackgroundImage);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.btnSelectButtonTextColor);
            this.Controls.Add(this.btnSelectButtonColor);
            this.Controls.Add(this.btnSelectTextColor);
            this.Controls.Add(this.btnSelectFormShadowColor);
            this.Controls.Add(this.btnSelectFormBgColor);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private Guna.UI2.WinForms.Guna2Button btnSelectFormBgColor;
        private Guna.UI2.WinForms.Guna2Button btnSelectFormShadowColor;
        private Guna.UI2.WinForms.Guna2Button btnSelectTextColor;
        private Guna.UI2.WinForms.Guna2Button btnSelectButtonColor;
        private Guna.UI2.WinForms.Guna2Button btnSelectButtonTextColor;
        private Guna.UI2.WinForms.Guna2Button btnSaveSettings;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Guna.UI2.WinForms.Guna2Button btnChooseBackgroundImage;
        private Guna.UI2.WinForms.Guna2Button btnClearBackgroundImage;
        private System.Windows.Forms.Label txtBackgroundImagePath;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbFormBorderStyle;
    }
}