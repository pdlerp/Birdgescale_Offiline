using BridgeScaleDotNet.Models;
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

namespace BridgeScaleDotNet
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

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
                    string query = "SELECT * FROM Scale_Operator WHERE Name=@user AND Pass=@pass";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);


                    LocalStorage.UserName = username;

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        //MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Open main form
                        FrmMain main = new FrmMain();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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
