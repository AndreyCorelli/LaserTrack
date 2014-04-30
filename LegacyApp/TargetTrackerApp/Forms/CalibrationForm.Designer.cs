namespace TargetTrackerApp.Forms
{
    partial class CalibrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.cbShowSpot = new System.Windows.Forms.CheckBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.imageListGlyph = new System.Windows.Forms.ImageList(this.components);
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lbCameras = new System.Windows.Forms.ListBox();
            this.panelCamerasTop = new System.Windows.Forms.Panel();
            this.btnRemoveCamera = new System.Windows.Forms.Button();
            this.btnAddCamera = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelTool = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMaxSizeOfDot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPixelsInSpot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSpotTolerance = new System.Windows.Forms.TextBox();
            this.panelSpot = new System.Windows.Forms.Panel();
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.rbModeSpot = new System.Windows.Forms.RadioButton();
            this.rbModeMarker = new System.Windows.Forms.RadioButton();
            this.pbFrame = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelCamerasTop.SuspendLayout();
            this.panelTool.SuspendLayout();
            this.groupMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblScore);
            this.panelTop.Controls.Add(this.label5);
            this.panelTop.Controls.Add(this.cbShowSpot);
            this.panelTop.Controls.Add(this.btnStartStop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(772, 26);
            this.panelTop.TabIndex = 0;
            // 
            // cbShowSpot
            // 
            this.cbShowSpot.AutoSize = true;
            this.cbShowSpot.Location = new System.Drawing.Point(244, 4);
            this.cbShowSpot.Name = "cbShowSpot";
            this.cbShowSpot.Size = new System.Drawing.Size(87, 17);
            this.cbShowSpot.TabIndex = 1;
            this.cbShowSpot.Text = "показ точки";
            this.cbShowSpot.UseVisualStyleBackColor = true;
            this.cbShowSpot.CheckedChanged += new System.EventHandler(this.CbShowSpotCheckedChanged);
            // 
            // btnStartStop
            // 
            this.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartStop.ImageIndex = 0;
            this.btnStartStop.ImageList = this.imageListGlyph;
            this.btnStartStop.Location = new System.Drawing.Point(116, 0);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(24, 23);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.BtnStartStopClick);
            // 
            // imageListGlyph
            // 
            this.imageListGlyph.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGlyph.ImageStream")));
            this.imageListGlyph.TransparentColor = System.Drawing.Color.Yellow;
            this.imageListGlyph.Images.SetKeyName(0, "pictStart.bmp");
            this.imageListGlyph.Images.SetKeyName(1, "pictStop.bmp");
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnAccept);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 518);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(772, 27);
            this.panelBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(106, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(3, 3);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Принять";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.BtnAcceptClick);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.lbCameras);
            this.panelLeft.Controls.Add(this.panelCamerasTop);
            this.panelLeft.Controls.Add(this.panelTool);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 26);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(115, 492);
            this.panelLeft.TabIndex = 2;
            // 
            // lbCameras
            // 
            this.lbCameras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCameras.FormattingEnabled = true;
            this.lbCameras.Location = new System.Drawing.Point(0, 256);
            this.lbCameras.Name = "lbCameras";
            this.lbCameras.Size = new System.Drawing.Size(115, 236);
            this.lbCameras.TabIndex = 2;
            this.lbCameras.SelectedIndexChanged += new System.EventHandler(this.LbCamerasSelectedIndexChanged);
            // 
            // panelCamerasTop
            // 
            this.panelCamerasTop.Controls.Add(this.btnRemoveCamera);
            this.panelCamerasTop.Controls.Add(this.btnAddCamera);
            this.panelCamerasTop.Controls.Add(this.label2);
            this.panelCamerasTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCamerasTop.Location = new System.Drawing.Point(0, 207);
            this.panelCamerasTop.Name = "panelCamerasTop";
            this.panelCamerasTop.Size = new System.Drawing.Size(115, 49);
            this.panelCamerasTop.TabIndex = 1;
            // 
            // btnRemoveCamera
            // 
            this.btnRemoveCamera.Location = new System.Drawing.Point(34, 19);
            this.btnRemoveCamera.Name = "btnRemoveCamera";
            this.btnRemoveCamera.Size = new System.Drawing.Size(22, 23);
            this.btnRemoveCamera.TabIndex = 2;
            this.btnRemoveCamera.Text = "-";
            this.btnRemoveCamera.UseVisualStyleBackColor = true;
            this.btnRemoveCamera.Click += new System.EventHandler(this.BtnRemoveCameraClick);
            // 
            // btnAddCamera
            // 
            this.btnAddCamera.Location = new System.Drawing.Point(6, 19);
            this.btnAddCamera.Name = "btnAddCamera";
            this.btnAddCamera.Size = new System.Drawing.Size(22, 23);
            this.btnAddCamera.TabIndex = 1;
            this.btnAddCamera.Text = "+";
            this.btnAddCamera.UseVisualStyleBackColor = true;
            this.btnAddCamera.Click += new System.EventHandler(this.BtnAddCameraClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Камеры";
            // 
            // panelTool
            // 
            this.panelTool.Controls.Add(this.label4);
            this.panelTool.Controls.Add(this.tbMaxSizeOfDot);
            this.panelTool.Controls.Add(this.label3);
            this.panelTool.Controls.Add(this.tbPixelsInSpot);
            this.panelTool.Controls.Add(this.label1);
            this.panelTool.Controls.Add(this.tbSpotTolerance);
            this.panelTool.Controls.Add(this.panelSpot);
            this.panelTool.Controls.Add(this.groupMode);
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTool.Location = new System.Drawing.Point(0, 0);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(115, 207);
            this.panelTool.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "макс. точка";
            // 
            // tbMaxSizeOfDot
            // 
            this.tbMaxSizeOfDot.Location = new System.Drawing.Point(8, 184);
            this.tbMaxSizeOfDot.Name = "tbMaxSizeOfDot";
            this.tbMaxSizeOfDot.Size = new System.Drawing.Size(83, 20);
            this.tbMaxSizeOfDot.TabIndex = 11;
            this.tbMaxSizeOfDot.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "пикселей в метке";
            // 
            // tbPixelsInSpot
            // 
            this.tbPixelsInSpot.Location = new System.Drawing.Point(8, 148);
            this.tbPixelsInSpot.Name = "tbPixelsInSpot";
            this.tbPixelsInSpot.Size = new System.Drawing.Size(83, 20);
            this.tbPixelsInSpot.TabIndex = 9;
            this.tbPixelsInSpot.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "погрешность метки";
            // 
            // tbSpotTolerance
            // 
            this.tbSpotTolerance.Location = new System.Drawing.Point(8, 111);
            this.tbSpotTolerance.Name = "tbSpotTolerance";
            this.tbSpotTolerance.Size = new System.Drawing.Size(83, 20);
            this.tbSpotTolerance.TabIndex = 7;
            this.tbSpotTolerance.Text = "5000";
            // 
            // panelSpot
            // 
            this.panelSpot.BackColor = System.Drawing.Color.White;
            this.panelSpot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpot.Location = new System.Drawing.Point(8, 70);
            this.panelSpot.Name = "panelSpot";
            this.panelSpot.Size = new System.Drawing.Size(20, 19);
            this.panelSpot.TabIndex = 6;
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.rbModeSpot);
            this.groupMode.Controls.Add(this.rbModeMarker);
            this.groupMode.Location = new System.Drawing.Point(2, 2);
            this.groupMode.Name = "groupMode";
            this.groupMode.Size = new System.Drawing.Size(109, 62);
            this.groupMode.TabIndex = 5;
            this.groupMode.TabStop = false;
            this.groupMode.Text = "Режим";
            // 
            // rbModeSpot
            // 
            this.rbModeSpot.AutoSize = true;
            this.rbModeSpot.Location = new System.Drawing.Point(6, 42);
            this.rbModeSpot.Name = "rbModeSpot";
            this.rbModeSpot.Size = new System.Drawing.Size(53, 17);
            this.rbModeSpot.TabIndex = 1;
            this.rbModeSpot.Text = "точка";
            this.rbModeSpot.UseVisualStyleBackColor = true;
            // 
            // rbModeMarker
            // 
            this.rbModeMarker.AutoSize = true;
            this.rbModeMarker.Checked = true;
            this.rbModeMarker.Location = new System.Drawing.Point(6, 19);
            this.rbModeMarker.Name = "rbModeMarker";
            this.rbModeMarker.Size = new System.Drawing.Size(71, 17);
            this.rbModeMarker.TabIndex = 0;
            this.rbModeMarker.TabStop = true;
            this.rbModeMarker.Text = "маркеры";
            this.rbModeMarker.UseVisualStyleBackColor = true;
            // 
            // pbFrame
            // 
            this.pbFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFrame.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbFrame.Location = new System.Drawing.Point(116, 26);
            this.pbFrame.Name = "pbFrame";
            this.pbFrame.Size = new System.Drawing.Size(640, 480);
            this.pbFrame.TabIndex = 3;
            this.pbFrame.TabStop = false;
            this.pbFrame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PbFrameMouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "очков:";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(404, 5);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(10, 13);
            this.lblScore.TabIndex = 12;
            this.lblScore.Text = "-";
            // 
            // CalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 545);
            this.Controls.Add(this.pbFrame);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "CalibrationForm";
            this.Text = "Калибровать";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalibrationFormFormClosing);
            this.Load += new System.EventHandler(this.CalibrationFormLoad);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelCamerasTop.ResumeLayout(false);
            this.panelCamerasTop.PerformLayout();
            this.panelTool.ResumeLayout(false);
            this.panelTool.PerformLayout();
            this.groupMode.ResumeLayout(false);
            this.groupMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.ListBox lbCameras;
        private System.Windows.Forms.Panel panelCamerasTop;
        private System.Windows.Forms.Button btnRemoveCamera;
        private System.Windows.Forms.Button btnAddCamera;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelTool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSpotTolerance;
        private System.Windows.Forms.Panel panelSpot;
        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.RadioButton rbModeSpot;
        private System.Windows.Forms.RadioButton rbModeMarker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPixelsInSpot;
        private System.Windows.Forms.PictureBox pbFrame;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ImageList imageListGlyph;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMaxSizeOfDot;
        private System.Windows.Forms.CheckBox cbShowSpot;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label5;
    }
}