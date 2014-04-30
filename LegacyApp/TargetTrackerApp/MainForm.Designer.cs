namespace TargetTrackerApp
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
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.btnStartExPoint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(12, 12);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(220, 23);
            this.btnCalibrate.TabIndex = 0;
            this.btnCalibrate.Text = "Калибровать";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.BtnCalibrateClick);
            // 
            // btnStartExPoint
            // 
            this.btnStartExPoint.Enabled = false;
            this.btnStartExPoint.Location = new System.Drawing.Point(12, 41);
            this.btnStartExPoint.Name = "btnStartExPoint";
            this.btnStartExPoint.Size = new System.Drawing.Size(220, 23);
            this.btnStartExPoint.TabIndex = 1;
            this.btnStartExPoint.Text = "Тест - Наведение";
            this.btnStartExPoint.UseVisualStyleBackColor = true;
            this.btnStartExPoint.Click += new System.EventHandler(this.BtnStartExPointClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 188);
            this.Controls.Add(this.btnStartExPoint);
            this.Controls.Add(this.btnCalibrate);
            this.Name = "MainForm";
            this.Text = "Target Tracker";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Button btnStartExPoint;
    }
}

