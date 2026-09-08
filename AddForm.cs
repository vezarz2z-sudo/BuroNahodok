using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BuroNahodok
{
    public partial class AddForm : Form
    {
        private Label lblTitle;
        private Label lblName;
        private Label lblCategory;
        private Label lblDescription;
        private Label lblColor;
        private Label lblBrand;
        private Label lblDate;
        private Label lblTime;
        private Label lblPlace;
        private Label lblStorage;

        private TextBox txtName;
        private TextBox txtCategory;
        private TextBox txtDescription;
        private TextBox txtColor;
        private TextBox txtBrand;
        private DateTimePicker dtpDate;
        private DateTimePicker dtpTime;
        private TextBox txtPlace;
        private TextBox txtStorage;

        private Button btnSave;
        private Button btnCancel;

        public AddForm()
        {
            InitializeComponent();
            CreateInterface();
        }

        private void CreateInterface()
        {
            Text = "Бюро находок — Добавление найденной вещи";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            ClientSize = new Size(700, 650);

            lblTitle = new Label();
            lblTitle.Text = "Добавление найденной вещи";
            lblTitle.Font = new Font(
                "Segoe UI",
                18,
                FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(35, 25);
            Controls.Add(lblTitle);

            lblName = new Label();
            lblName.Text = "Название вещи:";
            lblName.Font = new Font("Segoe UI", 10);
            lblName.AutoSize = true;
            lblName.Location = new Point(35, 85);
            Controls.Add(lblName);

            txtName = new TextBox();
            txtName.Font = new Font("Segoe UI", 10);
            txtName.Location = new Point(200, 80);
            txtName.Size = new Size(430, 30);
            Controls.Add(txtName);

            lblCategory = new Label();
            lblCategory.Text = "Категория:";
            lblCategory.Font = new Font("Segoe UI", 10);
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(35, 130);
            Controls.Add(lblCategory);

            txtCategory = new TextBox();
            txtCategory.Font = new Font("Segoe UI", 10);
            txtCategory.Location = new Point(200, 125);
            txtCategory.Size = new Size(430, 30);
            Controls.Add(txtCategory);

            lblDescription = new Label();
            lblDescription.Text = "Описание:";
            lblDescription.Font = new Font("Segoe UI", 10);
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(35, 175);
            Controls.Add(lblDescription);

            txtDescription = new TextBox();
            txtDescription.Font = new Font("Segoe UI", 10);
            txtDescription.Location = new Point(200, 170);
            txtDescription.Size = new Size(430, 70);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            Controls.Add(txtDescription);

            lblColor = new Label();
            lblColor.Text = "Цвет:";
            lblColor.Font = new Font("Segoe UI", 10);
            lblColor.AutoSize = true;
            lblColor.Location = new Point(35, 260);
            Controls.Add(lblColor);

            txtColor = new TextBox();
            txtColor.Font = new Font("Segoe UI", 10);
            txtColor.Location = new Point(200, 255);
            txtColor.Size = new Size(430, 30);
            Controls.Add(txtColor);

            lblBrand = new Label();
            lblBrand.Text = "Бренд:";
            lblBrand.Font = new Font("Segoe UI", 10);
            lblBrand.AutoSize = true;
            lblBrand.Location = new Point(35, 305);
            Controls.Add(lblBrand);

            txtBrand = new TextBox();
            txtBrand.Font = new Font("Segoe UI", 10);
            txtBrand.Location = new Point(200, 300);
            txtBrand.Size = new Size(430, 30);
            Controls.Add(txtBrand);

            lblDate = new Label();
            lblDate.Text = "Дата находки:";
            lblDate.Font = new Font("Segoe UI", 10);
            lblDate.AutoSize = true;
            lblDate.Location = new Point(35, 350);
            Controls.Add(lblDate);

            dtpDate = new DateTimePicker();
            dtpDate.Font = new Font("Segoe UI", 10);
            dtpDate.Location = new Point(200, 345);
            dtpDate.Size = new Size(200, 30);
            dtpDate.Format = DateTimePickerFormat.Short;
            Controls.Add(dtpDate);

            lblTime = new Label();
            lblTime.Text = "Время находки:";
            lblTime.Font = new Font("Segoe UI", 10);
            lblTime.AutoSize = true;
            lblTime.Location = new Point(35, 395);
            Controls.Add(lblTime);

            dtpTime = new DateTimePicker();
            dtpTime.Font = new Font("Segoe UI", 10);
            dtpTime.Location = new Point(200, 390);
            dtpTime.Size = new Size(200, 30);
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ShowUpDown = true;
            Controls.Add(dtpTime);

            lblPlace = new Label();
            lblPlace.Text = "Место находки:";
            lblPlace.Font = new Font("Segoe UI", 10);
            lblPlace.AutoSize = true;
            lblPlace.Location = new Point(35, 440);
            Controls.Add(lblPlace);

            txtPlace = new TextBox();
            txtPlace.Font = new Font("Segoe UI", 10);
            txtPlace.Location = new Point(200, 435);
            txtPlace.Size = new Size(430, 30);
            Controls.Add(txtPlace);

            lblStorage = new Label();
            lblStorage.Text = "Место хранения:";
            lblStorage.Font = new Font("Segoe UI", 10);
            lblStorage.AutoSize = true;
            lblStorage.Location = new Point(35, 485);
            Controls.Add(lblStorage);

            txtStorage = new TextBox();
            txtStorage.Font = new Font("Segoe UI", 10);
            txtStorage.Location = new Point(200, 480);
            txtStorage.Size = new Size(430, 30);
            Controls.Add(txtStorage);

            btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Font = new Font("Segoe UI", 10);
            btnSave.Location = new Point(350, 550);
            btnSave.Size = new Size(130, 45);
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Font = new Font("Segoe UI", 10);
            btnCancel.Location = new Point(500, 550);
            btnCancel.Size = new Size(130, 45);
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);

            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Now;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string itemName = txtName.Text.Trim();
            string category = txtCategory.Text.Trim();
            string description = txtDescription.Text.Trim();
            string color = txtColor.Text.Trim();
            string brand = txtBrand.Text.Trim();
            string placeName = txtPlace.Text.Trim();
            string storage = txtStorage.Text.Trim();

            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show(
                    "Введите название найденной вещи.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtName.Focus();
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

            if (string.IsNullOrWhiteSpace(placeName))
            {
                MessageBox.Show(
                    "Введите место находки.",
                    "Проверка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPlace.Focus();
                return;
            }

            try
            {
                using SqlConnection connection = Database.GetConnection();
                connection.Open();

                // Получаем или создаём место находки
                int placeId = 0;

                string findPlaceQuery = @"
                    SELECT PlaceId
                    FROM FindPlaces
                    WHERE PlaceName = @PlaceName";

                using SqlCommand findPlaceCommand =
                    new SqlCommand(findPlaceQuery, connection);

                findPlaceCommand.Parameters.AddWithValue(
                    "@PlaceName",
                    placeName);

                object placeResult =
                    findPlaceCommand.ExecuteScalar();

                if (placeResult != null)
                {
                    placeId = Convert.ToInt32(placeResult);
                }
                else
                {
                    string addPlaceQuery = @"
                        INSERT INTO FindPlaces
                        (
                            PlaceName,
                            Address,
                            Description
                        )
                        VALUES
                        (
                            @PlaceName,
                            '',
                            ''
                        );

                        SELECT SCOPE_IDENTITY();";

                    using SqlCommand addPlaceCommand =
                        new SqlCommand(addPlaceQuery, connection);

                    addPlaceCommand.Parameters.AddWithValue(
                        "@PlaceName",
                        placeName);

                    placeId = Convert.ToInt32(
                        addPlaceCommand.ExecuteScalar());
                }

                // Получаем статус "Найдена"
                int statusId = 0;

                string statusQuery = @"
                    SELECT StatusId
                    FROM Statuses
                    WHERE StatusName = N'Найдена'";

                using SqlCommand statusCommand =
                    new SqlCommand(statusQuery, connection);

                object statusResult =
                    statusCommand.ExecuteScalar();

                if (statusResult != null)
                {
                    statusId = Convert.ToInt32(statusResult);
                }
                else
                {
                    string addStatusQuery = @"
                        INSERT INTO Statuses
                        (
                            StatusName,
                            Description
                        )
                        VALUES
                        (
                            N'Найдена',
                            N'Найденная вещь зарегистрирована в системе'
                        );

                        SELECT SCOPE_IDENTITY();";

                    using SqlCommand addStatusCommand =
                        new SqlCommand(addStatusQuery, connection);

                    statusId = Convert.ToInt32(
                        addStatusCommand.ExecuteScalar());
                }

                // Добавляем найденную вещь
                string insertItemQuery = @"
                    INSERT INTO FoundItems
                    (
                        ItemName,
                        Category,
                        Description,
                        Color,
                        Brand,
                        FoundDate,
                        FoundTime,
                        PlaceId,
                        StatusId,
                        EmployeeId,
                        OwnerId,
                        StorageLocation,
                        CreatedAt
                    )
                    VALUES
                    (
                        @ItemName,
                        @Category,
                        @Description,
                        @Color,
                        @Brand,
                        @FoundDate,
                        @FoundTime,
                        @PlaceId,
                        @StatusId,
                        NULL,
                        NULL,
                        @StorageLocation,
                        GETDATE()
                    )";

                using SqlCommand insertItemCommand =
                    new SqlCommand(insertItemQuery, connection);

                insertItemCommand.Parameters.AddWithValue(
                    "@ItemName",
                    itemName);

                insertItemCommand.Parameters.AddWithValue(
                    "@Category",
                    category);

                insertItemCommand.Parameters.AddWithValue(
                    "@Description",
                    description);

                insertItemCommand.Parameters.AddWithValue(
                    "@Color",
                    color);

                insertItemCommand.Parameters.AddWithValue(
                    "@Brand",
                    brand);

                insertItemCommand.Parameters.AddWithValue(
                    "@FoundDate",
                    dtpDate.Value.Date);

                insertItemCommand.Parameters.AddWithValue(
                    "@FoundTime",
                    dtpTime.Value.TimeOfDay);

                insertItemCommand.Parameters.AddWithValue(
                    "@PlaceId",
                    placeId);

                insertItemCommand.Parameters.AddWithValue(
                    "@StatusId",
                    statusId);

                insertItemCommand.Parameters.AddWithValue(
                    "@StorageLocation",
                    storage);

                insertItemCommand.ExecuteNonQuery();

                MessageBox.Show(
                    "Найденная вещь успешно добавлена в базу данных.",
                    "Сохранение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка сохранения найденной вещи:\n\n" +
                    ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtCategory.Clear();
            txtDescription.Clear();
            txtColor.Clear();
            txtBrand.Clear();
            txtPlace.Clear();
            txtStorage.Clear();

            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Now;

            txtName.Focus();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}