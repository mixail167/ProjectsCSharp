namespace BinaryTraversals
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
            this.label4 = new System.Windows.Forms.Label();
            this.depthFirstTextBox = new System.Windows.Forms.TextBox();
            this.postorderTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inorderTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.preorderTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.depthFirstTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.postorderTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.inorderTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.preorderTextBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 105);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 27);
            this.label4.TabIndex = 14;
            this.label4.Text = "Depth-first";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // depthFirstTextBox
            // 
            this.depthFirstTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.depthFirstTextBox.Location = new System.Drawing.Point(73, 81);
            this.depthFirstTextBox.Multiline = true;
            this.depthFirstTextBox.Name = "depthFirstTextBox";
            this.depthFirstTextBox.ReadOnly = true;
            this.depthFirstTextBox.Size = new System.Drawing.Size(323, 21);
            this.depthFirstTextBox.TabIndex = 13;
            // 
            // postorderTextBox
            // 
            this.postorderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.postorderTextBox.Location = new System.Drawing.Point(73, 55);
            this.postorderTextBox.Multiline = true;
            this.postorderTextBox.Name = "postorderTextBox";
            this.postorderTextBox.ReadOnly = true;
            this.postorderTextBox.Size = new System.Drawing.Size(323, 20);
            this.postorderTextBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 26);
            this.label3.TabIndex = 10;
            this.label3.Text = "Postorder";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inorderTextBox
            // 
            this.inorderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inorderTextBox.Location = new System.Drawing.Point(73, 29);
            this.inorderTextBox.Multiline = true;
            this.inorderTextBox.Name = "inorderTextBox";
            this.inorderTextBox.ReadOnly = true;
            this.inorderTextBox.Size = new System.Drawing.Size(323, 20);
            this.inorderTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Inorder";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Preorder";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // preorderTextBox
            // 
            this.preorderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preorderTextBox.Location = new System.Drawing.Point(73, 3);
            this.preorderTextBox.Multiline = true;
            this.preorderTextBox.Name = "preorderTextBox";
            this.preorderTextBox.ReadOnly = true;
            this.preorderTextBox.Size = new System.Drawing.Size(323, 20);
            this.preorderTextBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 105);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "BinaryTraversals";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox depthFirstTextBox;
        private System.Windows.Forms.TextBox postorderTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inorderTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox preorderTextBox;
    }
}

