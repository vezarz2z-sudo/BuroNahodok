using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BuroNahodok
{
    public partial class FoundForm : Form
    {
        private Label lblTitle;
        private Label lblInfo;

        private DataGridView dgvFoundItems;

        private Button btnRefresh;
        private Button btnIssue;
        private Button btnClose;

        public FoundForm()
        {
            CreateInterface();
            LoadFoundItems();
        }

        private void CreateInterface()
        {
            Text = "Бюро находок — Найденные вещи";

            StartPosition =
                FormStartPosition.CenterScreen;

            FormBorderStyle =
                FormBorderStyle.FixedSingle;

            MaximizeBox = false;
            MinimizeBox = true;

            ClientSize =
                new Size(1100, 650);

            lblTitle = new Label();

            lblTitle.Text =
                "Список найденных вещей";

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

            dgvFoundItems =
                new DataGridView();

            dgvFoundItems.Location =
                new Point(40, 105);

            dgvFoundItems.Size =
                new Size(1035, 400);

            dgvFoundItems.ReadOnly = true;

            dgvFoundItems.AllowUserToAddRows =
                false;

            dgvFoundItems.AllowUserToDeleteRows =
                false;

            dgvFoundItems.AllowUserToResizeRows =
                false;

            dgvFoundItems.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvFoundItems.MultiSelect = false;

            dgvFoundItems.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvFoundItems.RowHeadersVisible = false;

            dgvFoundItems.BackgroundColor =
                Color.White;

            Controls.Add(dgvFoundItems);

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

            btnIssue = new Button();

            btnIssue.Text =
                "Выдать владельцу";

            btnIssue.Font =
                new Font("Segoe UI", 10);

            btnIssue.Location =
                new Point(210, 555);

            btnIssue.Size =
                new Size(180, 45);

            btnIssue.Click +=
                BtnIssue_Click;

            Controls.Add(btnIssue);

            btnClose = new Button();

            btnClose.Text =
                "Закрыть";

            btnClose.Font =
                new Font("Segoe UI", 10);

            btnClose.Location =
                new Point(925, 555);

            btnClose.Size =
                new Size(150, 45);

            btnClose.Click +=
                BtnClose_Click;

            Controls.Add(btnClose);
        }

        private void LoadFoundItems()
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
                        f.StorageLocation AS [Место хранения]
                    FROM FoundItems f
                    LEFT JOIN FindPlaces p
                        ON f.PlaceId = p.PlaceId
                    LEFT JOIN Statuses s
                        ON f.StatusId = s.StatusId
                    ORDER BY f.FoundItemId DESC";

                using SqlCommand command =
                    new SqlCommand(
                        query,
                        connection);

                using SqlDataAdapter adapter =
                    new SqlDataAdapter(command);

                DataTable table =
                    new DataTable();

                adapter.Fill(table);

                dgvFoundItems.DataSource = null;

                dgvFoundItems.DataSource = table;

                lblInfo.Text =
                    "Всего найденных вещей: " +
                    table.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки найденных вещей:\n\n" +
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
            LoadFoundItems();
        }

        private void BtnIssue_Click(
            object sender,
            EventArgs e)
        {
            if (dgvFoundItems.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Сначала выберите найденную вещь.",
                    "Выдача вещи",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow row =
                dgvFoundItems.SelectedRows[0];

            if (row.Cells.Count == 0)
            {
                MessageBox.Show(
                    "Не удалось получить данные выбранной вещи.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            int foundItemId = 0;

            object idValue =
                row.Cells[0].Value;

            if (idValue == null)
            {
                MessageBox.Show(
                    "Не удалось определить ID найденной вещи.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            foundItemId =
                Convert.ToInt32(idValue);

            string itemName = "";

            if (row.Cells.Count > 1)
            {
                if (row.Cells[1].Value != null)
                {
                    itemName =
                        row.Cells[1].Value.ToString();
                }
            }

            string status = "";

            if (row.Cells.Count > 9)
            {
                if (row.Cells[9].Value != null)
                {
                    status =
                        row.Cells[9].Value.ToString();
                }
            }

            if (status == "Выдана")
            {
                MessageBox.Show(
                    "Эта вещь уже выдана владельцу.",
                    "Выдача вещи",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using IssueForm issueForm =
                new IssueForm(
                    foundItemId,
                    itemName);

            DialogResult result =
                issueForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadFoundItems();
            }
        }

        private void BtnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }

    public class IssueForm : Form
    {
        private int foundItemId;
        private string itemName;

        private Label lblTitle;
        private Label lblItem;
        private Label lblOwner;
        private Label lblPhone;
        private Label lblDocument;
        private Label lblComment;

        private ComboBox cmbOwner;
        private TextBox txtPhone;
        private TextBox txtDocument;
        private TextBox txtComment;

        private Button btnSave;
        private Button btnCancel;

        public IssueForm(
            int foundItemId,
            string itemName)
        {
            this.foundItemId =
                foundItemId;

            this.itemName =
                itemName;

            CreateInterface();

            LoadOwners();
        }

        private void CreateInterface()
        {
            Text =
                "Бюро находок — Выдача вещи";

            StartPosition =
                FormStartPosition.CenterScreen;

            FormBorderStyle =
                FormBorderStyle.FixedSingle;

            MaximizeBox = false;
            MinimizeBox = false;

            ClientSize =
                new Size(650, 500);

            lblTitle = new Label();

            lblTitle.Text =
                "Оформление выдачи найденной вещи";

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    17,
                    FontStyle.Bold);

            lblTitle.AutoSize = true;

            lblTitle.Location =
                new Point(25, 25);

            Controls.Add(lblTitle);

            lblItem = new Label();

            lblItem.Text =
                "Вещь: " + itemName;

            lblItem.Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold);

            lblItem.AutoSize = true;

            lblItem.Location =
                new Point(25, 75);

            Controls.Add(lblItem);

            lblOwner = new Label();

            lblOwner.Text =
                "Владелец:";

            lblOwner.Font =
                new Font(
                    "Segoe UI",
                    10);

            lblOwner.AutoSize = true;

            lblOwner.Location =
                new Point(25, 125);

            Controls.Add(lblOwner);

            cmbOwner = new ComboBox();

            cmbOwner.Font =
                new Font(
                    "Segoe UI",
                    10);

            cmbOwner.Location =
                new Point(180, 120);

            cmbOwner.Size =
                new Size(400, 30);

            cmbOwner.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbOwner.SelectedIndexChanged +=
                CmbOwner_SelectedIndexChanged;

            Controls.Add(cmbOwner);

            lblPhone = new Label();

            lblPhone.Text =
                "Телефон:";

            lblPhone.Font =
                new Font(
                    "Segoe UI",
                    10);

            lblPhone.AutoSize = true;

            lblPhone.Location =
                new Point(25, 175);

            Controls.Add(lblPhone);

            txtPhone = new TextBox();

            txtPhone.Font =
                new Font(
                    "Segoe UI",
                    10);

            txtPhone.Location =
                new Point(180, 170);

            txtPhone.Size =
                new Size(400, 30);

            txtPhone.ReadOnly = true;

            Controls.Add(txtPhone);

            lblDocument = new Label();

            lblDocument.Text =
                "№ документа:";

            lblDocument.Font =
                new Font(
                    "Segoe UI",
                    10);

            lblDocument.AutoSize = true;

            lblDocument.Location =
                new Point(25, 225);

            Controls.Add(lblDocument);

            txtDocument = new TextBox();

            txtDocument.Font =
                new Font(
                    "Segoe UI",
                    10);

            txtDocument.Location =
                new Point(180, 220);

            txtDocument.Size =
                new Size(400, 30);

            Controls.Add(txtDocument);

            lblComment = new Label();

            lblComment.Text =
                "Комментарий:";

            lblComment.Font =
                new Font(
                    "Segoe UI",
                    10);

            lblComment.AutoSize = true;

            lblComment.Location =
                new Point(25, 275);

            Controls.Add(lblComment);

            txtComment = new TextBox();

            txtComment.Font =
                new Font(
                    "Segoe UI",
                    10);

            txtComment.Location =
                new Point(180, 270);

            txtComment.Size =
                new Size(400, 70);

            txtComment.Multiline = true;

            txtComment.ScrollBars =
                ScrollBars.Vertical;

            Controls.Add(txtComment);

            btnSave = new Button();

            btnSave.Text =
                "Оформить выдачу";

            btnSave.Font =
                new Font(
                    "Segoe UI",
                    10);

            btnSave.Location =
                new Point(300, 390);

            btnSave.Size =
                new Size(180, 45);

            btnSave.Click +=
                BtnSave_Click;

            Controls.Add(btnSave);

            btnCancel = new Button();

            btnCancel.Text =
                "Отмена";

            btnCancel.Font =
                new Font(
                    "Segoe UI",
                    10);

            btnCancel.Location =
                new Point(490, 390);

            btnCancel.Size =
                new Size(90, 45);

            btnCancel.Click +=
                BtnCancel_Click;

            Controls.Add(btnCancel);
        }

        private void LoadOwners()
        {
            try
            {
                using SqlConnection connection =
                    Database.GetConnection();

                connection.Open();

                string query = @"
                    SELECT
                        OwnerId,
                        FullName,
                        Phone
                    FROM Owners
                    ORDER BY FullName";

                using SqlCommand command =
                    new SqlCommand(
                        query,
                        connection);

                using SqlDataReader reader =
                    command.ExecuteReader();

                while (reader.Read())
                {
                    OwnerItem owner =
                        new OwnerItem();

                    owner.OwnerId =
                        Convert.ToInt32(
                            reader["OwnerId"]);

                    owner.FullName =
                        reader["FullName"].ToString();

                    owner.Phone =
                        reader["Phone"].ToString();

                    cmbOwner.Items.Add(owner);
                }

                if (cmbOwner.Items.Count > 0)
                {
                    cmbOwner.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка загрузки владельцев:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CmbOwner_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbOwner.SelectedItem == null)
            {
                txtPhone.Clear();
                return;
            }

            OwnerItem owner =
                (OwnerItem)cmbOwner.SelectedItem;

            txtPhone.Text =
                owner.Phone;
        }

        private void BtnSave_Click(
            object sender,
            EventArgs e)
        {
            if (cmbOwner.SelectedItem == null)
            {
                MessageBox.Show(
                    "Выберите владельца.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            OwnerItem owner =
                (OwnerItem)cmbOwner.SelectedItem;

            string documentNumber =
                txtDocument.Text.Trim();

            string comment =
                txtComment.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                documentNumber))
            {
                MessageBox.Show(
                    "Введите номер документа.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtDocument.Focus();

                return;
            }

            DialogResult confirm =
                MessageBox.Show(
                    "Оформить выдачу вещи \"" +
                    itemName +
                    "\" владельцу " +
                    owner.FullName +
                    "?",
                    "Подтверждение выдачи",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using SqlConnection connection =
                    Database.GetConnection();

                connection.Open();

                SqlTransaction transaction =
                    connection.BeginTransaction();

                try
                {
                    int employeeId =
                        GetEmployeeId(
                            connection,
                            transaction);

                    int statusId =
                        GetIssuedStatusId(
                            connection,
                            transaction);

                    string updateItemQuery = @"
                        UPDATE FoundItems
                        SET
                            OwnerId = @OwnerId,
                            StatusId = @StatusId
                        WHERE FoundItemId = @FoundItemId";

                    using SqlCommand updateItemCommand =
                        new SqlCommand(
                            updateItemQuery,
                            connection,
                            transaction);

                    updateItemCommand.Parameters.AddWithValue(
                        "@OwnerId",
                        owner.OwnerId);

                    updateItemCommand.Parameters.AddWithValue(
                        "@StatusId",
                        statusId);

                    updateItemCommand.Parameters.AddWithValue(
                        "@FoundItemId",
                        foundItemId);

                    int updatedRows =
                        updateItemCommand.ExecuteNonQuery();

                    if (updatedRows == 0)
                    {
                        throw new Exception(
                            "Найденная вещь не найдена.");
                    }

                    string insertIssueQuery = @"
                        INSERT INTO ItemIssues
                        (
                            FoundItemId,
                            OwnerId,
                            EmployeeId,
                            IssueDate,
                            DocumentNumber,
                            Comment
                        )
                        VALUES
                        (
                            @FoundItemId,
                            @OwnerId,
                            @EmployeeId,
                            GETDATE(),
                            @DocumentNumber,
                            @Comment
                        )";

                    using SqlCommand insertIssueCommand =
                        new SqlCommand(
                            insertIssueQuery,
                            connection,
                            transaction);

                    insertIssueCommand.Parameters.AddWithValue(
                        "@FoundItemId",
                        foundItemId);

                    insertIssueCommand.Parameters.AddWithValue(
                        "@OwnerId",
                        owner.OwnerId);

                    if (employeeId > 0)
                    {
                        insertIssueCommand.Parameters.AddWithValue(
                            "@EmployeeId",
                            employeeId);
                    }
                    else
                    {
                        insertIssueCommand.Parameters.AddWithValue(
                            "@EmployeeId",
                            DBNull.Value);
                    }

                    insertIssueCommand.Parameters.AddWithValue(
                        "@DocumentNumber",
                        documentNumber);

                    insertIssueCommand.Parameters.AddWithValue(
                        "@Comment",
                        comment);

                    insertIssueCommand.ExecuteNonQuery();

                    string notificationQuery = @"
                        INSERT INTO Notifications
                        (
                            OwnerId,
                            FoundItemId,
                            Message,
                            CreatedAt,
                            IsRead
                        )
                        VALUES
                        (
                            @OwnerId,
                            @FoundItemId,
                            @Message,
                            GETDATE(),
                            0
                        )";

                    using SqlCommand notificationCommand =
                        new SqlCommand(
                            notificationQuery,
                            connection,
                            transaction);

                    notificationCommand.Parameters.AddWithValue(
                        "@OwnerId",
                        owner.OwnerId);

                    notificationCommand.Parameters.AddWithValue(
                        "@FoundItemId",
                        foundItemId);

                    notificationCommand.Parameters.AddWithValue(
                        "@Message",
                        "Найденная вещь \"" +
                        itemName +
                        "\" выдана владельцу.");

                    notificationCommand.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show(
                        "Выдача успешно оформлена.\n\n" +
                        "Владелец: " +
                        owner.FullName +
                        "\nВещь: " +
                        itemName +
                        "\nСтатус: Выдана",
                        "Выдача вещи",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult =
                        DialogResult.OK;

                    Close();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка оформления выдачи:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private int GetIssuedStatusId(
            SqlConnection connection,
            SqlTransaction transaction)
        {
            string query = @"
                SELECT StatusId
                FROM Statuses
                WHERE StatusName = N'Выдана'";

            using SqlCommand command =
                new SqlCommand(
                    query,
                    connection,
                    transaction);

            object result =
                command.ExecuteScalar();

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            string insertQuery = @"
                INSERT INTO Statuses
                (
                    StatusName,
                    Description
                )
                VALUES
                (
                    N'Выдана',
                    N'Найденная вещь выдана владельцу'
                );

                SELECT SCOPE_IDENTITY();";

            using SqlCommand insertCommand =
                new SqlCommand(
                    insertQuery,
                    connection,
                    transaction);

            return Convert.ToInt32(
                insertCommand.ExecuteScalar());
        }

        private int GetEmployeeId(
            SqlConnection connection,
            SqlTransaction transaction)
        {
            string query = @"
                SELECT TOP 1 EmployeeId
                FROM Employees
                ORDER BY EmployeeId";

            using SqlCommand command =
                new SqlCommand(
                    query,
                    connection,
                    transaction);

            object result =
                command.ExecuteScalar();

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            return 0;
        }

        private void BtnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }

    public class OwnerItem
    {
        public int OwnerId { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public override string ToString()
        {
            return FullName +
                   " — " +
                   Phone;
        }
    }
}