namespace AdaptiveGridIntegration
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
            this.numBoxesTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.minBoxAreaTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.areaTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.integrateButton = new System.Windows.Forms.Button();
            this.numRowsColsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.graphPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // numBoxesTextBox
            // 
            this.numBoxesTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numBoxesTextBox.Location = new System.Drawing.Point(143, 390);
            this.numBoxesTextBox.Name = "numBoxesTextBox";
            this.numBoxesTextBox.ReadOnly = true;
            this.numBoxesTextBox.Size = new System.Drawing.Size(94, 20);
            this.numBoxesTextBox.TabIndex = 22;
            this.numBoxesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 393);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "# Boxes:";
            // 
            // minBoxAreaTextBox
            // 
            this.minBoxAreaTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.minBoxAreaTextBox.Location = new System.Drawing.Point(120, 330);
            this.minBoxAreaTextBox.Name = "minBoxAreaTextBox";
            this.minBoxAreaTextBox.Size = new System.Drawing.Size(53, 20);
            this.minBoxAreaTextBox.TabIndex = 18;
            this.minBoxAreaTextBox.Text = "0.001";
            this.minBoxAreaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Min Box Area:";
            // 
            // areaTextBox
            // 
            this.areaTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.areaTextBox.Location = new System.Drawing.Point(143, 364);
            this.areaTextBox.Name = "areaTextBox";
            this.areaTextBox.ReadOnly = true;
            this.areaTextBox.Size = new System.Drawing.Size(94, 20);
            this.areaTextBox.TabIndex = 21;
            this.areaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 367);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Area:";
            // 
            // resetButton
            // 
            this.resetButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.resetButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.resetButton.Location = new System.Drawing.Point(199, 328);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 20;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // integrateButton
            // 
            this.integrateButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.integrateButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.integrateButton.Location = new System.Drawing.Point(199, 299);
            this.integrateButton.Name = "integrateButton";
            this.integrateButton.Size = new System.Drawing.Size(75, 23);
            this.integrateButton.TabIndex = 19;
            this.integrateButton.Text = "Integrate";
            this.integrateButton.UseVisualStyleBackColor = true;
            this.integrateButton.Click += new System.EventHandler(this.integrateButton_Click);
            // 
            // numRowsColsTextBox
            // 
            this.numRowsColsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numRowsColsTextBox.Location = new System.Drawing.Point(120, 301);
            this.numRowsColsTextBox.Name = "numRowsColsTextBox";
            this.numRowsColsTextBox.Size = new System.Drawing.Size(53, 20);
            this.numRowsColsTextBox.TabIndex = 17;
            this.numRowsColsTextBox.Text = "4";
            this.numRowsColsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Rows/Columns:";
            // 
            // graphPictureBox
            // 
            this.graphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.graphPictureBox.Location = new System.Drawing.Point(12, 11);
            this.graphPictureBox.Name = "graphPictureBox";
            this.graphPictureBox.Size = new System.Drawing.Size(282, 282);
            this.graphPictureBox.TabIndex = 23;
            this.graphPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.integrateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 421);
            this.Controls.Add(this.numBoxesTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.minBoxAreaTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.areaTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.integrateButton);
            this.Controls.Add(this.numRowsColsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graphPictureBox);
            this.Name = "Form1";
            this.Text = "AdaptiveGridIntegration";
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox numBoxesTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox minBoxAreaTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox areaTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button integrateButton;
        private System.Windows.Forms.TextBox numRowsColsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox graphPictureBox;
    }
}

