namespace PartitionProblem
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
            this.weightsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.algorithmsGroupBox = new System.Windows.Forms.GroupBox();
            this.treeSizeLabel = new System.Windows.Forms.Label();
            this.allowShortCircuitCheckBox = new System.Windows.Forms.CheckBox();
            this.branchAndBoundTimeTextBox = new System.Windows.Forms.TextBox();
            this.branchAndBoundDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.branchAndBoundNodesVisitedTextBox = new System.Windows.Forms.TextBox();
            this.branchAndBoundButton = new System.Windows.Forms.Button();
            this.improvementsTimeTextBox = new System.Windows.Forms.TextBox();
            this.improvementsDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.improvementsNodesVisitedTextBox = new System.Windows.Forms.TextBox();
            this.improvementsButton = new System.Windows.Forms.Button();
            this.sortedHillClimbingTimeTextBox = new System.Windows.Forms.TextBox();
            this.sortedHillClimbingDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.sortedHillClimbingNodesVisitedTextBox = new System.Windows.Forms.TextBox();
            this.sortedHillClimbingButton = new System.Windows.Forms.Button();
            this.hillClimbingTimeTextBox = new System.Windows.Forms.TextBox();
            this.hillClimbingDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.hillClimbingNodesVisitedTextBox = new System.Windows.Forms.TextBox();
            this.hillClimbingButton = new System.Windows.Forms.Button();
            this.exhaustiveTimeTextBox = new System.Windows.Forms.TextBox();
            this.exhaustiveDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.exhaustiveNodesVisitedTextBox = new System.Windows.Forms.TextBox();
            this.exhaustiveButton = new System.Windows.Forms.Button();
            this.randomTimeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.randomDifferenceTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.randomNodesVisitedTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.randomButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buildButton = new System.Windows.Forms.Button();
            this.numWeightsTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maxWeightTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minWeightTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.algorithmsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // weightsTextBox
            // 
            this.weightsTextBox.Location = new System.Drawing.Point(13, 27);
            this.weightsTextBox.Multiline = true;
            this.weightsTextBox.Name = "weightsTextBox";
            this.weightsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.weightsTextBox.Size = new System.Drawing.Size(99, 88);
            this.weightsTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Weights:";
            // 
            // algorithmsGroupBox
            // 
            this.algorithmsGroupBox.Controls.Add(this.treeSizeLabel);
            this.algorithmsGroupBox.Controls.Add(this.allowShortCircuitCheckBox);
            this.algorithmsGroupBox.Controls.Add(this.branchAndBoundTimeTextBox);
            this.algorithmsGroupBox.Controls.Add(this.branchAndBoundDifferenceTextBox);
            this.algorithmsGroupBox.Controls.Add(this.branchAndBoundNodesVisitedTextBox);
            this.algorithmsGroupBox.Controls.Add(this.branchAndBoundButton);
            this.algorithmsGroupBox.Controls.Add(this.improvementsTimeTextBox);
            this.algorithmsGroupBox.Controls.Add(this.improvementsDifferenceTextBox);
            this.algorithmsGroupBox.Controls.Add(this.improvementsNodesVisitedTextBox);
            this.algorithmsGroupBox.Controls.Add(this.improvementsButton);
            this.algorithmsGroupBox.Controls.Add(this.sortedHillClimbingTimeTextBox);
            this.algorithmsGroupBox.Controls.Add(this.sortedHillClimbingDifferenceTextBox);
            this.algorithmsGroupBox.Controls.Add(this.sortedHillClimbingNodesVisitedTextBox);
            this.algorithmsGroupBox.Controls.Add(this.sortedHillClimbingButton);
            this.algorithmsGroupBox.Controls.Add(this.hillClimbingTimeTextBox);
            this.algorithmsGroupBox.Controls.Add(this.hillClimbingDifferenceTextBox);
            this.algorithmsGroupBox.Controls.Add(this.hillClimbingNodesVisitedTextBox);
            this.algorithmsGroupBox.Controls.Add(this.hillClimbingButton);
            this.algorithmsGroupBox.Controls.Add(this.exhaustiveTimeTextBox);
            this.algorithmsGroupBox.Controls.Add(this.exhaustiveDifferenceTextBox);
            this.algorithmsGroupBox.Controls.Add(this.exhaustiveNodesVisitedTextBox);
            this.algorithmsGroupBox.Controls.Add(this.exhaustiveButton);
            this.algorithmsGroupBox.Controls.Add(this.randomTimeTextBox);
            this.algorithmsGroupBox.Controls.Add(this.label7);
            this.algorithmsGroupBox.Controls.Add(this.randomDifferenceTextBox);
            this.algorithmsGroupBox.Controls.Add(this.label6);
            this.algorithmsGroupBox.Controls.Add(this.randomNodesVisitedTextBox);
            this.algorithmsGroupBox.Controls.Add(this.label5);
            this.algorithmsGroupBox.Controls.Add(this.randomButton);
            this.algorithmsGroupBox.Location = new System.Drawing.Point(10, 121);
            this.algorithmsGroupBox.Name = "algorithmsGroupBox";
            this.algorithmsGroupBox.Size = new System.Drawing.Size(386, 303);
            this.algorithmsGroupBox.TabIndex = 9;
            this.algorithmsGroupBox.TabStop = false;
            this.algorithmsGroupBox.Text = "Algorithms";
            // 
            // treeSizeLabel
            // 
            this.treeSizeLabel.AutoSize = true;
            this.treeSizeLabel.Location = new System.Drawing.Point(13, 272);
            this.treeSizeLabel.Name = "treeSizeLabel";
            this.treeSizeLabel.Size = new System.Drawing.Size(0, 13);
            this.treeSizeLabel.TabIndex = 28;
            // 
            // allowShortCircuitCheckBox
            // 
            this.allowShortCircuitCheckBox.AutoSize = true;
            this.allowShortCircuitCheckBox.Location = new System.Drawing.Point(16, 24);
            this.allowShortCircuitCheckBox.Name = "allowShortCircuitCheckBox";
            this.allowShortCircuitCheckBox.Size = new System.Drawing.Size(111, 17);
            this.allowShortCircuitCheckBox.TabIndex = 0;
            this.allowShortCircuitCheckBox.Text = "Allow Short Circuit";
            this.allowShortCircuitCheckBox.UseVisualStyleBackColor = true;
            this.allowShortCircuitCheckBox.CheckedChanged += new System.EventHandler(this.allowShortCircuitCheckBox_CheckedChanged);
            // 
            // branchAndBoundTimeTextBox
            // 
            this.branchAndBoundTimeTextBox.Location = new System.Drawing.Point(224, 232);
            this.branchAndBoundTimeTextBox.Name = "branchAndBoundTimeTextBox";
            this.branchAndBoundTimeTextBox.ReadOnly = true;
            this.branchAndBoundTimeTextBox.Size = new System.Drawing.Size(72, 20);
            this.branchAndBoundTimeTextBox.TabIndex = 26;
            this.branchAndBoundTimeTextBox.TabStop = false;
            this.branchAndBoundTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // branchAndBoundDifferenceTextBox
            // 
            this.branchAndBoundDifferenceTextBox.Location = new System.Drawing.Point(304, 232);
            this.branchAndBoundDifferenceTextBox.Name = "branchAndBoundDifferenceTextBox";
            this.branchAndBoundDifferenceTextBox.ReadOnly = true;
            this.branchAndBoundDifferenceTextBox.Size = new System.Drawing.Size(72, 20);
            this.branchAndBoundDifferenceTextBox.TabIndex = 27;
            this.branchAndBoundDifferenceTextBox.TabStop = false;
            this.branchAndBoundDifferenceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // branchAndBoundNodesVisitedTextBox
            // 
            this.branchAndBoundNodesVisitedTextBox.Location = new System.Drawing.Point(144, 232);
            this.branchAndBoundNodesVisitedTextBox.Name = "branchAndBoundNodesVisitedTextBox";
            this.branchAndBoundNodesVisitedTextBox.ReadOnly = true;
            this.branchAndBoundNodesVisitedTextBox.Size = new System.Drawing.Size(72, 20);
            this.branchAndBoundNodesVisitedTextBox.TabIndex = 25;
            this.branchAndBoundNodesVisitedTextBox.TabStop = false;
            this.branchAndBoundNodesVisitedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // branchAndBoundButton
            // 
            this.branchAndBoundButton.Location = new System.Drawing.Point(16, 232);
            this.branchAndBoundButton.Name = "branchAndBoundButton";
            this.branchAndBoundButton.Size = new System.Drawing.Size(120, 24);
            this.branchAndBoundButton.TabIndex = 24;
            this.branchAndBoundButton.Text = "Branch && Bound";
            this.branchAndBoundButton.UseVisualStyleBackColor = true;
            this.branchAndBoundButton.Click += new System.EventHandler(this.branchAndBoundButton_Click);
            // 
            // improvementsTimeTextBox
            // 
            this.improvementsTimeTextBox.Location = new System.Drawing.Point(224, 136);
            this.improvementsTimeTextBox.Name = "improvementsTimeTextBox";
            this.improvementsTimeTextBox.ReadOnly = true;
            this.improvementsTimeTextBox.Size = new System.Drawing.Size(72, 20);
            this.improvementsTimeTextBox.TabIndex = 14;
            this.improvementsTimeTextBox.TabStop = false;
            this.improvementsTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // improvementsDifferenceTextBox
            // 
            this.improvementsDifferenceTextBox.Location = new System.Drawing.Point(304, 136);
            this.improvementsDifferenceTextBox.Name = "improvementsDifferenceTextBox";
            this.improvementsDifferenceTextBox.ReadOnly = true;
            this.improvementsDifferenceTextBox.Size = new System.Drawing.Size(72, 20);
            this.improvementsDifferenceTextBox.TabIndex = 15;
            this.improvementsDifferenceTextBox.TabStop = false;
            this.improvementsDifferenceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // improvementsNodesVisitedTextBox
            // 
            this.improvementsNodesVisitedTextBox.Location = new System.Drawing.Point(144, 136);
            this.improvementsNodesVisitedTextBox.Name = "improvementsNodesVisitedTextBox";
            this.improvementsNodesVisitedTextBox.ReadOnly = true;
            this.improvementsNodesVisitedTextBox.Size = new System.Drawing.Size(72, 20);
            this.improvementsNodesVisitedTextBox.TabIndex = 13;
            this.improvementsNodesVisitedTextBox.TabStop = false;
            this.improvementsNodesVisitedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // improvementsButton
            // 
            this.improvementsButton.Location = new System.Drawing.Point(16, 136);
            this.improvementsButton.Name = "improvementsButton";
            this.improvementsButton.Size = new System.Drawing.Size(120, 24);
            this.improvementsButton.TabIndex = 12;
            this.improvementsButton.Text = "Improvements...";
            this.improvementsButton.UseVisualStyleBackColor = true;
            this.improvementsButton.Click += new System.EventHandler(this.improvementsButton_Click);
            // 
            // sortedHillClimbingTimeTextBox
            // 
            this.sortedHillClimbingTimeTextBox.Location = new System.Drawing.Point(224, 200);
            this.sortedHillClimbingTimeTextBox.Name = "sortedHillClimbingTimeTextBox";
            this.sortedHillClimbingTimeTextBox.ReadOnly = true;
            this.sortedHillClimbingTimeTextBox.Size = new System.Drawing.Size(72, 20);
            this.sortedHillClimbingTimeTextBox.TabIndex = 22;
            this.sortedHillClimbingTimeTextBox.TabStop = false;
            this.sortedHillClimbingTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sortedHillClimbingDifferenceTextBox
            // 
            this.sortedHillClimbingDifferenceTextBox.Location = new System.Drawing.Point(304, 200);
            this.sortedHillClimbingDifferenceTextBox.Name = "sortedHillClimbingDifferenceTextBox";
            this.sortedHillClimbingDifferenceTextBox.ReadOnly = true;
            this.sortedHillClimbingDifferenceTextBox.Size = new System.Drawing.Size(72, 20);
            this.sortedHillClimbingDifferenceTextBox.TabIndex = 23;
            this.sortedHillClimbingDifferenceTextBox.TabStop = false;
            this.sortedHillClimbingDifferenceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sortedHillClimbingNodesVisitedTextBox
            // 
            this.sortedHillClimbingNodesVisitedTextBox.Location = new System.Drawing.Point(144, 200);
            this.sortedHillClimbingNodesVisitedTextBox.Name = "sortedHillClimbingNodesVisitedTextBox";
            this.sortedHillClimbingNodesVisitedTextBox.ReadOnly = true;
            this.sortedHillClimbingNodesVisitedTextBox.Size = new System.Drawing.Size(72, 20);
            this.sortedHillClimbingNodesVisitedTextBox.TabIndex = 21;
            this.sortedHillClimbingNodesVisitedTextBox.TabStop = false;
            this.sortedHillClimbingNodesVisitedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sortedHillClimbingButton
            // 
            this.sortedHillClimbingButton.Location = new System.Drawing.Point(16, 200);
            this.sortedHillClimbingButton.Name = "sortedHillClimbingButton";
            this.sortedHillClimbingButton.Size = new System.Drawing.Size(120, 24);
            this.sortedHillClimbingButton.TabIndex = 20;
            this.sortedHillClimbingButton.Text = "Sorted Hill Climbing";
            this.sortedHillClimbingButton.UseVisualStyleBackColor = true;
            this.sortedHillClimbingButton.Click += new System.EventHandler(this.sortedHillClimbingButton_Click);
            // 
            // hillClimbingTimeTextBox
            // 
            this.hillClimbingTimeTextBox.Location = new System.Drawing.Point(224, 168);
            this.hillClimbingTimeTextBox.Name = "hillClimbingTimeTextBox";
            this.hillClimbingTimeTextBox.ReadOnly = true;
            this.hillClimbingTimeTextBox.Size = new System.Drawing.Size(72, 20);
            this.hillClimbingTimeTextBox.TabIndex = 18;
            this.hillClimbingTimeTextBox.TabStop = false;
            this.hillClimbingTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hillClimbingDifferenceTextBox
            // 
            this.hillClimbingDifferenceTextBox.Location = new System.Drawing.Point(304, 168);
            this.hillClimbingDifferenceTextBox.Name = "hillClimbingDifferenceTextBox";
            this.hillClimbingDifferenceTextBox.ReadOnly = true;
            this.hillClimbingDifferenceTextBox.Size = new System.Drawing.Size(72, 20);
            this.hillClimbingDifferenceTextBox.TabIndex = 19;
            this.hillClimbingDifferenceTextBox.TabStop = false;
            this.hillClimbingDifferenceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hillClimbingNodesVisitedTextBox
            // 
            this.hillClimbingNodesVisitedTextBox.Location = new System.Drawing.Point(144, 168);
            this.hillClimbingNodesVisitedTextBox.Name = "hillClimbingNodesVisitedTextBox";
            this.hillClimbingNodesVisitedTextBox.ReadOnly = true;
            this.hillClimbingNodesVisitedTextBox.Size = new System.Drawing.Size(72, 20);
            this.hillClimbingNodesVisitedTextBox.TabIndex = 17;
            this.hillClimbingNodesVisitedTextBox.TabStop = false;
            this.hillClimbingNodesVisitedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hillClimbingButton
            // 
            this.hillClimbingButton.Location = new System.Drawing.Point(16, 168);
            this.hillClimbingButton.Name = "hillClimbingButton";
            this.hillClimbingButton.Size = new System.Drawing.Size(120, 24);
            this.hillClimbingButton.TabIndex = 16;
            this.hillClimbingButton.Text = "Hill Climbing";
            this.hillClimbingButton.UseVisualStyleBackColor = true;
            this.hillClimbingButton.Click += new System.EventHandler(this.hillClimbingButton_Click);
            // 
            // exhaustiveTimeTextBox
            // 
            this.exhaustiveTimeTextBox.Location = new System.Drawing.Point(224, 72);
            this.exhaustiveTimeTextBox.Name = "exhaustiveTimeTextBox";
            this.exhaustiveTimeTextBox.ReadOnly = true;
            this.exhaustiveTimeTextBox.Size = new System.Drawing.Size(72, 20);
            this.exhaustiveTimeTextBox.TabIndex = 6;
            this.exhaustiveTimeTextBox.TabStop = false;
            this.exhaustiveTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // exhaustiveDifferenceTextBox
            // 
            this.exhaustiveDifferenceTextBox.Location = new System.Drawing.Point(304, 72);
            this.exhaustiveDifferenceTextBox.Name = "exhaustiveDifferenceTextBox";
            this.exhaustiveDifferenceTextBox.ReadOnly = true;
            this.exhaustiveDifferenceTextBox.Size = new System.Drawing.Size(72, 20);
            this.exhaustiveDifferenceTextBox.TabIndex = 7;
            this.exhaustiveDifferenceTextBox.TabStop = false;
            this.exhaustiveDifferenceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // exhaustiveNodesVisitedTextBox
            // 
            this.exhaustiveNodesVisitedTextBox.Location = new System.Drawing.Point(144, 72);
            this.exhaustiveNodesVisitedTextBox.Name = "exhaustiveNodesVisitedTextBox";
            this.exhaustiveNodesVisitedTextBox.ReadOnly = true;
            this.exhaustiveNodesVisitedTextBox.Size = new System.Drawing.Size(72, 20);
            this.exhaustiveNodesVisitedTextBox.TabIndex = 5;
            this.exhaustiveNodesVisitedTextBox.TabStop = false;
            this.exhaustiveNodesVisitedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // exhaustiveButton
            // 
            this.exhaustiveButton.Location = new System.Drawing.Point(16, 72);
            this.exhaustiveButton.Name = "exhaustiveButton";
            this.exhaustiveButton.Size = new System.Drawing.Size(120, 24);
            this.exhaustiveButton.TabIndex = 4;
            this.exhaustiveButton.Text = "Exhaustive";
            this.exhaustiveButton.UseVisualStyleBackColor = true;
            this.exhaustiveButton.Click += new System.EventHandler(this.exhaustiveButton_Click);
            // 
            // randomTimeTextBox
            // 
            this.randomTimeTextBox.Location = new System.Drawing.Point(224, 104);
            this.randomTimeTextBox.Name = "randomTimeTextBox";
            this.randomTimeTextBox.ReadOnly = true;
            this.randomTimeTextBox.Size = new System.Drawing.Size(72, 20);
            this.randomTimeTextBox.TabIndex = 10;
            this.randomTimeTextBox.TabStop = false;
            this.randomTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(224, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Time (sec)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // randomDifferenceTextBox
            // 
            this.randomDifferenceTextBox.Location = new System.Drawing.Point(304, 104);
            this.randomDifferenceTextBox.Name = "randomDifferenceTextBox";
            this.randomDifferenceTextBox.ReadOnly = true;
            this.randomDifferenceTextBox.Size = new System.Drawing.Size(72, 20);
            this.randomDifferenceTextBox.TabIndex = 11;
            this.randomDifferenceTextBox.TabStop = false;
            this.randomDifferenceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(304, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Difference";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // randomNodesVisitedTextBox
            // 
            this.randomNodesVisitedTextBox.Location = new System.Drawing.Point(144, 104);
            this.randomNodesVisitedTextBox.Name = "randomNodesVisitedTextBox";
            this.randomNodesVisitedTextBox.ReadOnly = true;
            this.randomNodesVisitedTextBox.Size = new System.Drawing.Size(72, 20);
            this.randomNodesVisitedTextBox.TabIndex = 9;
            this.randomNodesVisitedTextBox.TabStop = false;
            this.randomNodesVisitedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(144, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Nodes Visited";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(16, 104);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(120, 24);
            this.randomButton.TabIndex = 8;
            this.randomButton.Text = "Random...";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buildButton);
            this.groupBox1.Controls.Add(this.numWeightsTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maxWeightTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.minWeightTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(118, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 104);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Random Weights";
            // 
            // buildButton
            // 
            this.buildButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buildButton.Location = new System.Drawing.Point(102, 73);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(75, 23);
            this.buildButton.TabIndex = 6;
            this.buildButton.Text = "Build";
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // numWeightsTextBox
            // 
            this.numWeightsTextBox.Location = new System.Drawing.Point(115, 21);
            this.numWeightsTextBox.Name = "numWeightsTextBox";
            this.numWeightsTextBox.Size = new System.Drawing.Size(48, 20);
            this.numWeightsTextBox.TabIndex = 1;
            this.numWeightsTextBox.Text = "20";
            this.numWeightsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "# Weights:";
            // 
            // maxWeightTextBox
            // 
            this.maxWeightTextBox.Location = new System.Drawing.Point(203, 47);
            this.maxWeightTextBox.Name = "maxWeightTextBox";
            this.maxWeightTextBox.Size = new System.Drawing.Size(48, 20);
            this.maxWeightTextBox.TabIndex = 5;
            this.maxWeightTextBox.Text = "50";
            this.maxWeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "and";
            // 
            // minWeightTextBox
            // 
            this.minWeightTextBox.Location = new System.Drawing.Point(115, 47);
            this.minWeightTextBox.Name = "minWeightTextBox";
            this.minWeightTextBox.Size = new System.Drawing.Size(48, 20);
            this.minWeightTextBox.TabIndex = 3;
            this.minWeightTextBox.Text = "30";
            this.minWeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Weights between:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 434);
            this.Controls.Add(this.weightsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.algorithmsGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "PartitionProblem";
            this.algorithmsGroupBox.ResumeLayout(false);
            this.algorithmsGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox weightsTextBox;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox algorithmsGroupBox;
        private System.Windows.Forms.Label treeSizeLabel;
        internal System.Windows.Forms.CheckBox allowShortCircuitCheckBox;
        internal System.Windows.Forms.TextBox branchAndBoundTimeTextBox;
        internal System.Windows.Forms.TextBox branchAndBoundDifferenceTextBox;
        internal System.Windows.Forms.TextBox branchAndBoundNodesVisitedTextBox;
        internal System.Windows.Forms.Button branchAndBoundButton;
        internal System.Windows.Forms.TextBox improvementsTimeTextBox;
        internal System.Windows.Forms.TextBox improvementsDifferenceTextBox;
        internal System.Windows.Forms.TextBox improvementsNodesVisitedTextBox;
        internal System.Windows.Forms.Button improvementsButton;
        internal System.Windows.Forms.TextBox sortedHillClimbingTimeTextBox;
        internal System.Windows.Forms.TextBox sortedHillClimbingDifferenceTextBox;
        internal System.Windows.Forms.TextBox sortedHillClimbingNodesVisitedTextBox;
        internal System.Windows.Forms.Button sortedHillClimbingButton;
        internal System.Windows.Forms.TextBox hillClimbingTimeTextBox;
        internal System.Windows.Forms.TextBox hillClimbingDifferenceTextBox;
        internal System.Windows.Forms.TextBox hillClimbingNodesVisitedTextBox;
        internal System.Windows.Forms.Button hillClimbingButton;
        internal System.Windows.Forms.TextBox exhaustiveTimeTextBox;
        internal System.Windows.Forms.TextBox exhaustiveDifferenceTextBox;
        internal System.Windows.Forms.TextBox exhaustiveNodesVisitedTextBox;
        internal System.Windows.Forms.Button exhaustiveButton;
        internal System.Windows.Forms.TextBox randomTimeTextBox;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox randomDifferenceTextBox;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox randomNodesVisitedTextBox;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Button randomButton;
        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Button buildButton;
        internal System.Windows.Forms.TextBox numWeightsTextBox;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox maxWeightTextBox;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox minWeightTextBox;
        internal System.Windows.Forms.Label label2;
    }
}

