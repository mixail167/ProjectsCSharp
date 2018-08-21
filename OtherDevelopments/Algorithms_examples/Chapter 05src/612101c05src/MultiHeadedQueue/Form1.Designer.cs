namespace MultiHeadedQueue
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
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.averageWaitTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tellersTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.queueTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.speedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.maxDurationNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.maxArrivalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.minDurationNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.minArrivalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numTellersNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.minuteTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxDurationNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxArrivalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDurationNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minArrivalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTellersNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // timeTextBox
            // 
            this.timeTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.timeTextBox.Location = new System.Drawing.Point(112, 298);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.ReadOnly = true;
            this.timeTextBox.Size = new System.Drawing.Size(101, 20);
            this.timeTextBox.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(73, 301);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Time:";
            // 
            // averageWaitTextBox
            // 
            this.averageWaitTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.averageWaitTextBox.Location = new System.Drawing.Point(112, 324);
            this.averageWaitTextBox.Name = "averageWaitTextBox";
            this.averageWaitTextBox.ReadOnly = true;
            this.averageWaitTextBox.Size = new System.Drawing.Size(101, 20);
            this.averageWaitTextBox.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(73, 327);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Wait:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Tellers:";
            // 
            // tellersTextBox
            // 
            this.tellersTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tellersTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tellersTextBox.Location = new System.Drawing.Point(63, 272);
            this.tellersTextBox.Name = "tellersTextBox";
            this.tellersTextBox.Size = new System.Drawing.Size(210, 20);
            this.tellersTextBox.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Queue:";
            // 
            // queueTextBox
            // 
            this.queueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queueTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queueTextBox.Location = new System.Drawing.Point(63, 176);
            this.queueTextBox.Multiline = true;
            this.queueTextBox.Name = "queueTextBox";
            this.queueTextBox.Size = new System.Drawing.Size(210, 90);
            this.queueTextBox.TabIndex = 12;
            // 
            // startButton
            // 
            this.startButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startButton.Location = new System.Drawing.Point(106, 147);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.speedNumericUpDown);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.maxDurationNumericUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maxArrivalNumericUpDown);
            this.groupBox1.Controls.Add(this.minDurationNumericUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.minArrivalNumericUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numTellersNumericUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 129);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(134, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "steps per second";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(201, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "minutes";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(201, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "minutes";
            // 
            // speedNumericUpDown
            // 
            this.speedNumericUpDown.Location = new System.Drawing.Point(89, 101);
            this.speedNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speedNumericUpDown.Name = "speedNumericUpDown";
            this.speedNumericUpDown.Size = new System.Drawing.Size(39, 20);
            this.speedNumericUpDown.TabIndex = 11;
            this.speedNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Speed:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "to";
            // 
            // maxDurationNumericUpDown
            // 
            this.maxDurationNumericUpDown.Location = new System.Drawing.Point(156, 75);
            this.maxDurationNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxDurationNumericUpDown.Name = "maxDurationNumericUpDown";
            this.maxDurationNumericUpDown.Size = new System.Drawing.Size(39, 20);
            this.maxDurationNumericUpDown.TabIndex = 8;
            this.maxDurationNumericUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "to";
            // 
            // maxArrivalNumericUpDown
            // 
            this.maxArrivalNumericUpDown.Location = new System.Drawing.Point(156, 49);
            this.maxArrivalNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxArrivalNumericUpDown.Name = "maxArrivalNumericUpDown";
            this.maxArrivalNumericUpDown.Size = new System.Drawing.Size(39, 20);
            this.maxArrivalNumericUpDown.TabIndex = 6;
            this.maxArrivalNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // minDurationNumericUpDown
            // 
            this.minDurationNumericUpDown.Location = new System.Drawing.Point(89, 75);
            this.minDurationNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minDurationNumericUpDown.Name = "minDurationNumericUpDown";
            this.minDurationNumericUpDown.Size = new System.Drawing.Size(39, 20);
            this.minDurationNumericUpDown.TabIndex = 5;
            this.minDurationNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Duration:";
            // 
            // minArrivalNumericUpDown
            // 
            this.minArrivalNumericUpDown.Location = new System.Drawing.Point(89, 49);
            this.minArrivalNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minArrivalNumericUpDown.Name = "minArrivalNumericUpDown";
            this.minArrivalNumericUpDown.Size = new System.Drawing.Size(39, 20);
            this.minArrivalNumericUpDown.TabIndex = 3;
            this.minArrivalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Arrival Mins:";
            // 
            // numTellersNumericUpDown
            // 
            this.numTellersNumericUpDown.Location = new System.Drawing.Point(89, 23);
            this.numTellersNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTellersNumericUpDown.Name = "numTellersNumericUpDown";
            this.numTellersNumericUpDown.Size = new System.Drawing.Size(39, 20);
            this.numTellersNumericUpDown.TabIndex = 1;
            this.numTellersNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "# Tellers:";
            // 
            // minuteTimer
            // 
            this.minuteTimer.Tick += new System.EventHandler(this.minuteTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 356);
            this.Controls.Add(this.timeTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.averageWaitTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tellersTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.queueTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "MultiHeadedQueue";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxDurationNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxArrivalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDurationNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minArrivalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTellersNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox averageWaitTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tellersTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox queueTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown speedNumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown maxDurationNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown maxArrivalNumericUpDown;
        private System.Windows.Forms.NumericUpDown minDurationNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown minArrivalNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTellersNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer minuteTimer;
    }
}

