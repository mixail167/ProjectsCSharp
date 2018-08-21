namespace GcdTimes
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
            this.yminLabel = new System.Windows.Forms.Label();
            this.ymaxLabel = new System.Windows.Forms.Label();
            this.xmaxLabel = new System.Windows.Forms.Label();
            this.xminLabel = new System.Windows.Forms.Label();
            this.graphPictureBox = new System.Windows.Forms.PictureBox();
            this.goButton = new System.Windows.Forms.Button();
            this.numTrialsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // yminLabel
            // 
            this.yminLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yminLabel.AutoSize = true;
            this.yminLabel.Location = new System.Drawing.Point(12, 225);
            this.yminLabel.Name = "yminLabel";
            this.yminLabel.Size = new System.Drawing.Size(0, 13);
            this.yminLabel.TabIndex = 6;
            // 
            // ymaxLabel
            // 
            this.ymaxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ymaxLabel.AutoSize = true;
            this.ymaxLabel.Location = new System.Drawing.Point(12, 40);
            this.ymaxLabel.Name = "ymaxLabel";
            this.ymaxLabel.Size = new System.Drawing.Size(0, 13);
            this.ymaxLabel.TabIndex = 5;
            // 
            // xmaxLabel
            // 
            this.xmaxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xmaxLabel.AutoSize = true;
            this.xmaxLabel.Location = new System.Drawing.Point(537, 241);
            this.xmaxLabel.Name = "xmaxLabel";
            this.xmaxLabel.Size = new System.Drawing.Size(0, 13);
            this.xmaxLabel.TabIndex = 8;
            this.xmaxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // xminLabel
            // 
            this.xminLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xminLabel.AutoSize = true;
            this.xminLabel.Location = new System.Drawing.Point(43, 241);
            this.xminLabel.Name = "xminLabel";
            this.xminLabel.Size = new System.Drawing.Size(0, 13);
            this.xminLabel.TabIndex = 7;
            // 
            // graphPictureBox
            // 
            this.graphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPictureBox.BackColor = System.Drawing.Color.White;
            this.graphPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.graphPictureBox.Location = new System.Drawing.Point(18, 40);
            this.graphPictureBox.Name = "graphPictureBox";
            this.graphPictureBox.Size = new System.Drawing.Size(554, 198);
            this.graphPictureBox.TabIndex = 11;
            this.graphPictureBox.TabStop = false;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(497, 12);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 4;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // numTrialsTextBox
            // 
            this.numTrialsTextBox.Location = new System.Drawing.Point(229, 14);
            this.numTrialsTextBox.Name = "numTrialsTextBox";
            this.numTrialsTextBox.Size = new System.Drawing.Size(80, 20);
            this.numTrialsTextBox.TabIndex = 3;
            this.numTrialsTextBox.Text = "10000";
            this.numTrialsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "# Trials:";
            // 
            // maxTextBox
            // 
            this.maxTextBox.Location = new System.Drawing.Point(76, 14);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(80, 20);
            this.maxTextBox.TabIndex = 1;
            this.maxTextBox.Text = "100000000";
            this.maxTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Largest #:";
            // 
            // Form1
            // 
            this.AcceptButton = this.goButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 264);
            this.Controls.Add(this.maxTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.yminLabel);
            this.Controls.Add(this.ymaxLabel);
            this.Controls.Add(this.xmaxLabel);
            this.Controls.Add(this.xminLabel);
            this.Controls.Add(this.graphPictureBox);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.numTrialsTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "GcdTimes";
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label yminLabel;
        private System.Windows.Forms.Label ymaxLabel;
        private System.Windows.Forms.Label xmaxLabel;
        private System.Windows.Forms.Label xminLabel;
        private System.Windows.Forms.PictureBox graphPictureBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.TextBox numTrialsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.Label label2;
    }
}

