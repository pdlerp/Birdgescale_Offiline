namespace BridgeScaleDotNet
{
    partial class FrmInformation
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
            this.cmbWeightID = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSyncStatus = new System.Windows.Forms.Label();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.btnBuyer = new System.Windows.Forms.Button();
            this.btnSeller = new System.Windows.Forms.Button();
            this.transferPanel = new System.Windows.Forms.GroupBox();
            this.cmbTransferTo = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbTransferFrom = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.chkTransfer = new System.Windows.Forms.CheckBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.hiddenWeightIDReport = new System.Windows.Forms.Label();
            this.txtHiddenWeightID = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtBuyerAddress = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTareWeightTime = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblGrossWeightTime = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSellerCode = new System.Windows.Forms.TextBox();
            this.txtBuyerCode = new System.Windows.Forms.TextBox();
            this.txtGrossWeight = new System.Windows.Forms.TextBox();
            this.txtTareWeight = new System.Windows.Forms.TextBox();
            this.txtChallanNo = new System.Windows.Forms.TextBox();
            this.txtTruckNo = new System.Windows.Forms.TextBox();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.txtSellerAddress = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblChallan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.cmbBuyer = new System.Windows.Forms.ComboBox();
            this.cmbSeller = new System.Windows.Forms.ComboBox();
            this.cmbWeightType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtScaleWeight = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.txtOperatorName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.transferPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbWeightID
            // 
            this.cmbWeightID.FormattingEnabled = true;
            this.cmbWeightID.ItemHeight = 16;
            this.cmbWeightID.Location = new System.Drawing.Point(244, 15);
            this.cmbWeightID.Name = "cmbWeightID";
            this.cmbWeightID.Size = new System.Drawing.Size(329, 24);
            this.cmbWeightID.TabIndex = 2;
            this.cmbWeightID.TextUpdate += new System.EventHandler(this.cmbWeightID_TextUpdate);
            this.cmbWeightID.SelectedValueChanged += new System.EventHandler(this.cmbWeightID_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblSyncStatus);
            this.panel1.Controls.Add(this.textMessage);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnBuyer);
            this.panel1.Controls.Add(this.btnSeller);
            this.panel1.Controls.Add(this.transferPanel);
            this.panel1.Controls.Add(this.chkTransfer);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.hiddenWeightIDReport);
            this.panel1.Controls.Add(this.txtHiddenWeightID);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.txtBuyerAddress);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lblTareWeightTime);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblGrossWeightTime);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtSellerCode);
            this.panel1.Controls.Add(this.txtBuyerCode);
            this.panel1.Controls.Add(this.txtGrossWeight);
            this.panel1.Controls.Add(this.txtTareWeight);
            this.panel1.Controls.Add(this.txtChallanNo);
            this.panel1.Controls.Add(this.txtTruckNo);
            this.panel1.Controls.Add(this.txtDriverName);
            this.panel1.Controls.Add(this.txtSellerAddress);
            this.panel1.Controls.Add(this.txtQty);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.lblChallan);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbMaterial);
            this.panel1.Controls.Add(this.cmbBuyer);
            this.panel1.Controls.Add(this.cmbSeller);
            this.panel1.Controls.Add(this.cmbWeightType);
            this.panel1.Controls.Add(this.cmbWeightID);
            this.panel1.Location = new System.Drawing.Point(40, 146);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1317, 680);
            this.panel1.TabIndex = 3;
            // 
            // lblSyncStatus
            // 
            this.lblSyncStatus.AutoSize = true;
            this.lblSyncStatus.Location = new System.Drawing.Point(1162, 545);
            this.lblSyncStatus.Name = "lblSyncStatus";
            this.lblSyncStatus.Size = new System.Drawing.Size(88, 16);
            this.lblSyncStatus.TabIndex = 31;
            this.lblSyncStatus.Text = "lblSyncStatus";
            // 
            // textMessage
            // 
            this.textMessage.BackColor = System.Drawing.Color.MistyRose;
            this.textMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMessage.ForeColor = System.Drawing.Color.Coral;
            this.textMessage.Location = new System.Drawing.Point(302, 585);
            this.textMessage.Multiline = true;
            this.textMessage.Name = "textMessage";
            this.textMessage.ReadOnly = true;
            this.textMessage.Size = new System.Drawing.Size(1004, 90);
            this.textMessage.TabIndex = 30;
            this.textMessage.Text = "Sync Pending";
            // 
            // btnSync
            // 
            this.btnSync.BackColor = System.Drawing.Color.Purple;
            this.btnSync.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSync.ForeColor = System.Drawing.Color.White;
            this.btnSync.Location = new System.Drawing.Point(915, 529);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(200, 57);
            this.btnSync.TabIndex = 29;
            this.btnSync.Text = "Data Sync";
            this.btnSync.UseVisualStyleBackColor = false;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnBuyer
            // 
            this.btnBuyer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBuyer.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuyer.ForeColor = System.Drawing.Color.White;
            this.btnBuyer.Location = new System.Drawing.Point(1276, 160);
            this.btnBuyer.Name = "btnBuyer";
            this.btnBuyer.Size = new System.Drawing.Size(40, 40);
            this.btnBuyer.TabIndex = 28;
            this.btnBuyer.Text = "+";
            this.btnBuyer.UseVisualStyleBackColor = false;
            this.btnBuyer.Click += new System.EventHandler(this.btnBuyer_Click);
            // 
            // btnSeller
            // 
            this.btnSeller.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSeller.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeller.ForeColor = System.Drawing.Color.White;
            this.btnSeller.Location = new System.Drawing.Point(1276, 8);
            this.btnSeller.Name = "btnSeller";
            this.btnSeller.Size = new System.Drawing.Size(40, 38);
            this.btnSeller.TabIndex = 27;
            this.btnSeller.Text = "+";
            this.btnSeller.UseVisualStyleBackColor = false;
            this.btnSeller.Click += new System.EventHandler(this.btnSeller_Click);
            // 
            // transferPanel
            // 
            this.transferPanel.Controls.Add(this.cmbTransferTo);
            this.transferPanel.Controls.Add(this.label22);
            this.transferPanel.Controls.Add(this.cmbTransferFrom);
            this.transferPanel.Controls.Add(this.label18);
            this.transferPanel.Location = new System.Drawing.Point(9, 308);
            this.transferPanel.Name = "transferPanel";
            this.transferPanel.Size = new System.Drawing.Size(1307, 43);
            this.transferPanel.TabIndex = 26;
            this.transferPanel.TabStop = false;
            this.transferPanel.Visible = false;
            // 
            // cmbTransferTo
            // 
            this.cmbTransferTo.FormattingEnabled = true;
            this.cmbTransferTo.ItemHeight = 16;
            this.cmbTransferTo.Location = new System.Drawing.Point(938, 19);
            this.cmbTransferTo.Name = "cmbTransferTo";
            this.cmbTransferTo.Size = new System.Drawing.Size(329, 24);
            this.cmbTransferTo.TabIndex = 24;
            this.cmbTransferTo.SelectedValueChanged += new System.EventHandler(this.cmbTransferTo_SelectedValueChanged);
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(714, 19);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(216, 24);
            this.label22.TabIndex = 25;
            this.label22.Text = "Transfer To  :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTransferFrom
            // 
            this.cmbTransferFrom.FormattingEnabled = true;
            this.cmbTransferFrom.ItemHeight = 16;
            this.cmbTransferFrom.Location = new System.Drawing.Point(237, 15);
            this.cmbTransferFrom.Name = "cmbTransferFrom";
            this.cmbTransferFrom.Size = new System.Drawing.Size(329, 24);
            this.cmbTransferFrom.TabIndex = 22;
            this.cmbTransferFrom.SelectedValueChanged += new System.EventHandler(this.cmbTransferFrom_SelectedValueChanged);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(13, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(216, 24);
            this.label18.TabIndex = 23;
            this.label18.Text = "Transfer From  :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTransfer
            // 
            this.chkTransfer.AutoSize = true;
            this.chkTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTransfer.ForeColor = System.Drawing.Color.Maroon;
            this.chkTransfer.Location = new System.Drawing.Point(588, 272);
            this.chkTransfer.MaximumSize = new System.Drawing.Size(100, 100);
            this.chkTransfer.MinimumSize = new System.Drawing.Size(200, 30);
            this.chkTransfer.Name = "chkTransfer";
            this.chkTransfer.Size = new System.Drawing.Size(200, 30);
            this.chkTransfer.TabIndex = 21;
            this.chkTransfer.Text = "Is Transfer ?";
            this.chkTransfer.UseVisualStyleBackColor = true;
            this.chkTransfer.CheckedChanged += new System.EventHandler(this.chkTransfer_CheckedChanged);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(298, 529);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(200, 57);
            this.btnReport.TabIndex = 12;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(504, 529);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 57);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // hiddenWeightIDReport
            // 
            this.hiddenWeightIDReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hiddenWeightIDReport.Location = new System.Drawing.Point(18, 582);
            this.hiddenWeightIDReport.Name = "hiddenWeightIDReport";
            this.hiddenWeightIDReport.Size = new System.Drawing.Size(192, 24);
            this.hiddenWeightIDReport.TabIndex = 5;
            this.hiddenWeightIDReport.Text = "hiddenWeightIDReport";
            this.hiddenWeightIDReport.Visible = false;
            this.hiddenWeightIDReport.Click += new System.EventHandler(this.label7_Click);
            // 
            // txtHiddenWeightID
            // 
            this.txtHiddenWeightID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHiddenWeightID.Location = new System.Drawing.Point(18, 558);
            this.txtHiddenWeightID.Name = "txtHiddenWeightID";
            this.txtHiddenWeightID.Size = new System.Drawing.Size(214, 24);
            this.txtHiddenWeightID.TabIndex = 5;
            this.txtHiddenWeightID.Text = "hiddenWeightID";
            this.txtHiddenWeightID.Visible = false;
            this.txtHiddenWeightID.Click += new System.EventHandler(this.label7_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Crimson;
            this.button3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(710, 529);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 57);
            this.button3.TabIndex = 13;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtBuyerAddress
            // 
            this.txtBuyerAddress.Location = new System.Drawing.Point(947, 254);
            this.txtBuyerAddress.Multiline = true;
            this.txtBuyerAddress.Name = "txtBuyerAddress";
            this.txtBuyerAddress.ReadOnly = true;
            this.txtBuyerAddress.Size = new System.Drawing.Size(329, 58);
            this.txtBuyerAddress.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(698, 250);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(243, 24);
            this.label13.TabIndex = 6;
            this.label13.Text = "Buyer Address :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(696, 208);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(243, 24);
            this.label12.TabIndex = 4;
            this.label12.Text = "Buyer Code. :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTareWeightTime
            // 
            this.lblTareWeightTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTareWeightTime.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTareWeightTime.Location = new System.Drawing.Point(514, 457);
            this.lblTareWeightTime.Name = "lblTareWeightTime";
            this.lblTareWeightTime.Size = new System.Drawing.Size(425, 24);
            this.lblTareWeightTime.TabIndex = 4;
            this.lblTareWeightTime.Text = "Date and Time";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DarkRed;
            this.label19.Location = new System.Drawing.Point(964, 414);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(325, 83);
            this.label19.TabIndex = 4;
            this.label19.Text = "Out Weight = Unload Truck In Weight = Load Truck";
            // 
            // lblGrossWeightTime
            // 
            this.lblGrossWeightTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossWeightTime.ForeColor = System.Drawing.Color.DarkRed;
            this.lblGrossWeightTime.Location = new System.Drawing.Point(514, 418);
            this.lblGrossWeightTime.Name = "lblGrossWeightTime";
            this.lblGrossWeightTime.Size = new System.Drawing.Size(425, 24);
            this.lblGrossWeightTime.TabIndex = 4;
            this.lblGrossWeightTime.Text = "Date and Time";
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(18, 411);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(162, 24);
            this.label16.TabIndex = 4;
            this.label16.Text = "Gross Weight :";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(18, 453);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(162, 24);
            this.label15.TabIndex = 4;
            this.label15.Text = "Tare Weight :";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 24);
            this.label6.TabIndex = 4;
            this.label6.Text = "Truck No. :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(696, 168);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(243, 24);
            this.label11.TabIndex = 4;
            this.label11.Text = "Buyer Name :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Driver Name :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSellerCode
            // 
            this.txtSellerCode.Location = new System.Drawing.Point(945, 58);
            this.txtSellerCode.Multiline = true;
            this.txtSellerCode.Name = "txtSellerCode";
            this.txtSellerCode.ReadOnly = true;
            this.txtSellerCode.Size = new System.Drawing.Size(329, 34);
            this.txtSellerCode.TabIndex = 20;
            // 
            // txtBuyerCode
            // 
            this.txtBuyerCode.Location = new System.Drawing.Point(945, 208);
            this.txtBuyerCode.Multiline = true;
            this.txtBuyerCode.Name = "txtBuyerCode";
            this.txtBuyerCode.ReadOnly = true;
            this.txtBuyerCode.Size = new System.Drawing.Size(329, 34);
            this.txtBuyerCode.TabIndex = 20;
            // 
            // txtGrossWeight
            // 
            this.txtGrossWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrossWeight.Location = new System.Drawing.Point(179, 411);
            this.txtGrossWeight.Multiline = true;
            this.txtGrossWeight.Name = "txtGrossWeight";
            this.txtGrossWeight.Size = new System.Drawing.Size(329, 34);
            this.txtGrossWeight.TabIndex = 0;
            this.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTareWeight
            // 
            this.txtTareWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTareWeight.Location = new System.Drawing.Point(179, 453);
            this.txtTareWeight.Multiline = true;
            this.txtTareWeight.Name = "txtTareWeight";
            this.txtTareWeight.Size = new System.Drawing.Size(329, 34);
            this.txtTareWeight.TabIndex = 0;
            this.txtTareWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtChallanNo
            // 
            this.txtChallanNo.Location = new System.Drawing.Point(246, 268);
            this.txtChallanNo.Multiline = true;
            this.txtChallanNo.Name = "txtChallanNo";
            this.txtChallanNo.Size = new System.Drawing.Size(329, 34);
            this.txtChallanNo.TabIndex = 8;
            // 
            // txtTruckNo
            // 
            this.txtTruckNo.Location = new System.Drawing.Point(246, 227);
            this.txtTruckNo.Multiline = true;
            this.txtTruckNo.Name = "txtTruckNo";
            this.txtTruckNo.Size = new System.Drawing.Size(329, 34);
            this.txtTruckNo.TabIndex = 7;
            // 
            // txtDriverName
            // 
            this.txtDriverName.Location = new System.Drawing.Point(246, 184);
            this.txtDriverName.Multiline = true;
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(329, 37);
            this.txtDriverName.TabIndex = 6;
            // 
            // txtSellerAddress
            // 
            this.txtSellerAddress.Location = new System.Drawing.Point(945, 101);
            this.txtSellerAddress.Multiline = true;
            this.txtSellerAddress.Name = "txtSellerAddress";
            this.txtSellerAddress.ReadOnly = true;
            this.txtSellerAddress.Size = new System.Drawing.Size(329, 58);
            this.txtSellerAddress.TabIndex = 20;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(246, 142);
            this.txtQty.Multiline = true;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(329, 34);
            this.txtQty.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(696, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(243, 24);
            this.label10.TabIndex = 4;
            this.label10.Text = "Seller Address :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quantity :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(696, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(243, 24);
            this.label9.TabIndex = 4;
            this.label9.Text = "Seller Code :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Material Code :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(698, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(243, 24);
            this.label8.TabIndex = 4;
            this.label8.Text = "Seller Name :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Weight Type :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(4, 497);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1306, 24);
            this.label21.TabIndex = 4;
            this.label21.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------------------------------------" +
    "--------------------------";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(7, 375);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1305, 24);
            this.label20.TabIndex = 4;
            this.label20.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------------------------------------" +
    "-------------------------";
            // 
            // lblChallan
            // 
            this.lblChallan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChallan.Location = new System.Drawing.Point(23, 272);
            this.lblChallan.Name = "lblChallan";
            this.lblChallan.Size = new System.Drawing.Size(213, 24);
            this.lblChallan.TabIndex = 4;
            this.lblChallan.Text = "Challan No. :";
            this.lblChallan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Weight ID :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Location = new System.Drawing.Point(246, 103);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(329, 24);
            this.cmbMaterial.TabIndex = 4;
            this.cmbMaterial.TextChanged += new System.EventHandler(this.cmbMaterial_TextChanged);
            // 
            // cmbBuyer
            // 
            this.cmbBuyer.AccessibleRole = System.Windows.Forms.AccessibleRole.Application;
            this.cmbBuyer.FormattingEnabled = true;
            this.cmbBuyer.Location = new System.Drawing.Point(947, 169);
            this.cmbBuyer.Name = "cmbBuyer";
            this.cmbBuyer.Size = new System.Drawing.Size(329, 24);
            this.cmbBuyer.TabIndex = 10;
            this.cmbBuyer.SelectedIndexChanged += new System.EventHandler(this.cmbBuyer_SelectedIndexChanged);
            // 
            // cmbSeller
            // 
            this.cmbSeller.AccessibleRole = System.Windows.Forms.AccessibleRole.Application;
            this.cmbSeller.FormattingEnabled = true;
            this.cmbSeller.Location = new System.Drawing.Point(945, 13);
            this.cmbSeller.Name = "cmbSeller";
            this.cmbSeller.Size = new System.Drawing.Size(329, 24);
            this.cmbSeller.TabIndex = 9;
            this.cmbSeller.SelectedIndexChanged += new System.EventHandler(this.cmbSeller_SelectedIndexChanged);
            // 
            // cmbWeightType
            // 
            this.cmbWeightType.FormattingEnabled = true;
            this.cmbWeightType.Location = new System.Drawing.Point(246, 60);
            this.cmbWeightType.Name = "cmbWeightType";
            this.cmbWeightType.Size = new System.Drawing.Size(329, 24);
            this.cmbWeightType.TabIndex = 3;
            this.cmbWeightType.SelectedIndexChanged += new System.EventHandler(this.cmbWeightType_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Segoe UI Variable Text", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label14.Location = new System.Drawing.Point(553, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(196, 35);
            this.label14.TabIndex = 4;
            this.label14.Text = "Scale Weight :";
            // 
            // txtScaleWeight
            // 
            this.txtScaleWeight.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtScaleWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtScaleWeight.Font = new System.Drawing.Font("Times New Roman", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScaleWeight.ForeColor = System.Drawing.Color.White;
            this.txtScaleWeight.Location = new System.Drawing.Point(761, 12);
            this.txtScaleWeight.Name = "txtScaleWeight";
            this.txtScaleWeight.Size = new System.Drawing.Size(494, 75);
            this.txtScaleWeight.TabIndex = 9;
            this.txtScaleWeight.Text = "1200";
            this.txtScaleWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnRefresh.BackgroundImage = global::BridgeScaleDotNet.Properties.Resources.refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1268, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 75);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(-4, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "Operator :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(-4, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(158, 24);
            this.label17.TabIndex = 11;
            this.label17.Text = "Scale Name:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbScale
            // 
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.ItemHeight = 16;
            this.cmbScale.Location = new System.Drawing.Point(149, 57);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(329, 24);
            this.cmbScale.TabIndex = 1;
            this.cmbScale.SelectedIndexChanged += new System.EventHandler(this.cmbScale_SelectedIndexChanged);
            this.cmbScale.SelectedValueChanged += new System.EventHandler(this.cmbScale_SelectedValueChanged);
            // 
            // txtOperatorName
            // 
            this.txtOperatorName.Enabled = false;
            this.txtOperatorName.Location = new System.Drawing.Point(149, 29);
            this.txtOperatorName.Name = "txtOperatorName";
            this.txtOperatorName.ReadOnly = true;
            this.txtOperatorName.Size = new System.Drawing.Size(329, 22);
            this.txtOperatorName.TabIndex = 0;
            // 
            // FrmInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1399, 838);
            this.Controls.Add(this.txtOperatorName);
            this.Controls.Add(this.cmbScale);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtScaleWeight);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label14);
            this.Name = "FrmInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scale Information";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.transferPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbWeightID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbWeightType;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTruckNo;
        private System.Windows.Forms.TextBox txtDriverName;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBuyerAddress;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBuyerCode;
        private System.Windows.Forms.TextBox txtSellerAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblChallan;
        public System.Windows.Forms.ComboBox cmbSeller;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtGrossWeight;
        private System.Windows.Forms.TextBox txtTareWeight;
        private System.Windows.Forms.Label lblTareWeightTime;
        private System.Windows.Forms.Label lblGrossWeightTime;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.ComboBox cmbBuyer;
        private System.Windows.Forms.TextBox txtSellerCode;
        private System.Windows.Forms.TextBox txtChallanNo;
        private System.Windows.Forms.Label txtHiddenWeightID;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label hiddenWeightIDReport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label txtScaleWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.TextBox txtOperatorName;
        private System.Windows.Forms.CheckBox chkTransfer;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbTransferFrom;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbTransferTo;
        private System.Windows.Forms.GroupBox transferPanel;
        private System.Windows.Forms.Button btnSeller;
        private System.Windows.Forms.Button btnBuyer;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.Label lblSyncStatus;
    }
}