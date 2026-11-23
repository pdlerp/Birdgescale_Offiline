namespace BridgeScaleDotNet
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblAppContext = new System.Windows.Forms.Label();
            this.panelHero = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblHeroTitle = new System.Windows.Forms.Label();
            this.lblHeroSubtitle = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnWeightReportLocal = new System.Windows.Forms.Button();
            this.lblActionHeading = new System.Windows.Forms.Label();
            this.lblActionHint = new System.Windows.Forms.Label();
            this.btnNewWeight = new System.Windows.Forms.Button();
            this.btnWeightAdd = new System.Windows.Forms.Button();
            this.btnWeightReport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatusIndicator = new System.Windows.Forms.Label();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.textMessage1 = new System.Windows.Forms.TextBox();
            this.panelTitleBar.SuspendLayout();
            this.panelHero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panelActions.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(50)))), ((int)(((byte)(91)))));
            this.panelTitleBar.Controls.Add(this.lblAppName);
            this.panelTitleBar.Controls.Add(this.lblAppContext);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1000, 36);
            this.panelTitleBar.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(28, 7);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(127, 23);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "BRIDGE SCALE";
            // 
            // lblAppContext
            // 
            this.lblAppContext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAppContext.AutoSize = true;
            this.lblAppContext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAppContext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblAppContext.Location = new System.Drawing.Point(831, 8);
            this.lblAppContext.Name = "lblAppContext";
            this.lblAppContext.Size = new System.Drawing.Size(139, 20);
            this.lblAppContext.TabIndex = 1;
            this.lblAppContext.Text = "Operations Console";
            // 
            // panelHero
            // 
            this.panelHero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(82)))), ((int)(((byte)(155)))));
            this.panelHero.Controls.Add(this.picLogo);
            this.panelHero.Controls.Add(this.lblHeroTitle);
            this.panelHero.Controls.Add(this.lblHeroSubtitle);
            this.panelHero.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHero.Location = new System.Drawing.Point(0, 36);
            this.panelHero.Name = "panelHero";
            this.panelHero.Size = new System.Drawing.Size(1000, 126);
            this.panelHero.TabIndex = 1;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(82)))), ((int)(((byte)(155)))));
            this.picLogo.Image = global::BridgeScaleDotNet.Properties.Resources.logo;
            this.picLogo.Location = new System.Drawing.Point(28, 14);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(126, 92);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblHeroTitle
            // 
            this.lblHeroTitle.AutoSize = true;
            this.lblHeroTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblHeroTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeroTitle.Location = new System.Drawing.Point(187, 20);
            this.lblHeroTitle.Name = "lblHeroTitle";
            this.lblHeroTitle.Size = new System.Drawing.Size(272, 54);
            this.lblHeroTitle.TabIndex = 1;
            this.lblHeroTitle.Text = "DASHBOARD";
            // 
            // lblHeroSubtitle
            // 
            this.lblHeroSubtitle.AutoSize = true;
            this.lblHeroSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHeroSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.lblHeroSubtitle.Location = new System.Drawing.Point(186, 84);
            this.lblHeroSubtitle.Name = "lblHeroSubtitle";
            this.lblHeroSubtitle.Size = new System.Drawing.Size(398, 25);
            this.lblHeroSubtitle.TabIndex = 2;
            this.lblHeroSubtitle.Text = "Manage weighing operations with confidence";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActions.Controls.Add(this.btnWeightReportLocal);
            this.panelActions.Controls.Add(this.lblActionHeading);
            this.panelActions.Controls.Add(this.lblActionHint);
            this.panelActions.Controls.Add(this.btnNewWeight);
            this.panelActions.Controls.Add(this.btnWeightAdd);
            this.panelActions.Controls.Add(this.btnWeightReport);
            this.panelActions.Controls.Add(this.btnExit);
            this.panelActions.Location = new System.Drawing.Point(28, 176);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(944, 286);
            this.panelActions.TabIndex = 2;
            // 
            // btnWeightReportLocal
            // 
            this.btnWeightReportLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(119)))), ((int)(((byte)(100)))));
            this.btnWeightReportLocal.FlatAppearance.BorderSize = 0;
            this.btnWeightReportLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeightReportLocal.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnWeightReportLocal.ForeColor = System.Drawing.Color.White;
            this.btnWeightReportLocal.Location = new System.Drawing.Point(236, 174);
            this.btnWeightReportLocal.Name = "btnWeightReportLocal";
            this.btnWeightReportLocal.Size = new System.Drawing.Size(218, 78);
            this.btnWeightReportLocal.TabIndex = 6;
            this.btnWeightReportLocal.Text = "LOCAL REPORT";
            this.btnWeightReportLocal.UseVisualStyleBackColor = false;
            this.btnWeightReportLocal.Click += new System.EventHandler(this.btnWeightReportLocal_Click);
            // 
            // lblActionHeading
            // 
            this.lblActionHeading.AutoSize = true;
            this.lblActionHeading.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblActionHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.lblActionHeading.Location = new System.Drawing.Point(28, 16);
            this.lblActionHeading.Name = "lblActionHeading";
            this.lblActionHeading.Size = new System.Drawing.Size(177, 35);
            this.lblActionHeading.TabIndex = 0;
            this.lblActionHeading.Text = "Quick Actions";
            // 
            // lblActionHint
            // 
            this.lblActionHint.AutoSize = true;
            this.lblActionHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblActionHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblActionHint.Location = new System.Drawing.Point(31, 51);
            this.lblActionHint.Name = "lblActionHint";
            this.lblActionHint.Size = new System.Drawing.Size(241, 20);
            this.lblActionHint.TabIndex = 1;
            this.lblActionHint.Text = "Choose an operation to get started";
            // 
            // btnNewWeight
            // 
            this.btnNewWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNewWeight.FlatAppearance.BorderSize = 0;
            this.btnNewWeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewWeight.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnNewWeight.ForeColor = System.Drawing.Color.White;
            this.btnNewWeight.Location = new System.Drawing.Point(30, 84);
            this.btnNewWeight.Name = "btnNewWeight";
            this.btnNewWeight.Size = new System.Drawing.Size(424, 78);
            this.btnNewWeight.TabIndex = 2;
            this.btnNewWeight.Text = "NET WEIGHT";
            this.btnNewWeight.UseVisualStyleBackColor = false;
            this.btnNewWeight.Click += new System.EventHandler(this.btnNewWeight_Click);
            // 
            // btnWeightAdd
            // 
            this.btnWeightAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(145)))), ((int)(((byte)(67)))));
            this.btnWeightAdd.FlatAppearance.BorderSize = 0;
            this.btnWeightAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeightAdd.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnWeightAdd.ForeColor = System.Drawing.Color.White;
            this.btnWeightAdd.Location = new System.Drawing.Point(468, 84);
            this.btnWeightAdd.Name = "btnWeightAdd";
            this.btnWeightAdd.Size = new System.Drawing.Size(424, 78);
            this.btnWeightAdd.TabIndex = 3;
            this.btnWeightAdd.Text = "WEIGHT ADD";
            this.btnWeightAdd.UseVisualStyleBackColor = false;
            this.btnWeightAdd.Click += new System.EventHandler(this.btnWeightAdd_Click);
            // 
            // btnWeightReport
            // 
            this.btnWeightReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(119)))), ((int)(((byte)(20)))));
            this.btnWeightReport.FlatAppearance.BorderSize = 0;
            this.btnWeightReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeightReport.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnWeightReport.ForeColor = System.Drawing.Color.White;
            this.btnWeightReport.Location = new System.Drawing.Point(30, 174);
            this.btnWeightReport.Name = "btnWeightReport";
            this.btnWeightReport.Size = new System.Drawing.Size(194, 78);
            this.btnWeightReport.TabIndex = 4;
            this.btnWeightReport.Text = "REPORT";
            this.btnWeightReport.UseVisualStyleBackColor = false;
            this.btnWeightReport.Click += new System.EventHandler(this.btnWeightReport_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(31)))), ((int)(((byte)(39)))));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(468, 174);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(424, 78);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.White;
            this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatus.Controls.Add(this.lblStatusIndicator);
            this.panelStatus.Controls.Add(this.lblStatusText);
            this.panelStatus.Controls.Add(this.textMessage1);
            this.panelStatus.Location = new System.Drawing.Point(28, 475);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(944, 50);
            this.panelStatus.TabIndex = 3;
            // 
            // lblStatusIndicator
            // 
            this.lblStatusIndicator.AutoSize = true;
            this.lblStatusIndicator.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblStatusIndicator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(145)))), ((int)(((byte)(67)))));
            this.lblStatusIndicator.Location = new System.Drawing.Point(20, 9);
            this.lblStatusIndicator.Name = "lblStatusIndicator";
            this.lblStatusIndicator.Size = new System.Drawing.Size(33, 37);
            this.lblStatusIndicator.TabIndex = 0;
            this.lblStatusIndicator.Text = "●";
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStatusText.Location = new System.Drawing.Point(49, 14);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(118, 23);
            this.lblStatusText.TabIndex = 1;
            this.lblStatusText.Text = "System ready";
            // 
            // textMessage1
            // 
            this.textMessage1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textMessage1.BackColor = System.Drawing.Color.White;
            this.textMessage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textMessage1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.textMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.textMessage1.Location = new System.Drawing.Point(496, 13);
            this.textMessage1.Multiline = true;
            this.textMessage1.Name = "textMessage1";
            this.textMessage1.ReadOnly = true;
            this.textMessage1.Size = new System.Drawing.Size(424, 23);
            this.textMessage1.TabIndex = 2;
            this.textMessage1.Text = "Data sync to server every 5 minutes";
            this.textMessage1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1000, 570);
            this.ControlBox = false;
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHero);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bridge Scale";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelHero.ResumeLayout(false);
            this.panelHero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelActions.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblAppContext;
        private System.Windows.Forms.Panel panelHero;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblHeroTitle;
        private System.Windows.Forms.Label lblHeroSubtitle;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Label lblActionHeading;
        private System.Windows.Forms.Label lblActionHint;
        private System.Windows.Forms.Button btnWeightReport;
        private System.Windows.Forms.Button btnWeightAdd;
        private System.Windows.Forms.Button btnNewWeight;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatusIndicator;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.TextBox textMessage1;
        private System.Windows.Forms.Button btnWeightReportLocal;
    }
}
