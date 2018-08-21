namespace QueueSelectionsort
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
            this.sortButton = new System.Windows.Forms.Button();
            this.itemsListBox = new System.Windows.Forms.ListBox();
            this.maximumTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minimumTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.makeItemsButton = new System.Windows.Forms.Button();
            this.numItemsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sortButton
            // 
            this.sortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sortButton.Location = new System.Drawing.Point(199, 91);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(75, 23);
            this.sortButton.TabIndex = 19;
            this.sortButton.Text = "Sort";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // itemsListBox
            // 
            this.itemsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsListBox.FormattingEnabled = true;
            this.itemsListBox.Location = new System.Drawing.Point(10, 91);
            this.itemsListBox.Name = "itemsListBox";
            this.itemsListBox.Size = new System.Drawing.Size(173, 160);
            this.itemsListBox.TabIndex = 20;
            // 
            // maximumTextBox
            // 
            this.maximumTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maximumTextBox.Location = new System.Drawing.Point(70, 65);
            this.maximumTextBox.Name = "maximumTextBox";
            this.maximumTextBox.Size = new System.Drawing.Size(113, 20);
            this.maximumTextBox.TabIndex = 17;
            this.maximumTextBox.Text = "9999";
            this.maximumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Maximum:";
            // 
            // minimumTextBox
            // 
            this.minimumTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minimumTextBox.Location = new System.Drawing.Point(70, 39);
            this.minimumTextBox.Name = "minimumTextBox";
            this.minimumTextBox.Size = new System.Drawing.Size(113, 20);
            this.minimumTextBox.TabIndex = 16;
            this.minimumTextBox.Text = "1000";
            this.minimumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Minimum:";
            // 
            // makeItemsButton
            // 
            this.makeItemsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.makeItemsButton.Location = new System.Drawing.Point(199, 37);
            this.makeItemsButton.Name = "makeItemsButton";
            this.makeItemsButton.Size = new System.Drawing.Size(75, 23);
            this.makeItemsButton.TabIndex = 18;
            this.makeItemsButton.Text = "Make Items";
            this.makeItemsButton.UseVisualStyleBackColor = true;
            this.makeItemsButton.Click += new System.EventHandler(this.makeItemsButton_Click);
            // 
            // numItemsTextBox
            // 
            this.numItemsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numItemsTextBox.Location = new System.Drawing.Point(70, 13);
            this.numItemsTextBox.Name = "numItemsTextBox";
            this.numItemsTextBox.Size = new System.Drawing.Size(113, 20);
            this.numItemsTextBox.TabIndex = 15;
            this.numItemsTextBox.Text = "100";
            this.numItemsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "# Items:";
            // 
            // Form1
            // 
            this.AcceptButton = this.makeItemsButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.itemsListBox);
            this.Controls.Add(this.maximumTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.minimumTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.makeItemsButton);
            this.Controls.Add(this.numItemsTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "QueueSelectionsort";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.ListBox itemsListBox;
        private System.Windows.Forms.TextBox maximumTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox minimumTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button makeItemsButton;
        private System.Windows.Forms.TextBox numItemsTextBox;
        private System.Windows.Forms.Label label1;
    }
}

