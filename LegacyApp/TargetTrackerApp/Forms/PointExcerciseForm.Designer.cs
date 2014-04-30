namespace TargetTrackerApp.Forms
{
    partial class PointExcerciseForm
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCheckFalstart = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbStartDelay = new System.Windows.Forms.TextBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.cbRandomCamera = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMinScore = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbInterBetweenIters = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTimeToHold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbItersCount = new System.Windows.Forms.TextBox();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.tbResults = new System.Windows.Forms.RichTextBox();
            this.timerExcercise = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Controls.Add(this.tabPageResults);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(513, 342);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.lblStatus);
            this.tabPageSettings.Controls.Add(this.label6);
            this.tabPageSettings.Controls.Add(this.cbCheckFalstart);
            this.tabPageSettings.Controls.Add(this.label5);
            this.tabPageSettings.Controls.Add(this.tbStartDelay);
            this.tabPageSettings.Controls.Add(this.btnStartStop);
            this.tabPageSettings.Controls.Add(this.cbRandomCamera);
            this.tabPageSettings.Controls.Add(this.label4);
            this.tabPageSettings.Controls.Add(this.tbMinScore);
            this.tabPageSettings.Controls.Add(this.label3);
            this.tabPageSettings.Controls.Add(this.tbInterBetweenIters);
            this.tabPageSettings.Controls.Add(this.label2);
            this.tabPageSettings.Controls.Add(this.tbTimeToHold);
            this.tabPageSettings.Controls.Add(this.label1);
            this.tabPageSettings.Controls.Add(this.tbItersCount);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(505, 316);
            this.tabPageSettings.TabIndex = 0;
            this.tabPageSettings.Text = "Настройки";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(200, 194);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(10, 13);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "статус:";
            // 
            // cbCheckFalstart
            // 
            this.cbCheckFalstart.AutoSize = true;
            this.cbCheckFalstart.Location = new System.Drawing.Point(8, 160);
            this.cbCheckFalstart.Name = "cbCheckFalstart";
            this.cbCheckFalstart.Size = new System.Drawing.Size(136, 17);
            this.cbCheckFalstart.TabIndex = 12;
            this.cbCheckFalstart.Text = "проверять фальстарт";
            this.cbCheckFalstart.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(74, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "задержка старта, с";
            // 
            // tbStartDelay
            // 
            this.tbStartDelay.Location = new System.Drawing.Point(8, 7);
            this.tbStartDelay.Name = "tbStartDelay";
            this.tbStartDelay.Size = new System.Drawing.Size(60, 20);
            this.tbStartDelay.TabIndex = 10;
            this.tbStartDelay.Text = "10..12";
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(6, 189);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(121, 23);
            this.btnStartStop.TabIndex = 9;
            this.btnStartStop.Text = "Старт!";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.BtnStartStopClick);
            // 
            // cbRandomCamera
            // 
            this.cbRandomCamera.AutoSize = true;
            this.cbRandomCamera.Location = new System.Drawing.Point(8, 137);
            this.cbRandomCamera.Name = "cbRandomCamera";
            this.cbRandomCamera.Size = new System.Drawing.Size(119, 17);
            this.cbRandomCamera.TabIndex = 8;
            this.cbRandomCamera.Text = "случайная камера";
            this.cbRandomCamera.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "мин. очков удержания";
            // 
            // tbMinScore
            // 
            this.tbMinScore.Location = new System.Drawing.Point(8, 111);
            this.tbMinScore.Name = "tbMinScore";
            this.tbMinScore.Size = new System.Drawing.Size(60, 20);
            this.tbMinScore.TabIndex = 6;
            this.tbMinScore.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "интервал между повторами, c";
            // 
            // tbInterBetweenIters
            // 
            this.tbInterBetweenIters.Location = new System.Drawing.Point(8, 85);
            this.tbInterBetweenIters.Name = "tbInterBetweenIters";
            this.tbInterBetweenIters.Size = new System.Drawing.Size(60, 20);
            this.tbInterBetweenIters.TabIndex = 4;
            this.tbInterBetweenIters.Text = "3..10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "время удержания, c";
            // 
            // tbTimeToHold
            // 
            this.tbTimeToHold.Location = new System.Drawing.Point(8, 59);
            this.tbTimeToHold.Name = "tbTimeToHold";
            this.tbTimeToHold.Size = new System.Drawing.Size(60, 20);
            this.tbTimeToHold.TabIndex = 2;
            this.tbTimeToHold.Text = "4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "повторений";
            // 
            // tbItersCount
            // 
            this.tbItersCount.Location = new System.Drawing.Point(8, 33);
            this.tbItersCount.Name = "tbItersCount";
            this.tbItersCount.Size = new System.Drawing.Size(60, 20);
            this.tbItersCount.TabIndex = 0;
            this.tbItersCount.Text = "10";
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.tbResults);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResults.Size = new System.Drawing.Size(505, 316);
            this.tabPageResults.TabIndex = 1;
            this.tabPageResults.Text = "Результаты";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // tbResults
            // 
            this.tbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResults.Location = new System.Drawing.Point(3, 3);
            this.tbResults.Name = "tbResults";
            this.tbResults.Size = new System.Drawing.Size(499, 310);
            this.tbResults.TabIndex = 0;
            this.tbResults.Text = "";
            // 
            // timerExcercise
            // 
            this.timerExcercise.Tick += new System.EventHandler(this.TimerExcerciseTick);
            // 
            // PointExcerciseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 342);
            this.Controls.Add(this.tabControlMain);
            this.Name = "PointExcerciseForm";
            this.Text = "Отработка наведения";
            this.Load += new System.EventHandler(this.PointExcerciseFormLoad);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.tabPageResults.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.TabPage tabPageResults;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.CheckBox cbRandomCamera;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMinScore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbInterBetweenIters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTimeToHold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbItersCount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbCheckFalstart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbStartDelay;
        private System.Windows.Forms.Timer timerExcercise;
        private System.Windows.Forms.RichTextBox tbResults;
    }
}