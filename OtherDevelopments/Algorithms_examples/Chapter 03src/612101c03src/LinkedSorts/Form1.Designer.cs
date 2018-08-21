namespace LinkedSorts
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
            this.maximumTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.minimumTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.insertionsortButton = new System.Windows.Forms.Button();
            this.selectionsortButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.valueListBox = new System.Windows.Forms.ListBox();
            this.makeItemsButton = new System.Windows.Forms.Button();
            this.numItemsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // maximumTextBox
            // 
            this.maximumTextBox.Location = new System.Drawing.Point(71, 65);
            this.maximumTextBox.Name = "maximumTextBox";
            this.maximumTextBox.Size = new System.Drawing.Size(72, 20);
            this.maximumTextBox.TabIndex = 15;
            this.maximumTextBox.Text = "999999999";
            this.maximumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Maximum:";
            // 
            // minimumTextBox
            // 
            this.minimumTextBox.Location = new System.Drawing.Point(71, 39);
            this.minimumTextBox.Name = "minimumTextBox";
            this.minimumTextBox.Size = new System.Drawing.Size(72, 20);
            this.minimumTextBox.TabIndex = 14;
            this.minimumTextBox.Text = "100000000";
            this.minimumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Minimum:";
            // 
            // timeTextBox
            // 
            this.timeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timeTextBox.Location = new System.Drawing.Point(50, 272);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.ReadOnly = true;
            this.timeTextBox.Size = new System.Drawing.Size(100, 20);
            this.timeTextBox.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Time:";
            // 
            // insertionsortButton
            // 
            this.insertionsortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertionsortButton.Location = new System.Drawing.Point(193, 243);
            this.insertionsortButton.Name = "insertionsortButton";
            this.insertionsortButton.Size = new System.Drawing.Size(85, 23);
            this.insertionsortButton.TabIndex = 21;
            this.insertionsortButton.Text = "Insertionsort";
            this.insertionsortButton.UseVisualStyleBackColor = true;
            this.insertionsortButton.Click += new System.EventHandler(this.insertionsortButton_Click);
            // 
            // selectionsortButton
            // 
            this.selectionsortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectionsortButton.Location = new System.Drawing.Point(102, 243);
            this.selectionsortButton.Name = "selectionsortButton";
            this.selectionsortButton.Size = new System.Drawing.Size(85, 23);
            this.selectionsortButton.TabIndex = 19;
            this.selectionsortButton.Text = "Selectionsort";
            this.selectionsortButton.UseVisualStyleBackColor = true;
            this.selectionsortButton.Click += new System.EventHandler(this.selectionsortButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.Location = new System.Drawing.Point(11, 243);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(85, 23);
            this.resetButton.TabIndex = 18;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // valueListBox
            // 
            this.valueListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueListBox.FormattingEnabled = true;
            this.valueListBox.Location = new System.Drawing.Point(11, 91);
            this.valueListBox.Name = "valueListBox";
            this.valueListBox.Size = new System.Drawing.Size(265, 147);
            this.valueListBox.TabIndex = 17;
            // 
            // makeItemsButton
            // 
            this.makeItemsButton.Location = new System.Drawing.Point(149, 37);
            this.makeItemsButton.Name = "makeItemsButton";
            this.makeItemsButton.Size = new System.Drawing.Size(75, 23);
            this.makeItemsButton.TabIndex = 16;
            this.makeItemsButton.Text = "Make Items";
            this.makeItemsButton.UseVisualStyleBackColor = true;
            this.makeItemsButton.Click += new System.EventHandler(this.makeItemsButton_Click);
            // 
            // numItemsTextBox
            // 
            this.numItemsTextBox.Location = new System.Drawing.Point(71, 13);
            this.numItemsTextBox.Name = "numItemsTextBox";
            this.numItemsTextBox.Size = new System.Drawing.Size(72, 20);
            this.numItemsTextBox.TabIndex = 12;
            this.numItemsTextBox.Text = "10000";
            this.numItemsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "# Items:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 305);
            this.Controls.Add(this.maximumTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.minimumTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.insertionsortButton);
            this.Controls.Add(this.selectionsortButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.valueListBox);
            this.Controls.Add(this.makeItemsButton);
            this.Controls.Add(this.numItemsTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "LinkedSorts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox maximumTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox minimumTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button insertionsortButton;
        private System.Windows.Forms.Button selectionsortButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.ListBox valueListBox;
        private System.Windows.Forms.Button makeItemsButton;
        private System.Windows.Forms.TextBox numItemsTextBox;
        private System.Windows.Forms.Label label1;
    }
}

