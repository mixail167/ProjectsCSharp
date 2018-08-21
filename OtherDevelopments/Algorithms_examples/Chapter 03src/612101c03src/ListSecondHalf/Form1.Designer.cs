namespace ListSecondHalf
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.halfWoMiddleTextBox = new System.Windows.Forms.TextBox();
            this.halfWithMiddleTextBox = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.stringTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "W/o Middle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "W/Middle:";
            // 
            // halfWoMiddleTextBox
            // 
            this.halfWoMiddleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.halfWoMiddleTextBox.Location = new System.Drawing.Point(84, 66);
            this.halfWoMiddleTextBox.Name = "halfWoMiddleTextBox";
            this.halfWoMiddleTextBox.ReadOnly = true;
            this.halfWoMiddleTextBox.Size = new System.Drawing.Size(147, 20);
            this.halfWoMiddleTextBox.TabIndex = 11;
            // 
            // halfWithMiddleTextBox
            // 
            this.halfWithMiddleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.halfWithMiddleTextBox.Location = new System.Drawing.Point(84, 92);
            this.halfWithMiddleTextBox.Name = "halfWithMiddleTextBox";
            this.halfWithMiddleTextBox.ReadOnly = true;
            this.halfWithMiddleTextBox.Size = new System.Drawing.Size(147, 20);
            this.halfWithMiddleTextBox.TabIndex = 12;
            // 
            // goButton
            // 
            this.goButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.goButton.Location = new System.Drawing.Point(84, 37);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 10;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // stringTextBox
            // 
            this.stringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringTextBox.Location = new System.Drawing.Point(84, 11);
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.Size = new System.Drawing.Size(147, 20);
            this.stringTextBox.TabIndex = 8;
            this.stringTextBox.Text = "ABCDEF";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "String:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 123);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.halfWoMiddleTextBox);
            this.Controls.Add(this.halfWithMiddleTextBox);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.stringTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "ListSecondHalf";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox halfWoMiddleTextBox;
        private System.Windows.Forms.TextBox halfWithMiddleTextBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.TextBox stringTextBox;
        private System.Windows.Forms.Label label1;
    }
}

