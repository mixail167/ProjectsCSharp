namespace MakeStrings
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
            this.stringsListBox = new System.Windows.Forms.ListBox();
            this.makeStringsButton = new System.Windows.Forms.Button();
            this.numLettersTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.countLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stringsListBox
            // 
            this.stringsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringsListBox.FormattingEnabled = true;
            this.stringsListBox.Location = new System.Drawing.Point(12, 41);
            this.stringsListBox.Name = "stringsListBox";
            this.stringsListBox.Size = new System.Drawing.Size(199, 199);
            this.stringsListBox.TabIndex = 7;
            // 
            // makeStringsButton
            // 
            this.makeStringsButton.Location = new System.Drawing.Point(125, 12);
            this.makeStringsButton.Name = "makeStringsButton";
            this.makeStringsButton.Size = new System.Drawing.Size(86, 23);
            this.makeStringsButton.TabIndex = 6;
            this.makeStringsButton.Text = "Make Strings";
            this.makeStringsButton.UseVisualStyleBackColor = true;
            this.makeStringsButton.Click += new System.EventHandler(this.makeStringsButton_Click);
            // 
            // numLettersTextBox
            // 
            this.numLettersTextBox.Location = new System.Drawing.Point(70, 14);
            this.numLettersTextBox.Name = "numLettersTextBox";
            this.numLettersTextBox.Size = new System.Drawing.Size(49, 20);
            this.numLettersTextBox.TabIndex = 5;
            this.numLettersTextBox.Text = "4";
            this.numLettersTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "# Letters:";
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(12, 242);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(126, 13);
            this.countLabel.TabIndex = 8;
            this.countLabel.Text = "(First 1000 strings shown)";
            // 
            // Form1
            // 
            this.AcceptButton = this.makeStringsButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 264);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.stringsListBox);
            this.Controls.Add(this.makeStringsButton);
            this.Controls.Add(this.numLettersTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "MakeStrings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox stringsListBox;
        private System.Windows.Forms.Button makeStringsButton;
        private System.Windows.Forms.TextBox numLettersTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label countLabel;
    }
}

