namespace Acutrol
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
            this.label1 = new System.Windows.Forms.Label();
            this.ShowAxis = new System.Windows.Forms.Timer(this.components);
            this.textReadPos = new System.Windows.Forms.TextBox();
            this.textReadRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textReadAcc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ExecuteCommendButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSetAcc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSetPos = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSetRate = new System.Windows.Forms.TextBox();
            this.ReturnLocalButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SinInputButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxSetPhase = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxSetMagn = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxSetFreq = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxSelectMode = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxLimitPosL = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxLimitRate = new System.Windows.Forms.TextBox();
            this.textBoxLimitPosH = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Position:";
            // 
            // ShowAxis
            // 
            this.ShowAxis.Enabled = true;
            this.ShowAxis.Tick += new System.EventHandler(this.ShowAxis_Tick);
            // 
            // textReadPos
            // 
            this.textReadPos.Location = new System.Drawing.Point(164, 30);
            this.textReadPos.Name = "textReadPos";
            this.textReadPos.Size = new System.Drawing.Size(100, 20);
            this.textReadPos.TabIndex = 3;
            // 
            // textReadRate
            // 
            this.textReadRate.Location = new System.Drawing.Point(164, 88);
            this.textReadRate.Name = "textReadRate";
            this.textReadRate.Size = new System.Drawing.Size(100, 20);
            this.textReadRate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Rate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Acceleration:";
            // 
            // textReadAcc
            // 
            this.textReadAcc.Location = new System.Drawing.Point(164, 136);
            this.textReadAcc.Name = "textReadAcc";
            this.textReadAcc.Size = new System.Drawing.Size(100, 20);
            this.textReadAcc.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Edwardian Script ITC", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(227, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(349, 76);
            this.label4.TabIndex = 9;
            this.label4.Text = "Acutrol Control";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textReadAcc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textReadPos);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textReadRate);
            this.panel1.Location = new System.Drawing.Point(43, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 203);
            this.panel1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Parameter Readings:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ExecuteCommendButton);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBoxSetAcc);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBoxSetPos);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBoxSetRate);
            this.panel2.Location = new System.Drawing.Point(748, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(317, 203);
            this.panel2.TabIndex = 11;
            // 
            // ExecuteCommendButton
            // 
            this.ExecuteCommendButton.Location = new System.Drawing.Point(239, 165);
            this.ExecuteCommendButton.Name = "ExecuteCommendButton";
            this.ExecuteCommendButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteCommendButton.TabIndex = 10;
            this.ExecuteCommendButton.Text = "Execute";
            this.ExecuteCommendButton.UseVisualStyleBackColor = true;
            this.ExecuteCommendButton.Click += new System.EventHandler(this.ExecuteCommendButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Parameter Settings:";
            // 
            // textBoxSetAcc
            // 
            this.textBoxSetAcc.Location = new System.Drawing.Point(164, 136);
            this.textBoxSetAcc.Name = "textBoxSetAcc";
            this.textBoxSetAcc.Size = new System.Drawing.Size(100, 20);
            this.textBoxSetAcc.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Position Commend:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = " Acceleration Commend:";
            // 
            // textBoxSetPos
            // 
            this.textBoxSetPos.Location = new System.Drawing.Point(164, 30);
            this.textBoxSetPos.Name = "textBoxSetPos";
            this.textBoxSetPos.Size = new System.Drawing.Size(100, 20);
            this.textBoxSetPos.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Rate Commend:";
            // 
            // textBoxSetRate
            // 
            this.textBoxSetRate.Location = new System.Drawing.Point(164, 88);
            this.textBoxSetRate.Name = "textBoxSetRate";
            this.textBoxSetRate.Size = new System.Drawing.Size(100, 20);
            this.textBoxSetRate.TabIndex = 4;
            // 
            // ReturnLocalButton
            // 
            this.ReturnLocalButton.Location = new System.Drawing.Point(630, 474);
            this.ReturnLocalButton.Name = "ReturnLocalButton";
            this.ReturnLocalButton.Size = new System.Drawing.Size(75, 41);
            this.ReturnLocalButton.TabIndex = 12;
            this.ReturnLocalButton.Text = "Return Local Mode";
            this.ReturnLocalButton.UseVisualStyleBackColor = true;
            this.ReturnLocalButton.Click += new System.EventHandler(this.ReturnLocalButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SinInputButton);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.textBoxSetPhase);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.textBoxSetMagn);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.textBoxSetFreq);
            this.panel3.Location = new System.Drawing.Point(43, 359);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(317, 203);
            this.panel3.TabIndex = 13;
            // 
            // SinInputButton
            // 
            this.SinInputButton.Location = new System.Drawing.Point(220, 162);
            this.SinInputButton.Name = "SinInputButton";
            this.SinInputButton.Size = new System.Drawing.Size(75, 23);
            this.SinInputButton.TabIndex = 10;
            this.SinInputButton.Text = "Execute";
            this.SinInputButton.UseVisualStyleBackColor = true;
            this.SinInputButton.Click += new System.EventHandler(this.SinInputButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Sinusoidal Input:";
            // 
            // textBoxSetPhase
            // 
            this.textBoxSetPhase.Location = new System.Drawing.Point(164, 136);
            this.textBoxSetPhase.Name = "textBoxSetPhase";
            this.textBoxSetPhase.Size = new System.Drawing.Size(100, 20);
            this.textBoxSetPhase.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Magnitude:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 139);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Phase:";
            // 
            // textBoxSetMagn
            // 
            this.textBoxSetMagn.Location = new System.Drawing.Point(164, 30);
            this.textBoxSetMagn.Name = "textBoxSetMagn";
            this.textBoxSetMagn.Size = new System.Drawing.Size(100, 20);
            this.textBoxSetMagn.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Frequency:";
            // 
            // textBoxSetFreq
            // 
            this.textBoxSetFreq.Location = new System.Drawing.Point(164, 88);
            this.textBoxSetFreq.Name = "textBoxSetFreq";
            this.textBoxSetFreq.Size = new System.Drawing.Size(100, 20);
            this.textBoxSetFreq.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(39, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Axis 1 (Default):";
            // 
            // comboBoxSelectMode
            // 
            this.comboBoxSelectMode.FormattingEnabled = true;
            this.comboBoxSelectMode.Location = new System.Drawing.Point(42, 97);
            this.comboBoxSelectMode.Name = "comboBoxSelectMode";
            this.comboBoxSelectMode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSelectMode.TabIndex = 14;
            this.comboBoxSelectMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectMode_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(40, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Select Mode:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBoxLimitPosH);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.textBoxLimitPosL);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.textBoxLimitRate);
            this.panel4.Location = new System.Drawing.Point(403, 135);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(317, 203);
            this.panel4.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(239, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Execute";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(176, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Parameter Limitation Settings:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(164, 136);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Position Limitation:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 139);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(119, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = " Acceleration Limitation:";
            // 
            // textBoxLimitPosL
            // 
            this.textBoxLimitPosL.Location = new System.Drawing.Point(137, 33);
            this.textBoxLimitPosL.Name = "textBoxLimitPosL";
            this.textBoxLimitPosL.Size = new System.Drawing.Size(70, 20);
            this.textBoxLimitPosL.TabIndex = 3;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 88);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "Rate Limitation:";
            // 
            // textBoxLimitRate
            // 
            this.textBoxLimitRate.Location = new System.Drawing.Point(164, 88);
            this.textBoxLimitRate.Name = "textBoxLimitRate";
            this.textBoxLimitRate.Size = new System.Drawing.Size(100, 20);
            this.textBoxLimitRate.TabIndex = 4;
            // 
            // textBoxLimitPosH
            // 
            this.textBoxLimitPosH.Location = new System.Drawing.Point(227, 33);
            this.textBoxLimitPosH.Name = "textBoxLimitPosH";
            this.textBoxLimitPosH.Size = new System.Drawing.Size(70, 20);
            this.textBoxLimitPosH.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 621);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.comboBoxSelectMode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.ReturnLocalButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer ShowAxis;
        private System.Windows.Forms.TextBox textReadPos;
        private System.Windows.Forms.TextBox textReadRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textReadAcc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSetAcc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSetPos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSetRate;
        private System.Windows.Forms.Button ExecuteCommendButton;
        private System.Windows.Forms.Button ReturnLocalButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button SinInputButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSetPhase;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxSetMagn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxSetFreq;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxSelectMode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxLimitPosH;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxLimitPosL;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxLimitRate;
    }
}

