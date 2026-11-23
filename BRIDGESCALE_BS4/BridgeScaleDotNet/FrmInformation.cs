using BridgeScaleDotNet.Models;
using BridgeScaleDotNet.Services;
using BTLERP.Web.Report.Scale;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BridgeScaleDotNet
{
    public partial class FrmInformation : Form
    {
        // 🔐 এখানে নিজের আসল connection string বসাও
        //string connString = "Server=10.200.111.30;Database=BTLERP;User Id=sa;Password=pdLe@!p~~~23&&eb23;";
        string connString = $@"Server=(localdb)\MSSQLLocalDB;AttachDbFilename=D:\Data\BS4.mdf;Integrated Security=True;Connect Timeout=30;";


        DataTable dt;
        DataTable dt2;
        SerialPort serialPort1 = new SerialPort();
        int status = 2;
        //int isfirstWeight = 1;
        //string sql = string.Empty;
        int weightID = 0;
        string informationID = null;

        bool _isBindingScale = false;
        bool _isBindingWeightID = false;
        bool _isLoadingWeightInformation = false;

        double gWeight = 0;
        double tWeight = 0;
        string firstWeightType = null;

        DateTime? gw_time;
        DateTime? tw_time;
        bool exists;

        private DataTable _allWeightData;

        private DataTable _allMeterialCode;
        private bool _isBinding = false;


        private DataTable _allSellerData;
        private DataTable _allBuyerData;
        private DataTable _allMaterialData;
        private DataTable _allOwnTruckData;
        private bool _isLoadingOwnTrucks;
        private bool _isFilteringWeight = false;
        private bool _isFilteringSeller = false;
        private bool _isFilteringBuyer = false;
        private bool _isFilteringMaterial = false;
        private bool _isFilteringOwnTruck = false;


   
        string globalMessge = string.Empty;

        // Timer Code
      
        private Timer countdownTimer;

  
        private int remainingSeconds;
        private int _countDown = 300; // 5 minutes

        private Timer syncTimer;
        private int _syncSeconds = 1 * 60;
        private bool _isSyncRunning = false;

        private readonly SyncService _syncService = new SyncService();
        public FrmInformation()
        {
            

            InitializeComponent();
            //ApplyProfessionalStyle();
            Timer();
            informationID = null;

            // Serial Port basic config
            serialPort1.PortName = "COM1";
            serialPort1.BaudRate = 9600;
            serialPort1.Parity = Parity.None;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.ReadTimeout = 1000; // 1 second timeout

            txtOperatorName.Text = LocalStorage.UserName;

            Buyer_information_S();
            SellerInformation_S();
            WeightType("ALL");
            LoadMaterials();
            LoadOwnTrucks();
            GetOperatorBSName();

            txtHiddenWeightID.Text = null;

            // Search option enable for cmbWeightID
            cmbWeightID.DropDownStyle = ComboBoxStyle.DropDown;
            cmbWeightID.AutoCompleteMode = AutoCompleteMode.None;
            cmbWeightID.AutoCompleteSource = AutoCompleteSource.None;
            // Search option enable for cmbSeller
            cmbSeller.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSeller.AutoCompleteMode = AutoCompleteMode.None;
            cmbSeller.AutoCompleteSource = AutoCompleteSource.None;
            cmbSeller.TextUpdate += cmbSeller_TextUpdate;

            // Search option enable for cmbBuyer
            cmbBuyer.DropDownStyle = ComboBoxStyle.DropDown;
            cmbBuyer.AutoCompleteMode = AutoCompleteMode.None;
            cmbBuyer.AutoCompleteSource = AutoCompleteSource.None;
            cmbBuyer.TextUpdate += cmbBuyer_TextUpdate;


            // Search option enable for cmbMaterial
            cmbMaterial.DropDownStyle = ComboBoxStyle.DropDown;
            cmbMaterial.AutoCompleteMode = AutoCompleteMode.None;
            cmbMaterial.AutoCompleteSource = AutoCompleteSource.None;
            cmbMaterial.TextUpdate += cmbMaterial_TextUpdate;

            cmbOwnTruck.DropDownStyle = ComboBoxStyle.DropDown;
            cmbOwnTruck.AutoCompleteMode = AutoCompleteMode.None;
            cmbOwnTruck.AutoCompleteSource = AutoCompleteSource.None;
            cmbOwnTruck.DisplayMember = "TruckNo";
            cmbOwnTruck.ValueMember = "TruckID";
            cmbOwnTruck.TextUpdate += cmbOwnTruck_TextUpdate;
        }

        //private void ApplyProfessionalStyle()
        //{
        //    var pageBack = Color.FromArgb(236, 242, 249);
        //    var panelBack = Color.White;
        //    var labelColor = Color.FromArgb(51, 65, 85);
        //    var inputBack = Color.FromArgb(248, 250, 252);
        //    var readOnlyBack = Color.FromArgb(241, 245, 249);
        //    var borderColor = Color.FromArgb(203, 213, 225);
        //    var primary = Color.FromArgb(37, 99, 235);
        //    var success = Color.FromArgb(22, 163, 74);
        //    var danger = Color.FromArgb(220, 38, 38);
        //    var warning = Color.FromArgb(124, 45, 18);
        //    var sync = Color.FromArgb(79, 70, 229);

        //    BackColor = pageBack;
        //    Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        //    ClientSize = new Size(ClientSize.Width, 760);

        //    panel1.Location = new Point(panel1.Left, 108);
        //    panel1.Size = new Size(panel1.Width, 620);
        //    panel1.BackColor = panelBack;
        //    panel1.BorderStyle = BorderStyle.None;
        //    panel1.Padding = new Padding(8);
        //    panel1.Paint += (sender, e) =>
        //    {
        //        using (var pen = new Pen(borderColor))
        //        {
        //            e.Graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1);
        //        }
        //    };

        //    transferPanel.BackColor = panelBack;
        //    transferPanel.ForeColor = labelColor;
        //    transferPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        //    transferPanel.Height = 52;

        //    foreach (var label in new[]
        //    {
        //        label1, label2, label3, label4, label5, label6, label7, label8, label9,
        //        label10, label11, label12, label13, label15, label16, label17,
        //        label18, label22, lblChallan
        //    })
        //    {
        //        label.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        //        label.ForeColor = labelColor;
        //    }

        //    label14.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        //    label14.ForeColor = Color.FromArgb(15, 23, 42);

        //    label19.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        //    label19.ForeColor = warning;
        //    label19.Text = "Out Weight = Unload Truck\r\nIn Weight = Load Truck";

        //    foreach (var separator in new[] { label20, label21 })
        //    {
        //        separator.Text = "";
        //        separator.Height = 1;
        //        separator.BackColor = borderColor;
        //    }

        //    foreach (var combo in new[]
        //    {
        //        cmbScale, cmbWeightID, cmbWeightType, cmbMaterial, cmbSeller, cmbBuyer,
        //        cmbTransferFrom, cmbTransferTo
        //    })
        //    {
        //        combo.Font = new Font("Segoe UI", 11F);
        //        combo.BackColor = Color.White;
        //        combo.ForeColor = Color.FromArgb(15, 23, 42);
        //        combo.ItemHeight = 24;
        //        combo.Size = new Size(combo.Width, 34);
        //        combo.IntegralHeight = false;
        //    }

        //    foreach (var textBox in new[]
        //    {
        //        txtOperatorName, txtQty, txtDriverName, txtTruckNo, txtChallanNo,
        //        txtSellerName, txtSellerAddress, txtBuyerName, txtBuyerAddress
        //    })
        //    {
        //        textBox.Font = new Font("Segoe UI", 10F);
        //        textBox.BorderStyle = BorderStyle.FixedSingle;
        //        textBox.BackColor = textBox.ReadOnly ? readOnlyBack : inputBack;
        //        textBox.ForeColor = Color.FromArgb(15, 23, 42);
        //    }

        //    txtOperatorName.Enabled = true;
        //    txtOperatorName.ReadOnly = true;
        //    txtOperatorName.TabStop = false;
        //    txtOperatorName.BackColor = readOnlyBack;

        //    foreach (var weightTextBox in new[] { txtGrossWeight, txtTareWeight })
        //    {
        //        weightTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        //        weightTextBox.BorderStyle = BorderStyle.FixedSingle;
        //        weightTextBox.BackColor = Color.FromArgb(236, 253, 245);
        //        weightTextBox.ForeColor = Color.FromArgb(6, 78, 59);
        //    }

        //    foreach (var timeLabel in new[] { lblGrossWeightTime, lblTareWeightTime })
        //    {
        //        timeLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        //        timeLabel.ForeColor = Color.FromArgb(100, 116, 139);
        //    }

        //    txtScaleWeight.BackColor = Color.FromArgb(15, 23, 42);
        //    txtScaleWeight.ForeColor = Color.White;
        //    txtScaleWeight.BorderStyle = BorderStyle.None;
        //    txtScaleWeight.Font = new Font("Segoe UI", 38F, FontStyle.Bold);

        //    StyleButton(btnSave, success);
        //    StyleButton(btnReport, primary);
        //    StyleButton(button3, danger);
        //    StyleButton(btnSync, sync);
        //    StyleButton(btnSeller, primary);
        //    StyleButton(btnBuyer, primary);
        //    StyleButton(btnMaterial, primary);

        //    btnReport.Location = new Point(298, 510);
        //    btnSave.Location = new Point(504, 510);
        //    button3.Location = new Point(710, 510);
        //    btnSync.Location = new Point(915, 510);
        //    btnReport.Size = new Size(200, 48);
        //    btnSave.Size = new Size(200, 48);
        //    button3.Size = new Size(200, 48);
        //    btnSync.Size = new Size(200, 48);

        //    btnSeller.Text = "+";
        //    btnBuyer.Text = "+";
        //    btnSeller.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        //    btnBuyer.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        //    btnMaterial.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

        //    btnRefresh.FlatStyle = FlatStyle.Flat;
        //    btnRefresh.FlatAppearance.BorderSize = 0;
        //    btnRefresh.BackColor = Color.FromArgb(226, 232, 240);

        //    chkTransfer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        //    chkTransfer.ForeColor = Color.FromArgb(127, 29, 29);
        //    chkTransfer.BackColor = panelBack;

        //    textMessage.BorderStyle = BorderStyle.FixedSingle;
        //    textMessage.BackColor = Color.FromArgb(255, 251, 235);
        //    textMessage.ForeColor = Color.FromArgb(146, 64, 14);
        //    textMessage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        //    lblSyncStatus.AutoSize = false;
        //    lblSyncStatus.Location = new Point(8, 565);
        //    lblSyncStatus.Size = new Size(420, 22);

        //    textMessage.Location = new Point(8, 588);
        //    textMessage.Size = new Size(1300, 26);

        //    lblSyncStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        //    lblSyncStatus.ForeColor = Color.FromArgb(79, 70, 229);

        //    btnRefresh.Size = new Size(56, 56);
        //    btnRefresh.Location = new Point(1286, 20);
        //    btnRefresh.Cursor = Cursors.Hand;
        //}

        //private void StyleButton(System.Windows.Forms.Button button, Color backColor)
        //{
        //    button.FlatStyle = FlatStyle.Flat;
        //    button.FlatAppearance.BorderSize = 0;
        //    button.BackColor = backColor;
        //    button.ForeColor = Color.White;
        //    button.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        //    button.Cursor = Cursors.Hand;
        //}

        private void Timer()
        {
            syncTimer = new Timer();
            syncTimer.Interval = 1000;
            _syncSeconds = 300;
            syncTimer.Start();
            lblSyncStatus.Text = "Data sync every 5 minutes";
            
        }
        private async void syncTimer_Tick(object sender, EventArgs e)
        {
            
            _syncSeconds--;

            lblSyncStatus.Text = $"Next sync in {_syncSeconds / 60:00}:{_syncSeconds % 60:00}";

            if (_syncSeconds <= 0)
            {
                await RunSyncAsync();
            }
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
                remainingSeconds--;
            ShowCountdown();
        }

        private void ShowCountdown()
        {
            TimeSpan time = TimeSpan.FromSeconds(remainingSeconds);

            if (lblSyncStatus != null)
            {
                lblSyncStatus.Text = $"Next Sync: {time.Minutes:D2}:{time.Seconds:D2}";
            }
        }


        private async void btnSync_Click(object sender, EventArgs e)
        {

            if (_isSyncRunning) return;

            try
            {
                _isSyncRunning = true;
                syncTimer.Stop();

                await RunSyncAsync(); // your sync method

                // reset countdown after sync complete
                _countDown = 300;
                lblSyncStatus.Text = "Next sync in ...";

                syncTimer.Start();
            }
            catch (Exception ex)
            {
                lblSyncStatus.Text = "Sync failed: " + ex.Message;

                _countDown = 300;
                syncTimer.Start();
            }
            finally
            {
                _isSyncRunning = false;
            }
        }

        private async Task RunSyncAsync()
        {
            btnSync.Enabled = false;
            string message = string.Empty;

            try
            {
                message = await _syncService.SyncScaleInformationAsync();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sync failed: " + ex.Message);
            }
            finally
            {
                btnSync.Enabled = true;
            }

            textMessage.Text = message;
        }

        //private async void syncTimer_Tick(object sender, EventArgs e)
        //{
        //    await RunSyncAsync(true);
        //}

        #region Master Load

        public void GetOperatorBSName()
        {
            _isBindingScale = true;

            string query = $@"select a.CounterID, BSName = b.ScaleName 
                            from Scale_Operator_ScalePermission a
                            left join Scale_Counter b on a.CounterID = b.CounterID
                            left join Scale_Operator o on a.OperatorID = o.OperatorID
                            Where isnull(b.IsActive,0) = 1 and o.Name =  '{LocalStorage.UserName}' and a.CounterID = '{LocalStorage.CounterID}'";

            dt = DbContext.Execute(query);

            cmbScale.DataSource = null;
            cmbScale.Items.Clear();
            cmbScale.DisplayMember = "BSName";
            cmbScale.ValueMember = "CounterID";
            cmbScale.DataSource = dt;

            // 🧠 VERY IMPORTANT — force binding to complete
            cmbScale.BindingContext = new BindingContext();

            //cmbScale.SelectedValue = dt.Rows[0]["CounterID"];

            if (AppSession.SelectedCounterID > 0)
            {
                cmbScale.SelectedValue = AppSession.SelectedCounterID;
            }

            GetWeightID();

            _isBindingScale = false;
        }

        private void cmbScale_SelectedValueChanged(object sender, EventArgs e)
        {

            // Binding চলাকালীন ignore করো
            if (_isBindingScale) return;

            if (cmbScale.SelectedValue == null ||
                string.IsNullOrWhiteSpace(cmbScale.SelectedValue.ToString()))
            {
                return;
            }

            //LocalStorage.CounterID = (int)cmbScale.SelectedValue;

            if (cmbScale.SelectedValue != null)
            {
                AppSession.SelectedCounterID = Convert.ToInt32(cmbScale.SelectedValue);
                AppSession.SelectedCounterName = cmbScale.Text;
            }

            GetWeightID();
        }

        private void cmbScale_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void WeightType(string loadType)
        {
            var items = new List<ComboItem>
            {
                //new ComboItem { Value = 0, Text = "-- Select Type --" }
            };

            // loadType অনুযায়ী অপশন যোগ করা
            switch ((loadType ?? "").Trim().ToUpperInvariant())
            {
                case "IN":
                    items.Add(new ComboItem { Value = 1, Text = "IN WEIGHT" });
                    break;

                case "OUT":
                    items.Add(new ComboItem { Value = 2, Text = "OUT WEIGHT" });
                    break;

                case "ALL":
                default:
                    items.Add(new ComboItem { Value = 0, Text = "-- Select Type --" });
                    items.Add(new ComboItem { Value = 1, Text = "IN WEIGHT" });
                    items.Add(new ComboItem { Value = 2, Text = "OUT WEIGHT" });
                    break;
            }

            cmbWeightType.DisplayMember = "Text";
            cmbWeightType.ValueMember = "Value";
            cmbWeightType.DataSource = items;

            cmbWeightType.SelectedValue = 0;
        }


        #endregion

        public void GetWeightID2()
        {
            if (cmbScale.SelectedValue == null)
                return;

            string counterID = cmbScale.SelectedValue.ToString();

            string query = $@"
                SELECT TOP 500 weight_id, BSNameWithID, EntryDate 
                FROM Scale_Information with (nolock)
                WHERE CounterID = '{counterID}' 
                  AND EntryDate >= DATEADD(DAY, -8, CAST(GETDATE() AS DATE)) 
                ORDER BY weight_id DESC";

            if (LocalStorage.IsFirstWeight == 1)
            {
                query = $@"SELECT BSNameWithID =  a.ScaleName + cast(ISNULL(MAX(weight_id), 115000) + 1 as varchar(150))
                        ,weight_id = ISNULL(MAX(weight_id), 115000) + 1
                        FROM Scale_Counter  a
                        left join Scale_Information b with (nolock) on a.CounterID = b.CounterID
                        WHERE a.CounterID = {counterID}
                        group by a.ScaleName";
            }

            dt = DbContext.Execute(query);

            _isBindingWeightID = true;
            try
            {
                cmbWeightID.DataSource = null;
                cmbWeightID.Items.Clear();
                cmbWeightID.DisplayMember = "BSNameWithID";
                cmbWeightID.ValueMember = "weight_id";
                cmbWeightID.DataSource = dt;

                txtHiddenWeightID.Text = null;

                if (LocalStorage.IsFirstWeight == 1 && dt.Rows.Count > 0)
                {
                    txtHiddenWeightID.Text = dt.Rows[0]["weight_id"].ToString();
                }
                else
                {
                    cmbWeightID.SelectedIndex = -1;
                    cmbWeightID.Text = "";
                }
            }
            finally
            {
                _isBindingWeightID = false;
            }
        }
        public void GetWeightID()
        {
            if (cmbScale.SelectedValue == null)
                return;

            string counterID = Convert.ToString(cmbScale.SelectedValue);

            string query = $@"
            SELECT top 1000 weight_id, BSNameWithID=BSName+''+convert(varchar(100),weight_id), EntryDate 
            FROM Scale_Information with (nolock)
            WHERE CounterID = '{counterID}' 
             AND EntryDate >= DATEADD(DAY, -8, CAST(GETDATE() AS DATE)) 
            ORDER BY EntryDate DESC";

            if (LocalStorage.IsFirstWeight == 1)
            {
                query = $@"
            SELECT 
                BSNameWithID = a.ScaleName + CAST(ISNULL(MAX(weight_id), 115000) + 1 AS varchar(150)),
                weight_id = ISNULL(MAX(weight_id), 115000) + 1
            FROM Scale_Counter a
            LEFT JOIN Scale_Information b with (nolock) 
                ON a.CounterID = b.CounterID
            WHERE a.CounterID = {counterID}
            GROUP BY a.ScaleName";
            }

            dt = DbContext.Execute(query);

            _allWeightData = dt != null ? dt.Copy() : null;

            _isBindingWeightID = true;
            try
            {
                cmbWeightID.DataSource = null;
                cmbWeightID.Items.Clear();
                cmbWeightID.DisplayMember = "BSNameWithID";
                cmbWeightID.ValueMember = "weight_id";
                cmbWeightID.DataSource = dt;

                txtHiddenWeightID.Text = null;

                if (LocalStorage.IsFirstWeight == 1 && dt != null && dt.Rows.Count > 0)
                {
                    txtHiddenWeightID.Text = Convert.ToString(dt.Rows[0]["weight_id"]);
                }
                else
                {
                    cmbWeightID.SelectedIndex = -1;
                    cmbWeightID.Text = "";
                }
            }
            finally
            {
                _isBindingWeightID = false;
            }
        }

        public string GetLastIdentityValue()
        {
            string counterID = cmbScale.SelectedValue.ToString();
            string query = $"SELECT InformationID = MAX(InformationID) FROM Scale_Information with (nolock) WHERE CounterID = '{counterID}'";

            dt = DbContext.Execute(query);
            if (dt.Rows.Count > 0)
            {
                informationID = dt.Rows[0]["InformationID"].ToString();
            }
            return informationID;
        }

        #region Events   

        private void cmbWeightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingWeightInformation) return;

            //LocalStorage.IsFirstWeight = 0;

            if (LocalStorage.IsFirstWeight == 1)
            {
                status = 2;
                // added 19 aug26
                gWeight = 0;
                tWeight = 0;
            }
            else
            {
                status = 1;
            }

            if (!string.IsNullOrWhiteSpace(cmbWeightType.Text))
            {
                // challan textbox দেখাও
                txtChallanNo.Visible = true;

                string informationID = txtHiddenWeightID.Text;

                // Scale থেকে weight নাও
                ReadScaleData();

                if (status == 2)
                {
                    if (cmbWeightType.Text == "IN WEIGHT")
                    {
                        if (gWeight == 0)
                        {
                            txtGrossWeight.Text = "";
                            txtTareWeight.Text = "";
                            txtGrossWeight.Text = txtScaleWeight.Text;
                            btnSave.Text = "Gross Weight";
                        }

                    }
                    else if (cmbWeightType.Text == "OUT WEIGHT")
                    {
                        if (tWeight == 0)
                        {
                            txtTareWeight.Text = "";
                            txtGrossWeight.Text = "";
                            txtTareWeight.Text = txtScaleWeight.Text;
                            btnSave.Text = "Tare Weight";
                        }
                        else
                        {
                            MessageBox.Show("Tare Weight Already Taken !");
                        }

                    }
                    else
                    {
                        txtGrossWeight.Text = "";
                        txtTareWeight.Text = "";
                        btnSave.Text = "Save";
                    }
                }

                if (status == 1)
                {
                    if (cmbWeightType.Text == "IN WEIGHT")
                    {
                        txtTareWeight.Text = "";
                        txtTareWeight.Text = txtScaleWeight.Text;
                    }
                    else
                    {
                        txtGrossWeight.Text = "";
                        txtGrossWeight.Text = txtScaleWeight.Text;
                    }
                }

                // Focus material এ
                cmbMaterial.Focus();
            }
        }

        private void cmbWeightID_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_isBindingWeightID || _isFilteringWeight) return;

            string weightTypeID = "";

            // প্রথমে weightID clear করো
            weightID = 0;
            txtHiddenWeightID.Text = string.Empty;

            if (LocalStorage.IsFirstWeight == 0)
            {
                // পুরনো weight থেকে load করার সময়
                if (cmbWeightID.SelectedValue == null || !int.TryParse(cmbWeightID.SelectedValue.ToString(), out weightID))
                {
                    return; // valid weightID নাই
                }
            }
            else
            {
                // first weight হলে hidden textbox থেকেই নেব (সাধারণত এখানে আসার কথা না, তবু রাখা)
                if (!int.TryParse(txtHiddenWeightID.Text, out weightID))
                {
                    return;
                }
            }

            if (weightID <= 0)
                return;

            txtHiddenWeightID.Text = weightID.ToString();
            ResetForm();

            //string bsName = cmbScale.SelectedValue.ToString();
            //string queryInfo = $"SELECT * FROM Scale_Information WHERE weight_id = @weightID AND BSName = '{bsName}'";

            string counterID = cmbScale.SelectedValue.ToString();
            string queryInfo = $"SELECT * FROM Scale_Information with (nolock) WHERE EntryDate >= DATEADD(DAY, -8, CAST(GETDATE() AS DATE)) and weight_id = @weightID AND CounterID = '{counterID}' ";
            string queryClient = "SELECT * FROM Scale_Clint WHERE clint_code = @clintCode";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryInfo, conn))
                {
                    cmd.Parameters.AddWithValue("@weightID", weightID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtInfo = new DataTable();
                    da.Fill(dtInfo);

                    if (dtInfo.Rows.Count > 0)
                    {
                        DataRow r = dtInfo.Rows[0];

                        bool hasGrossWeight = false;
                        bool hasTareWeight = false;

                        _isLoadingWeightInformation = true;
                        try
                        {
                            hiddenWeightIDReport.Text = r["InformationID"].ToString();

                            // Seller / Buyer
                            if (r["ClientID_Seller"] != DBNull.Value)
                            {
                                cmbSeller.SelectedValue = (int)r["ClientID_Seller"];
                            }
                            else
                            {
                                cmbSeller.SelectedValue = 0;
                                cmbSeller.Text = r["SellerName"].ToString();
                                txtSellerName.Text = r["SellerName"].ToString();
                            }


                            if (r["ClientID_Buyer"] != DBNull.Value)
                            {
                                cmbBuyer.SelectedValue = (int)r["ClientID_Buyer"];
                            }
                            else
                            {
                                cmbBuyer.SelectedValue = 0;
                                cmbBuyer.Text = r["BuyerName"].ToString();
                                txtBuyerName.Text = r["BuyerName"].ToString();
                            }


                            // Weight type
                            //weightTypeID = SafeValue(r["weight_type"]);
                            weightTypeID = SafeValue(r["weight_typeID"]);
                            cmbWeightType.SelectedValue = (weightTypeID == "1") ? 2 : 1;


                            LoadMaterials();

                            // Material
                            if (!string.IsNullOrEmpty(SafeValue(r["matrial_code"])))
                            {
                                cmbMaterial.DroppedDown = false;
                                cmbMaterial.SelectedValue = SafeValue(r["matrial_code"]);
                                cmbMaterial.DroppedDown = false;
                            }

                            // Counter
                            if (!string.IsNullOrEmpty(SafeValue(r["CounterID"])))
                            {
                                cmbScale.SelectedValue = SafeValue(r["CounterID"]);
                            }

                            txtQty.Text = SafeValue(r["quantity"]);

                            // Challan
                            if (!string.IsNullOrEmpty(r["challan"]?.ToString()))
                            {
                                lblChallan.Visible = true;
                                txtChallanNo.Visible = true;
                                txtChallanNo.Text = r["challan"].ToString();
                            }

                            // Operator / Driver / Truck Info
                            txtDriverName.Text = r["driver_name"]?.ToString();
                            txtTruckNo.Text = r["truck_no"]?.ToString();

                            // Weight values from database get priority. Live scale fills only missing values.
                            string grossWeightFromDb = SafeValue(r["g_w_unite"]);
                            string tareWeightFromDb = SafeValue(r["t_w_unite"]);
                            hasGrossWeight = TryGetValidWeight(grossWeightFromDb, out gWeight);
                            hasTareWeight = TryGetValidWeight(tareWeightFromDb, out tWeight);

                            txtGrossWeight.Text = hasGrossWeight ? grossWeightFromDb : "";
                            txtTareWeight.Text = hasTareWeight ? tareWeightFromDb : "";

                            if (hasGrossWeight && hasTareWeight)
                                status = 0;

                            // Date/Time labels
                            lblGrossWeightTime.Text = "Date and Time";
                            gw_time = null;
                            tw_time = null;
                            if (r["g_w_time"] != DBNull.Value)
                            {
                                var dtGross = Convert.ToDateTime(r["g_w_time"]);
                                lblGrossWeightTime.Text = dtGross.ToString("dd-MMM-yyyy  HH:mm tt");
                                gw_time = dtGross;
                            }

                            lblTareWeightTime.Text = "Date and Time";
                            if (r["t_w_time"] != DBNull.Value)
                            {
                                var dtTare = Convert.ToDateTime(r["t_w_time"]);
                                lblTareWeightTime.Text = dtTare.ToString("dd-MMM-yyyy  HH:mm tt");
                                tw_time = dtTare;
                            }

                            firstWeightType = SafeValue(r["weight_type"]);



                            // Client table থেকে আবার name load করতে চাইলে (তোমার আগের logic)
                            using (SqlCommand cmdClient = new SqlCommand(queryClient, conn))
                            {
                                cmdClient.Parameters.AddWithValue("@clintCode", cmbSeller.Text);
                                SqlDataAdapter daClient = new SqlDataAdapter(cmdClient);
                                DataTable dtClient = new DataTable();
                                daClient.Fill(dtClient);

                                if (dtClient.Rows.Count > 0)
                                {
                                    // NOTE: এখানে আসল কলাম নাম অনুযায়ী ঠিক করে নিতে হবে
                                    // cmbSeller.SelectedValue = dtClient.Rows[0]["ClientID"]?.ToString();
                                }
                            }

                            if (firstWeightType == "IN WEIGHT")
                            {
                                WeightType("IN");
                                cmbWeightType.SelectedValue = 1;
                            }

                            if (firstWeightType == "OUT WEIGHT")
                            {
                                WeightType("OUT");
                                cmbWeightType.SelectedValue = 2;
                            }
                        }
                        finally
                        {
                            _isLoadingWeightInformation = false;
                            BeginInvoke(new Action(() => cmbMaterial.DroppedDown = false));
                        }

                        // Live scale weight update (optional)
                        if (!string.IsNullOrEmpty(cmbWeightType.Text))
                        {
                            if (status == 1 || status == 2)
                            {
                                if (cmbWeightType.Text == "IN WEIGHT" && !hasTareWeight)
                                    txtTareWeight.Text = txtScaleWeight.Text;
                                else if (cmbWeightType.Text != "IN WEIGHT" && !hasGrossWeight)
                                    txtGrossWeight.Text = txtScaleWeight.Text;
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!ValidateForm()) return;

            string bsName = cmbScale.Text;
            string bSNameWithID = cmbWeightID.Text;

            //string sellerName = cmbSeller.Text;
            //string buyerName = cmbBuyer.Text;

            string sellerName = txtSellerName.Text;
            string buyerName = txtBuyerName.Text;

            int counterID = (int)cmbScale.SelectedValue;


            if (!int.TryParse(txtHiddenWeightID.Text, out int weightId))
            {
                MessageBox.Show("Invalid Weight ID.");
                return;
            }

            string sqlCheck = $"SELECT * FROM Scale_Information with (nolock) WHERE weight_id = {weightId} AND CounterID = '{counterID}'";
            DataTable dtInfo = DbContext.Execute(sqlCheck);
            exists = dtInfo.Rows.Count > 0;

            var gWeight = txtGrossWeight.Text;
            var tWeight = txtTareWeight.Text;

            bool validTransferData = true;

            if (chkTransfer.Checked && (cmbTransferFrom.SelectedIndex < 1 & cmbTransferTo.SelectedIndex < 1))
            {
                validTransferData = false;
                MessageBox.Show("Transfer From & Transfer To Invalid", "Invalid Transfer Info!");
            }

            if (validTransferData)
            {
                if (!exists)
                {
                    InsertScaleInformation(weightId, bsName, bSNameWithID, counterID, sellerName, buyerName);
                    GetWeightID();

                    // To Last Value to open report
                    informationID = GetLastIdentityValue();
                    hiddenWeightIDReport.Text = informationID;
                }
                else
                {
                    UpdateScaleInformation(weightId, bsName, bSNameWithID, counterID);
                }

                informationID = hiddenWeightIDReport.Text;



                // ---------- SAVE TO Scale_Information1 TABLE ----------
                //DbContext.Exec(
                //    @"INSERT INTO Scale_Information1 (InformationID, weight_id, material_code, g_w_u)
                //  VALUES (@InformationID, @weight_id, @matrial_code, @g_w_unite)",
                //    new Dictionary<string, object>
                //    {
                //    { "@InformationID", informationID },
                //    { "@weight_id", weightId },
                //    { "@matrial_code", cmbMaterial.SelectedValue },
                //    { "@g_w_unite", txtGrossWeight.Text }
                //    });

                //MessageBox.Show("Data Saved Successfully!");



                ResetForm();

                if (!String.IsNullOrEmpty(informationID))
                {
                    GetReport(informationID);
                }


                showMainForm();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(informationID))
            {
                GetReport(informationID);
            }
            else
            {
                MessageBox.Show("Please First Save");
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReadScaleData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showMainForm();
        }

        private void showMainForm()
        {
            this.Hide();
            FrmMain frmMain = new FrmMain();
            frmMain.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e) { }
        private void label7_Click_1(object sender, EventArgs e) { }

        #endregion

        #region Scale Read

        private void ReadScaleData()
        {
            try
            {
                var ports = SerialPort.GetPortNames();
                if (ports.Contains(serialPort1.PortName, StringComparer.OrdinalIgnoreCase))
                {
                    if (!serialPort1.IsOpen)
                        serialPort1.Open();

                    // পুরানো data clear
                    serialPort1.DiscardInBuffer();

                    // Scale কে একটু time দাও
                    System.Threading.Thread.Sleep(300);

                    string inString = serialPort1.ReadExisting();

                    if (!string.IsNullOrWhiteSpace(inString))
                    {
                        int x = inString.IndexOf("+");

                        if (x >= 0 && inString.Length >= x + 7)
                        {
                            string weightStr = inString.Substring(x, 7).Trim();

                            if (double.TryParse(weightStr, out double weightValue))
                            {
                                txtScaleWeight.Text = weightValue.ToString("0.###");
                                // ❌ আর এখানে cmbWeightType_SelectedIndexChanged() কল করছি না
                            }
                            else
                            {
                                txtScaleWeight.Text = weightStr;
                            }
                        }
                        else
                        {
                            txtScaleWeight.Text = "0";
                        }
                    }
                    else
                    {
                        txtScaleWeight.Text = "0";
                    }
                }
                else
                {
                    MessageBox.Show(serialPort1.PortName + " PORT NOT FOUND");
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Read timeout while reading scale data.");
                txtScaleWeight.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading COM port: " + ex.Message);
                txtScaleWeight.Text = "0";
            }
            finally
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
            }
        }

        #endregion

        #region DB Save Helpers

        public string SafeValue(object val)
        {
            return val == DBNull.Value ? "0" : val.ToString();
        }

        private bool TryGetValidWeight(string value, out double weight)
        {
            return double.TryParse(value, out weight) && weight != 0;
        }

        private void InsertScaleInformation(int weightId, string bsName, string bSNameWithID, int counterID, string sellerName, string buyerName)
        {
            string sql = @"
                INSERT INTO Scale_Information
                (CounterID, BSNameWithID, BSName, date_c, weight_id, weight_type, quantity,
                 operator_name, driver_name, truck_no, challan, ClientID_Seller,
                 ClientID_Buyer, status, g_w_time, t_w_time, matrial_code, matrial_name, g_w_unite, t_w_unite, TransferFrom, TransferTo, SellerName, BuyerName)
                VALUES
                (@CounterID, @BSNameWithID, @BSName, @date_c, @weight_id, @weight_type, @quantity,
                 @operator_name, @driver_name, @truck_no, @challan, @ClientID_Seller,
                 @ClientID_Buyer, @status, @g_w_time, @t_w_time, @matrial_code, @matrial_name, @g_w_unite, @t_w_unite, @TransferFrom, @TransferTo, @SellerName, @BuyerName)";

            DbContext.Exec(sql, BuildParameters(weightId, bsName, bSNameWithID, counterID));
        }

        private void UpdateScaleInformation(int weightId, string bsName, string bSNameWithID, int counterID)
        {
            string sql = @"
                UPDATE Scale_Information SET
                    SecondWeightDate = @SecondWeightDate,
                    weight_typeID    = @weight_typeID,
                    weight_type      = @weight_type,
                    matrial_code     = @matrial_code,
                    matrial_name     = @matrial_name,
                    quantity         = @quantity,
                    operator_name    = @operator_name,
                    driver_name      = @driver_name,
                    truck_no         = @truck_no,
                    challan          = @challan,
                    ClientID_Seller  = @ClientID_Seller,
                    ClientID_Buyer   = @ClientID_Buyer,
                    g_w_unite        = @g_w_unite,
                    t_w_unite        = @t_w_unite,
                    status           = @status,
                    g_w_time         = @g_w_time,
                    t_w_time         = @t_w_time,
                    TransferFrom     = @TransferFrom,
                    TransferTo       = @TransferTo,
                    SellerName       = @SellerName,
                    BuyerName        = @BuyerName,
                    IsSyncDataToServer = @IsSyncDataToServer
                    WHERE weight_id      = @weight_id AND CounterID = @CounterID";

            DbContext.Exec(sql, BuildParameters(weightId, bsName, bSNameWithID, counterID));
        }

        private Dictionary<string, object> BuildParameters(int weightId, string bsName, string bSNameWithID, int counterID)
        {

            string operatorName = txtOperatorName.Text;

            string gw_unit = txtGrossWeight.Text; // "IN WEIGHT"
            string tw_unit = txtTareWeight.Text; // "IN WEIGHT"

            //if (!exists)

            if ((gw_time == null) && gw_unit != "")
            {
                //gw_time = DateTime.Now.ToString("dd-MM-yyyy:hh mm ss");
                gw_time = DateTime.Now;
            }

            if ((tw_time == null) && tw_unit != "")
            {
                tw_time = DateTime.Now;
            }

            return new Dictionary<string, object>
            {
                //{ "@InformationID", informationID },
                { "@CounterID", counterID },
                { "@BSNameWithID", bSNameWithID },
                { "@BSName", bsName },
                { "@date_c", DateTime.Now },
                { "@SecondWeightDate", DateTime.Now },
                { "@weight_id", weightId },
                { "@weight_typeID", cmbWeightType.SelectedValue },
                { "@weight_type", cmbWeightType.Text },
                { "@matrial_code", cmbMaterial.SelectedValue },
                { "@matrial_name", cmbMaterial.Text },
                { "@quantity", txtQty.Text },
                { "@operator_name", operatorName },
                { "@driver_name", txtDriverName.Text },
                { "@truck_no", txtTruckNo.Text },
                { "@challan", txtChallanNo.Text },
                { "@ClientID_Seller", cmbSeller.SelectedValue },
                { "@ClientID_Buyer", cmbBuyer.SelectedValue },
                { "@g_w_unite", txtGrossWeight.Text },
                { "@t_w_unite", txtTareWeight.Text },
                { "@status", cmbWeightType.Text == "IN WEIGHT" ? "I" : "O" },
                { "@g_w_time", gw_time },
                { "@t_w_time", tw_time },
                { "@TransferFrom",  cmbTransferFrom.SelectedValue },
                { "@TransferTo", cmbTransferTo.SelectedValue },
                { "@BuyerName", txtBuyerName.Text },
                { "@SellerName", txtSellerName.Text },
                { "@IsSyncDataToServer", 0 },
            };
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtHiddenWeightID.Text))
            {
                MessageBox.Show("Please Enter Weight ID.");
                cmbWeightID.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbWeightType.Text))
            {
                MessageBox.Show("Please Enter Weight Type.");
                cmbWeightType.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbMaterial.Text))
            {
                MessageBox.Show("Please Enter Material Code.");
                cmbMaterial.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTruckNo.Text))
            {
                MessageBox.Show("Please Enter Truck No.");
                txtTruckNo.Focus();
                return false;
            }
            return true;
        }

        private void ResetForm()
        {
            txtQty.Text = "";
            txtDriverName.Text = "";
            txtTruckNo.Text = "";
            txtGrossWeight.Text = "";
            txtTareWeight.Text = "";
            txtChallanNo.Text = "";
            cmbMaterial.SelectedValue = 0;
            cmbBuyer.SelectedValue = 0;
            cmbSeller.SelectedValue = 0;

            cmbBuyer.Text = "";
            cmbSeller.Text = "";

            cmbTransferFrom.SelectedValue = 0;
            cmbTransferTo.SelectedValue = 0;
            cmbWeightType.Focus();
        }

        #endregion

        #region Report

        public void GetReport2()
        {
            string bsName = cmbScale.SelectedValue.ToString();

            if (!string.IsNullOrWhiteSpace(hiddenWeightIDReport.Text))
            {
                informationID = hiddenWeightIDReport.Text;
            }
            else
            {
                MessageBox.Show("Please enter or select a weight ID.", "Scale Report",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string url = "https://btlerp.badshatex.com/RptScale/ScaleReport?informationID=" +
                         informationID + "&bsName=" + bsName;

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the report.\n" + ex.Message);
            }
        }

        public void GetReport(string infoID)
        {
            string bsName = cmbScale.SelectedValue.ToString();

            var infoIDStr = infoID.ToString();


            if (!string.IsNullOrWhiteSpace(infoIDStr))
            {
                informationID = infoIDStr;
            }
            else
            {
                MessageBox.Show("Please enter or select a weight ID.", "Scale Report",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                rptScaleInfoSub report = new rptScaleInfoSub();

                report.Parameters["InformationID"].Value = informationID;
                report.Parameters["InformationID"].Visible = false;

                report.RequestParameters = false;

                report.CreateDocument();

                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the report.\n" + ex.Message);
            }
        }        

        #endregion


        private void cmbSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSeller.SelectedValue != null &&
                int.TryParse(Convert.ToString(cmbSeller.SelectedValue), out int sellerId) &&
                sellerId > 0)
            {
                string query = $"SELECT * FROM Scale_Clint WHERE ClientTypeID = 1 AND ISNULL(IsDelete, 0) = 0 and ClientID = '{sellerId}'";
                dt = DbContext.Execute(query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtSellerName.Text = dt.Rows[0]["C_name"] == DBNull.Value
                        ? ""
                        : dt.Rows[0]["C_name"].ToString();

                    txtSellerAddress.Text = dt.Rows[0]["Address"] == DBNull.Value
                        ? ""
                        : dt.Rows[0]["Address"].ToString();
                }
                else
                {
                    txtSellerName.Text = "";
                    txtSellerAddress.Text = "";
                }
            }
            else
            {
                txtSellerName.Text = "";
                txtSellerAddress.Text = "";
            }
        }

        private void cmbBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBuyer.SelectedValue != null &&
                int.TryParse(Convert.ToString(cmbBuyer.SelectedValue), out int buyerId) &&
                buyerId > 0)
            {
                string query = $"SELECT * FROM Scale_Clint WHERE ClientTypeID = 2 AND ISNULL(IsDelete, 0) = 0 AND ClientID = '{buyerId}'";
                dt = DbContext.Execute(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtBuyerName.Text = Convert.ToString(row["C_name"]);
                    txtBuyerAddress.Text = Convert.ToString(row["Address"]);
                }
                else
                {
                    txtBuyerName.Text = string.Empty;
                    txtBuyerAddress.Text = string.Empty;
                }
            }
            else
            {
                txtBuyerName.Text = string.Empty;
                txtBuyerAddress.Text = string.Empty;
            }
        }
        private void FillTransferCombo()
        {
            string query = "SELECT * FROM Scale_Counter WHERE ISNULL(IsActive, 0) = 1";
            dt = DbContext.Execute(query);
            dt2 = DbContext.Execute(query);

            //Fill Transfer From
            cmbTransferFrom.DataSource = null;
            cmbTransferFrom.Items.Clear();
            cmbTransferFrom.ValueMember = "CounterID";
            cmbTransferFrom.DisplayMember = "UnitName";
            cmbTransferFrom.DataSource = dt;
            cmbTransferFrom.SelectedValue = 0;
            cmbTransferFrom.SelectedText = "";

            //Fill Transfer To
            cmbTransferTo.DataSource = null;
            cmbTransferTo.Items.Clear();
            cmbTransferTo.ValueMember = "CounterID";
            cmbTransferTo.DisplayMember = "UnitName";
            cmbTransferTo.DataSource = dt2;
            cmbTransferTo.SelectedValue = 0;
            cmbTransferTo.SelectedText = "";
        }

        private void chkTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTransfer.Checked)
            {
                transferPanel.Visible = true;
                FillTransferCombo();
            }
            else
            {
                transferPanel.Visible = false;
            }
        }

        private void cmbTransferTo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbTransferFrom.SelectedValue?.Equals(cmbTransferTo.SelectedValue) == true)
            {
                cmbTransferTo.SelectedIndex = -1;
                MessageBox.Show("Transfer From & Transfer To Must Be Different", "Invalid Data Selected!");
            }
        }

        private void cmbTransferFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbTransferFrom.SelectedValue?.Equals(cmbTransferTo.SelectedValue) == true)
            {
                cmbTransferFrom.SelectedIndex = -1;
                MessageBox.Show("Transfer From & Transfer To Must Be Different", "Invalid Data Selected!");
            }
        }

        private void cmbMaterial_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSeller_Click(object sender, EventArgs e)
        {
            using (FrmSupplier frmSupplier = new FrmSupplier())
            {
                frmSupplier.ShowDialog(this);

                if (frmSupplier.SavedSupplierId.HasValue)
                {
                    SellerInformation_S(true);
                    cmbSeller.SelectedValue = frmSupplier.SavedSupplierId.Value;
                }
            }
        }

        private void btnBuyer_Click(object sender, EventArgs e)
        {
            using (FrmBuyer frmBuyer = new FrmBuyer())
            {
                frmBuyer.ShowDialog(this);

                if (frmBuyer.SavedBuyerId.HasValue)
                {
                    Buyer_information_S(true);
                    cmbBuyer.SelectedValue = frmBuyer.SavedBuyerId.Value;
                }
            }
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            using (FrmMaterial frmMaterial = new FrmMaterial())
            {
                frmMaterial.ShowDialog(this);

                if (frmMaterial.SavedMaterialId.HasValue)
                {
                    LoadMaterials(true);
                    cmbMaterial.SelectedValue = frmMaterial.SavedMaterialId.Value;
                }
            }
        }

        private void btnOwnTruck_Click(object sender, EventArgs e)
        {
            using (FrmTruck frmTruck = new FrmTruck())
            {
                frmTruck.ShowDialog(this);

                if (frmTruck.SavedTruckId.HasValue)
                {
                    LoadOwnTrucks(true);
                    cmbOwnTruck.SelectedValue = frmTruck.SavedTruckId.Value;
                }
            }
        }

        private void cmbOwnTruck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingOwnTrucks || _isFilteringOwnTruck)
                return;

            if (cmbOwnTruck.SelectedItem is DataRowView row)
            {
                txtTruckNo.Text = Convert.ToString(row["TruckNo"]);
                txtDriverName.Text = Convert.ToString(row["DriverName"]);
            }
        }

        private void cmbOwnTruck_TextUpdate(object sender, EventArgs e)
        {
            if (_isLoadingOwnTrucks || _isFilteringOwnTruck || _allOwnTruckData == null)
                return;

            try
            {
                _isFilteringOwnTruck = true;
                string searchText = cmbOwnTruck.Text.Trim().Replace("'", "''");
                DataView view = _allOwnTruckData.DefaultView;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    view.RowFilter = string.Empty;
                }
                else
                {
                    view.RowFilter =
                        $"TruckNo LIKE '%{searchText}%' " +
                        $"OR DriverName LIKE '%{searchText}%' " +
                        $"OR MobileNo LIKE '%{searchText}%' " +
                        $"OR Convert(TruckID,'System.String') LIKE '%{searchText}%'";
                }

                DataTable filteredTrucks = view.ToTable();
                cmbOwnTruck.DataSource = null;
                cmbOwnTruck.DataSource = filteredTrucks;
                cmbOwnTruck.DisplayMember = "TruckNo";
                cmbOwnTruck.ValueMember = "TruckID";
                cmbOwnTruck.Text = searchText;
                cmbOwnTruck.SelectionStart = cmbOwnTruck.Text.Length;
                cmbOwnTruck.DroppedDown = true;
            }
            finally
            {
                _isFilteringOwnTruck = false;
            }
        }

        private void cmbWeightID_TextUpdate(object sender, EventArgs e)
        {
            if (_isFilteringWeight) return;
            if (_allWeightData == null) return;

            try
            {
                _isFilteringWeight = true;

                string searchText = cmbWeightID.Text.Trim().Replace("'", "''");

                DataView dv = _allWeightData.DefaultView;

                if (string.IsNullOrWhiteSpace(searchText))
                    dv.RowFilter = "";
                else
                    dv.RowFilter = $"BSNameWithID LIKE '%{searchText}%' OR Convert(weight_id,'System.String') LIKE '%{searchText}%'";

                DataTable dt = dv.ToTable();

                cmbWeightID.DataSource = null;
                cmbWeightID.DataSource = dt;
                cmbWeightID.DisplayMember = "BSNameWithID";
                cmbWeightID.ValueMember = "weight_id";

                cmbWeightID.Text = searchText;
                cmbWeightID.SelectionStart = cmbWeightID.Text.Length;
                cmbWeightID.DroppedDown = true;
            }
            finally
            {
                _isFilteringWeight = false;
            }
        }


        #region Material Dropdownlist


        public void LoadMaterials(bool forceReload = false)
        {
            if (forceReload || string.IsNullOrWhiteSpace(cmbMaterial.Text))
            {
                string query = "SELECT *, Matrial_desc_WithCode = concat(Matrial_code, '  ', Matrial_desc) FROM Scale_Matrial WHERE ISNULL(IsDelete, 0) = 0";
                dt = DbContext.Execute(query);

                _allMeterialCode = dt;
                _allMaterialData = dt != null ? dt.Copy() : null;

                cmbMaterial.DataSource = null;
                cmbMaterial.Items.Clear();
                cmbMaterial.ValueMember = "MaterialID";
                cmbMaterial.DisplayMember = "Matrial_desc_WithCode";
                cmbMaterial.DataSource = dt;

                cmbMaterial.SelectedValue = 0;
                cmbMaterial.SelectedText = "";
            }
        }

        private void cmbMaterial_TextUpdate(object sender, EventArgs e)
        {
            if (_isLoadingWeightInformation) return;
            if (_isFilteringMaterial) return;
            if (_allMaterialData == null) return;

            try
            {
                _isFilteringMaterial = true;
                string searchText = cmbMaterial.Text.Trim().Replace("'", "''");
                DataView dv = _allMaterialData.DefaultView;

                if (string.IsNullOrWhiteSpace(searchText))
                    dv.RowFilter = "";
                else
                    dv.RowFilter = $"Matrial_desc LIKE '%{searchText}%' OR Convert(MaterialID,'System.String') LIKE '%{searchText}%'";

                DataTable dt = dv.ToTable();

                cmbMaterial.DataSource = null;
                cmbMaterial.DataSource = dt;
                cmbMaterial.DisplayMember = "Matrial_desc_WithCode";
                cmbMaterial.ValueMember = "MaterialID";
                cmbMaterial.Text = searchText;
                cmbMaterial.SelectionStart = cmbMaterial.Text.Length;
                cmbMaterial.DroppedDown = true;
            }
            finally
            {
                _isFilteringMaterial = false;
            }
        }
        #endregion

        #region Buyer & Selleer

        #region Own Truck

        public void LoadOwnTrucks(bool forceReload = false)
        {
            if (!forceReload && _allOwnTruckData != null)
                return;

            const string query = @"
                SELECT TruckID, TruckNo, DriverName, MobileNo
                FROM Scale_Truck
                ORDER BY TruckNo";

            try
            {
                _isLoadingOwnTrucks = true;
                DataTable trucks = DbContext.Execute(query);
                _allOwnTruckData = trucks != null ? trucks.Copy() : new DataTable();

                cmbOwnTruck.DataSource = null;
                cmbOwnTruck.Items.Clear();
                cmbOwnTruck.DisplayMember = "TruckNo";
                cmbOwnTruck.ValueMember = "TruckID";
                cmbOwnTruck.DataSource = _allOwnTruckData;
                cmbOwnTruck.SelectedIndex = -1;
                cmbOwnTruck.Text = string.Empty;
            }
            catch (Exception ex)
            {
                _allOwnTruckData = new DataTable();
                cmbOwnTruck.DataSource = null;
                cmbOwnTruck.Items.Clear();
                textMessage.Text = "Own truck list unavailable: " + ex.Message;
            }
            finally
            {
                _isLoadingOwnTrucks = false;
            }
        }

        #endregion Own Truck

        public void Buyer_information_S(bool forceReload = false)
        {
            if (forceReload || string.IsNullOrWhiteSpace(cmbBuyer.Text))
            {
                string query = "SELECT * FROM Scale_Clint WHERE ClientTypeID = 2 AND ISNULL(IsDelete, 0) = 0 AND isnull(C_name,'') != ''";
                dt = DbContext.Execute(query);
                _allBuyerData = dt != null ? dt.Copy() : null;

                cmbBuyer.DataSource = null;
                cmbBuyer.Items.Clear();
                cmbBuyer.DisplayMember = "C_name";
                cmbBuyer.ValueMember = "ClientID";
                cmbBuyer.DataSource = dt;

                cmbBuyer.SelectedValue = 0;
                cmbBuyer.SelectedText = "";
            }
        }

        private void cmbBuyer_TextUpdate(object sender, EventArgs e)
        {
            if (_isFilteringBuyer) return;

            try
            {
                _isFilteringBuyer = true;

                string searchText = cmbBuyer.Text.Trim().Replace("'", "''");

                DataView dv = _allBuyerData.DefaultView;

                if (string.IsNullOrWhiteSpace(searchText))
                    dv.RowFilter = "";
                else
                    dv.RowFilter = $"C_name LIKE '%{searchText}%' OR Convert(ClientID,'System.String') LIKE '%{searchText}%'";

                DataTable dt = dv.ToTable();

                cmbBuyer.DataSource = null;
                cmbBuyer.DataSource = dt;
                cmbBuyer.DisplayMember = "C_name";
                cmbBuyer.ValueMember = "ClientID";
                cmbBuyer.Text = searchText;
                cmbBuyer.SelectionStart = cmbBuyer.Text.Length;
                cmbBuyer.DroppedDown = true;
            }
            finally
            {
                _isFilteringBuyer = false;
            }
        }

        public void SellerInformation_S(bool forceReload = false)
        {
            if (forceReload || string.IsNullOrWhiteSpace(cmbSeller.Text))
            {
                string query = "SELECT * FROM Scale_Clint WHERE ClientTypeID = 1 AND ISNULL(IsDelete, 0) = 0 AND isnull(C_name,'') != ''";
                dt = DbContext.Execute(query);
                _allSellerData = dt != null ? dt.Copy() : null;

                cmbSeller.DataSource = null;
                cmbSeller.Items.Clear();
                cmbSeller.DisplayMember = "C_name";
                cmbSeller.ValueMember = "ClientID";
                cmbSeller.DataSource = dt;

                cmbSeller.SelectedValue = 0;
                cmbSeller.SelectedText = "";
            }
        }

        private void cmbSeller_TextUpdate(object sender, EventArgs e)
        {
            if (_isFilteringSeller) return;

            try
            {
                _isFilteringSeller = true;

                string searchText = cmbSeller.Text.Trim().Replace("'", "''");

                DataView dv = _allSellerData.DefaultView;

                if (string.IsNullOrWhiteSpace(searchText))
                    dv.RowFilter = "";
                else
                    dv.RowFilter = $"C_name LIKE '%{searchText}%' OR Convert(ClientID,'System.String') LIKE '%{searchText}%'";

                DataTable dt = dv.ToTable();

                cmbSeller.DataSource = null;
                cmbSeller.DataSource = dt;
                cmbSeller.DisplayMember = "C_name";
                cmbSeller.ValueMember = "ClientID";

                cmbSeller.Text = searchText;
                cmbSeller.SelectionStart = cmbSeller.Text.Length;
                cmbSeller.DroppedDown = true;
            }
            finally
            {
                _isFilteringSeller = false;
            }
        }



        #endregion Buyer & Selleer             
    }

    public static class AppSession
    {
        public static int SelectedCounterID { get; set; } = 0;
        public static string SelectedCounterName { get; set; }
    }

    public class Scale_Information
    {
        public int? InformationId { get; set; }
        public DateTime? date_c { get; set; }
        public int? CounterID { get; set; }
        public int? weight_id { get; set; }
        public string weight_type { get; set; }
        public string operator_name { get; set; }
        public string driver_name { get; set; }
        public string ClientID_Seller { get; set; }
        public string ClientID_Buyer { get; set; }
        public DateTime? g_w_time { get; set; }
        public DateTime? t_w_time { get; set; }
        public string Status { get; set; }
        public string Challan { get; set; }
        public string matrial_code { get; set; }
        public string matrial_name { get; set; }
        public string truck_no { get; set; }
        public string Quantity { get; set; }
        public string g_w_unite { get; set; }
        public string t_w_unite { get; set; }
        public string BSName { get; set; }
        public string ItNotes { get; set; }
        public string BSNameWithID { get; set; }
        public string BSNameWithID1 { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public List<Scale_Information> Childs { get; set; }
        public List<ScaleSyncResultDtls> SavedItems { get; set; }
    }

    public class ScaleSyncResultDtls
    {
        public int? InformationId { get; set; }
        public int? weight_id { get; set; }
        public int? CounterID { get; set; }
        public string StatusMessage { get; set; }
    }
}
