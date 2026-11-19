using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BridgeScaleDotNet
{
    public partial class FrmSupplier : Form
    {
        private const int SupplierClientTypeId = 1;
        private int? _editingSupplierId;
        private bool _isSaving;
        private DataTable _allSuppliers;

        public int? SavedSupplierId { get; private set; }

        public FrmSupplier()
        {
            InitializeComponent();
            LoadSupplierLogo();
            LoadSuppliers();
        }

        private void LoadSupplierLogo()
        {
            string logoPath = Path.Combine(Application.StartupPath, "images", "btlpng.png");
            if (!File.Exists(logoPath))
                return;

            using (Image source = Image.FromFile(logoPath))
            {
                picLogo.Image = new Bitmap(source);
            }
        }

        private void LoadSuppliers()
        {
            const string query = @"
                SELECT ClientID, C_name, Address
                FROM Scale_Clint
                WHERE ClientTypeID = 1
                  AND ISNULL(IsDelete, 0) = 0
                ORDER BY ClientID DESC";

            try
            {
                DataTable suppliers = DbContext.Execute(query);
                _allSuppliers = suppliers != null ? suppliers.Copy() : new DataTable();
                dgvSuppliers.DataSource = null;
                ConfigureGrid();
                dgvSuppliers.DataSource = _allSuppliers;
                UpdateTotalLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load suppliers.\r\n" + ex.Message,
                    "Supplier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            dgvSuppliers.AutoGenerateColumns = false;
            dgvSuppliers.Columns.Clear();

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SL",
                HeaderText = "SL",
                Width = 58,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ClientID",
                DataPropertyName = "ClientID",
                Visible = false
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "C_name",
                DataPropertyName = "C_name",
                HeaderText = "Supplier Name",
                Width = 320
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                DataPropertyName = "Address",
                HeaderText = "Address",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvSuppliers.Columns.Add(new DataGridViewButtonColumn
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

            dgvSuppliers.CellFormatting -= dgvSuppliers_CellFormatting;
            dgvSuppliers.CellFormatting += dgvSuppliers_CellFormatting;
        }

        private void dgvSuppliers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string columnName = dgvSuppliers.Columns[e.ColumnIndex].Name;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allSuppliers == null)
                return;

            string searchText = txtSearch.Text.Trim().Replace("'", "''");
            DataView view = _allSuppliers.DefaultView;
            view.RowFilter = string.IsNullOrWhiteSpace(searchText)
                ? string.Empty
                : $"C_name LIKE '%{searchText}%' OR Address LIKE '%{searchText}%'";

            dgvSuppliers.DataSource = view.ToTable();
            UpdateTotalLabel();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadSuppliers();
            lblStatus.Text = "Supplier list refreshed.";
        }

        private void UpdateTotalLabel()
        {
            lblTotal.Text = "Total: " + dgvSuppliers.Rows.Count + " Suppliers";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV file (*.csv)|*.csv";
                dialog.FileName = "Suppliers.csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                StringBuilder csv = new StringBuilder();
                csv.AppendLine("SL,Supplier Name,Address");

                for (int index = 0; index < dgvSuppliers.Rows.Count; index++)
                {
                    DataGridViewRow row = dgvSuppliers.Rows[index];
                    csv.AppendLine(string.Join(",",
                        index + 1,
                        CsvValue(row.Cells["C_name"].Value),
                        CsvValue(row.Cells["Address"].Value)));
                }

                File.WriteAllText(dialog.FileName, csv.ToString(), Encoding.UTF8);
                lblStatus.Text = "Supplier list exported.";
            }
        }

        private static string CsvValue(object value)
        {
            string text = Convert.ToString(value) ?? string.Empty;
            return "\"" + text.Replace("\"", "\"\"") + "\"";
        }

        private void dgvSuppliers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isSaving || !ValidateSupplier())
                return;

            _isSaving = true;
            btnSave.Enabled = false;

            try
            {
                SaveSupplier();
                LoadSuppliers();
                ResetSupplierForm();
                lblStatus.Text = "Supplier saved locally.";
                MessageBox.Show("Supplier saved successfully.", "Supplier",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save supplier.\r\n" + ex.Message,
                    "Supplier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                _isSaving = false;
            }
        }

        private void SaveSupplier()
        {
            string name = txtSupplierName.Text.Trim();
            string address = txtSupplierAddress.Text.Trim();

            using (SqlConnection connection = DbContext.GetConn())
            {
                connection.Open();

                const string duplicateQuery = @"
                    SELECT COUNT(1)
                    FROM Scale_Clint
                    WHERE ClientTypeID = 1
                      AND ISNULL(IsDelete, 0) = 0
                      AND UPPER(LTRIM(RTRIM(C_name))) = UPPER(@C_name)
                      AND ClientID <> ISNULL(@ClientID, 0)";

                using (SqlCommand duplicateCommand = new SqlCommand(duplicateQuery, connection))
                {
                    AddUnicodeTextParameter(duplicateCommand, "@C_name", name, 100);
                    duplicateCommand.Parameters.Add("@ClientID", SqlDbType.Int).Value =
                        (object)_editingSupplierId ?? DBNull.Value;

                    if (Convert.ToInt32(duplicateCommand.ExecuteScalar()) > 0)
                        throw new InvalidOperationException("This supplier already exists.");
                }

                if (_editingSupplierId.HasValue)
                {
                    const string updateQuery = @"
                        UPDATE Scale_Clint
                        SET C_name = @C_name,
                            Clint_type = 'SUPPLIER',
                            ClientTypeID = @ClientTypeID,
                            Address = @Address
                        WHERE ClientID = @ClientID
                          AND ClientTypeID = 1
                          AND ISNULL(IsDelete, 0) = 0";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        AddUnicodeTextParameter(command, "@C_name", name, 100);
                        AddUnicodeTextParameter(command, "@Address", address, 250);
                        command.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = SupplierClientTypeId;
                        command.Parameters.Add("@ClientID", SqlDbType.Int).Value = _editingSupplierId.Value;
                        command.ExecuteNonQuery();
                    }

                    SavedSupplierId = _editingSupplierId.Value;
                }
                else
                {
                    const string insertQuery = @"
                        INSERT INTO Scale_Clint (C_name, Clint_type, ClientTypeID, Address, IsDelete)
                        VALUES (@C_name, 'SUPPLIER', @ClientTypeID, @Address, 0);
                        SELECT CAST(SCOPE_IDENTITY() AS int);";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        AddUnicodeTextParameter(command, "@C_name", name, 100);
                        AddUnicodeTextParameter(command, "@Address", address, 250);
                        command.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = SupplierClientTypeId;
                        SavedSupplierId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
        }

        private static void AddUnicodeTextParameter(SqlCommand command, string name, string value, int size)
        {
            command.Parameters.Add(name, SqlDbType.NVarChar, size).Value =
                (object)value ?? DBNull.Value;
        }

        private bool ValidateSupplier()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Please enter supplier name.", "Supplier",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            if (txtSupplierName.Text.Trim().Length > 100)
            {
                MessageBox.Show("Supplier name cannot exceed 100 characters.", "Supplier",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            return true;
        }

        private void dgvSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvSuppliers.Columns[e.ColumnIndex].Name != "Edit")
                return;

            int supplierId = Convert.ToInt32(
                dgvSuppliers.Rows[e.RowIndex].Cells["ClientID"].Value);
            DataGridViewRow row = FindSupplierRow(supplierId);
            if (row == null)
                return;

            _editingSupplierId = supplierId;
            txtSupplierName.Text = Convert.ToString(row.Cells["C_name"].Value);
            txtSupplierAddress.Text = Convert.ToString(row.Cells["Address"].Value);
            btnSave.Text = "Update";
            txtSupplierName.Focus();
        }

        private DataGridViewRow FindSupplierRow(int supplierId)
        {
            foreach (DataGridViewRow row in dgvSuppliers.Rows)
            {
                if (!row.IsNewRow && Convert.ToInt32(row.Cells["ClientID"].Value) == supplierId)
                    return row;
            }

            return null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetSupplierForm();
        }

        private void ResetSupplierForm()
        {
            _editingSupplierId = null;
            txtSupplierName.Clear();
            txtSupplierAddress.Clear();
            btnSave.Text = "Save";
            txtSupplierName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
