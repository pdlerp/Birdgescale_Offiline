using System.Drawing;
using System.Windows.Forms;

namespace BridgeScaleDotNet
{
    partial class FrmTruck
    {
        private void InitializeComponent()
        {
            this.panelHero = new Panel();
            this.picLogo = new PictureBox();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.panelInformation = new Panel();
            this.lblInformation = new Label();
            this.lblTruckNo = new Label();
            this.txtTruckNo = new TextBox();
            this.lblDriverName = new Label();
            this.txtDriverName = new TextBox();
            this.lblMobileNo = new Label();
            this.txtMobileNo = new TextBox();
            this.btnSave = new Button();
            this.btnClear = new Button();
            this.btnClose = new Button();
            this.panelList = new Panel();
            this.lblListTitle = new Label();
            this.txtSearch = new TextBox();
            this.btnRefresh = new Button();
            this.btnExport = new Button();
            this.dgvTrucks = new DataGridView();
            this.lblStatus = new Label();
            this.lblTotal = new Label();
            this.panelHero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panelInformation.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrucks)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHero
            // 
            this.panelHero.BackColor = Color.FromArgb(15, 76, 129);
            this.panelHero.Controls.Add(this.picLogo);
            this.panelHero.Controls.Add(this.lblTitle);
            this.panelHero.Controls.Add(this.lblSubtitle);
            this.panelHero.Location = new Point(18, 18);
            this.panelHero.Name = "panelHero";
            this.panelHero.Size = new Size(1164, 112);
            this.panelHero.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = Color.Transparent;
            this.picLogo.Location = new Point(26, 20);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(72, 72);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(120, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(301, 54);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Own Truck Master";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = Color.White;
            this.lblSubtitle.Location = new Point(124, 75);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(330, 25);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Create, update and manage own trucks";
            // 
            // panelInformation
            // 
            this.panelInformation.BackColor = Color.White;
            this.panelInformation.BorderStyle = BorderStyle.FixedSingle;
            this.panelInformation.Controls.Add(this.lblInformation);
            this.panelInformation.Controls.Add(this.lblTruckNo);
            this.panelInformation.Controls.Add(this.txtTruckNo);
            this.panelInformation.Controls.Add(this.lblDriverName);
            this.panelInformation.Controls.Add(this.txtDriverName);
            this.panelInformation.Controls.Add(this.lblMobileNo);
            this.panelInformation.Controls.Add(this.txtMobileNo);
            this.panelInformation.Controls.Add(this.btnSave);
            this.panelInformation.Controls.Add(this.btnClear);
            this.panelInformation.Controls.Add(this.btnClose);
            this.panelInformation.Location = new Point(18, 144);
            this.panelInformation.Name = "panelInformation";
            this.panelInformation.Size = new Size(1164, 218);
            this.panelInformation.TabIndex = 1;
            // 
            // lblInformation
            // 
            this.lblInformation.BackColor = Color.FromArgb(226, 236, 249);
            this.lblInformation.Dock = DockStyle.Top;
            this.lblInformation.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblInformation.ForeColor = Color.FromArgb(15, 63, 117);
            this.lblInformation.Location = new Point(0, 0);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Padding = new Padding(20, 6, 0, 0);
            this.lblInformation.Size = new Size(1162, 48);
            this.lblInformation.TabIndex = 0;
            this.lblInformation.Text = "Truck Information";
            // 
            // lblTruckNo
            // 
            this.lblTruckNo.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            this.lblTruckNo.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTruckNo.Location = new Point(28, 76);
            this.lblTruckNo.Name = "lblTruckNo";
            this.lblTruckNo.Size = new Size(108, 30);
            this.lblTruckNo.TabIndex = 1;
            this.lblTruckNo.Text = "Truck No *";
            this.lblTruckNo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTruckNo
            // 
            this.txtTruckNo.BorderStyle = BorderStyle.FixedSingle;
            this.txtTruckNo.Font = new Font("Segoe UI", 10.5F);
            this.txtTruckNo.Location = new Point(150, 76);
            this.txtTruckNo.MaxLength = 50;
            this.txtTruckNo.Name = "txtTruckNo";
            this.txtTruckNo.Size = new Size(260, 31);
            this.txtTruckNo.TabIndex = 2;
            // 
            // lblDriverName
            // 
            this.lblDriverName.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            this.lblDriverName.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblDriverName.Location = new Point(430, 76);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new Size(108, 30);
            this.lblDriverName.TabIndex = 3;
            this.lblDriverName.Text = "Driver Name";
            this.lblDriverName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDriverName
            // 
            this.txtDriverName.BorderStyle = BorderStyle.FixedSingle;
            this.txtDriverName.Font = new Font("Segoe UI", 10.5F);
            this.txtDriverName.Location = new Point(552, 76);
            this.txtDriverName.MaxLength = 50;
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new Size(260, 31);
            this.txtDriverName.TabIndex = 4;
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            this.lblMobileNo.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblMobileNo.Location = new Point(833, 76);
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.Size = new Size(90, 30);
            this.lblMobileNo.TabIndex = 5;
            this.lblMobileNo.Text = "Mobile No";
            this.lblMobileNo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.BorderStyle = BorderStyle.FixedSingle;
            this.txtMobileNo.Font = new Font("Segoe UI", 10.5F);
            this.txtMobileNo.Location = new Point(935, 76);
            this.txtMobileNo.MaxLength = 50;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new Size(190, 31);
            this.txtMobileNo.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = Color.FromArgb(22, 145, 67);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(28, 137);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(170, 42);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = Color.FromArgb(234, 119, 20);
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnClear.ForeColor = Color.White;
            this.btnClear.Location = new Point(214, 137);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(170, 42);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = Color.FromArgb(202, 31, 39);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(400, 137);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(170, 42);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelList
            // 
            this.panelList.BackColor = Color.White;
            this.panelList.BorderStyle = BorderStyle.FixedSingle;
            this.panelList.Controls.Add(this.lblListTitle);
            this.panelList.Controls.Add(this.txtSearch);
            this.panelList.Controls.Add(this.btnRefresh);
            this.panelList.Controls.Add(this.btnExport);
            this.panelList.Controls.Add(this.dgvTrucks);
            this.panelList.Location = new Point(18, 378);
            this.panelList.Name = "panelList";
            this.panelList.Size = new Size(1164, 354);
            this.panelList.TabIndex = 2;
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblListTitle.ForeColor = Color.FromArgb(15, 63, 117);
            this.lblListTitle.Location = new Point(25, 12);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new Size(152, 37);
            this.lblListTitle.TabIndex = 0;
            this.lblListTitle.Text = "Truck List";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.Font = new Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = Color.FromArgb(71, 85, 105);
            this.txtSearch.Location = new Point(503, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(365, 30);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = Color.White;
            this.btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(15, 76, 129);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefresh.ForeColor = Color.FromArgb(15, 76, 129);
            this.btnRefresh.Location = new Point(883, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(86, 31);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = Color.White;
            this.btnExport.FlatAppearance.BorderColor = Color.FromArgb(15, 76, 129);
            this.btnExport.FlatStyle = FlatStyle.Flat;
            this.btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnExport.ForeColor = Color.FromArgb(15, 76, 129);
            this.btnExport.Location = new Point(978, 23);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(145, 31);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export CSV";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvTrucks
            // 
            this.dgvTrucks.AllowUserToAddRows = false;
            this.dgvTrucks.AllowUserToDeleteRows = false;
            this.dgvTrucks.AllowUserToResizeRows = false;
            this.dgvTrucks.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(244, 248, 253)
            };
            this.dgvTrucks.BackgroundColor = Color.White;
            this.dgvTrucks.BorderStyle = BorderStyle.None;
            this.dgvTrucks.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTrucks.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(15, 76, 129),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(15, 76, 129),
                SelectionForeColor = Color.White
            };
            this.dgvTrucks.ColumnHeadersHeight = 36;
            this.dgvTrucks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTrucks.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(15, 23, 42)
            };
            this.dgvTrucks.EnableHeadersVisualStyles = false;
            this.dgvTrucks.GridColor = Color.FromArgb(203, 213, 225);
            this.dgvTrucks.Location = new Point(14, 73);
            this.dgvTrucks.MultiSelect = false;
            this.dgvTrucks.Name = "dgvTrucks";
            this.dgvTrucks.ReadOnly = true;
            this.dgvTrucks.RowHeadersVisible = false;
            this.dgvTrucks.RowTemplate.Height = 32;
            this.dgvTrucks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrucks.Size = new Size(1134, 262);
            this.dgvTrucks.TabIndex = 4;
            this.dgvTrucks.CellContentClick += new DataGridViewCellEventHandler(this.dgvTrucks_CellContentClick);
            this.dgvTrucks.DataError += new DataGridViewDataErrorEventHandler(this.dgvTrucks_DataError);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Font = new Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblStatus.Location = new Point(20, 748);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(650, 26);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Ready";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.FromArgb(15, 76, 129);
            this.lblTotal.Location = new Point(860, 748);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new Size(322, 26);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total: 0 Own Trucks";
            this.lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FrmTruck
            // 
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(241, 246, 252);
            this.ClientSize = new Size(1198, 790);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelInformation);
            this.Controls.Add(this.panelHero);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTruck";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Own Truck Master";
            this.panelHero.ResumeLayout(false);
            this.panelHero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panelInformation.ResumeLayout(false);
            this.panelInformation.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrucks)).EndInit();
            this.ResumeLayout(false);
        }

        private Panel panelHero;
        private PictureBox picLogo;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelInformation;
        private Label lblInformation;
        private Label lblTruckNo;
        private TextBox txtTruckNo;
        private Label lblDriverName;
        private TextBox txtDriverName;
        private Label lblMobileNo;
        private TextBox txtMobileNo;
        private Button btnSave;
        private Button btnClear;
        private Button btnClose;
        private Panel panelList;
        private Label lblListTitle;
        private TextBox txtSearch;
        private Button btnRefresh;
        private Button btnExport;
        private DataGridView dgvTrucks;
        private Label lblStatus;
        private Label lblTotal;
    }
}
