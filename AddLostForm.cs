using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BuroNahodok
{
    public partial class AddLostForm : Form
    {
        private Label lblTitle;

        private Label lblOwner;
        private Label lblPhone;
        private Label lblEmail;

        private Label lblItemName;
        private Label lblCategory;
        private Label lblDescription;
        private Label lblColor;
        private Label lblBrand;
        private Label lblPlace;
        private Label lblDate;

        private TextBox txtOwner;
        private TextBox txtPhone;
        private TextBox txtEmail;

        private TextBox txtItemName;
        private TextBox txtCategory;
        private TextBox txtDescription;
        private TextBox txtColor;
        private TextBox txtBrand;
        private TextBox txtPlace;

        private DateTimePicker dtpLostDate;

        private Button btnSave;
        private Button btnCancel;

        public AddLostForm()
        {
            InitializeComponent();
            CreateInterface();
        }

        private void CreateInterface()
        {
            Text = "Бюро находок — Добавление заявления о пропаже";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            ClientSize = new Size(720, 700);

            lblTitle = new Label();
            lblTitle.Text = "Добавление заявления о пропаже";
            lblTitle.Font = new Font(
                "Segoe UI",
                18,
                FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);
            Controls.Add(lblTitle);

            // Владелец

            lblOwner = new Label();
            lblOwner.Text = "ФИО владельца:";
            lblOwner.Font = new Font("Segoe UI", 10);
            lblOwner.AutoSize = true;
            lblOwner.Location = new Point(30, 75);
            Controls.Add(lblOwner);

            txtOwner = new TextBox();
            txtOwner.Font = new Font("Segoe UI", 10);
            txtOwner.Location = new Point(210, 70);
            txtOwner.Size = new Size(450, 30);
            Controls.Add(txtOwner);

            // Телефон

            lblPhone = new Label();
            lblPhone.Text = "Телефон:";
            lblPhone.Font = new Font("Segoe UI", 10);
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(30, 120);
            Controls.Add(lblPhone);

            txtPhone = new TextBox();
            txtPhone.Font = new Font("Segoe UI", 10);
            txtPhone.Location = new Point(210, 115);
            txtPhone.Size = new Size(450, 30);
            Controls.Add(txtPhone);

            // Email

            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = new Font("Segoe UI", 10);
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(30, 165);
            Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Font = new Font("Segoe UI", 10);
            txtEmail.Location = new Point(210, 160);
            txtEmail.Size = new Size(450, 30);
            Controls.Add(txtEmail);

            // Название вещи

            lblItemName = new Label();
            lblItemName.Text = "Название вещи:";
            lblItemName.Font = new Font("Segoe UI", 10);
            lblItemName.AutoSize = true;
            lblItemName.Location = new Point(30, 215);
            Controls.Add(lblItemName);

            txtItemName = new TextBox();
            txtItemName.Font = new Font("Segoe UI", 10);
            txtItemName.Location = new Point(210, 210);
            txtItemName.Size = new Size(450, 30);
            Controls.Add(txtItemName);

            // Категория

            lblCategory = new Label();
            lblCategory.Text = "Категория:";
            lblCategory.Font = new Font("Segoe UI", 10);
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(30, 260);
            Controls.Add(lblCategory);

            txtCategory = new TextBox();
            txtCategory.Font = new Font("Segoe UI", 10);
            txtCategory.Location = new Point(210, 255);
            txtCategory.Size = new Size(450, 30);
            Controls.Add(txtCategory);

            // Описание

            lblDescription = new Label();
            lblDescription.Text = "Описание:";
            lblDescription.Font = new Font("Segoe UI", 10);
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(30, 305);
            Controls.Add(lblDescription);

            txtDescription = new TextBox();
            txtDescription.Font = new Font("Segoe UI", 10);
            txtDescription.Location = new Point(210, 300);
            txtDescription.Size = new Size(450, 65);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            Controls.Add(txtDescription);

            // Цвет

            lblColor = new Label();
            lblColor.Text = "Цвет:";
            lblColor.Font = new Font("Segoe UI", 10);
            lblColor.AutoSize = true;
            lblColor.Location = new Point(30, 385);
            Controls.Add(lblColor);

            txtColor = new TextBox();
            txtColor.Font = new Font("Segoe UI", 10);
            txtColor.Location = new Point(210, 380);
            txtColor.Size = new Size(450, 30);
            Controls.Add(txtColor);

            // Бренд

            lblBrand = new Label();
            lblBrand.Text = "Бренд:";
            lblBrand.Font = new Font("Segoe UI", 10);
            lblBrand.AutoSize = true;
            lblBrand.Location = new Point(30, 430);
            Controls.Add(lblBrand);

            txtBrand = new TextBox();
            txtBrand.Font = new Font("Segoe UI", 10);
            txtBrand.Location = new Point(210, 425);
            txtBrand.Size = new Size(450, 30);
            Controls.Add(txtBrand);

            // Место пропажи

            lblPlace = new Label();
            lblPlace.Text = "Место пропажи:";
            lblPlace.Font = new Font("Segoe UI", 10);
            lblPlace.AutoSize = true;
            lblPlace.Location = new Point(30, 475);
            Controls.Add(lblPlace);

            txtPlace = new TextBox();
            txtPlace.Font = new Font("Segoe UI", 10);
            txtPlace.Location = new Point(210, 470);
            txtPlace.Size = new Size(450, 30);
            Controls.Add(txtPlace);

            // Дата пропажи

            lblDate = new Label();
            lblDate.Text = "Дата пропажи:";
            lblDate.Font = new Font("Segoe UI", 10);
            lblDate.AutoSize = true;
            lblDate.Location = new Point(30, 520);
            Controls.Add(lblDate);

            dtpLostDate = new DateTimePicker();
            dtpLostDate.Font = new Font("Segoe UI", 10);
            dtpLostDate.Location = new Point(210, 515);
            dtpLostDate.Size = new Size(200, 30);
            dtpLostDate.Format = DateTimePickerFormat.Short;
            dtpLostDate.Value = DateTime.Today;
            Controls.Add(dtpLostDate);

            // Кнопка сохранения

            btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Font = new Font("Segoe UI", 10);
            btnSave.Location = new Point(370, 600);
            btnSave.Size = new Size(140, 45);
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            // Кнопка отмены

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Font = new Font("Segoe UI", 10);
            btnCancel.Location = new Point(520, 600);
            btnCancel.Size = new Size(140, 45);
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string ownerName = txtOwner.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            string itemName = txtItemName.Text.Trim();
            string category = txtCategory.Text.Trim();
            string description = txtDescription.Text.Trim();
            string color = txtColor.Text.Trim();
            string brand = txtBrand.Text.Trim();
            string lostPlace = txtPlace.Text.Trim();

            if (string.IsNullOrWhiteSpace(ownerName))
            {
                MessageBox.Show(
                    "Введите ФИО владельца.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtOwner.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show(
                    "Введите телефон владельца.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show(
                    "Введите название пропавшей вещи.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtItemName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show(
                    "Введите категорию вещи.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(lostPlace))
            {
                MessageBox.Show(
                    "Введите место пропажи.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPlace.Focus();
                return;
            }

            try
            {
                using SqlConnection connection =
                    Database.GetConnection();

                connection.Open();

                // Ищем владельца

                int ownerId = 0;

                string findOwnerQuery = @"
                    SELECT OwnerId
                    FROM Owners
                    WHERE FullName = @FullName
                      AND Phone = @Phone";

                using SqlCommand findOwnerCommand =
                    new SqlCommand(
                        findOwnerQuery,
                        connection);

                findOwnerCommand.Parameters.AddWithValue(
                    "@FullName",
                    ownerName);

                findOwnerCommand.Parameters.AddWithValue(
                    "@Phone",
                    phone);

                object ownerResult =
                    findOwnerCommand.ExecuteScalar();

                // Если владелец найден

                if (ownerResult != null)
                {
                    ownerId = Convert.ToInt32(ownerResult);
                }
                else
                {
                    // Создаём нового владельца

                    string addOwnerQuery = @"
                        INSERT INTO Owners
                        (
                            FullName,
                            Phone,
                            Email,
                            Description
                        )
                        VALUES
                        (
                            @FullName,
                            @Phone,
                            @Email,
                            N'Владелец зарегистрирован через заявление о пропаже'
                        );

                        SELECT SCOPE_IDENTITY();";

                    using SqlCommand addOwnerCommand =
                        new SqlCommand(
                            addOwnerQuery,
                            connection);

                    addOwnerCommand.Parameters.AddWithValue(
                        "@FullName",
                        ownerName);

                    addOwnerCommand.Parameters.AddWithValue(
                        "@Phone",
                        phone);

                    addOwnerCommand.Parameters.AddWithValue(
                        "@Email",
                        email);

                    ownerId = Convert.ToInt32(
                        addOwnerCommand.ExecuteScalar());
                }

                // Добавляем заявление о пропаже

                string insertLostReportQuery = @"
                    INSERT INTO LostReports
                    (
                        OwnerId,
                        ItemName,
                        Category,
                        Description,
                        Color,
                        Brand,
                        LostDate,
                        LostPlace,
                        Status,
                        CreatedAt
                    )
                    VALUES
                    (
                        @OwnerId,
                        @ItemName,
                        @Category,
                        @Description,
                        @Color,
                        @Brand,
                        @LostDate,
                        @LostPlace,
                        N'Ищется',
                        GETDATE()
                    )";

                using SqlCommand insertLostReportCommand =
                    new SqlCommand(
                        insertLostReportQuery,
                        connection);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@OwnerId",
                    ownerId);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@ItemName",
                    itemName);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@Category",
                    category);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@Description",
                    description);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@Color",
                    color);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@Brand",
                    brand);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@LostDate",
                    dtpLostDate.Value.Date);

                insertLostReportCommand.Parameters.AddWithValue(
                    "@LostPlace",
                    lostPlace);

                insertLostReportCommand.ExecuteNonQuery();

                MessageBox.Show(
                    "Заявление о пропаже успешно добавлено.",
                    "Сохранение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка сохранения заявления:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtOwner.Clear();
            txtPhone.Clear();
            txtEmail.Clear();

            txtItemName.Clear();
            txtCategory.Clear();
            txtDescription.Clear();
            txtColor.Clear();
            txtBrand.Clear();
            txtPlace.Clear();

            dtpLostDate.Value = DateTime.Today;

            txtOwner.Focus();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}