namespace LetterFrequencies
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
            this.percentListBox = new System.Windows.Forms.ListBox();
            this.evaluateButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // percentListBox
            // 
            this.percentListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.percentListBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentListBox.FormattingEnabled = true;
            this.percentListBox.ItemHeight = 14;
            this.percentListBox.Location = new System.Drawing.Point(15, 65);
            this.percentListBox.Name = "percentListBox";
            this.percentListBox.Size = new System.Drawing.Size(257, 186);
            this.percentListBox.Sorted = true;
            this.percentListBox.TabIndex = 7;
            // 
            // evaluateButton
            // 
            this.evaluateButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.evaluateButton.Location = new System.Drawing.Point(105, 36);
            this.evaluateButton.Name = "evaluateButton";
            this.evaluateButton.Size = new System.Drawing.Size(75, 23);
            this.evaluateButton.TabIndex = 6;
            this.evaluateButton.Text = "Evaluate";
            this.evaluateButton.UseVisualStyleBackColor = true;
            this.evaluateButton.Click += new System.EventHandler(this.evaluateButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(77, 10);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(195, 20);
            this.messageTextBox.TabIndex = 5;
            this.messageTextBox.Text = "THIS IS THE SECRET MESSAGE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message:";
            // 
            // Form1
            // 
            this.AcceptButton = this.evaluateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.percentListBox);
            this.Controls.Add(this.evaluateButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "LetterFrequencies";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox percentListBox;
        private System.Windows.Forms.Button evaluateButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label label1;
    }
}

