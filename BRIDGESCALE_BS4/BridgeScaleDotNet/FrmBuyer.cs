using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BridgeScaleDotNet
{
    public partial class FrmBuyer : Form
    {
        private const int BuyerClientTypeId = 2;
        private int? _editingBuyerId;
        private bool _isSaving;
        private DataTable _allBuyers;

        public int? SavedBuyerId { get; private set; }

        public FrmBuyer()
        {
            InitializeComponent();
            LoadBuyerLogo();
            LoadBuyers();
        }

        private void LoadBuyerLogo()
        {
            string logoPath = Path.Combine(Application.StartupPath, "images", "btlpng.png");
            if (!File.Exists(logoPath))
                return;

            using (Image source = Image.FromFile(logoPath))
            {
                picLogo.Image = new Bitmap(source);
            }
        }

        private void LoadBuyers()
        {
            const string query = @"
                SELECT ClientID, C_name, Address
                FROM Scale_Clint
                WHERE ClientTypeID = 2
                  AND ISNULL(IsDelete, 0) = 0
                ORDER BY ClientID DESC";

            try
            {
                DataTable buyers = DbContext.Execute(query);
                _allBuyers = buyers != null ? buyers.Copy() : new DataTable();
                dgvBuyers.DataSource = null;
                ConfigureGrid();
                dgvBuyers.DataSource = _allBuyers;
                UpdateTotalLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load buyers.\r\n" + ex.Message,
                    "Buyer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            dgvBuyers.AutoGenerateColumns = false;
            dgvBuyers.Columns.Clear();

            dgvBuyers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SL",
                HeaderText = "SL",
                Width = 58,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            dgvBuyers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ClientID",
                DataPropertyName = "ClientID",
                Visible = false
            });

            dgvBuyers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "C_name",
                DataPropertyName = "C_name",
                HeaderText = "Buyer Name",
                Width = 320
            });

            dgvBuyers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                DataPropertyName = "Address",
                HeaderText = "Address",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvBuyers.Columns.Add(new DataGridViewButtonColumn
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
                    Padding = new Padding(6, 3, 6, 3),
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
                }
            });

            dgvBuyers.CellFormatting -= dgvBuyers_CellFormatting;
            dgvBuyers.CellFormatting += dgvBuyers_CellFormatting;
        }

        private void dgvBuyers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string columnName = dgvBuyers.Columns[e.ColumnIndex].Name;
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
            if (_allBuyers == null)
                return;

            string searchText = txtSearch.Text.Trim().Replace("'", "''");
            DataView view = _allBuyers.DefaultView;
            view.RowFilter = string.IsNullOrWhiteSpace(searchText)
                ? string.Empty
                : $"C_name LIKE '%{searchText}%' OR Address LIKE '%{searchText}%'";

            dgvBuyers.DataSource = view.ToTable();
            UpdateTotalLabel();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadBuyers();
            lblStatus.Text = "Buyer list refreshed.";
        }

        private void UpdateTotalLabel()
        {
            lblTotal.Text = "Total: " + dgvBuyers.Rows.Count + " Buyers";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV file (*.csv)|*.csv";
                dialog.FileName = "Buyers.csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                StringBuilder csv = new StringBuilder();
                csv.AppendLine("SL,Buyer Name,Address");

                for (int index = 0; index < dgvBuyers.Rows.Count; index++)
                {
                    DataGridViewRow row = dgvBuyers.Rows[index];
                    csv.AppendLine(string.Join(",",
                        index + 1,
                        CsvValue(row.Cells["C_name"].Value),
                        CsvValue(row.Cells["Address"].Value)));
                }

                File.WriteAllText(dialog.FileName, csv.ToString(), Encoding.UTF8);
                lblStatus.Text = "Buyer list exported.";
            }
        }

        private static string CsvValue(object value)
        {
            string text = Convert.ToString(value) ?? string.Empty;
            return "\"" + text.Replace("\"", "\"\"") + "\"";
        }

        private void dgvBuyers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isSaving || !ValidateBuyer())
                return;

            _isSaving = true;
            btnSave.Enabled = false;

            try
            {
                SaveBuyer();
                LoadBuyers();
                ResetBuyerForm();
                lblStatus.Text = "Buyer saved locally.";
                MessageBox.Show("Buyer saved successfully.", "Buyer",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save buyer.\r\n" + ex.Message,
                    "Buyer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                _isSaving = false;
            }
        }

        private void SaveBuyer()
        {
            string name = txtBuyerName.Text.Trim();
            string address = txtBuyerAddress.Text.Trim();

            using (SqlConnection connection = DbContext.GetConn())
            {
                connection.Open();

                const string duplicateQuery = @"
                    SELECT COUNT(1)
                    FROM Scale_Clint
                    WHERE ClientTypeID = 2
                      AND ISNULL(IsDelete, 0) = 0
                      AND UPPER(LTRIM(RTRIM(C_name))) = UPPER(@C_name)
                      AND ClientID <> ISNULL(@ClientID, 0)";

                using (SqlCommand duplicateCommand = new SqlCommand(duplicateQuery, connection))
                {
                    AddUnicodeTextParameter(duplicateCommand, "@C_name", name, 100);
                    duplicateCommand.Parameters.Add("@ClientID", SqlDbType.Int).Value =
                        (object)_editingBuyerId ?? DBNull.Value;

                    if (Convert.ToInt32(duplicateCommand.ExecuteScalar()) > 0)
                        throw new InvalidOperationException("This buyer already exists.");
                }

                if (_editingBuyerId.HasValue)
                {
                    const string updateQuery = @"
                        UPDATE Scale_Clint
                        SET C_name = @C_name,
                            Clint_type = 'BUYER',
                            ClientTypeID = @ClientTypeID,
                            Address = @Address
                        WHERE ClientID = @ClientID
                          AND ClientTypeID = 2
                          AND ISNULL(IsDelete, 0) = 0";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        AddUnicodeTextParameter(command, "@C_name", name, 100);
                        AddUnicodeTextParameter(command, "@Address", address, 250);
                        command.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = BuyerClientTypeId;
                        command.Parameters.Add("@ClientID", SqlDbType.Int).Value = _editingBuyerId.Value;
                        command.ExecuteNonQuery();
                    }

                    SavedBuyerId = _editingBuyerId.Value;
                }
                else
                {
                    const string insertQuery = @"
                        INSERT INTO Scale_Clint (C_name, Clint_type, ClientTypeID, Address, IsDelete)
                        VALUES (@C_name, 'BUYER', @ClientTypeID, @Address, 0);
                        SELECT CAST(SCOPE_IDENTITY() AS int);";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        AddUnicodeTextParameter(command, "@C_name", name, 100);
                        AddUnicodeTextParameter(command, "@Address", address, 250);
                        command.Parameters.Add("@ClientTypeID", SqlDbType.Int).Value = BuyerClientTypeId;
                        SavedBuyerId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
        }

        private static void AddUnicodeTextParameter(SqlCommand command, string name, string value, int size)
        {
            command.Parameters.Add(name, SqlDbType.NVarChar, size).Value =
                (object)value ?? DBNull.Value;
        }

        private bool ValidateBuyer()
        {
            if (string.IsNullOrWhiteSpace(txtBuyerName.Text))
            {
                MessageBox.Show("Please enter buyer name.", "Buyer",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuyerName.Focus();
                return false;
            }

            if (txtBuyerName.Text.Trim().Length > 100)
            {
                MessageBox.Show("Buyer name cannot exceed 100 characters.", "Buyer",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuyerName.Focus();
                return false;
            }

            return true;
        }

        private void dgvBuyers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvBuyers.Columns[e.ColumnIndex].Name != "Edit")
                return;

            int buyerId = Convert.ToInt32(dgvBuyers.Rows[e.RowIndex].Cells["ClientID"].Value);
            DataGridViewRow row = FindBuyerRow(buyerId);
            if (row == null)
                return;

            _editingBuyerId = buyerId;
            txtBuyerName.Text = Convert.ToString(row.Cells["C_name"].Value);
            txtBuyerAddress.Text = Convert.ToString(row.Cells["Address"].Value);
            btnSave.Text = "Update";
            txtBuyerName.Focus();
        }

        private DataGridViewRow FindBuyerRow(int buyerId)
        {
            foreach (DataGridViewRow row in dgvBuyers.Rows)
            {
                if (!row.IsNewRow && Convert.ToInt32(row.Cells["ClientID"].Value) == buyerId)
                    return row;
            }

            return null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetBuyerForm();
        }

        private void ResetBuyerForm()
        {
            _editingBuyerId = null;
            txtBuyerName.Clear();
            txtBuyerAddress.Clear();
            btnSave.Text = "Save";
            txtBuyerName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
