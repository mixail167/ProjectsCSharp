namespace GraphicalTowerOfHanoi
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
            this.components = new System.ComponentModel.Container();
            this.speedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.pegPictureBox = new System.Windows.Forms.PictureBox();
            this.solveButton = new System.Windows.Forms.Button();
            this.numDisksNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pegPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisksNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // speedNumericUpDown
            // 
            this.speedNumericUpDown.Location = new System.Drawing.Point(274, 15);
            this.speedNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.speedNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speedNumericUpDown.Name = "speedNumericUpDown";
            this.speedNumericUpDown.Size = new System.Drawing.Size(42, 20);
            this.speedNumericUpDown.TabIndex = 15;
            this.speedNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Speed:";
            // 
            // pegPictureBox
            // 
            this.pegPictureBox.BackColor = System.Drawing.Color.White;
            this.pegPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pegPictureBox.Location = new System.Drawing.Point(12, 41);
            this.pegPictureBox.Name = "pegPictureBox";
            this.pegPictureBox.Size = new System.Drawing.Size(200, 100);
            this.pegPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pegPictureBox.TabIndex = 13;
            this.pegPictureBox.TabStop = false;
            // 
            // solveButton
            // 
            this.solveButton.Location = new System.Drawing.Point(109, 12);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(75, 23);
            this.solveButton.TabIndex = 12;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // numDisksNumericUpDown
            // 
            this.numDisksNumericUpDown.Location = new System.Drawing.Point(64, 15);
            this.numDisksNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDisksNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDisksNumericUpDown.Name = "numDisksNumericUpDown";
            this.numDisksNumericUpDown.Size = new System.Drawing.Size(39, 20);
            this.numDisksNumericUpDown.TabIndex = 11;
            this.numDisksNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "# Disks:";
            // 
            // moveTimer
            // 
            this.moveTimer.Tick += new System.EventHandler(this.moveTimer_Tick);
            // 
            // Form1
            // 
            this.AcceptButton = this.solveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 235);
            this.Controls.Add(this.speedNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pegPictureBox);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.numDisksNumericUpDown);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "GraphicalTowerOfHanoi";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pegPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisksNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown speedNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pegPictureBox;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.NumericUpDown numDisksNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer moveTimer;
    }
}

