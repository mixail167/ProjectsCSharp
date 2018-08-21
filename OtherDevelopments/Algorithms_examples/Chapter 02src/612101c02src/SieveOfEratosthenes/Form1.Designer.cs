namespace SieveOfEratosthenes
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
            this.countLabel = new System.Windows.Forms.Label();
            this.primesListBox = new System.Windows.Forms.ListBox();
            this.findPrimesButton = new System.Windows.Forms.Button();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // countLabel
            // 
            this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(11, 241);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(0, 13);
            this.countLabel.TabIndex = 9;
            // 
            // primesListBox
            // 
            this.primesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primesListBox.FormattingEnabled = true;
            this.primesListBox.Location = new System.Drawing.Point(11, 40);
            this.primesListBox.Name = "primesListBox";
            this.primesListBox.Size = new System.Drawing.Size(258, 212);
            this.primesListBox.TabIndex = 8;
            // 
            // findPrimesButton
            // 
            this.findPrimesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findPrimesButton.Location = new System.Drawing.Point(194, 11);
            this.findPrimesButton.Name = "findPrimesButton";
            this.findPrimesButton.Size = new System.Drawing.Size(75, 23);
            this.findPrimesButton.TabIndex = 7;
            this.findPrimesButton.Text = "Find Primes";
            this.findPrimesButton.UseVisualStyleBackColor = true;
            this.findPrimesButton.Click += new System.EventHandler(this.findPrimesButton_Click);
            // 
            // maxTextBox
            // 
            this.maxTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maxTextBox.Location = new System.Drawing.Point(111, 13);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(77, 20);
            this.maxTextBox.TabIndex = 6;
            this.maxTextBox.Text = "10000";
            this.maxTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Maximum Number:";
            // 
            // Form1
            // 
            this.AcceptButton = this.findPrimesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 264);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.primesListBox);
            this.Controls.Add(this.findPrimesButton);
            this.Controls.Add(this.maxTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "SieveOfEratosthenes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.ListBox primesListBox;
        private System.Windows.Forms.Button findPrimesButton;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.Label label1;
    }
}

