using BridgeScaleDotNet.Models;
using BridgeScaleDotNet.Services;
using BTLERP.Web.Report.Scale;
using DevExpress.Utils.Html.Internal;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        //bool _isBindingWeightID = false;   // 👈 নতুন লাইন

        double gWeight = 0;
        double tWeight = 0;
        string firstWeightType = null;

        DateTime? gw_time;
        DateTime? tw_time;
        bool exists;

        private DataTable _allWeightData;
        private bool _isWeightFiltering = false;

        private DataTable _allMeterialCode;
        private bool _isBinding = false;


        private DataTable _allSellerData;
        private DataTable _allBuyerData;
        private DataTable _allMaterialData;
        private bool _isFilteringWeight = false;
        private bool _isFilteringSeller = false;
        private bool _isFilteringBuyer = false;
        private bool _isFilteringMaterial = false;


   
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
            GetOperatorBSName();

            txtHiddenWeightID.Text = null;

            // Search option enable for cmbWeightID
            cmbWeightID.DropDownStyle = ComboBoxStyle.DropDown;
            cmbWeightID.AutoCompleteMode = AutoCompleteMode.None;
            cmbWeightID.AutoCompleteSource = AutoCompleteSource.None;
            cmbWeightID.TextUpdate += cmbWeightID_TextUpdate;

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
        }

        private void Timer()
        {
            syncTimer = new Timer();
            syncTimer.Interval = 1000;
            _syncSeconds = 300;
            syncTimer.Start();
            lblSyncStatus.Text = "Next sync in ...";
            
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
                            Where isnull(b.IsActive,0) = 1 and o.Name =  '{LocalStorage.UserName}'";

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
                query = $@"SELECT BSNameWithID =  a.ScaleName + cast(ISNULL(MAX(weight_id), 10000) + 1 as varchar(150))
                        ,weight_id = ISNULL(MAX(weight_id), 10000) + 1
                        FROM Scale_Counter  a
                        left join Scale_Information b with (nolock) on a.CounterID = b.CounterID
                        WHERE a.CounterID = {counterID}
                        group by a.ScaleName";
            }

            dt = DbContext.Execute(query);

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
                BSNameWithID = a.ScaleName + CAST(ISNULL(MAX(weight_id), 10000) + 1 AS varchar(150)),
                weight_id = ISNULL(MAX(weight_id), 10000) + 1
            FROM Scale_Counter a
            LEFT JOIN Scale_Information b with (nolock) 
                ON a.CounterID = b.CounterID
            WHERE a.CounterID = {counterID}
            GROUP BY a.ScaleName";
            }

            dt = DbContext.Execute(query);

            _allWeightData = dt != null ? dt.Copy() : null;

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
            //LocalStorage.IsFirstWeight = 0;

            if (LocalStorage.IsFirstWeight == 1)
            {
                status = 2;
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
            if (_isFilteringWeight) return;
            ResetForm();

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
                        }


                        if (r["ClientID_Buyer"] != DBNull.Value)
                        {
                            cmbBuyer.SelectedValue = (int)r["ClientID_Buyer"];
                        }
                        else
                        {
                            cmbBuyer.SelectedValue = 0;
                            cmbBuyer.Text = r["BuyerName"].ToString();
                        }


                        // Weight type
                        //weightTypeID = SafeValue(r["weight_type"]);
                        weightTypeID = SafeValue(r["weight_typeID"]);
                        cmbWeightType.SelectedValue = (weightTypeID == "1") ? 2 : 1;


                        LoadMaterials();

                        // Material
                        if (!string.IsNullOrEmpty(SafeValue(r["matrial_code"])))
                        {
                            cmbMaterial.SelectedValue = SafeValue(r["matrial_code"]);
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

                        // Weight values
                        txtGrossWeight.Text = SafeValue(r["g_w_unite"]);
                        txtTareWeight.Text = SafeValue(r["t_w_unite"]);


                        double.TryParse(SafeValue(r["g_w_unite"]), out gWeight);
                        double.TryParse(SafeValue(r["t_w_unite"]), out tWeight);

                        if (gWeight != 0 && tWeight != 0)
                            status = 0;

                        // Date/Time labels
                        lblGrossWeightTime.Text = "Date and Time";
                        gw_time = null;
                        tw_time = null;
                        if (gWeight != 0 && r["g_w_time"] != DBNull.Value)
                        {
                            var dtGross = Convert.ToDateTime(r["g_w_time"]);
                            lblGrossWeightTime.Text = dtGross.ToString("dd-MMM-yyyy  HH:mm tt");
                            gw_time = Convert.ToDateTime(r["g_w_time"]);
                        }

                        lblTareWeightTime.Text = "Date and Time";
                        if (tWeight != 0 && r["t_w_time"] != DBNull.Value)
                        {
                            var dtTare = Convert.ToDateTime(r["t_w_time"]);
                            lblTareWeightTime.Text = dtTare.ToString("dd-MMM-yyyy  HH:mm tt");
                            tw_time = Convert.ToDateTime(r["t_w_time"]);
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
                        }

                        if (firstWeightType == "OUT WEIGHT")
                        {
                            WeightType("OUT");
                        }

                        // Live scale weight update (optional)
                        if (!string.IsNullOrEmpty(cmbWeightType.Text))
                        {
                            if (status == 1 || status == 2)
                            {
                                if (cmbWeightType.Text == "IN WEIGHT")
                                    txtTareWeight.Text = txtScaleWeight.Text;
                                else
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
            string sellerName = cmbSeller.Text;
            string buyerName = cmbBuyer.Text;

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

        private void InsertScaleInformation(int weightId, string bsName, string bSNameWithID, int counterID, string sellerName, string buyerName)
        {
            string sql = @"
                INSERT INTO Scale_Information
                (CounterID, BSNameWithID, BSName, date_c, weight_id, weight_type, quantity,
                 operator_name, driver_name, truck_no, challan, ClientID_Seller,
                 ClientID_Buyer, status, g_w_time, t_w_time, matrial_code, g_w_unite, t_w_unite, TransferFrom, TransferTo, SellerName, BuyerName)
                VALUES
                (@CounterID, @BSNameWithID, @BSName, @date_c, @weight_id, @weight_type, @quantity,
                 @operator_name, @driver_name, @truck_no, @challan, @ClientID_Seller,
                 @ClientID_Buyer, @status, @g_w_time, @t_w_time, @matrial_code, @g_w_unite, @t_w_unite, @TransferFrom, @TransferTo, @SellerName, @BuyerName)";

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
                    BuyerName        = @BuyerName
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
                { "@BuyerName", cmbBuyer.Text },
                { "@SellerName", cmbSeller.Text },
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
            if (cmbSeller.SelectedIndex > 0)
            {
                string id = null;
                id = cmbSeller.SelectedValue.ToString();

                string query = $"SELECT * FROM Scale_Clint WHERE Clint_type = 'SUPPLIER' AND ISNULL(IsDelete, 0) = 0 and ClientID = '{id}'";
                dt = DbContext.Execute(query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtSellerCode.Text = dt.Rows[0]["Clint_code"] == DBNull.Value
                        ? ""
                        : dt.Rows[0]["Clint_code"].ToString();

                    txtSellerAddress.Text = dt.Rows[0]["Address"] == DBNull.Value
                        ? ""
                        : dt.Rows[0]["Address"].ToString();
                }
                else
                {
                    txtSellerCode.Text = "";
                    txtSellerAddress.Text = "";
                }
            }
        }

        private void cmbBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBuyer.SelectedIndex > 0 && cmbBuyer.SelectedValue != null)
            {
                string id = Convert.ToString(cmbBuyer.SelectedValue);

                if (!string.IsNullOrWhiteSpace(id))
                {
                    string query = $"SELECT * FROM Scale_Clint WHERE Clint_type = 'BUYER' AND ISNULL(IsDelete, 0) = 0 AND ClientID = '{id}'";
                    dt = DbContext.Execute(query);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        txtBuyerCode.Text = Convert.ToString(row["Clint_code"]);
                        txtBuyerAddress.Text = Convert.ToString(row["Address"]);
                    }
                    else
                    {
                        txtBuyerCode.Text = string.Empty;
                        txtBuyerAddress.Text = string.Empty;
                    }
                }
                else
                {
                    txtBuyerCode.Text = string.Empty;
                    txtBuyerAddress.Text = string.Empty;
                }
            }
            else
            {
                txtBuyerCode.Text = string.Empty;
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
            string url = "https://btlerp.badshatex.com/Scale/ScaleClient?menuId=1939&pageName=ScaleClient";

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

        private void btnBuyer_Click(object sender, EventArgs e)
        {
            string url = "https://btlerp.badshatex.com/Scale/ScaleClient?menuId=1939&pageName=ScaleClient";

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

        private void cmbWeightID_TextUpdate(object sender, EventArgs e)
        {
            if (_isFilteringWeight) return;

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


        public void LoadMaterials()
        {
            if (string.IsNullOrWhiteSpace(cmbMaterial.Text))
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
            if (_isFilteringMaterial) return;

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

        public void Buyer_information_S()
        {
            if (string.IsNullOrWhiteSpace(cmbBuyer.Text))
            {
                string query = "SELECT * FROM Scale_Clint WHERE Clint_type = 'CUSTOMER' AND ISNULL(IsDelete, 0) = 0 AND isnull(C_name,'') != ''";
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

        public void SellerInformation_S()
        {
            if (string.IsNullOrWhiteSpace(cmbSeller.Text))
            {
                string query = "SELECT * FROM Scale_Clint WHERE Clint_type = 'SUPPLIER' AND ISNULL(IsDelete, 0) = 0 AND isnull(C_name,'') != ''";
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
