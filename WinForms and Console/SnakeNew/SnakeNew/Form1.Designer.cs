namespace SnakeNew
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяИграToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.уровеньСложностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оченьПростоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.простоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.среднееToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сложноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оченьСложноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рекордыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правилаИгрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обАвтореToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.играToolStripMenuItem,
            this.рекордыToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(501, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem,
            this.уровеньСложностиToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.играToolStripMenuItem.Text = "Игра";
            // 
            // новаяИграToolStripMenuItem
            // 
            this.новаяИграToolStripMenuItem.Name = "новаяИграToolStripMenuItem";
            this.новаяИграToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.новаяИграToolStripMenuItem.Text = "Новая игра";
            this.новаяИграToolStripMenuItem.Click += new System.EventHandler(this.новаяИграToolStripMenuItem_Click);
            // 
            // уровеньСложностиToolStripMenuItem
            // 
            this.уровеньСложностиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оченьПростоToolStripMenuItem,
            this.простоToolStripMenuItem,
            this.среднееToolStripMenuItem,
            this.сложноToolStripMenuItem,
            this.оченьСложноToolStripMenuItem});
            this.уровеньСложностиToolStripMenuItem.Name = "уровеньСложностиToolStripMenuItem";
            this.уровеньСложностиToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.уровеньСложностиToolStripMenuItem.Text = "Уровень сложности";
            // 
            // оченьПростоToolStripMenuItem
            // 
            this.оченьПростоToolStripMenuItem.Checked = true;
            this.оченьПростоToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.оченьПростоToolStripMenuItem.Name = "оченьПростоToolStripMenuItem";
            this.оченьПростоToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.оченьПростоToolStripMenuItem.Text = "Очень просто";
            this.оченьПростоToolStripMenuItem.Click += new System.EventHandler(this.оченьПростоToolStripMenuItem_Click);
            // 
            // простоToolStripMenuItem
            // 
            this.простоToolStripMenuItem.Name = "простоToolStripMenuItem";
            this.простоToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.простоToolStripMenuItem.Text = "Просто";
            this.простоToolStripMenuItem.Click += new System.EventHandler(this.простоToolStripMenuItem_Click);
            // 
            // среднееToolStripMenuItem
            // 
            this.среднееToolStripMenuItem.Name = "среднееToolStripMenuItem";
            this.среднееToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.среднееToolStripMenuItem.Text = "Среднее";
            this.среднееToolStripMenuItem.Click += new System.EventHandler(this.среднееToolStripMenuItem_Click);
            // 
            // сложноToolStripMenuItem
            // 
            this.сложноToolStripMenuItem.Name = "сложноToolStripMenuItem";
            this.сложноToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.сложноToolStripMenuItem.Text = "Сложно";
            this.сложноToolStripMenuItem.Click += new System.EventHandler(this.сложноToolStripMenuItem_Click);
            // 
            // оченьСложноToolStripMenuItem
            // 
            this.оченьСложноToolStripMenuItem.Name = "оченьСложноToolStripMenuItem";
            this.оченьСложноToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.оченьСложноToolStripMenuItem.Text = "Очень сложно";
            this.оченьСложноToolStripMenuItem.Click += new System.EventHandler(this.оченьСложноToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // рекордыToolStripMenuItem
            // 
            this.рекордыToolStripMenuItem.Name = "рекордыToolStripMenuItem";
            this.рекордыToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.рекордыToolStripMenuItem.Text = "Рекорды";
            this.рекордыToolStripMenuItem.Click += new System.EventHandler(this.рекордыToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.правилаИгрыToolStripMenuItem,
            this.обАвтореToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // правилаИгрыToolStripMenuItem
            // 
            this.правилаИгрыToolStripMenuItem.Name = "правилаИгрыToolStripMenuItem";
            this.правилаИгрыToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.правилаИгрыToolStripMenuItem.Text = "Правила игры";
            // 
            // обАвтореToolStripMenuItem
            // 
            this.обАвтореToolStripMenuItem.Name = "обАвтореToolStripMenuItem";
            this.обАвтореToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.обАвтореToolStripMenuItem.Text = "Об авторе";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 534);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(501, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.FrameRate = 30;
            this.openGLControl1.Location = new System.Drawing.Point(0, 24);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(500, 500);
            this.openGLControl1.TabIndex = 3;
            this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
            this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.Resized += new System.EventHandler(this.openGLControl1_Resized);
            this.openGLControl1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.openGLControl1_PreviewKeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 556);
            this.Controls.Add(this.openGLControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Snake";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem играToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяИграToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem уровеньСложностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оченьПростоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem простоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem среднееToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сложноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оченьСложноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правилаИгрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обАвтореToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem рекордыToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private SharpGL.OpenGLControl openGLControl1;
    }
}

