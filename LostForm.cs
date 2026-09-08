using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BuroNahodok
{
    public partial class LostForm : Form
    {
        private Label lblTitle;
        private Label lblInfo;

        private DataGridView dgvLostReports;

        private Button btnRefresh;
        private Button btnDelete;
        private Button btnClose;

        public LostForm()
        {
            CreateInterface();
            LoadLostReports();
        }

        private void CreateInterface()
        {
            Text = "Бюро находок — Заявления о пропаже";

            StartPosition =
                FormStartPosition.CenterScreen;

            FormBorderStyle =
                FormBorderStyle.FixedSingle;

            MaximizeBox = false;
            MinimizeBox = true;

            ClientSize =
                new Size(1150, 650);

            lblTitle = new Label();

            lblTitle.Text =
                "Список заявлений о пропаже";

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold);

            lblTitle.AutoSize = true;

            lblTitle.Location =
                new Point(40, 25);

            Controls.Add(lblTitle);

            lblInfo = new Label();

            lblInfo.Text =
                "Загрузка данных...";

            lblInfo.Font =
                new Font("Segoe UI", 10);

            lblInfo.AutoSize = true;

            lblInfo.Location =
                new Point(40, 70);

            Controls.Add(lblInfo);

            dgvLostReports =
                new DataGridView();

            dgvLostReports.Location =
                new Point(40, 105);

            dgvLostReports.Size =
                new Size(1070, 400);

            dgvLostReports.ReadOnly = true;

            dgvLostReports.AllowUserToAddRows =
                false;

            dgvLostReports.AllowUserToDeleteRows =
                false;

            dgvLostReports.AllowUserToResizeRows =
                false;

            dgvLostReports.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLostReports.MultiSelect = false;

            dgvLostReports.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvLostReports.RowHeadersVisible = false;

            dgvLostReports.BackgroundColor =
                Color.White;

            Controls.Add(dgvLostReports);

            btnRefresh = new Button();

            btnRefresh.Text =
                "Обновить";

            btnRefresh.Font =
                new Font("Segoe UI", 10);

            btnRefresh.Location =
                new Point(40, 555);

            btnRefresh.Size =
                new Size(150, 45);

            btnRefresh.Click +=
                BtnRefresh_Click;

            Controls.Add(btnRefresh);

            btnDelete = new Button();

            btnDelete.Text =
                "Удалить заявление";

            btnDelete.Font =
                new Font("Segoe UI", 10);

            btnDelete.Location =
                new Point(210, 555);

            btnDelete.Size =
                new Size(180, 45);

            btnDelete.Click +=
                BtnDelete_Click;

            Controls.Add(btnDelete);

            btnClose = new Button();

            btnClose.Text =
                "Закрыть";

            btnClose.Font =
                new Font("Segoe UI", 10);

            btnClose.Location =
                new Point(960, 555);

            btnClose.Size =
                new Size(150, 45);

            btnClose.Click +=
                BtnClose_Click;

            Controls.Add(btnClose);
        }

        private void LoadLostReports()
        {
            try
            {
                using SqlConnection connection =
                    Database.GetConnection();

                connection.Open();

                string query = @"
                    SELECT
                        l.LostReportId AS [ID],
                        o.FullName AS [Владелец],
                        o.Phone AS [Телефон],
                        l.ItemName AS [Название],
                        l.Category AS [Категория],
                        l.Description AS [Описание],
                        l.Color AS [Цвет],
                        l.Brand AS [Бренд],
                        l.LostDate AS [Дата пропажи],
                        l.LostPlace AS [Место пропажи],
                        l.Status AS [Статус],
                        l.CreatedAt AS [Дата заявления]
                    FROM LostReports l
                    LEFT JOIN Owners o
                        ON l.OwnerId = o.OwnerId
                    ORDER BY
                        l.LostReportId DESC";

                using SqlCommand command =
                    new SqlCommand(
                        query,
                        connection);

                using SqlDataAdapter adapter =
                    new SqlDataAdapter(command);

                DataTable table =
                    new DataTable();

                adapter.Fill(table);

                dgvLostReports.DataSource = null;

                dgvLostReports.DataSource = table;

                lblInfo.Text =
                    "Всего заявлений: " +
                    table.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки заявлений о пропаже:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(
            object sender,
            EventArgs e)
        {
            LoadLostReports();
        }

        private void BtnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (dgvLostReports.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Сначала выберите заявление.",
                    "Удаление",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow row =
                dgvLostReports.SelectedRows[0];

            if (row.Cells.Count == 0)
            {
                MessageBox.Show(
                    "Не удалось определить выбранное заявление.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            object idValue =
                row.Cells[0].Value;

            if (idValue == null)
            {
                MessageBox.Show(
                    "Не удалось определить ID заявления.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            int lostReportId =
                Convert.ToInt32(idValue);

            string itemName = "";

            if (row.Cells.Count > 3 &&
                row.Cells[3].Value != null)
            {
                itemName =
                    row.Cells[3].Value.ToString();
            }

            string ownerName = "";

            if (row.Cells.Count > 1 &&
                row.Cells[1].Value != null)
            {
                ownerName =
                    row.Cells[1].Value.ToString();
            }

            DialogResult result =
                MessageBox.Show(
                    "Удалить заявление о пропаже?\n\n" +
                    "Владелец: " +
                    ownerName +
                    "\nВещь: " +
                    itemName,
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using SqlConnection connection =
                    Database.GetConnection();

                connection.Open();

                string query = @"
                    DELETE FROM LostReports
                    WHERE LostReportId = @LostReportId";

                using SqlCommand command =
                    new SqlCommand(
                        query,
                        connection);

                command.Parameters.Add(
                    "@LostReportId",
                    System.Data.SqlDbType.Int).Value =
                    lostReportId;

                int affectedRows =
                    command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show(
                        "Заявление успешно удалено.",
                        "Удаление",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadLostReports();
                }
                else
                {
                    MessageBox.Show(
                        "Заявление не найдено.",
                        "Удаление",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка удаления заявления:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}