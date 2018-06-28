namespace Algorithms
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 442);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.radioButton2);
            this.tabPage1.Controls.Add(this.radioButton1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Поиск образа в строке";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(111, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(497, 96);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "Получены данные пользователя";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(210, 134);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(143, 17);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "Алгоритм Бойера-Мура";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(11, 134);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(193, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Алгоритм Кнута-Морриса-Пратта";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(533, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Поиск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(111, 105);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(497, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "данные";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Образ:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Исходная строка:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.radioButton6);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.radioButton5);
            this.tabPage2.Controls.Add(this.radioButton4);
            this.tabPage2.Controls.Add(this.radioButton3);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Сортировка";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(11, 141);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(138, 17);
            this.radioButton6.TabIndex = 8;
            this.radioButton6.Text = "Сортировка слиянием";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(514, 118);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "По убыванию";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(533, 142);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Сортировка";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(300, 118);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(145, 17);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.Text = "Сортировка пузырьком";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(151, 118);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(143, 17);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.Text = "Сортировка вставками";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(11, 118);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(134, 17);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Сортировка выбором";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(11, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(597, 80);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(114, 6);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDown1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Число элементов:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.radioButton8);
            this.tabPage3.Controls.Add(this.radioButton7);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.numericUpDown4);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.numericUpDown3);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Controls.Add(this.numericUpDown2);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(616, 416);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Кратчайший путь";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Checked = true;
            this.radioButton7.Location = new System.Drawing.Point(11, 297);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(129, 17);
            this.radioButton7.TabIndex = 13;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Алгоритм Дейкстры";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(11, 385);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "Показать путь";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 349);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 11;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(508, 294);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Расчет";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(427, 12);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown4.TabIndex = 9;
            this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown3_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(335, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Конечный узел:";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(261, 12);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown3.TabIndex = 7;
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown3_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Начальный узел:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(508, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Показать граф";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Location = new System.Drawing.Point(11, 38);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(597, 250);
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(88, 12);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            this.numericUpDown2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown2_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Число узлов:";
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(146, 297);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(118, 17);
            this.radioButton8.TabIndex = 14;
            this.radioButton8.Text = "Алгоритм Флойда";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Algorithms";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
    }
}

