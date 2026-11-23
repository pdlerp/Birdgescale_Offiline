using BridgeScaleDotNet.Models;
using BridgeScaleDotNet.Services;
using BTLERP.Web.Report.Scale;
using DevExpress.XtraReports.UI;
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
        private bool _isReportLoading;
        string globalMessge = string.Empty;
        private readonly SyncService _syncService = new SyncService();
        public FrmMain()
        {
            // Timer 
            syncTimer = new System.Windows.Forms.Timer();
            syncTimer.Interval = 300000; // 5 minutes
            //syncTimer.Interval = 60000; // 1 Min
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

        private async void btnWeightReport_Click(object sender, EventArgs e)
        {
            FrmInformation frmInformation = new FrmInformation();
            int counterID = (int)frmInformation.cmbScale.SelectedValue;

            SyncService syncService = new SyncService();

            if(counterID != 4)
            {
                bool serverAvailable = await syncService.IsServerAvailableAsync();
                if (serverAvailable)
                {
                    //Web report
                    GetWebScaleList();
                }
            }
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
            LocalStorage.GrossWeight = 0;
            LocalStorage.TareWeight = 0;
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

        private async void btnWeightReportLocal_Click(object sender, EventArgs e)
        {
            if (_isReportLoading)
                return;

            _isReportLoading = true;
            btnWeightReportLocal.Enabled = false;
            UseWaitCursor = true;

            ReportLoadingForm loadingForm = new ReportLoadingForm();
            loadingForm.Show(this);
            loadingForm.BringToFront();
            loadingForm.Refresh();

            try
            {
                rptScaleWeightInfo report = await Task.Run(() =>
                {
                    rptScaleWeightInfo result = new rptScaleWeightInfo
                    {
                        RequestParameters = true
                    };
                    result.CreateDocument();
                    return result;
                });

                if (!IsDisposed)
                {
                    loadingForm.Close();
                    ReportPrintTool printTool = new ReportPrintTool(report);
                    printTool.ShowRibbonPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the report.\n" + ex.Message,
                    "Local Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!loadingForm.IsDisposed)
                    loadingForm.Close();

                UseWaitCursor = false;
                btnWeightReportLocal.Enabled = true;
                _isReportLoading = false;
            }
        }
        public void GetSummaryReport()
        {
            try
            {
                rptScaleWeightInfo report = new rptScaleWeightInfo();
                report.RequestParameters = true;
                report.CreateDocument();
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the report.\n" + ex.Message);
            }
        }
    }

    internal sealed class ReportLoadingForm : Form
    {
        private readonly Label _messageLabel;
        private readonly ProgressBar _progressBar;

        public ReportLoadingForm(string message = "Preparing local report...")
        {
            Text = "Local Report";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(360, 118);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            _messageLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 58,
                Text = message + "\r\nPlease wait.",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(15, 63, 117),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            _progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Dock = DockStyle.Bottom,
                Height = 12
            };

            Controls.Add(_messageLabel);
            Controls.Add(_progressBar);
        }
    }
}
