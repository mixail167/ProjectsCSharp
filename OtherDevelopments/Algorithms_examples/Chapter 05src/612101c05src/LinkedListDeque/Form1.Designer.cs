namespace LinkedListDeque
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
            this.dequeueBottomButton = new System.Windows.Forms.Button();
            this.enqueueBottomButton = new System.Windows.Forms.Button();
            this.queueListBox = new System.Windows.Forms.ListBox();
            this.itemTextBox = new System.Windows.Forms.TextBox();
            this.dequeueTopButton = new System.Windows.Forms.Button();
            this.enqueueTopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dequeueBottomButton
            // 
            this.dequeueBottomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dequeueBottomButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dequeueBottomButton.Location = new System.Drawing.Point(197, 59);
            this.dequeueBottomButton.Name = "dequeueBottomButton";
            this.dequeueBottomButton.Size = new System.Drawing.Size(75, 39);
            this.dequeueBottomButton.TabIndex = 21;
            this.dequeueBottomButton.Text = "Dequeue Bottom";
            this.dequeueBottomButton.UseVisualStyleBackColor = true;
            this.dequeueBottomButton.Click += new System.EventHandler(this.dequeueBottomButton_Click);
            // 
            // enqueueBottomButton
            // 
            this.enqueueBottomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enqueueBottomButton.Location = new System.Drawing.Point(116, 59);
            this.enqueueBottomButton.Name = "enqueueBottomButton";
            this.enqueueBottomButton.Size = new System.Drawing.Size(75, 39);
            this.enqueueBottomButton.TabIndex = 20;
            this.enqueueBottomButton.Text = "Enqueue Bottom";
            this.enqueueBottomButton.UseVisualStyleBackColor = true;
            this.enqueueBottomButton.Click += new System.EventHandler(this.enqueueBottomButton_Click);
            // 
            // queueListBox
            // 
            this.queueListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queueListBox.FormattingEnabled = true;
            this.queueListBox.Location = new System.Drawing.Point(15, 104);
            this.queueListBox.Name = "queueListBox";
            this.queueListBox.Size = new System.Drawing.Size(257, 147);
            this.queueListBox.TabIndex = 19;
            // 
            // itemTextBox
            // 
            this.itemTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemTextBox.Location = new System.Drawing.Point(48, 16);
            this.itemTextBox.Name = "itemTextBox";
            this.itemTextBox.Size = new System.Drawing.Size(62, 20);
            this.itemTextBox.TabIndex = 16;
            // 
            // dequeueTopButton
            // 
            this.dequeueTopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dequeueTopButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dequeueTopButton.Location = new System.Drawing.Point(197, 14);
            this.dequeueTopButton.Name = "dequeueTopButton";
            this.dequeueTopButton.Size = new System.Drawing.Size(75, 39);
            this.dequeueTopButton.TabIndex = 18;
            this.dequeueTopButton.Text = "Dequeue Top";
            this.dequeueTopButton.UseVisualStyleBackColor = true;
            this.dequeueTopButton.Click += new System.EventHandler(this.dequeueTopButton_Click);
            // 
            // enqueueTopButton
            // 
            this.enqueueTopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enqueueTopButton.Location = new System.Drawing.Point(116, 14);
            this.enqueueTopButton.Name = "enqueueTopButton";
            this.enqueueTopButton.Size = new System.Drawing.Size(75, 39);
            this.enqueueTopButton.TabIndex = 17;
            this.enqueueTopButton.Text = "Enqueue Top";
            this.enqueueTopButton.UseVisualStyleBackColor = true;
            this.enqueueTopButton.Click += new System.EventHandler(this.enqueueTopButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Item:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dequeueBottomButton);
            this.Controls.Add(this.enqueueBottomButton);
            this.Controls.Add(this.queueListBox);
            this.Controls.Add(this.itemTextBox);
            this.Controls.Add(this.dequeueTopButton);
            this.Controls.Add(this.enqueueTopButton);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "LinkedListDeque";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dequeueBottomButton;
        private System.Windows.Forms.Button enqueueBottomButton;
        private System.Windows.Forms.ListBox queueListBox;
        private System.Windows.Forms.TextBox itemTextBox;
        private System.Windows.Forms.Button dequeueTopButton;
        private System.Windows.Forms.Button enqueueTopButton;
        private System.Windows.Forms.Label label1;
    }
}

