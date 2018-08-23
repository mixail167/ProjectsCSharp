namespace Naves
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pnlViewPort = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnComenzar = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblScore = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.picVida3 = new System.Windows.Forms.PictureBox();
            this.picVida2 = new System.Windows.Forms.PictureBox();
            this.picVida1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLevel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrPaint = new System.Windows.Forms.Timer(this.components);
            this.pnlViewPort.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVida3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVida2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVida1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlViewPort
            // 
            this.pnlViewPort.Controls.Add(this.panel3);
            this.pnlViewPort.Controls.Add(this.panel4);
            this.pnlViewPort.Controls.Add(this.panel5);
            this.pnlViewPort.Controls.Add(this.panel2);
            this.pnlViewPort.Location = new System.Drawing.Point(12, 12);
            this.pnlViewPort.Name = "pnlViewPort";
            this.pnlViewPort.Size = new System.Drawing.Size(750, 540);
            this.pnlViewPort.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnReiniciar);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnComenzar);
            this.panel3.Location = new System.Drawing.Point(595, 465);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(152, 72);
            this.panel3.TabIndex = 1;
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Location = new System.Drawing.Point(78, 30);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(71, 31);
            this.btnReiniciar.TabIndex = 12;
            this.btnReiniciar.Text = "Reiniciar";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
            this.btnReiniciar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_KeyUp);
            this.btnReiniciar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Game";
            // 
            // btnComenzar
            // 
            this.btnComenzar.Location = new System.Drawing.Point(4, 30);
            this.btnComenzar.Name = "btnComenzar";
            this.btnComenzar.Size = new System.Drawing.Size(71, 31);
            this.btnComenzar.TabIndex = 10;
            this.btnComenzar.Text = "Comenzar";
            this.btnComenzar.UseVisualStyleBackColor = true;
            this.btnComenzar.Click += new System.EventHandler(this.button3_Click);
            this.btnComenzar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_KeyUp);
            this.btnComenzar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblScore);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(595, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(152, 72);
            this.panel4.TabIndex = 1;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(14, 42);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(16, 16);
            this.lblScore.TabIndex = 12;
            this.lblScore.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Score";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.picVida3);
            this.panel5.Controls.Add(this.picVida2);
            this.panel5.Controls.Add(this.picVida1);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(3, 465);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(152, 72);
            this.panel5.TabIndex = 1;
            // 
            // picVida3
            // 
            this.picVida3.Image = ((System.Drawing.Image)(resources.GetObject("picVida3.Image")));
            this.picVida3.Location = new System.Drawing.Point(104, 32);
            this.picVida3.Name = "picVida3";
            this.picVida3.Size = new System.Drawing.Size(41, 30);
            this.picVida3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picVida3.TabIndex = 21;
            this.picVida3.TabStop = false;
            // 
            // picVida2
            // 
            this.picVida2.Image = ((System.Drawing.Image)(resources.GetObject("picVida2.Image")));
            this.picVida2.Location = new System.Drawing.Point(57, 32);
            this.picVida2.Name = "picVida2";
            this.picVida2.Size = new System.Drawing.Size(41, 30);
            this.picVida2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picVida2.TabIndex = 20;
            this.picVida2.TabStop = false;
            // 
            // picVida1
            // 
            this.picVida1.Image = ((System.Drawing.Image)(resources.GetObject("picVida1.Image")));
            this.picVida1.Location = new System.Drawing.Point(10, 32);
            this.picVida1.Name = "picVida1";
            this.picVida1.Size = new System.Drawing.Size(41, 30);
            this.picVida1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picVida1.TabIndex = 19;
            this.picVida1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Lives";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblLevel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 72);
            this.panel2.TabIndex = 0;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.Location = new System.Drawing.Point(11, 38);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(16, 16);
            this.lblLevel.TabIndex = 10;
            this.lblLevel.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Level";
            // 
            // tmrPaint
            // 
            this.tmrPaint.Enabled = true;
            this.tmrPaint.Interval = 25;
            this.tmrPaint.Tick += new System.EventHandler(this.tmrPaint_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 570);
            this.Controls.Add(this.pnlViewPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Campo de asteroides";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.pnlViewPort.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVida3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVida2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVida1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlViewPort;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picVida3;
        private System.Windows.Forms.PictureBox picVida2;
        private System.Windows.Forms.PictureBox picVida1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer tmrPaint;
        private System.Windows.Forms.Button btnReiniciar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnComenzar;
    }
}

