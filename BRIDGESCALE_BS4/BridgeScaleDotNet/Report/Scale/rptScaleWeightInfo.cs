using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace BTLERP.Web.Report.Scale
{
    public partial class rptScaleWeightInfo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptScaleWeightInfo()
        {
            InitializeComponent();
        }

        private void xrTableCell20_BeforePrint(object sender, CancelEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;

            cell.ExpressionBindings.Add(
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ToolTip", "Click To Print")
                );
        }
    }
}
