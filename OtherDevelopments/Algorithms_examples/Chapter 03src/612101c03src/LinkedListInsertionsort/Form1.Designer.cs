namespace LinkedListInsertionsort
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.unsortedListBox = new System.Windows.Forms.ListBox();
            this.insertionsortButton = new System.Windows.Forms.Button();
            this.randomizeButton = new System.Windows.Forms.Button();
            this.selectionsortButton = new System.Windows.Forms.Button();
            this.selectionsortListBox = new System.Windows.Forms.ListBox();
            this.insertionsortListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.insertionsortListBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.unsortedListBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.selectionsortListBox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.insertionsortButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.randomizeButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectionsortButton, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 240);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // unsortedListBox
            // 
            this.unsortedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unsortedListBox.FormattingEnabled = true;
            this.unsortedListBox.Location = new System.Drawing.Point(100, 33);
            this.unsortedListBox.Name = "unsortedListBox";
            this.unsortedListBox.Size = new System.Drawing.Size(91, 204);
            this.unsortedListBox.TabIndex = 0;
            // 
            // insertionsortButton
            // 
            this.insertionsortButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.insertionsortButton.Enabled = false;
            this.insertionsortButton.Location = new System.Drawing.Point(103, 3);
            this.insertionsortButton.Name = "insertionsortButton";
            this.insertionsortButton.Size = new System.Drawing.Size(84, 23);
            this.insertionsortButton.TabIndex = 3;
            this.insertionsortButton.Text = "Insertionsort";
            this.insertionsortButton.UseVisualStyleBackColor = true;
            this.insertionsortButton.Click += new System.EventHandler(this.insertionsortButton_Click);
            // 
            // randomizeButton
            // 
            this.randomizeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.randomizeButton.Location = new System.Drawing.Point(6, 3);
            this.randomizeButton.Name = "randomizeButton";
            this.randomizeButton.Size = new System.Drawing.Size(84, 23);
            this.randomizeButton.TabIndex = 2;
            this.randomizeButton.Text = "Randomize";
            this.randomizeButton.UseVisualStyleBackColor = true;
            this.randomizeButton.Click += new System.EventHandler(this.randomizeButton_Click);
            // 
            // selectionsortButton
            // 
            this.selectionsortButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.selectionsortButton.Enabled = false;
            this.selectionsortButton.Location = new System.Drawing.Point(201, 3);
            this.selectionsortButton.Name = "selectionsortButton";
            this.selectionsortButton.Size = new System.Drawing.Size(84, 23);
            this.selectionsortButton.TabIndex = 3;
            this.selectionsortButton.Text = "Selectionsort";
            this.selectionsortButton.UseVisualStyleBackColor = true;
            this.selectionsortButton.Click += new System.EventHandler(this.selectionsortButton_Click);
            // 
            // selectionsortListBox
            // 
            this.selectionsortListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectionsortListBox.FormattingEnabled = true;
            this.selectionsortListBox.Location = new System.Drawing.Point(197, 33);
            this.selectionsortListBox.Name = "selectionsortListBox";
            this.selectionsortListBox.Size = new System.Drawing.Size(93, 204);
            this.selectionsortListBox.TabIndex = 1;
            // 
            // insertionsortListBox
            // 
            this.insertionsortListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.insertionsortListBox.FormattingEnabled = true;
            this.insertionsortListBox.Location = new System.Drawing.Point(3, 33);
            this.insertionsortListBox.Name = "insertionsortListBox";
            this.insertionsortListBox.Size = new System.Drawing.Size(91, 204);
            this.insertionsortListBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 264);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "LinkedListInsertionsort";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox unsortedListBox;
        private System.Windows.Forms.Button insertionsortButton;
        private System.Windows.Forms.Button randomizeButton;
        private System.Windows.Forms.Button selectionsortButton;
        private System.Windows.Forms.ListBox insertionsortListBox;
        private System.Windows.Forms.ListBox selectionsortListBox;
    }
}

