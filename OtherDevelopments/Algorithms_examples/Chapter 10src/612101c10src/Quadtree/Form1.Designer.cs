namespace Quadtree
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
            this.drawBoxesCheckBox = new System.Windows.Forms.CheckBox();
            this.createButton = new System.Windows.Forms.Button();
            this.numPointsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pointsPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pointsPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // drawBoxesCheckBox
            // 
            this.drawBoxesCheckBox.AutoSize = true;
            this.drawBoxesCheckBox.Checked = true;
            this.drawBoxesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawBoxesCheckBox.Location = new System.Drawing.Point(14, 42);
            this.drawBoxesCheckBox.Name = "drawBoxesCheckBox";
            this.drawBoxesCheckBox.Size = new System.Drawing.Size(83, 17);
            this.drawBoxesCheckBox.TabIndex = 9;
            this.drawBoxesCheckBox.Text = "Draw Boxes";
            this.drawBoxesCheckBox.UseVisualStyleBackColor = true;
            this.drawBoxesCheckBox.CheckedChanged += new System.EventHandler(this.drawBoxesCheckBox_CheckedChanged);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(142, 14);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 8;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // numPointsTextBox
            // 
            this.numPointsTextBox.Location = new System.Drawing.Point(69, 16);
            this.numPointsTextBox.Name = "numPointsTextBox";
            this.numPointsTextBox.Size = new System.Drawing.Size(67, 20);
            this.numPointsTextBox.TabIndex = 7;
            this.numPointsTextBox.Text = "200";
            this.numPointsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "# Points:";
            // 
            // pointsPictureBox
            // 
            this.pointsPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointsPictureBox.BackColor = System.Drawing.Color.White;
            this.pointsPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pointsPictureBox.Location = new System.Drawing.Point(14, 65);
            this.pointsPictureBox.Name = "pointsPictureBox";
            this.pointsPictureBox.Size = new System.Drawing.Size(460, 319);
            this.pointsPictureBox.TabIndex = 5;
            this.pointsPictureBox.TabStop = false;
            this.pointsPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pointsPictureBox_Paint);
            this.pointsPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pointsPictureBox_MouseClick);
            // 
            // Form1
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 398);
            this.Controls.Add(this.drawBoxesCheckBox);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.numPointsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pointsPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Quadtree";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pointsPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox drawBoxesCheckBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox numPointsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pointsPictureBox;
    }
}

