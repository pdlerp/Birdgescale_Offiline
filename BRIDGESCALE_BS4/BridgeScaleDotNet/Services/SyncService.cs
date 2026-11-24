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
using System.Data.SqlClient;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BridgeScaleDotNet.Services
{
    public class SyncService
    {
        //private const string DataMissingLocalStatus = "DATA_MISSING_LOCAL";
       //private const string DataMissingServerStatus = "DATA_MISSING_SERVER";

        DataTable dt;
        private Timer syncTimer;
        string globalMessge = string.Empty;
             

        public async Task<bool> IsServerAvailableAsync_backup()
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

        public async Task<bool> IsServerAvailableAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var response = await client.GetAsync(
                        "https://btlerp.badshatex.com/api/scaleInformation/ping"
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        MessageBox.Show(
                            $"Status: {(int)response.StatusCode} {response.StatusCode}\n\n" +
                            $"Response: {content}"
                        );

                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ping API failed.\n\n" +
                    $"Message: {ex.Message}\n\n" +
                    $"Inner: {ex.InnerException?.Message}"
                );

                return false;
            }
        }

        //public async bool PingServer()
        //{
        //    bool isServerAvailable = false;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.Timeout = TimeSpan.FromSeconds(10);

        //        var response = await client.GetAsync(
        //            "https://btlerp.badshatex.com/api/scaleInformation/ping"
        //        );

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();

        //            MessageBox.Show(
        //                $"Status: {(int)response.StatusCode} {response.StatusCode}\n\n" +
        //                $"Response: {content}"
        //            );

        //            isServerAvailable = false;
        //        }

        //        isServerAvailable = true;
        //    }
        //    return isServerAvailable;
        //}

        #region sync Scal Information

        public async Task<string> SyncScaleInformationAsync()
        {
            //btnSync.Enabled = false;

            if (!int.TryParse(BridgeScaleDotNet.Models.LocalStorage.CounterID, out int counterId) || counterId <= 0)
                return "Sync skipped: no counter is selected.";

            if (counterId == 4)
                return "Sync not valid for scale 4.";

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
                    matrial_name = row["matrial_name"]?.ToString(),
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
        #endregion

        #region Sync Material Data

        //public async Task<string> SyncMaterial()
        //{   

        //    var localData = new Scale_Matrial();
        //    var webData = new Scale_Matrial();

        //    webData = GetPendingMaterialData_WebServer();

        //    localData = GetPendingMaterialData_Local();


        //    if ((localData == null || localData.Childs == null || localData.Childs.Count == 0) &&
        //          (webData == null || webData.Childs == null || webData.Childs.Count == 0))
        //    {
        //        globalMessge = "No data found for sync.";
        //        return globalMessge;
        //    }

        //    var missingData = new Scale_Matrial
        //    {
        //        Childs = new List<Scale_Matrial>()
        //    };
        //    var localList = localData?.Childs ?? new List<Scale_Matrial>();
        //    var webList = webData?.Childs ?? new List<Scale_Matrial>();


        //    // Local data missing in Web
        //    foreach (var localItem in localList)
        //    {
        //        var existsInWeb = webList.Any(x => x.MaterialID == localItem.MaterialID);

        //        if (!existsInWeb)
        //        {
        //            localItem.IsSyncDataToServer = 0;
        //            localItem.SyncStatus = "Data Missing in server";
        //            missingData.Childs.Add(localItem);
        //        }
        //    }


        //    // Web data missing in Local
        //    foreach (var webItem in webList)
        //    {
        //        var existsInLocal = localList.Any(x => x.MaterialID == webItem.MaterialID);
        //        if (!existsInLocal)
        //        {
        //            webItem.SyncStatus = DataMissingLocalStatus;
        //            missingData.Childs.Add(webItem);
        //        }
        //    }
            
        //    if (missingData.Childs.Count > 0)
        //    {
        //        Scale_Matrial scale_Matrial = new Scale_Matrial();
        //        List<Scale_Matrial> localMissingData = missingData.Childs
        //        .Where(material => string.Equals(material?.SyncStatus, DataMissingLocalStatus, StringComparison.OrdinalIgnoreCase))
        //        .ToList();

        //        scale_Matrial.Childs = localMissingData;

        //        SqlConnection localCon = DbContext.GetConn();
        //        int insertedCount = InsertMissingMaterials(scale_Matrial, localCon);

        //        globalMessge = insertedCount == 0
        //            ? "No new server materials to insert locally."
        //            : insertedCount + " server material(s) inserted locally.";
        //    }
            

        //    //if (missingData.Childs.Count == 0)
        //    //{
        //    //    globalMessge = "Local and Web data are synchronized.";
        //    //    return globalMessge;
        //    //}

        //    return globalMessge;
        //}

        //public int InsertMissingMaterials(Scale_Matrial missingData, SqlConnection dbConnection)
        //{
        //    if (missingData?.Childs == null)
        //        return 0;

        //    List<Scale_Matrial> materialsToInsert = missingData.Childs;            

        //    if (materialsToInsert.Count == 0)
        //        return 0;

        //    using (SqlConnection connection = dbConnection)
        //    {
        //        connection.Open();
        //        using (SqlTransaction transaction = connection.BeginTransaction())
        //        {
        //            bool identityInsertEnabled = false;
        //            int insertedCount = 0;

        //            try
        //            {
        //                bool materialIdIsIdentity = Convert.ToInt32(new SqlCommand(
        //                    "SELECT COLUMNPROPERTY(OBJECT_ID('Scale_Matrial'), 'MaterialID', 'IsIdentity')",
        //                    connection,
        //                    transaction).ExecuteScalar()) == 1;

        //                if (materialIdIsIdentity && materialsToInsert.Any(material => material.MaterialID.HasValue))
        //                {
        //                    ExecuteMaterialCommand(
        //                        connection,
        //                        transaction,
        //                        "SET IDENTITY_INSERT Scale_Matrial ON");
        //                    identityInsertEnabled = true;
        //                }


        //                foreach (Scale_Matrial material in materialsToInsert)
        //                {
        //                    string insertQuery = @"
        //                            INSERT INTO Scale_Matrial
        //                                (MaterialID, Matrial_code, Matrial_desc, EntryBy, EntryDate, EditBy, EditDate, IsDelete, IsSyncDataToServer)
        //                            VALUES (@MaterialID, @Matrial_code, @Matrial_desc, @EntryBy, @EntryDate,@EditBy, @EditDate, @IsDelete, 1)";


        //                    using (SqlCommand insertCommand = new SqlCommand(
        //                           insertQuery, connection, transaction))
        //                    {
        //                        if (materialIdIsIdentity && material.MaterialID.HasValue)
        //                        {
        //                            insertCommand.Parameters.Add("@MaterialID", SqlDbType.Int).Value =
        //                                material.MaterialID.Value;
        //                        }

        //                        AddTextParameter(insertCommand, "@Matrial_code", material.Matrial_code, 30);
        //                        AddUnicodeTextParameter(insertCommand, "@Matrial_desc", material.Matrial_desc, 30);
        //                        AddNullableIntParameter(insertCommand, "@EntryBy", material.EntryBy);
        //                        AddNullableDateTimeParameter(insertCommand, "@EntryDate", material.EntryDate);
        //                        AddNullableIntParameter(insertCommand, "@EditBy", material.EditBy);
        //                        AddNullableDateTimeParameter(insertCommand, "@EditDate", material.EditDate);
        //                        insertCommand.Parameters.Add("@IsDelete", SqlDbType.Int).Value = (object)material.IsDelete ?? 0;
        //                        insertCommand.ExecuteNonQuery();
        //                        insertedCount++;
        //                    }
        //                }

        //                transaction.Commit();
        //                return insertedCount;
        //            }
        //            catch
        //            {
        //                if (identityInsertEnabled)
        //                {
        //                    try
        //                    {
        //                        ExecuteMaterialCommand(
        //                            connection,
        //                            transaction,
        //                            "SET IDENTITY_INSERT Scale_Matrial OFF");
        //                    }
        //                    catch
        //                    {
        //                        // Preserve the original database exception.
        //                    }
        //                }

        //                transaction.Rollback();
        //                throw;
        //            }
        //        }
        //    }
        //}

        private static void ExecuteMaterialCommand(
            SqlConnection connection,
            SqlTransaction transaction,
            string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void AddTextParameter(
            SqlCommand command,
            string name,
            string value,
            int size)
        {
            command.Parameters.Add(name, SqlDbType.VarChar, size).Value =
                (object)value ?? DBNull.Value;
        }

        private static void AddUnicodeTextParameter(
            SqlCommand command,
            string name,
            string value,
            int size)
        {
            command.Parameters.Add(name, SqlDbType.NVarChar, size).Value =
                (object)value ?? DBNull.Value;
        }

        private static void AddNullableIntParameter(
            SqlCommand command,
            string name,
            int? value)
        {
            command.Parameters.Add(name, SqlDbType.Int).Value =
                (object)value ?? DBNull.Value;
        }

        private static void AddNullableDateTimeParameter(
            SqlCommand command,
            string name,
            DateTime? value)
        {
            command.Parameters.Add(name, SqlDbType.DateTime).Value =
                (object)value ?? DBNull.Value;
        }

        public Scale_Matrial GetPendingMaterialData_Local()
        {
            string query = "SELECT * FROM Scale_Matrial where isnull(IsSyncDataToServer,0) = 0 ";
            dt = DbContext.Execute(query);
            var result = new Scale_Matrial
            {
                Childs = new List<Scale_Matrial>()
            };

            foreach (DataRow row in dt.Rows)
            {
                var matrial = new Scale_Matrial
                {
                    MaterialID = row["MaterialID"] != DBNull.Value ? Convert.ToInt32(row["MaterialID"]) : (int?)null,
                    Matrial_code = row["Matrial_code"]?.ToString(),
                    Matrial_desc = row["Matrial_desc"]?.ToString(),
                    EntryBy = row["EntryBy"] != DBNull.Value ? Convert.ToInt32(row["EntryBy"]) : (int?)null,
                    EntryDate = row["EntryDate"] != DBNull.Value ? Convert.ToDateTime(row["EntryDate"]) : (DateTime?)null,
                    EditBy = row["EditBy"] != DBNull.Value ? Convert.ToInt32(row["EditBy"]) : (int?)null,
                    EditDate = row["EditDate"] != DBNull.Value ? Convert.ToDateTime(row["EditDate"]) : (DateTime?)null,
                    IsDelete = row["IsDelete"] != DBNull.Value ? Convert.ToInt32(row["IsDelete"]) : (int?)null,
                    IsSyncDataToServer = row["IsSyncDataToServer"] != DBNull.Value ? Convert.ToInt32(row["IsSyncDataToServer"]) : (int?)null,
                    Childs = new List<Scale_Matrial>()
                };
                result.Childs.Add(matrial);
            }
            return result;
        }

        public Scale_Matrial GetPendingMaterialData_WebServer()
        {
            string query = "SELECT * FROM Scale_Matrial";
            dt = DbContext.ExecuteWebServer(query);
            var result = new Scale_Matrial
            {
                Childs = new List<Scale_Matrial>()
            };

            foreach (DataRow row in dt.Rows)
            {
                var matrial = new Scale_Matrial
                {
                    MaterialID = row["MaterialID"] != DBNull.Value ? Convert.ToInt32(row["MaterialID"]) : (int?)null,
                    Matrial_code = row["Matrial_code"]?.ToString(),
                    Matrial_desc = row["Matrial_desc"]?.ToString(),
                    EntryBy = row["EntryBy"] != DBNull.Value ? Convert.ToInt32(row["EntryBy"]) : (int?)null,
                    EntryDate = row["EntryDate"] != DBNull.Value ? Convert.ToDateTime(row["EntryDate"]) : (DateTime?)null,
                    EditBy = row["EditBy"] != DBNull.Value ? Convert.ToInt32(row["EditBy"]) : (int?)null,
                    EditDate = row["EditDate"] != DBNull.Value ? Convert.ToDateTime(row["EditDate"]) : (DateTime?)null,
                    IsDelete = row["IsDelete"] != DBNull.Value ? Convert.ToInt32(row["IsDelete"]) : (int?)null,
                    IsSyncDataToServer = row["IsSyncDataToServer"] != DBNull.Value ? Convert.ToInt32(row["IsSyncDataToServer"]) : (int?)null,
                    Childs = new List<Scale_Matrial>()
                };
                result.Childs.Add(matrial);
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

        #endregion

        #region sync material data

        private static int? ToNullableInt(object value)
        {
            return value == DBNull.Value || value == null ? (int?)null : Convert.ToInt32(value);
        }

        private static DateTime? ToNullableDateTime(object value)
        {
            return value == DBNull.Value || value == null
                ? (DateTime?)null
                : Convert.ToDateTime(value);
        }

        private static string ToNullableString(object value)
        {
            return value == DBNull.Value || value == null ? null : Convert.ToString(value);
        }

        #endregion

    }

    public class MaterialSyncRequest
    {
        public int? MaterialID { get; set; }
        public string Matrial_code { get; set; }
        public string Matrial_desc { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public int IsDelete { get; set; }
    }
}

