namespace CountTicTacToeBoards
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
            this.totalTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tiesTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.oWinsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xWinsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.countButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // totalTextBox
            // 
            this.totalTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.totalTextBox.Location = new System.Drawing.Point(134, 119);
            this.totalTextBox.Name = "totalTextBox";
            this.totalTextBox.Size = new System.Drawing.Size(66, 20);
            this.totalTextBox.TabIndex = 17;
            this.totalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Total:";
            // 
            // tiesTextBox
            // 
            this.tiesTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tiesTextBox.Location = new System.Drawing.Point(135, 93);
            this.tiesTextBox.Name = "tiesTextBox";
            this.tiesTextBox.Size = new System.Drawing.Size(66, 20);
            this.tiesTextBox.TabIndex = 15;
            this.tiesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Tie:";
            // 
            // oWinsTextBox
            // 
            this.oWinsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.oWinsTextBox.Location = new System.Drawing.Point(135, 67);
            this.oWinsTextBox.Name = "oWinsTextBox";
            this.oWinsTextBox.Size = new System.Drawing.Size(66, 20);
            this.oWinsTextBox.TabIndex = 13;
            this.oWinsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "O Wins:";
            // 
            // xWinsTextBox
            // 
            this.xWinsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.xWinsTextBox.Location = new System.Drawing.Point(134, 41);
            this.xWinsTextBox.Name = "xWinsTextBox";
            this.xWinsTextBox.Size = new System.Drawing.Size(66, 20);
            this.xWinsTextBox.TabIndex = 11;
            this.xWinsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "X Wins:";
            // 
            // countButton
            // 
            this.countButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.countButton.Location = new System.Drawing.Point(105, 12);
            this.countButton.Name = "countButton";
            this.countButton.Size = new System.Drawing.Size(75, 23);
            this.countButton.TabIndex = 9;
            this.countButton.Text = "Count";
            this.countButton.UseVisualStyleBackColor = true;
            this.countButton.Click += new System.EventHandler(this.countButton_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.countButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 151);
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
            this.Text = "CountTicTacToeBoards";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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

