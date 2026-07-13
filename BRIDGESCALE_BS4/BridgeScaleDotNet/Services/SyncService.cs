using DevExpress.XtraRichEdit.Import.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BridgeScaleDotNet.Services
{
    public class SyncService
    {
        DataTable dt;
        private Timer syncTimer;
        string globalMessge = string.Empty;
             

        public async Task<bool> IsServerAvailableAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    //var response = await client.GetAsync("http://localhost:30473/api/scaleInformation/ping");
                    var response = await client.GetAsync("https://btlerp.badshatex.com/api/scaleInformation/ping");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> SyncScaleInformationAsync()
        {
            //btnSync.Enabled = false;

            try
            {
                bool isAvailable = await IsServerAvailableAsync();

                if (isAvailable)
                {
                    //MessageBox.Show("Server is available. Sync can start.");
                    globalMessge = "Server is available. Sync can start.";
                    await SyncScaleInformation();
                }
                else
                {
                    //MessageBox.Show("Server is not available.");
                    globalMessge = "Server is not available.";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Sync check failed: " + ex.Message);
                globalMessge = "Sync check failed: " + ex.Message;
            }
            finally
            {
                //btnSync.Enabled = true;
                globalMessge = globalMessge + DateTime.Now.TimeOfDay.ToString();
            }
            return globalMessge;
        }

        public async Task SyncScaleInformation()
        {
            var pendingData = new Scale_Information();
            pendingData = GetPendingScaleData();

            if (pendingData == null || pendingData.Childs.Count == 0)
            {
                //MessageBox.Show("All data is already up to date. No new data to sync.");
                globalMessge = "All data is already up to date. No new data to sync.";
                return;
            }


            var json = JsonConvert.SerializeObject(pendingData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:30473/");
                client.BaseAddress = new Uri("https://btlerp.badshatex.com/");
                client.Timeout = TimeSpan.FromSeconds(60);

                var response = await client.PostAsync("api/scaleInformation/syncScaleDataFromDesktopApp", content);
                var responseText = await response.Content.ReadAsStringAsync();

                //MessageBox.Show(responseText);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var result = JsonSerializer.Deserialize<List<ScaleSyncResultDtls>>(responseText, options);

                    if (result != null)
                    {
                        MarkScaleInfoAsSynced(result);
                        //MessageBox.Show("Success!");                      
                        globalMessge = "Last Sync At ";
                    }
                }
                else
                {
                    MessageBox.Show(responseText);
                }
            }
        }

        public Scale_Information GetPendingScaleData()
        {
            string query = "SELECT * FROM Scale_Information where isnull(IsSyncDataToServer,0) = 0 ";
            dt = DbContext.Execute(query);

            var result = new Scale_Information
            {
                Childs = new List<Scale_Information>()
            };


            foreach (DataRow row in dt.Rows)
            {
                var info = new Scale_Information
                {
                    InformationId = row["InformationId"] != DBNull.Value ? Convert.ToInt32(row["InformationId"]) : (int?)null,
                    date_c = row["date_c"] != DBNull.Value ? Convert.ToDateTime(row["date_c"]) : (DateTime?)null,
                    weight_id = row["weight_id"] != DBNull.Value ? Convert.ToInt32(row["weight_id"]) : (int?)null,
                    CounterID = row["CounterID"] != DBNull.Value ? Convert.ToInt32(row["CounterID"]) : (int?)null,
                    weight_type = row["weight_type"]?.ToString(),
                    operator_name = row["operator_name"]?.ToString(),
                    driver_name = row["driver_name"]?.ToString(),
                    ClientID_Seller = row["ClientID_Seller"]?.ToString(),
                    ClientID_Buyer = row["ClientID_Buyer"]?.ToString(),
                    g_w_time = row["g_w_time"] != DBNull.Value ? Convert.ToDateTime(row["g_w_time"]) : (DateTime?)null,
                    t_w_time = row["t_w_time"] != DBNull.Value ? Convert.ToDateTime(row["t_w_time"]) : (DateTime?)null,
                    Status = row["Status"]?.ToString(),
                    Challan = row["Challan"]?.ToString(),
                    matrial_code = row["matrial_code"]?.ToString(),
                    truck_no = row["truck_no"]?.ToString(),
                    Quantity = row["Quantity"]?.ToString(),
                    g_w_unite = row["g_w_unite"]?.ToString(),
                    t_w_unite = row["t_w_unite"]?.ToString(),
                    BSName = row["BSName"]?.ToString(),
                    ItNotes = row["ItNotes"]?.ToString(),
                    BSNameWithID = row["BSNameWithID"]?.ToString(),
                    SellerName = row["SellerName"]?.ToString(),
                    BuyerName = row["BuyerName"]?.ToString(),
                    Childs = new List<Scale_Information>()
                };
                result.Childs.Add(info);
            }
            return result;
        }

        public void MarkScaleInfoAsSynced(List<ScaleSyncResultDtls> scaleInfo)
        {


            //int? informationID = 0;
            int? counterID = 0;
            int? weightID = 0;

            foreach (var row in scaleInfo)
            {
                counterID = row.CounterID;
                weightID = row.weight_id;

                string query = "update Scale_Information set IsSyncDataToServer = 1 where weight_id = " + weightID + " and counterID = " + counterID;
                DbContext.Execute(query);

            }
            //return result;
        }
    }
}
