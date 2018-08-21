namespace NewtonsMethod
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
            this.zero3TextBox = new System.Windows.Forms.TextBox();
            this.zero2TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.zero1TextBox = new System.Windows.Forms.TextBox();
            this.graphPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // zero3TextBox
            // 
            this.zero3TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.zero3TextBox.Location = new System.Drawing.Point(55, 370);
            this.zero3TextBox.Name = "zero3TextBox";
            this.zero3TextBox.ReadOnly = true;
            this.zero3TextBox.Size = new System.Drawing.Size(196, 20);
            this.zero3TextBox.TabIndex = 9;
            // 
            // zero2TextBox
            // 
            this.zero2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.zero2TextBox.Location = new System.Drawing.Point(55, 344);
            this.zero2TextBox.Name = "zero2TextBox";
            this.zero2TextBox.ReadOnly = true;
            this.zero2TextBox.Size = new System.Drawing.Size(196, 20);
            this.zero2TextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Zeros:";
            // 
            // zero1TextBox
            // 
            this.zero1TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.zero1TextBox.Location = new System.Drawing.Point(55, 318);
            this.zero1TextBox.Name = "zero1TextBox";
            this.zero1TextBox.ReadOnly = true;
            this.zero1TextBox.Size = new System.Drawing.Size(196, 20);
            this.zero1TextBox.TabIndex = 6;
            // 
            // graphPictureBox
            // 
            this.graphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPictureBox.BackColor = System.Drawing.Color.White;
            this.graphPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.graphPictureBox.Location = new System.Drawing.Point(12, 12);
            this.graphPictureBox.Name = "graphPictureBox";
            this.graphPictureBox.Size = new System.Drawing.Size(500, 300);
            this.graphPictureBox.TabIndex = 5;
            this.graphPictureBox.TabStop = false;
            this.graphPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.graphPictureBox_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 402);
            this.Controls.Add(this.zero3TextBox);
            this.Controls.Add(this.zero2TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zero1TextBox);
            this.Controls.Add(this.graphPictureBox);
            this.Name = "Form1";
            this.Text = "NewtonsMethod";
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox zero3TextBox;
        private System.Windows.Forms.TextBox zero2TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox zero1TextBox;
        private System.Windows.Forms.PictureBox graphPictureBox;
    }
}

