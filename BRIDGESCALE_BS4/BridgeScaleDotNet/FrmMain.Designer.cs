namespace BridgeScaleDotNet
{
    partial class FrmMain
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
            this.btnWeightReport = new System.Windows.Forms.Button();
            this.btnWeightAdd = new System.Windows.Forms.Button();
            this.btnNewWeight = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.textMessage1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnWeightReport
            // 
            this.btnWeightReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnWeightReport.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightReport.ForeColor = System.Drawing.Color.White;
            this.btnWeightReport.Location = new System.Drawing.Point(215, 243);
            this.btnWeightReport.Name = "btnWeightReport";
            this.btnWeightReport.Size = new System.Drawing.Size(280, 110);
            this.btnWeightReport.TabIndex = 0;
            this.btnWeightReport.Text = "REPORT";
            this.btnWeightReport.UseVisualStyleBackColor = false;
            this.btnWeightReport.Click += new System.EventHandler(this.btnWeightReport_Click);
            // 
            // btnWeightAdd
            // 
            this.btnWeightAdd.BackColor = System.Drawing.Color.Green;
            this.btnWeightAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightAdd.ForeColor = System.Drawing.Color.White;
            this.btnWeightAdd.Location = new System.Drawing.Point(501, 128);
            this.btnWeightAdd.Name = "btnWeightAdd";
            this.btnWeightAdd.Size = new System.Drawing.Size(280, 110);
            this.btnWeightAdd.TabIndex = 0;
            this.btnWeightAdd.Text = "WEIGHT ADD";
            this.btnWeightAdd.UseVisualStyleBackColor = false;
            this.btnWeightAdd.Click += new System.EventHandler(this.btnWeightAdd_Click);
            // 
            // btnNewWeight
            // 
            this.btnNewWeight.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNewWeight.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewWeight.ForeColor = System.Drawing.Color.White;
            this.btnNewWeight.Location = new System.Drawing.Point(215, 128);
            this.btnNewWeight.Name = "btnNewWeight";
            this.btnNewWeight.Size = new System.Drawing.Size(280, 110);
            this.btnNewWeight.TabIndex = 0;
            this.btnNewWeight.Text = "NET WEIGHT";
            this.btnNewWeight.UseVisualStyleBackColor = false;
            this.btnNewWeight.Click += new System.EventHandler(this.btnNewWeight_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(501, 243);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(280, 110);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // textMessage1
            // 
            this.textMessage1.BackColor = System.Drawing.Color.MistyRose;
            this.textMessage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMessage1.ForeColor = System.Drawing.Color.Coral;
            this.textMessage1.Location = new System.Drawing.Point(215, 359);
            this.textMessage1.Multiline = true;
            this.textMessage1.Name = "textMessage1";
            this.textMessage1.ReadOnly = true;
            this.textMessage1.Size = new System.Drawing.Size(566, 58);
            this.textMessage1.TabIndex = 31;
            this.textMessage1.Text = "Sync Pending";
            this.textMessage1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1001, 495);
            this.ControlBox = false;
            this.Controls.Add(this.textMessage1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNewWeight);
            this.Controls.Add(this.btnWeightAdd);
            this.Controls.Add(this.btnWeightReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HOME";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWeightReport;
        private System.Windows.Forms.Button btnWeightAdd;
        private System.Windows.Forms.Button btnNewWeight;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox textMessage1;
    }
}