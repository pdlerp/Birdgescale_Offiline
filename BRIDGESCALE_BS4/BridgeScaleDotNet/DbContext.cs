using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BridgeScaleDotNet
{
    public static class DbContext
    {

        //static string  connString = "Server=10.200.111.30;Database=BTLERP;User Id=sa;Password=pdLe@!p~~~23&&eb23;";
        public static readonly string connString =
           $@"Server=(localdb)\MSSQLLocalDB;AttachDbFilename=D:\Data\BS4.mdf;Integrated Security=True;Connect Timeout=30;";

        static DataTable dt;
        static int status = 2;
        public static SqlConnection GetConn()
        {
            return new SqlConnection(connString);
        }

        public static DataTable Execute(string query)
        {
            using (SqlConnection conn = GetConn())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void Exec(string query, Dictionary<string, object> p = null)
        {
            using (SqlConnection conn = GetConn())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (p != null)
                    foreach (var item in p)
                        cmd.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class ComboItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
