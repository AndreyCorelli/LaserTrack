namespace TrackerTest
{
    partial class MainForm
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
            this.btnTest = new System.Windows.Forms.Button();
            this.tbColor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDeviation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMinSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMaxSize = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 81);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Тест";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.BtnTestClick);
            // 
            // tbColor
            // 
            this.tbColor.Location = new System.Drawing.Point(12, 12);
            this.tbColor.Name = "tbColor";
            this.tbColor.Size = new System.Drawing.Size(72, 20);
            this.tbColor.TabIndex = 1;
            this.tbColor.Text = "255 255 255";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "цвет";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "погрешность";
            // 
            // tbDeviation
            // 
            this.tbDeviation.Location = new System.Drawing.Point(12, 38);
            this.tbDeviation.Name = "tbDeviation";
            this.tbDeviation.Size = new System.Drawing.Size(72, 20);
            this.tbDeviation.TabIndex = 3;
            this.tbDeviation.Text = "5000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "мин размер";
            // 
            // tbMinSize
            // 
            this.tbMinSize.Location = new System.Drawing.Point(165, 12);
            this.tbMinSize.Name = "tbMinSize";
            this.tbMinSize.Size = new System.Drawing.Size(64, 20);
            this.tbMinSize.TabIndex = 7;
            this.tbMinSize.Text = "20";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "макс размер";
            // 
            // tbMaxSize
            // 
            this.tbMaxSize.Location = new System.Drawing.Point(165, 38);
            this.tbMaxSize.Name = "tbMaxSize";
            this.tbMaxSize.Size = new System.Drawing.Size(64, 20);
            this.tbMaxSize.TabIndex = 9;
            this.tbMaxSize.Text = "200";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 117);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbMaxSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMinSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDeviation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbColor);
            this.Controls.Add(this.btnTest);
            this.Name = "MainForm";
            this.Text = "Тест";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox tbColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDeviation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMinSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbMaxSize;
    }
}

