using BridgeScaleDotNet.Models;
using DevExpress.DataAccess.Native.Data;
using DevExpress.Pdf.Native.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace BridgeScaleDotNet
{
    public partial class FrmLogin : Form
    {
        bool _isBindingScale = false;
        System.Data.DataTable dt;
        public FrmLogin()
        {
            InitializeComponent();
            GetOperatorBSName();
        }

        public void GetOperatorBSName()
        {
            _isBindingScale = true;

            string query = $@"select CounterID, ScaleName as BSName
                            from Scale_Counter 
                            Where isnull(IsActive,0) = 1 ";

            dt = DbContext.Execute(query);

            cmbScale.DrawMode = DrawMode.OwnerDrawFixed;
            cmbScale.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScale.ItemHeight = 30;
            cmbScale.DropDownHeight = 180;
            cmbScale.IntegralHeight = false;
            cmbScale.DrawItem += cmbScale_DrawItem;

            cmbScale.DataSource = null;
            cmbScale.Items.Clear();
            cmbScale.DisplayMember = "BSName";
            cmbScale.ValueMember = "CounterID";
            cmbScale.DataSource = dt;

            //MessageBox.Show(cmbScale.Items.Count.ToString());

            // 🧠 VERY IMPORTANT — force binding to complete

            cmbScale.BindingContext = new BindingContext();

            //cmbScale.SelectedValue = dt.Rows[0]["CounterID"];

            if (AppSession.SelectedCounterID > 0)
            {
                cmbScale.SelectedValue = AppSession.SelectedCounterID;
            }
            _isBindingScale = false;
        }

        private void cmbScale_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ComboBox combo = (ComboBox)sender;

            e.DrawBackground();

            string text = combo.GetItemText(combo.Items[e.Index]);

            TextRenderer.DrawText(
                e.Graphics,
                text,
                combo.Font,
                e.Bounds,
                e.ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
            );

            e.DrawFocusRectangle();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string counterID = cmbScale.SelectedValue.ToString();
            string sql = string.Empty;

            LocalStorage.UserName = username;
            LocalStorage.Password = password;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }

            try
            {
                // string connStr = "Server=10.200.111.30;Database=BTLERP;User Id=sa;Password=pdLe@!p~~~23&&eb23;";

                string connString =
                $@"Server=(localdb)\MSSQLLocalDB;AttachDbFilename=D:\Data\BS4.mdf;Integrated Security=True;Connect Timeout=30;";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"
                                    SELECT TOP 1 o.OperatorID
                                    FROM Scale_Operator o
                                    INNER JOIN Scale_Operator_ScalePermission p ON o.OperatorID = p.OperatorID
                                    WHERE o.Name = @user
                                      AND o.Pass = @pass
                                      AND p.CounterID = @counterID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@counterID", counterID);

                    //MessageBox.Show(
                    //    $"Server: {conn.DataSource}\n" +
                    //    $"Database: {conn.Database}\n" +
                    //    $"User: [{username}]\n" +
                    //    $"Pass: [{password}]\n" +
                    //    $"CounterID: [{counterID}]"
                    //);

                    object result = cmd.ExecuteScalar();

                    //MessageBox.Show(
                    //    result == null || result == DBNull.Value
                    //        ? "Result = NULL"
                    //        : $"OperatorID = {result}"
                    //);

                    if (result != null && result != DBNull.Value)
                    {
                        int operatorID = Convert.ToInt32(result);
                        LocalStorage.OperatorID = operatorID;
                        LocalStorage.UserName = username;
                        LocalStorage.CounterID = counterID;
                        FrmMain main = new FrmMain();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username, password, or scale access.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public string GetSqlQuery(SqlCommand cmd)
        {
            string debugSql = cmd.CommandText;

            foreach (SqlParameter p in cmd.Parameters)
            {
                string value;

                if (p.Value == null || p.Value == DBNull.Value)
                {
                    value = "NULL";
                }
                else if (p.Value is string || p.Value is DateTime)
                {
                    value = $"N'{p.Value.ToString().Replace("'", "''")}'";
                }
                else
                {
                    value = p.Value.ToString();
                }

                debugSql = debugSql.Replace(p.ParameterName, value);
            }
             return debugSql;
        }

        private void btlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick(); // trigger login button
                e.SuppressKeyPress = true; // stop beep sound
            }
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
