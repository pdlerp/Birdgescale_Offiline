using BridgeScaleDotNet.Models;
using BridgeScaleDotNet.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BridgeScaleDotNet
{
    public partial class FrmMain : Form
    {
        SerialPort serialPort1 = new SerialPort();
        private Timer syncTimer;
        string globalMessge = string.Empty;
        private readonly SyncService _syncService = new SyncService();
        public FrmMain()
        {
            // Timer 
            syncTimer = new System.Windows.Forms.Timer();
            //syncTimer.Interval = 300000; // 5 minutes
            syncTimer.Interval = 60000; // 1 Min
            syncTimer.Tick += syncTimer_Tick;
            syncTimer.Start();


            InitializeComponent();
            serialPort1.PortName = "COM1";


        }

        private async Task RunSyncAsync(bool showMessage)
        {
            //btnSync.Enabled = false;
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
                //btnSync.Enabled = true;
            }

            textMessage1.Text = message;
        }

        private async void syncTimer_Tick(object sender, EventArgs e)
        {
            await RunSyncAsync(true);
        }

        private void btnWeightReport_Click(object sender, EventArgs e)
        {
            GetWebScaleList();
        }

        public void GetWebScaleList()
        {
            string url;
            url = "https://btlerp.badshatex.com/Scale/ScaleInformation?menuId=1941&pageName=ScaleInformation";
            // Open in default browser
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
        // 1st Weight
        private void btnWeightAdd_Click(object sender, EventArgs e)
        { 
            FrmInformation frmInfo = new FrmInformation();
            frmInfo.Show();
            this.Hide();
            LocalStorage.IsFirstWeight = 1;
            frmInfo.GetWeightID();
            frmInfo.WeightType("ALL");
        }
        // 2nd Weight
        private void btnNewWeight_Click(object sender, EventArgs e)
        {
            FrmInformation frmInfo = new FrmInformation();
            frmInfo.Show();
            this.Hide();
            LocalStorage.IsFirstWeight = 0;
            frmInfo.GetWeightID();

            //frmInfo.cmbScale.SelectedValue = LocalStorage.CounterID;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Application.Exit();

            // Open main form
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
