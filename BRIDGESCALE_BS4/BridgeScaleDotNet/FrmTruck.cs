using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BridgeScaleDotNet
{
    public partial class FrmTruck : Form
    {
        private int? _editingTruckId;
        private bool _isSaving;
        private DataTable _allTrucks;

        public int? SavedTruckId { get; private set; }

        public FrmTruck()
        {
            InitializeComponent();
            LoadTruckLogo();
            LoadTrucks();
        }

        private void LoadTruckLogo()
        {
            string logoPath = Path.Combine(Application.StartupPath, "images", "btlpng.png");
            if (!File.Exists(logoPath))
                return;

            using (Image source = Image.FromFile(logoPath))
            {
                picLogo.Image = new Bitmap(source);
            }
        }

        private void LoadTrucks()
        {
            const string query = @"
                SELECT TruckID, TruckNo, DriverName, MobileNo
                FROM Scale_Truck
                ORDER BY TruckID DESC";

            try
            {
                DataTable trucks = DbContext.Execute(query);
                _allTrucks = trucks != null ? trucks.Copy() : new DataTable();
                dgvTrucks.DataSource = null;
                ConfigureGrid();
                dgvTrucks.DataSource = _allTrucks;
                UpdateTotalLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load own trucks. Please verify that Scale_Truck exists.\r\n" + ex.Message,
                    "Own Truck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            dgvTrucks.AutoGenerateColumns = false;
            dgvTrucks.Columns.Clear();

            dgvTrucks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SL",
                HeaderText = "SL",
                Width = 58,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            dgvTrucks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TruckID",
                DataPropertyName = "TruckID",
                Visible = false
            });

            dgvTrucks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TruckNo",
                DataPropertyName = "TruckNo",
                HeaderText = "Truck No",
                Width = 250
            });

            dgvTrucks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DriverName",
                DataPropertyName = "DriverName",
                HeaderText = "Driver Name",
                Width = 300
            });

            dgvTrucks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MobileNo",
                DataPropertyName = "MobileNo",
                HeaderText = "Mobile No",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvTrucks.Columns.Add(new DataGridViewButtonColumn
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
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(15, 76, 129),
                    SelectionBackColor = Color.FromArgb(219, 234, 254),
                    SelectionForeColor = Color.FromArgb(15, 76, 129),
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
                }
            });

            dgvTrucks.CellFormatting -= dgvTrucks_CellFormatting;
            dgvTrucks.CellFormatting += dgvTrucks_CellFormatting;
            dgvTrucks.DataError -= dgvTrucks_DataError;
            dgvTrucks.DataError += dgvTrucks_DataError;
        }

        private void dgvTrucks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string columnName = dgvTrucks.Columns[e.ColumnIndex].Name;
            if (columnName == "SL")
            {
                e.Value = (e.RowIndex + 1).ToString();
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
                return;
            }

            if (columnName != "Edit")
                return;

            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.BackColor = Color.White;
            e.CellStyle.ForeColor = Color.FromArgb(15, 76, 129);
            e.CellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            e.CellStyle.SelectionForeColor = Color.FromArgb(15, 76, 129);
            e.CellStyle.Padding = new Padding(6, 3, 6, 3);
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            e.FormattingApplied = true;
        }

        private void dgvTrucks_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void UpdateTotalLabel()
        {
            lblTotal.Text = "Total: " + dgvTrucks.Rows.Count + " Own Trucks";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allTrucks == null)
                return;

            string searchText = txtSearch.Text.Trim().Replace("'", "''");
            DataView view = _allTrucks.DefaultView;
            view.RowFilter = string.IsNullOrWhiteSpace(searchText)
                ? string.Empty
                : $"TruckNo LIKE '%{searchText}%' OR DriverName LIKE '%{searchText}%' OR MobileNo LIKE '%{searchText}%'";

            dgvTrucks.DataSource = view.ToTable();
            UpdateTotalLabel();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadTrucks();
            lblStatus.Text = "Own truck list refreshed.";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV file (*.csv)|*.csv";
                dialog.FileName = "OwnTrucks.csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                StringBuilder csv = new StringBuilder();
                csv.AppendLine("SL,Truck No,Driver Name,Mobile No");

                for (int index = 0; index < dgvTrucks.Rows.Count; index++)
                {
                    DataGridViewRow row = dgvTrucks.Rows[index];
                    csv.AppendLine(string.Join(",",
                        index + 1,
                        CsvValue(row.Cells["TruckNo"].Value),
                        CsvValue(row.Cells["DriverName"].Value),
                        CsvValue(row.Cells["MobileNo"].Value)));
                }

                File.WriteAllText(dialog.FileName, csv.ToString(), Encoding.UTF8);
                lblStatus.Text = "Own truck list exported.";
            }
        }

        private static string CsvValue(object value)
        {
            string text = Convert.ToString(value) ?? string.Empty;
            return "\"" + text.Replace("\"", "\"\"") + "\"";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isSaving || !ValidateTruck())
                return;

            _isSaving = true;
            btnSave.Enabled = false;

            try
            {
                SaveTruck();
                LoadTrucks();
                ResetTruckForm();
                lblStatus.Text = "Own truck saved locally.";
                MessageBox.Show("Own truck saved successfully.", "Own Truck",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save own truck.\r\n" + ex.Message,
                    "Own Truck", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                _isSaving = false;
            }
        }

        private void SaveTruck()
        {
            string truckNo = txtTruckNo.Text.Trim();
            string driverName = txtDriverName.Text.Trim();
            string mobileNo = txtMobileNo.Text.Trim();

            using (SqlConnection connection = DbContext.GetConn())
            {
                connection.Open();

                const string duplicateQuery = @"
                    SELECT COUNT(1)
                    FROM Scale_Truck
                    WHERE UPPER(LTRIM(RTRIM(TruckNo))) = UPPER(@TruckNo)
                      AND TruckID <> ISNULL(@TruckID, 0)";

                using (SqlCommand duplicateCommand = new SqlCommand(duplicateQuery, connection))
                {
                    AddTextParameter(duplicateCommand, "@TruckNo", truckNo, 50);
                    duplicateCommand.Parameters.Add("@TruckID", SqlDbType.Int).Value =
                        (object)_editingTruckId ?? DBNull.Value;

                    if (Convert.ToInt32(duplicateCommand.ExecuteScalar()) > 0)
                        throw new InvalidOperationException("This truck number already exists.");
                }

                if (_editingTruckId.HasValue)
                {
                    const string updateQuery = @"
                        UPDATE Scale_Truck
                        SET TruckNo = @TruckNo,
                            DriverName = @DriverName,
                            MobileNo = @MobileNo
                        WHERE TruckID = @TruckID";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        AddTextParameter(command, "@TruckNo", truckNo, 50);
                        AddTextParameter(command, "@DriverName", driverName, 50);
                        AddTextParameter(command, "@MobileNo", mobileNo, 50);
                        command.Parameters.Add("@TruckID", SqlDbType.Int).Value = _editingTruckId.Value;
                        command.ExecuteNonQuery();
                    }

                    SavedTruckId = _editingTruckId.Value;
                }
                else
                {
                    const string insertQuery = @"
                        INSERT INTO Scale_Truck (TruckNo, DriverName, MobileNo)
                        VALUES (@TruckNo, @DriverName, @MobileNo);
                        SELECT CAST(SCOPE_IDENTITY() AS int);";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        AddTextParameter(command, "@TruckNo", truckNo, 50);
                        AddTextParameter(command, "@DriverName", driverName, 50);
                        AddTextParameter(command, "@MobileNo", mobileNo, 50);
                        SavedTruckId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
        }

        private static void AddTextParameter(SqlCommand command, string name, string value, int size)
        {
            command.Parameters.Add(name, SqlDbType.VarChar, size).Value =
                string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value;
        }

        private bool ValidateTruck()
        {
            if (string.IsNullOrWhiteSpace(txtTruckNo.Text))
            {
                MessageBox.Show("Please enter truck number.", "Own Truck",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTruckNo.Focus();
                return false;
            }

            if (txtTruckNo.Text.Trim().Length > 50 ||
                txtDriverName.Text.Trim().Length > 50 ||
                txtMobileNo.Text.Trim().Length > 50)
            {
                MessageBox.Show("Truck number, driver name, and mobile number cannot exceed 50 characters.",
                    "Own Truck", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void dgvTrucks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvTrucks.Columns[e.ColumnIndex].Name != "Edit")
                return;

            int truckId = Convert.ToInt32(dgvTrucks.Rows[e.RowIndex].Cells["TruckID"].Value);
            DataGridViewRow row = FindTruckRow(truckId);
            if (row == null)
                return;

            _editingTruckId = truckId;
            txtTruckNo.Text = Convert.ToString(row.Cells["TruckNo"].Value);
            txtDriverName.Text = Convert.ToString(row.Cells["DriverName"].Value);
            txtMobileNo.Text = Convert.ToString(row.Cells["MobileNo"].Value);
            btnSave.Text = "Update";
            txtTruckNo.Focus();
        }

        private DataGridViewRow FindTruckRow(int truckId)
        {
            foreach (DataGridViewRow row in dgvTrucks.Rows)
            {
                if (!row.IsNewRow && Convert.ToInt32(row.Cells["TruckID"].Value) == truckId)
                    return row;
            }

            return null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetTruckForm();
        }

        private void ResetTruckForm()
        {
            _editingTruckId = null;
            txtTruckNo.Clear();
            txtDriverName.Clear();
            txtMobileNo.Clear();
            btnSave.Text = "Save";
            txtTruckNo.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
