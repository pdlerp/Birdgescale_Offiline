using BridgeScaleDotNet.Models;
using BridgeScaleDotNet.Services;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace BridgeScaleDotNet
{
    public partial class FrmMaterial : Form
    {
        private readonly SyncService _syncService = new SyncService();
        private int? _editingMaterialId;
        private bool _isSaving;
        string globalMessge = string.Empty;
        DataTable materials;

        public FrmMaterial()
        {
            InitializeComponent();
            LoadMaterials();
        }

        private void LoadMaterials()
        {
            const string query = @"
                SELECT MaterialID, Matrial_code, Matrial_desc, EntryBy, EntryDate,
                       EditBy, EditDate, IsDelete,
                NextMaterialCode = ISNULL(MAX(TRY_CONVERT(INT, Matrial_code)) OVER (), 0) + 1
                FROM Scale_Matrial
                WHERE ISNULL(IsDelete, 0) = 0
                ORDER BY MaterialID DESC";

            try
            {
                materials = DbContext.Execute(query);
                dgvMaterials.DataSource = null;
                ConfigureGrid();
                dgvMaterials.DataSource = materials;
                txtMaterialCode.Text = materials.Rows[0]["NextMaterialCode"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load materials.\r\n" + ex.Message,
                    "Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            dgvMaterials.AutoGenerateColumns = false;
            dgvMaterials.Columns.Clear();

            DataGridViewTextBoxColumn materialIdColumn = new DataGridViewTextBoxColumn
            {
                Name = "MaterialID",
                DataPropertyName = "MaterialID",
                Visible = false
            };

            DataGridViewTextBoxColumn codeColumn = new DataGridViewTextBoxColumn
            {
                Name = "Matrial_code",
                DataPropertyName = "Matrial_code",
                HeaderText = "Material Code",
                Width = 180
            };

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn
            {
                Name = "Matrial_desc",
                DataPropertyName = "Matrial_desc",
                HeaderText = "Material Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            DataGridViewTextBoxColumn entryDateColumn = new DataGridViewTextBoxColumn
            {
                Name = "EntryDate",
                DataPropertyName = "EntryDate",
                HeaderText = "Created",
                Width = 165,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd-MMM-yyyy HH:mm"
                }
            };

            DataGridViewTextBoxColumn editDateColumn = new DataGridViewTextBoxColumn
            {
                Name = "EditDate",
                DataPropertyName = "EditDate",
                HeaderText = "Updated",
                Width = 165,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd-MMM-yyyy HH:mm"
                }
            };

            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Action",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 85,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(37, 99, 235),
                    ForeColor = Color.White,
                    SelectionBackColor = Color.FromArgb(29, 78, 216),
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
                }
            };

            dgvMaterials.Columns.AddRange(new DataGridViewColumn[]
            {
                materialIdColumn,
                codeColumn,
                descriptionColumn,
                entryDateColumn,
                editDateColumn,
                editColumn
            });

            dgvMaterials.CellFormatting -= dgvMaterials_CellFormatting;
            dgvMaterials.CellFormatting += dgvMaterials_CellFormatting;
        }

        private void dgvMaterials_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.ColumnIndex < 0 ||
                dgvMaterials.Columns[e.ColumnIndex].Name != "Edit")
                return;

            // Reapply the action-cell style after alternating-row styles are inherited.
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.BackColor = Color.FromArgb(37, 99, 235);
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.SelectionBackColor = Color.FromArgb(29, 78, 216);
            e.CellStyle.SelectionForeColor = Color.White;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            e.FormattingApplied = true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_isSaving || !ValidateMaterial())
                return;

            _isSaving = true;
            btnSave.Enabled = false;

            try
            {
                MaterialSyncRequest material = SaveMaterial();
                LoadMaterials();
                ResetMaterialForm();

                // The local save finishes before the network sync starts.
                //string syncMessage = await _syncService.SyncMaterial();
                //lblSyncStatus.Text = syncMessage;
                MessageBox.Show("Material saved locally.\r\n",
                    "Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save material.\r\n" + ex.Message,
                    "Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                _isSaving = false;
            }
        }

        private MaterialSyncRequest SaveMaterial()
        {
            string code = txtMaterialCode.Text.Trim();
            string description = txtMaterialName.Text.Trim();
            int userId = LocalStorage.UserID;
            DateTime now = DateTime.Now;
            bool isEdit = _editingMaterialId.HasValue;
            int materialId;
            int? entryBy = userId;
            DateTime? entryDate = DateTime.Now;

            using (SqlConnection connection = DbContext.GetConn())
            {
                connection.Open();

                const string duplicateQuery = @"
                    SELECT COUNT(1)
                    FROM Scale_Matrial
                    WHERE ISNULL(IsDelete, 0) = 0
                      AND UPPER(LTRIM(RTRIM(Matrial_desc))) = UPPER(@Matrial_desc)
                      AND MaterialID <> ISNULL(@MaterialID, 0)";

                using (SqlCommand duplicateCommand = new SqlCommand(duplicateQuery, connection))
                {
                    AddTextParameter(duplicateCommand, "@Matrial_desc", description, 30);
                    duplicateCommand.Parameters.Add("@MaterialID", SqlDbType.Int).Value =
                        (object)_editingMaterialId ?? DBNull.Value;

                    if (Convert.ToInt32(duplicateCommand.ExecuteScalar()) > 0)
                        throw new InvalidOperationException("This material already exists.");
                }

                if (isEdit)
                {
                    const string existingQuery = @"
                        SELECT EntryBy, EntryDate
                        FROM Scale_Matrial
                        WHERE MaterialID = @MaterialID";

                    using (SqlCommand existingCommand = new SqlCommand(existingQuery, connection))
                    {
                        existingCommand.Parameters.Add("@MaterialID", SqlDbType.Int).Value =
                            _editingMaterialId.Value;
                        using (SqlDataReader reader = existingCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                entryBy = reader["EntryBy"] == DBNull.Value
                                    ? (int?)null
                                    : Convert.ToInt32(reader["EntryBy"]);
                                entryDate = reader["EntryDate"] == DBNull.Value
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(reader["EntryDate"]);
                            }
                        }
                    }

                    const string updateQuery = @"
                        UPDATE Scale_Matrial
                        SET Matrial_code = @Matrial_code,
                            Matrial_desc = @Matrial_desc,
                            EditBy = @EditBy,
                            EditDate = @EditDate
                        WHERE MaterialID = @MaterialID
                          AND ISNULL(IsDelete, 0) = 0";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        AddTextParameter(command, "@Matrial_code", code, 30);
                        AddUnicodeTextParameter(command, "@Matrial_desc", description, 30);
                        command.Parameters.Add("@EditBy", SqlDbType.Int).Value = userId;
                        command.Parameters.Add("@EditDate", SqlDbType.DateTime).Value = now;
                        command.Parameters.Add("@MaterialID", SqlDbType.Int).Value = _editingMaterialId.Value;
                        command.ExecuteNonQuery();
                    }

                    materialId = _editingMaterialId.Value;
                }
                else
                {
                    const string insertQuery = @"
                        INSERT INTO Scale_Matrial
                            (Matrial_code, Matrial_desc, EntryBy, EntryDate, IsDelete)
                        VALUES
                            (@Matrial_code, @Matrial_desc, @EntryBy, @EntryDate, 0);
                        SELECT CAST(SCOPE_IDENTITY() AS int);";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        AddTextParameter(command, "@Matrial_code", code, 30);
                        AddUnicodeTextParameter(command, "@Matrial_desc", description, 30);
                        command.Parameters.Add("@EntryBy", SqlDbType.Int).Value = userId;
                        command.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = now;
                        materialId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }

            return new MaterialSyncRequest
            {
                MaterialID = materialId,
                Matrial_code = code,
                Matrial_desc = description,
                EntryBy = isEdit ? entryBy : userId,
                EntryDate = isEdit ? entryDate : now,
                EditBy = isEdit ? (int?)userId : null,
                EditDate = isEdit ? (DateTime?)now : null,
                IsDelete = 0
            };
        }

        private static void AddTextParameter(SqlCommand command, string name, string value, int size)
        {
            command.Parameters.Add(name, SqlDbType.VarChar, size).Value =
                (object)value ?? DBNull.Value;
        }

        private static void AddUnicodeTextParameter(SqlCommand command, string name, string value, int size)
        {
            command.Parameters.Add(name, SqlDbType.NVarChar, size).Value =
                (object)value ?? DBNull.Value;
        }

        private bool ValidateMaterial()
        {
            string code = txtMaterialCode.Text.Trim();
            string description = txtMaterialName.Text.Trim();

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Please enter material code.", "Material",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaterialCode.Focus();
                return false;
            }

            if (code.Length > 30)
            {
                MessageBox.Show("Material code cannot exceed 30 characters.", "Material",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaterialCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please enter material description.", "Material",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaterialName.Focus();
                return false;
            }

            if (description.Length > 30)
            {
                MessageBox.Show("Material description cannot exceed 30 characters.", "Material",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaterialName.Focus();
                return false;
            }

            return true;
        }

        private void dgvMaterials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string action = dgvMaterials.Columns[e.ColumnIndex].Name;
            int materialId = Convert.ToInt32(dgvMaterials.Rows[e.RowIndex].Cells["MaterialID"].Value);

            if (action == "Edit")
                BeginEdit(materialId);
        }

        private void BeginEdit(int materialId)
        {
            DataGridViewRow row = FindMaterialRow(materialId);
            if (row == null)
                return;

            _editingMaterialId = materialId;
            txtMaterialCode.Text = Convert.ToString(row.Cells["Matrial_code"].Value);
            txtMaterialName.Text = Convert.ToString(row.Cells["Matrial_desc"].Value);
            btnSave.Text = "Update";
            txtMaterialCode.Focus();
        }

        private DataGridViewRow FindMaterialRow(int materialId)
        {
            foreach (DataGridViewRow row in dgvMaterials.Rows)
            {
                if (!row.IsNewRow &&
                    Convert.ToInt32(row.Cells["MaterialID"].Value) == materialId)
                    return row;
            }

            return null;
        }

        private void ResetMaterialForm()
        {
            _editingMaterialId = null;           
            txtMaterialName.Clear();
            btnSave.Text = "Save";
            txtMaterialCode.Focus();

            if (materials != null && materials.Rows.Count > 0)
            {
                txtMaterialCode.Text = materials.Rows[0]["NextMaterialCode"].ToString();
            }
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            btnSync.Enabled = false;

            SyncService syncService = new SyncService();

            

            bool isAvailable = await syncService.IsServerAvailableAsync();

            if (isAvailable)
            {
                //MessageBox.Show("Server is available. Sync can start.");
                //globalMessge = "Server is available. Sync can start.";
                //globalMessge = await syncService.SyncMaterial();
            }
            else
            {
                //MessageBox.Show("Server is not available.");
                globalMessge = "Server is not available.";
            }

            try
            {
               // globalMessge = await _syncService.SyncMaterial();
            }
            catch (Exception ex)
            {
                globalMessge = "Material sync failed: " + ex.Message;
            }
            finally
            {
                btnSync.Enabled = true;
            }

            textMessage.Text = globalMessge;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close();
            this.Hide();
            FrmMain frmMain = new FrmMain();
            frmMain.ShowDialog();
        }
    }

    public class Scale_Matrial
    {
        public int? MaterialID { get; set; }
        public string Matrial_code { get; set; }
        public string Matrial_desc { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public int? IsDelete { get; set; }
        public int? IsSyncDataToServer { get; set; }
        public string SyncStatus { get; set; }
        public List<Scale_Matrial> Childs { get; set; }
    }
}
