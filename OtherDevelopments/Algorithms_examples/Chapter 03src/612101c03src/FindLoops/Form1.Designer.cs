namespace FindLoops
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
            this.list2StatusLabel = new System.Windows.Forms.Label();
            this.list1StatusLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.unloopedListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.loopedListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // list2StatusLabel
            // 
            this.list2StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.list2StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.list2StatusLabel.Location = new System.Drawing.Point(117, 267);
            this.list2StatusLabel.Name = "list2StatusLabel";
            this.list2StatusLabel.Size = new System.Drawing.Size(100, 20);
            this.list2StatusLabel.TabIndex = 11;
            this.list2StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // list1StatusLabel
            // 
            this.list1StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.list1StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.list1StatusLabel.Location = new System.Drawing.Point(12, 267);
            this.list1StatusLabel.Name = "list1StatusLabel";
            this.list1StatusLabel.Size = new System.Drawing.Size(100, 20);
            this.list1StatusLabel.TabIndex = 10;
            this.list1StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(117, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fixed List";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // unloopedListBox
            // 
            this.unloopedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.unloopedListBox.FormattingEnabled = true;
            this.unloopedListBox.Location = new System.Drawing.Point(117, 35);
            this.unloopedListBox.Name = "unloopedListBox";
            this.unloopedListBox.Size = new System.Drawing.Size(100, 225);
            this.unloopedListBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Original List";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loopedListBox
            // 
            this.loopedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.loopedListBox.FormattingEnabled = true;
            this.loopedListBox.Location = new System.Drawing.Point(11, 35);
            this.loopedListBox.Name = "loopedListBox";
            this.loopedListBox.Size = new System.Drawing.Size(100, 225);
            this.loopedListBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 296);
            this.Controls.Add(this.list2StatusLabel);
            this.Controls.Add(this.list1StatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.unloopedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loopedListBox);
            this.Name = "Form1";
            this.Text = "FindLoops";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label list2StatusLabel;
        private System.Windows.Forms.Label list1StatusLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox unloopedListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox loopedListBox;
    }
}

