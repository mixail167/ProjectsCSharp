namespace MonteCarloIntegration
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
            this.totalPointsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.areaTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.integrateButton = new System.Windows.Forms.Button();
            this.numPointsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.graphPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // totalPointsTextBox
            // 
            this.totalPointsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.totalPointsTextBox.Location = new System.Drawing.Point(131, 344);
            this.totalPointsTextBox.Name = "totalPointsTextBox";
            this.totalPointsTextBox.ReadOnly = true;
            this.totalPointsTextBox.Size = new System.Drawing.Size(94, 20);
            this.totalPointsTextBox.TabIndex = 14;
            this.totalPointsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 347);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Total Points:";
            // 
            // areaTextBox
            // 
            this.areaTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.areaTextBox.Location = new System.Drawing.Point(131, 318);
            this.areaTextBox.Name = "areaTextBox";
            this.areaTextBox.ReadOnly = true;
            this.areaTextBox.Size = new System.Drawing.Size(94, 20);
            this.areaTextBox.TabIndex = 13;
            this.areaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Area:";
            // 
            // resetButton
            // 
            this.resetButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.resetButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.resetButton.Location = new System.Drawing.Point(197, 276);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 12;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // integrateButton
            // 
            this.integrateButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.integrateButton.Location = new System.Drawing.Point(116, 276);
            this.integrateButton.Name = "integrateButton";
            this.integrateButton.Size = new System.Drawing.Size(75, 23);
            this.integrateButton.TabIndex = 10;
            this.integrateButton.Text = "Integrate";
            this.integrateButton.UseVisualStyleBackColor = true;
            this.integrateButton.Click += new System.EventHandler(this.integrateButton_Click);
            // 
            // numPointsTextBox
            // 
            this.numPointsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numPointsTextBox.Location = new System.Drawing.Point(57, 278);
            this.numPointsTextBox.Name = "numPointsTextBox";
            this.numPointsTextBox.Size = new System.Drawing.Size(53, 20);
            this.numPointsTextBox.TabIndex = 8;
            this.numPointsTextBox.Text = "1000";
            this.numPointsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Points:";
            // 
            // graphPictureBox
            // 
            this.graphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.graphPictureBox.Location = new System.Drawing.Point(12, 10);
            this.graphPictureBox.Name = "graphPictureBox";
            this.graphPictureBox.Size = new System.Drawing.Size(260, 260);
            this.graphPictureBox.TabIndex = 9;
            this.graphPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.integrateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 374);
            this.Controls.Add(this.totalPointsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.areaTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.integrateButton);
            this.Controls.Add(this.numPointsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graphPictureBox);
            this.Name = "Form1";
            this.Text = "MonteCarloIntegration";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox totalPointsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox areaTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button integrateButton;
        private System.Windows.Forms.TextBox numPointsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox graphPictureBox;
    }
}

