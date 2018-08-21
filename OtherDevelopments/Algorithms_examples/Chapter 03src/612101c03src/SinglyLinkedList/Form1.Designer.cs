namespace SinglyLinkedList
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
            this.deleteButton = new System.Windows.Forms.Button();
            this.addAfterButton = new System.Windows.Forms.Button();
            this.addToTopButton = new System.Windows.Forms.Button();
            this.afterNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.peopleListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(242, 11);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 12;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addAfterButton
            // 
            this.addAfterButton.Location = new System.Drawing.Point(161, 40);
            this.addAfterButton.Name = "addAfterButton";
            this.addAfterButton.Size = new System.Drawing.Size(75, 23);
            this.addAfterButton.TabIndex = 10;
            this.addAfterButton.Text = "Add After";
            this.addAfterButton.UseVisualStyleBackColor = true;
            this.addAfterButton.Click += new System.EventHandler(this.addAfterButton_Click);
            // 
            // addToTopButton
            // 
            this.addToTopButton.Location = new System.Drawing.Point(161, 11);
            this.addToTopButton.Name = "addToTopButton";
            this.addToTopButton.Size = new System.Drawing.Size(75, 23);
            this.addToTopButton.TabIndex = 9;
            this.addToTopButton.Text = "Add To Top";
            this.addToTopButton.UseVisualStyleBackColor = true;
            this.addToTopButton.Click += new System.EventHandler(this.addToTopButton_Click);
            // 
            // afterNameTextBox
            // 
            this.afterNameTextBox.Location = new System.Drawing.Point(55, 42);
            this.afterNameTextBox.Name = "afterNameTextBox";
            this.afterNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.afterNameTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "After:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(55, 13);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name:";
            // 
            // peopleListBox
            // 
            this.peopleListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peopleListBox.FormattingEnabled = true;
            this.peopleListBox.Location = new System.Drawing.Point(11, 68);
            this.peopleListBox.Name = "peopleListBox";
            this.peopleListBox.Size = new System.Drawing.Size(305, 186);
            this.peopleListBox.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 264);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addAfterButton);
            this.Controls.Add(this.addToTopButton);
            this.Controls.Add(this.afterNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.peopleListBox);
            this.Name = "Form1";
            this.Text = "SinglyLinkedList";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addAfterButton;
        private System.Windows.Forms.Button addToTopButton;
        private System.Windows.Forms.TextBox afterNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox peopleListBox;
    }
}

