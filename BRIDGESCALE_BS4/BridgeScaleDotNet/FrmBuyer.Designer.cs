namespace BridgeScaleDotNet
{
    partial class FrmBuyer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHero = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelInformationHeader = new System.Windows.Forms.Panel();
            this.lblSectionTitle = new System.Windows.Forms.Label();
            this.lblBuyerName = new System.Windows.Forms.Label();
            this.txtBuyerName = new System.Windows.Forms.TextBox();
            this.lblBuyerAddress = new System.Windows.Forms.Label();
            this.txtBuyerAddress = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelList = new System.Windows.Forms.Panel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvBuyers = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panelHero.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelInformationHeader.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHero
            // 
            this.panelHero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(82)))), ((int)(((byte)(155)))));
            this.panelHero.Controls.Add(this.picLogo);
            this.panelHero.Controls.Add(this.lblTitle);
            this.panelHero.Controls.Add(this.lblSubtitle);
            this.panelHero.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHero.Location = new System.Drawing.Point(0, 0);
            this.panelHero.Name = "panelHero";
            this.panelHero.Size = new System.Drawing.Size(1200, 105);
            this.panelHero.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(155, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(289, 57);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Buyer Master";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.White;
            this.lblSubtitle.Location = new System.Drawing.Point(158, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(318, 28);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Create, Update and Manage Buyers";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panelInformationHeader);
            this.panelHeader.Controls.Add(this.lblBuyerName);
            this.panelHeader.Controls.Add(this.txtBuyerName);
            this.panelHeader.Controls.Add(this.lblBuyerAddress);
            this.panelHeader.Controls.Add(this.txtBuyerAddress);
            this.panelHeader.Controls.Add(this.btnSave);
            this.panelHeader.Controls.Add(this.btnClear);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Location = new System.Drawing.Point(18, 120);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1164, 230);
            this.panelHeader.TabIndex = 1;
            // 
            // panelInformationHeader
            // 
            this.panelInformationHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(236)))), ((int)(((byte)(250)))));
            this.panelInformationHeader.Controls.Add(this.lblSectionTitle);
            this.panelInformationHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInformationHeader.Location = new System.Drawing.Point(0, 0);
            this.panelInformationHeader.Name = "panelInformationHeader";
            this.panelInformationHeader.Size = new System.Drawing.Size(1162, 52);
            this.panelInformationHeader.TabIndex = 0;
            // 
            // lblSectionTitle
            // 
            this.lblSectionTitle.AutoSize = true;
            this.lblSectionTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSectionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.lblSectionTitle.Location = new System.Drawing.Point(25, 11);
            this.lblSectionTitle.Name = "lblSectionTitle";
            this.lblSectionTitle.Size = new System.Drawing.Size(253, 37);
            this.lblSectionTitle.TabIndex = 0;
            this.lblSectionTitle.Text = "Buyer Information";
            // 
            // lblBuyerName
            // 
            this.lblBuyerName.AutoSize = true;
            this.lblBuyerName.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblBuyerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBuyerName.Location = new System.Drawing.Point(40, 91);
            this.lblBuyerName.Name = "lblBuyerName";
            this.lblBuyerName.Size = new System.Drawing.Size(118, 25);
            this.lblBuyerName.TabIndex = 1;
            this.lblBuyerName.Text = "Buyer Name";
            // 
            // txtBuyerName
            // 
            this.txtBuyerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuyerName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBuyerName.Location = new System.Drawing.Point(200, 84);
            this.txtBuyerName.MaxLength = 100;
            this.txtBuyerName.Name = "txtBuyerName";
            this.txtBuyerName.Size = new System.Drawing.Size(380, 32);
            this.txtBuyerName.TabIndex = 2;
            // 
            // lblBuyerAddress
            // 
            this.lblBuyerAddress.AutoSize = true;
            this.lblBuyerAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblBuyerAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBuyerAddress.Location = new System.Drawing.Point(635, 91);
            this.lblBuyerAddress.Name = "lblBuyerAddress";
            this.lblBuyerAddress.Size = new System.Drawing.Size(80, 25);
            this.lblBuyerAddress.TabIndex = 3;
            this.lblBuyerAddress.Text = "Address";
            // 
            // txtBuyerAddress
            // 
            this.txtBuyerAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuyerAddress.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBuyerAddress.Location = new System.Drawing.Point(735, 84);
            this.txtBuyerAddress.MaxLength = 250;
            this.txtBuyerAddress.Multiline = true;
            this.txtBuyerAddress.Name = "txtBuyerAddress";
            this.txtBuyerAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBuyerAddress.Size = new System.Drawing.Size(390, 78);
            this.txtBuyerAddress.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(145)))), ((int)(((byte)(67)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(40, 178);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(175, 42);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(119)))), ((int)(((byte)(20)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(230, 178);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(175, 42);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(31)))), ((int)(((byte)(39)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(420, 178);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(175, 42);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelList
            // 
            this.panelList.BackColor = System.Drawing.Color.White;
            this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelList.Controls.Add(this.lblListTitle);
            this.panelList.Controls.Add(this.txtSearch);
            this.panelList.Controls.Add(this.btnRefresh);
            this.panelList.Controls.Add(this.btnExport);
            this.panelList.Controls.Add(this.dgvBuyers);
            this.panelList.Location = new System.Drawing.Point(18, 365);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(1164, 326);
            this.panelList.TabIndex = 2;
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(63)))), ((int)(((byte)(117)))));
            this.lblListTitle.Location = new System.Drawing.Point(25, 12);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(143, 37);
            this.lblListTitle.TabIndex = 0;
            this.lblListTitle.Text = "Buyer List";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.txtSearch.Location = new System.Drawing.Point(520, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(365, 30);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.btnRefresh.Location = new System.Drawing.Point(900, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.btnExport.Location = new System.Drawing.Point(995, 7);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(145, 30);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export CSV";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvBuyers
            // 
            this.dgvBuyers.AllowUserToAddRows = false;
            this.dgvBuyers.AllowUserToDeleteRows = false;
            this.dgvBuyers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(253)))));
            this.dgvBuyers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBuyers.BackgroundColor = System.Drawing.Color.White;
            this.dgvBuyers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBuyers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBuyers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBuyers.ColumnHeadersHeight = 36;
            this.dgvBuyers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBuyers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBuyers.EnableHeadersVisualStyles = false;
            this.dgvBuyers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.dgvBuyers.Location = new System.Drawing.Point(14, 62);
            this.dgvBuyers.MultiSelect = false;
            this.dgvBuyers.Name = "dgvBuyers";
            this.dgvBuyers.ReadOnly = true;
            this.dgvBuyers.RowHeadersVisible = false;
            this.dgvBuyers.RowHeadersWidth = 51;
            this.dgvBuyers.RowTemplate.Height = 32;
            this.dgvBuyers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBuyers.Size = new System.Drawing.Size(1134, 246);
            this.dgvBuyers.TabIndex = 4;
            this.dgvBuyers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBuyers_CellContentClick);
            this.dgvBuyers.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvBuyers_DataError);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStatus.Location = new System.Drawing.Point(28, 710);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(650, 26);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Ready";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.lblTotal.Location = new System.Drawing.Point(850, 710);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(325, 26);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total: 0 Buyers";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(82)))), ((int)(((byte)(155)))));
            this.picLogo.Image = global::BridgeScaleDotNet.Properties.Resources.logo;
            this.picLogo.Location = new System.Drawing.Point(19, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(110, 81);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 4;
            this.picLogo.TabStop = false;
            // 
            // FrmBuyer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelHero);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBuyer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buyer Master";
            this.panelHero.ResumeLayout(false);
            this.panelHero.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelInformationHeader.ResumeLayout(false);
            this.panelInformationHeader.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuyers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHero;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelInformationHeader;
        private System.Windows.Forms.Label lblSectionTitle;
        private System.Windows.Forms.Label lblBuyerName;
        private System.Windows.Forms.TextBox txtBuyerName;
        private System.Windows.Forms.Label lblBuyerAddress;
        private System.Windows.Forms.TextBox txtBuyerAddress;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView dgvBuyers;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.PictureBox picLogo;
    }
}
