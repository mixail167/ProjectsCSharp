namespace PlanetList
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
            this.planetListBox = new System.Windows.Forms.ListBox();
            this.diameterRadioButton = new System.Windows.Forms.RadioButton();
            this.massRadioButton = new System.Windows.Forms.RadioButton();
            this.distanceRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // planetListBox
            // 
            this.planetListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.planetListBox.FormattingEnabled = true;
            this.planetListBox.Location = new System.Drawing.Point(8, 38);
            this.planetListBox.Name = "planetListBox";
            this.planetListBox.Size = new System.Drawing.Size(222, 212);
            this.planetListBox.TabIndex = 7;
            // 
            // diameterRadioButton
            // 
            this.diameterRadioButton.AutoSize = true;
            this.diameterRadioButton.Location = new System.Drawing.Point(171, 15);
            this.diameterRadioButton.Name = "diameterRadioButton";
            this.diameterRadioButton.Size = new System.Drawing.Size(67, 17);
            this.diameterRadioButton.TabIndex = 6;
            this.diameterRadioButton.TabStop = true;
            this.diameterRadioButton.Text = "Diameter";
            this.diameterRadioButton.UseVisualStyleBackColor = true;
            this.diameterRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // massRadioButton
            // 
            this.massRadioButton.AutoSize = true;
            this.massRadioButton.Location = new System.Drawing.Point(115, 15);
            this.massRadioButton.Name = "massRadioButton";
            this.massRadioButton.Size = new System.Drawing.Size(50, 17);
            this.massRadioButton.TabIndex = 5;
            this.massRadioButton.TabStop = true;
            this.massRadioButton.Text = "Mass";
            this.massRadioButton.UseVisualStyleBackColor = true;
            this.massRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // distanceRadioButton
            // 
            this.distanceRadioButton.AutoSize = true;
            this.distanceRadioButton.Location = new System.Drawing.Point(8, 15);
            this.distanceRadioButton.Name = "distanceRadioButton";
            this.distanceRadioButton.Size = new System.Drawing.Size(101, 17);
            this.distanceRadioButton.TabIndex = 4;
            this.distanceRadioButton.TabStop = true;
            this.distanceRadioButton.Text = "Distance to Sun";
            this.distanceRadioButton.UseVisualStyleBackColor = true;
            this.distanceRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 264);
            this.Controls.Add(this.planetListBox);
            this.Controls.Add(this.diameterRadioButton);
            this.Controls.Add(this.massRadioButton);
            this.Controls.Add(this.distanceRadioButton);
            this.Name = "Form1";
            this.Text = "PlanetList";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox planetListBox;
        private System.Windows.Forms.RadioButton diameterRadioButton;
        private System.Windows.Forms.RadioButton massRadioButton;
        private System.Windows.Forms.RadioButton distanceRadioButton;
    }
}

