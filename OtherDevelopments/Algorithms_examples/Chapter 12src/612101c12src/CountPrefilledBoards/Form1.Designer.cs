namespace CountPrefilledBoards
{
    partial class Form1
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
            this.resetButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.label01 = new System.Windows.Forms.Label();
            this.label00 = new System.Windows.Forms.Label();
            this.totalTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tiesTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.oWinsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xWinsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.countButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // resetButton
            // 
            this.resetButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.resetButton.Location = new System.Drawing.Point(330, 175);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 30;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label22, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label21, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label20, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label02, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label01, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label00, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(24, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 240);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.White;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label22.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(164, 164);
            this.label22.Margin = new System.Windows.Forms.Padding(4);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 71);
            this.label22.TabIndex = 8;
            this.label22.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label21.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(84, 164);
            this.label21.Margin = new System.Windows.Forms.Padding(4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 71);
            this.label21.TabIndex = 7;
            this.label21.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 164);
            this.label20.Margin = new System.Windows.Forms.Padding(4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(71, 71);
            this.label20.TabIndex = 6;
            this.label20.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(164, 84);
            this.label12.Margin = new System.Windows.Forms.Padding(4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 71);
            this.label12.TabIndex = 5;
            this.label12.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(84, 84);
            this.label11.Margin = new System.Windows.Forms.Padding(4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 71);
            this.label11.TabIndex = 4;
            this.label11.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 84);
            this.label10.Margin = new System.Windows.Forms.Padding(4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 71);
            this.label10.TabIndex = 3;
            this.label10.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label02
            // 
            this.label02.BackColor = System.Drawing.Color.White;
            this.label02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label02.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label02.Location = new System.Drawing.Point(164, 4);
            this.label02.Margin = new System.Windows.Forms.Padding(4);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(71, 71);
            this.label02.TabIndex = 2;
            this.label02.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label01
            // 
            this.label01.BackColor = System.Drawing.Color.White;
            this.label01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label01.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label01.Location = new System.Drawing.Point(84, 4);
            this.label01.Margin = new System.Windows.Forms.Padding(4);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(71, 71);
            this.label01.TabIndex = 1;
            this.label01.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // label00
            // 
            this.label00.BackColor = System.Drawing.Color.White;
            this.label00.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label00.Font = new System.Drawing.Font("Times New Roman", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label00.Location = new System.Drawing.Point(4, 4);
            this.label00.Margin = new System.Windows.Forms.Padding(4);
            this.label00.Name = "label00";
            this.label00.Size = new System.Drawing.Size(71, 71);
            this.label00.TabIndex = 0;
            this.label00.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label00.Click += new System.EventHandler(this.labelSquare_Click);
            // 
            // totalTextBox
            // 
            this.totalTextBox.Location = new System.Drawing.Point(330, 118);
            this.totalTextBox.Name = "totalTextBox";
            this.totalTextBox.Size = new System.Drawing.Size(75, 20);
            this.totalTextBox.TabIndex = 28;
            this.totalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Total:";
            // 
            // tiesTextBox
            // 
            this.tiesTextBox.Location = new System.Drawing.Point(331, 92);
            this.tiesTextBox.Name = "tiesTextBox";
            this.tiesTextBox.Size = new System.Drawing.Size(75, 20);
            this.tiesTextBox.TabIndex = 26;
            this.tiesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Tie:";
            // 
            // oWinsTextBox
            // 
            this.oWinsTextBox.Location = new System.Drawing.Point(331, 66);
            this.oWinsTextBox.Name = "oWinsTextBox";
            this.oWinsTextBox.Size = new System.Drawing.Size(75, 20);
            this.oWinsTextBox.TabIndex = 24;
            this.oWinsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "O Wins:";
            // 
            // xWinsTextBox
            // 
            this.xWinsTextBox.Location = new System.Drawing.Point(330, 40);
            this.xWinsTextBox.Name = "xWinsTextBox";
            this.xWinsTextBox.Size = new System.Drawing.Size(75, 20);
            this.xWinsTextBox.TabIndex = 22;
            this.xWinsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "X Wins:";
            // 
            // countButton
            // 
            this.countButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.countButton.Location = new System.Drawing.Point(330, 11);
            this.countButton.Name = "countButton";
            this.countButton.Size = new System.Drawing.Size(75, 23);
            this.countButton.TabIndex = 20;
            this.countButton.Text = "Count";
            this.countButton.UseVisualStyleBackColor = true;
            this.countButton.Click += new System.EventHandler(this.countButton_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.countButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 263);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.totalTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tiesTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.oWinsTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.xWinsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.countButton);
            this.Name = "Form1";
            this.Text = "CountPrefilledBoards";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label00;
        private System.Windows.Forms.TextBox totalTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tiesTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox oWinsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xWinsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button countButton;
    }
}

