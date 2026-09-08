using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BuroNahodok
{
    public partial class SearchForm : Form
    {
        private Label lblTitle;
        private Label lblName;
        private Label lblCategory;
        private Label lblColor;
        private Label lblBrand;
        private Label lblPlace;
        private Label lblStatus;

        private TextBox txtName;
        private TextBox txtCategory;
        private TextBox txtColor;
        private TextBox txtBrand;
        private TextBox txtPlace;

        private ComboBox cmbStatus;

        private Button btnSearch;
        private Button btnClear;
        private Button btnClose;

        private DataGridView dgvResults;

        public SearchForm()
        {
            CreateInterface();
            LoadStatuses();
            SearchItems();
        }

        private void CreateInterface()
        {
            Text = "Бюро находок — Поиск найденных вещей";

            StartPosition =
                FormStartPosition.CenterScreen;

            FormBorderStyle =
                FormBorderStyle.FixedSingle;

            MaximizeBox = false;
            MinimizeBox = true;

            ClientSize =
                new Size(1150, 700);

            lblTitle = new Label();
            lblTitle.Text =
                "Поиск найденных вещей";
            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location =
                new Point(35, 20);
            Controls.Add(lblTitle);

            lblName = new Label();
            lblName.Text =
                "Название:";
            lblName.Font =
                new Font("Segoe UI", 10);
            lblName.AutoSize = true;
            lblName.Location =
                new Point(35, 80);
            Controls.Add(lblName);

            txtName = new TextBox();
            txtName.Font =
                new Font("Segoe UI", 10);
            txtName.Location =
                new Point(135, 75);
            txtName.Size =
                new Size(220, 30);
            Controls.Add(txtName);

            lblCategory = new Label();
            lblCategory.Text =
                "Категория:";
            lblCategory.Font =
                new Font("Segoe UI", 10);
            lblCategory.AutoSize = true;
            lblCategory.Location =
                new Point(380, 80);
            Controls.Add(lblCategory);

            txtCategory = new TextBox();
            txtCategory.Font =
                new Font("Segoe UI", 10);
            txtCategory.Location =
                new Point(480, 75);
            txtCategory.Size =
                new Size(220, 30);
            Controls.Add(txtCategory);

            lblColor = new Label();
            lblColor.Text =
                "Цвет:";
            lblColor.Font =
                new Font("Segoe UI", 10);
            lblColor.AutoSize = true;
            lblColor.Location =
                new Point(725, 80);
            Controls.Add(lblColor);

            txtColor = new TextBox();
            txtColor.Font =
                new Font("Segoe UI", 10);
            txtColor.Location =
                new Point(790, 75);
            txtColor.Size =
                new Size(220, 30);
            Controls.Add(txtColor);

            lblBrand = new Label();
            lblBrand.Text =
                "Бренд:";
            lblBrand.Font =
                new Font("Segoe UI", 10);
            lblBrand.AutoSize = true;
            lblBrand.Location =
                new Point(35, 125);
            Controls.Add(lblBrand);

            txtBrand = new TextBox();
            txtBrand.Font =
                new Font("Segoe UI", 10);
            txtBrand.Location =
                new Point(135, 120);
            txtBrand.Size =
                new Size(220, 30);
            Controls.Add(txtBrand);

            lblPlace = new Label();
            lblPlace.Text =
                "Место:";
            lblPlace.Font =
                new Font("Segoe UI", 10);
            lblPlace.AutoSize = true;
            lblPlace.Location =
                new Point(380, 125);
            Controls.Add(lblPlace);

            txtPlace = new TextBox();
            txtPlace.Font =
                new Font("Segoe UI", 10);
            txtPlace.Location =
                new Point(480, 120);
            txtPlace.Size =
                new Size(220, 30);
            Controls.Add(txtPlace);

            lblStatus = new Label();
            lblStatus.Text =
                "Статус:";
            lblStatus.Font =
                new Font("Segoe UI", 10);
            lblStatus.AutoSize = true;
            lblStatus.Location =
                new Point(725, 125);
            Controls.Add(lblStatus);

            cmbStatus = new ComboBox();
            cmbStatus.Font =
                new Font("Segoe UI", 10);
            cmbStatus.Location =
                new Point(790, 120);
            cmbStatus.Size =
                new Size(220, 30);
            cmbStatus.DropDownStyle =
                ComboBoxStyle.DropDownList;
            Controls.Add(cmbStatus);

            btnSearch = new Button();
            btnSearch.Text =
                "Найти";
            btnSearch.Font =
                new Font("Segoe UI", 10);
            btnSearch.Location =
                new Point(35, 175);
            btnSearch.Size =
                new Size(150, 45);
            btnSearch.Click +=
                BtnSearch_Click;
            Controls.Add(btnSearch);

            btnClear = new Button();
            btnClear.Text =
                "Очистить";
            btnClear.Font =
                new Font("Segoe UI", 10);
            btnClear.Location =
                new Point(200, 175);
            btnClear.Size =
                new Size(150, 45);
            btnClear.Click +=
                BtnClear_Click;
            Controls.Add(btnClear);

            dgvResults =
                new DataGridView();

            dgvResults.Location =
                new Point(35, 245);

            dgvResults.Size =
                new Size(1075, 350);

            dgvResults.ReadOnly = true;

            dgvResults.AllowUserToAddRows =
                false;

            dgvResults.AllowUserToDeleteRows =
                false;

            dgvResults.AllowUserToResizeRows =
                false;

            dgvResults.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvResults.MultiSelect =
                false;

            dgvResults.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvResults.RowHeadersVisible =
                false;

            dgvResults.BackgroundColor =
                Color.White;

            Controls.Add(dgvResults);

            btnClose = new Button();
            btnClose.Text =
                "Закрыть";
            btnClose.Font =
                new Font("Segoe UI", 10);
            btnClose.Location =
                new Point(960, 620);
            btnClose.Size =
                new Size(150, 45);
            btnClose.Click +=
                BtnClose_Click;
            Controls.Add(btnClose);

            AcceptButton =
                btnSearch;
        }

        private void LoadStatuses()
        {
            try
            {
                cmbStatus.Items.Clear();

                cmbStatus.Items.Add("Все статусы");

                using SqlConnection connection =
                    Database.GetConnection();

                connection.Open();

                string query = @"
                    SELECT StatusName
                    FROM Statuses
                    ORDER BY StatusId";

                using SqlCommand command =
                    new SqlCommand(
                        query,
                        connection);

                using SqlDataReader reader =
                    command.ExecuteReader();

                while (reader.Read())
                {
                    string status =
                        reader["StatusName"].ToString();

                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        cmbStatus.Items.Add(status);
                    }
                }

                cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки статусов:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("Все статусы");
                cmbStatus.SelectedIndex = 0;
            }
        }

        private void SearchItems()
        {
            try
            {
                using SqlConnection connection =
                    Database.GetConnection();

                connection.Open();

                string query = @"
                    SELECT
                        f.FoundItemId AS [ID],
                        f.ItemName AS [Название],
                        f.Category AS [Категория],
                        f.Description AS [Описание],
                        f.Color AS [Цвет],
                        f.Brand AS [Бренд],
                        f.FoundDate AS [Дата],
                        f.FoundTime AS [Время],
                        p.PlaceName AS [Место],
                        s.StatusName AS [Статус],
                        f.StorageLocation AS [Место хранения],
                        o.FullName AS [Владелец]
                    FROM FoundItems f
                    LEFT JOIN FindPlaces p
                        ON f.PlaceId = p.PlaceId
                    LEFT JOIN Statuses s
                        ON f.StatusId = s.StatusId
                    LEFT JOIN Owners o
                        ON f.OwnerId = o.OwnerId
                    WHERE
                        (
                            @ItemName = ''
                            OR f.ItemName LIKE '%' + @ItemName + '%'
                        )
                        AND
                        (
                            @Category = ''
                            OR f.Category LIKE '%' + @Category + '%'
                        )
                        AND
                        (
                            @Color = ''
                            OR f.Color LIKE '%' + @Color + '%'
                        )
                        AND
                        (
                            @Brand = ''
                            OR f.Brand LIKE '%' + @Brand + '%'
                        )
                        AND
                        (
                            @Place = ''
                            OR p.PlaceName LIKE '%' + @Place + '%'
                        )
                        AND
                        (
                            @Status = ''
                            OR s.StatusName = @Status
                        )
                    ORDER BY
                        f.FoundItemId DESC";

                using SqlCommand command =
                    new SqlCommand(
                        query,
                        connection);

                command.Parameters.Add(
                    "@ItemName",
                    SqlDbType.NVarChar,
                    200).Value =
                    txtName.Text.Trim();

                command.Parameters.Add(
                    "@Category",
                    SqlDbType.NVarChar,
                    200).Value =
                    txtCategory.Text.Trim();

                command.Parameters.Add(
                    "@Color",
                    SqlDbType.NVarChar,
                    100).Value =
                    txtColor.Text.Trim();

                command.Parameters.Add(
                    "@Brand",
                    SqlDbType.NVarChar,
                    200).Value =
                    txtBrand.Text.Trim();

                command.Parameters.Add(
                    "@Place",
                    SqlDbType.NVarChar,
                    200).Value =
                    txtPlace.Text.Trim();

                string selectedStatus = "";

                if (cmbStatus.SelectedIndex > 0)
                {
                    selectedStatus =
                        cmbStatus.SelectedItem.ToString();
                }

                command.Parameters.Add(
                    "@Status",
                    SqlDbType.NVarChar,
                    100).Value =
                    selectedStatus;

                using SqlDataAdapter adapter =
                    new SqlDataAdapter(command);

                DataTable table =
                    new DataTable();

                adapter.Fill(table);

                dgvResults.DataSource = null;

                dgvResults.DataSource = table;

                lblTitle.Text =
                    "Поиск найденных вещей — найдено: " +
                    table.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка поиска:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(
            object sender,
            EventArgs e)
        {
            SearchItems();
        }

        private void BtnClear_Click(
            object sender,
            EventArgs e)
        {
            txtName.Clear();
            txtCategory.Clear();
            txtColor.Clear();
            txtBrand.Clear();
            txtPlace.Clear();

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }

            SearchItems();

            txtName.Focus();
        }

        private void BtnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}