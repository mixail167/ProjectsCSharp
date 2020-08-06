namespace Graph
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.инициализацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjacencyMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incidenceMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjacencyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(430, 430);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(422, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Матрица смежности";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(416, 398);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(422, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Матрица инцидентности";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(422, 404);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Списоквершин";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(422, 404);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Список ребер";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.инициализацияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // инициализацияToolStripMenuItem
            // 
            this.инициализацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adjacencyMatrixToolStripMenuItem,
            this.incidenceMatrixToolStripMenuItem,
            this.adjacencyListToolStripMenuItem,
            this.edgesListToolStripMenuItem});
            this.инициализацияToolStripMenuItem.Name = "инициализацияToolStripMenuItem";
            this.инициализацияToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.инициализацияToolStripMenuItem.Text = "Инициализация";
            // 
            // adjacencyMatrixToolStripMenuItem
            // 
            this.adjacencyMatrixToolStripMenuItem.Name = "adjacencyMatrixToolStripMenuItem";
            this.adjacencyMatrixToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.adjacencyMatrixToolStripMenuItem.Text = "Матрица смежности";
            this.adjacencyMatrixToolStripMenuItem.Click += new System.EventHandler(this.AdjacencyMatrixToolStripMenuItem_Click);
            // 
            // incidenceMatrixToolStripMenuItem
            // 
            this.incidenceMatrixToolStripMenuItem.Name = "incidenceMatrixToolStripMenuItem";
            this.incidenceMatrixToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.incidenceMatrixToolStripMenuItem.Text = "Матрица инцидентности";
            this.incidenceMatrixToolStripMenuItem.Click += new System.EventHandler(this.incidenceMatrixToolStripMenuItem_Click);
            // 
            // adjacencyListToolStripMenuItem
            // 
            this.adjacencyListToolStripMenuItem.Name = "adjacencyListToolStripMenuItem";
            this.adjacencyListToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.adjacencyListToolStripMenuItem.Text = "Список смежности";
            this.adjacencyListToolStripMenuItem.Click += new System.EventHandler(this.adjacencyListToolStripMenuItem_Click);
            // 
            // edgesListToolStripMenuItem
            // 
            this.edgesListToolStripMenuItem.Name = "edgesListToolStripMenuItem";
            this.edgesListToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.edgesListToolStripMenuItem.Text = "Список ребер";
            this.edgesListToolStripMenuItem.Click += new System.EventHandler(this.edgesListToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Graph";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem инициализацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adjacencyMatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incidenceMatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adjacencyListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgesListToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

