namespace VKVideoDownloader
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroTextBoxPlaceHolder2 = new VKVideoDownloader.MetroTextBoxPlaceHolder();
            this.metroTextBoxPlaceHolder1 = new VKVideoDownloader.MetroTextBoxPlaceHolder();
            this.metroCheckBox1 = new MetroFramework.Controls.MetroCheckBox();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroTextBoxPlaceHolder2);
            this.groupBox1.Controls.Add(this.metroTextBoxPlaceHolder1);
            this.groupBox1.Controls.Add(this.metroCheckBox1);
            this.groupBox1.Controls.Add(this.metroProgressSpinner1);
            this.groupBox1.Controls.Add(this.metroButton1);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(23, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 140);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Авторизация";
            // 
            // metroTextBoxPlaceHolder2
            // 
            this.metroTextBoxPlaceHolder2.ForeColor = System.Drawing.Color.Gray;
            this.metroTextBoxPlaceHolder2.Location = new System.Drawing.Point(69, 46);
            this.metroTextBoxPlaceHolder2.Name = "metroTextBoxPlaceHolder2";
            this.metroTextBoxPlaceHolder2.PasswordChar = '●';
            this.metroTextBoxPlaceHolder2.Size = new System.Drawing.Size(224, 23);
            this.metroTextBoxPlaceHolder2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTextBoxPlaceHolder2.TabIndex = 8;
            this.metroTextBoxPlaceHolder2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTextBoxPlaceHolder2.UseSystemPasswordChar = true;
            this.metroTextBoxPlaceHolder2.TextChanged += new System.EventHandler(this.metroTextBoxPlaceHolder2_TextChanged);
            this.metroTextBoxPlaceHolder2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.metroTextBoxPlaceHolder2.PlaceHolder = "Пароль...";
            // 
            // metroTextBoxPlaceHolder1
            // 
            this.metroTextBoxPlaceHolder1.ForeColor = System.Drawing.Color.Gray;
            this.metroTextBoxPlaceHolder1.Location = new System.Drawing.Point(69, 15);
            this.metroTextBoxPlaceHolder1.Name = "metroTextBoxPlaceHolder1";
            this.metroTextBoxPlaceHolder1.PlaceHolder = "Логин...";
            this.metroTextBoxPlaceHolder1.Size = new System.Drawing.Size(224, 23);
            this.metroTextBoxPlaceHolder1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTextBoxPlaceHolder1.TabIndex = 7;
            this.metroTextBoxPlaceHolder1.Text = "Логин...";
            this.metroTextBoxPlaceHolder1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTextBoxPlaceHolder1.TextChanged += new System.EventHandler(this.metroTextBox1_TextChanged);
            this.metroTextBoxPlaceHolder1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // metroCheckBox1
            // 
            this.metroCheckBox1.AutoSize = true;
            this.metroCheckBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroCheckBox1.CustomForeColor = true;
            this.metroCheckBox1.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.metroCheckBox1.FontWeight = MetroFramework.MetroLinkWeight.Light;
            this.metroCheckBox1.Location = new System.Drawing.Point(95, 106);
            this.metroCheckBox1.Name = "metroCheckBox1";
            this.metroCheckBox1.Size = new System.Drawing.Size(92, 19);
            this.metroCheckBox1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroCheckBox1.TabIndex = 6;
            this.metroCheckBox1.Text = "Запомнить";
            this.metroCheckBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox1.UseVisualStyleBackColor = true;
            this.metroCheckBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(6, 97);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(32, 32);
            this.metroProgressSpinner1.Speed = 2F;
            this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroProgressSpinner1.TabIndex = 1;
            this.metroProgressSpinner1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroProgressSpinner1.Value = 50;
            this.metroProgressSpinner1.Visible = false;
            // 
            // metroButton1
            // 
            this.metroButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButton1.Location = new System.Drawing.Point(193, 106);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(100, 23);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton1.TabIndex = 5;
            this.metroButton1.Text = "Авторизоваться";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.CustomForeColor = true;
            this.metroLabel3.ForeColor = System.Drawing.Color.Red;
            this.metroLabel3.Location = new System.Drawing.Point(69, 72);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(224, 20);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel3.Visible = false;
            this.metroLabel3.MouseEnter += new System.EventHandler(this.metroLabel3_MouseEnter);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.CustomForeColor = true;
            this.metroLabel2.Location = new System.Drawing.Point(6, 50);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(57, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "Пароль:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.CustomForeColor = true;
            this.metroLabel1.Location = new System.Drawing.Point(6, 20);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(50, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Логин:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BackgroundImage = global::VKVideoDownloader.Properties.Resources.icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(23, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.CustomForeColor = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel4.ForeColor = System.Drawing.Color.White;
            this.metroLabel4.Location = new System.Drawing.Point(79, 21);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(188, 25);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel4.TabIndex = 2;
            this.metroLabel4.Text = "VKVideoDownloader";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 215);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "VKVideoDownloader";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroTextBoxPlaceHolder metroTextBoxPlaceHolder1;
        private MetroTextBoxPlaceHolder metroTextBoxPlaceHolder2;
    }
}

