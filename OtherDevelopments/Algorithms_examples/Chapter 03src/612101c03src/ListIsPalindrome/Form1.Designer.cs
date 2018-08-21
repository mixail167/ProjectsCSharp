namespace ListIsPalindrome
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
            this.recursiveLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.reverseHalfLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reverseLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkButton = new System.Windows.Forms.Button();
            this.stringTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // recursiveLabel
            // 
            this.recursiveLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recursiveLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.recursiveLabel.Location = new System.Drawing.Point(90, 123);
            this.recursiveLabel.Name = "recursiveLabel";
            this.recursiveLabel.Size = new System.Drawing.Size(129, 20);
            this.recursiveLabel.TabIndex = 17;
            this.recursiveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Recursive:";
            // 
            // reverseHalfLabel
            // 
            this.reverseHalfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reverseHalfLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.reverseHalfLabel.Location = new System.Drawing.Point(90, 98);
            this.reverseHalfLabel.Name = "reverseHalfLabel";
            this.reverseHalfLabel.Size = new System.Drawing.Size(129, 20);
            this.reverseHalfLabel.TabIndex = 15;
            this.reverseHalfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Reverse Half:";
            // 
            // reverseLabel
            // 
            this.reverseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reverseLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.reverseLabel.Location = new System.Drawing.Point(90, 73);
            this.reverseLabel.Name = "reverseLabel";
            this.reverseLabel.Size = new System.Drawing.Size(129, 20);
            this.reverseLabel.TabIndex = 13;
            this.reverseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Reverse:";
            // 
            // checkButton
            // 
            this.checkButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkButton.Location = new System.Drawing.Point(78, 38);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(75, 23);
            this.checkButton.TabIndex = 11;
            this.checkButton.Text = "Check";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // stringTextBox
            // 
            this.stringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringTextBox.Location = new System.Drawing.Point(55, 12);
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.Size = new System.Drawing.Size(164, 20);
            this.stringTextBox.TabIndex = 10;
            this.stringTextBox.Text = "Drab as a fool, aloof as a bard";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "String:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 154);
            this.Controls.Add(this.recursiveLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.reverseHalfLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.reverseLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.stringTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "ListIsPalindrome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label recursiveLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label reverseHalfLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label reverseLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.TextBox stringTextBox;
        private System.Windows.Forms.Label label1;
    }
}

