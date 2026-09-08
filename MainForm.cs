using System;
using System.Drawing;
using System.Windows.Forms;

namespace BuroNahodok
{
    public partial class MainForm : Form
    {
        private Label lblTitle;
        private Label lblSubtitle;

        private Button btnFound;
        private Button btnLost;
        private Button btnAdd;
        private Button btnSearch;
        private Button btnExit;

        public MainForm()
        {
            InitializeComponent();
            CreateInterface();
        }

        private void CreateInterface()
        {
            Text = "Бюро находок — Главное меню";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            ClientSize = new Size(650, 500);

            // Заголовок

            lblTitle = new Label();
            lblTitle.Text = "БЮРО НАХОДОК";
            lblTitle.Font = new Font(
                "Segoe UI",
                24,
                FontStyle.Bold);

            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(190, 35);

            Controls.Add(lblTitle);

            // Подзаголовок

            lblSubtitle = new Label();
            lblSubtitle.Text = "Информационная система учета найденных вещей";
            lblSubtitle.Font = new Font(
                "Segoe UI",
                10);

            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(155, 85);

            Controls.Add(lblSubtitle);

            // Найденные вещи

            btnFound = new Button();
            btnFound.Text = "Найденные вещи";
            btnFound.Font = new Font(
                "Segoe UI",
                11);

            btnFound.Location = new Point(75, 145);
            btnFound.Size = new Size(230, 55);

            btnFound.Click += BtnFound_Click;

            Controls.Add(btnFound);

            // Заявления о пропаже

            btnLost = new Button();
            btnLost.Text = "Заявления о пропаже";
            btnLost.Font = new Font(
                "Segoe UI",
                11);

            btnLost.Location = new Point(345, 145);
            btnLost.Size = new Size(230, 55);

            btnLost.Click += BtnLost_Click;

            Controls.Add(btnLost);

            // Добавить найденную вещь

            btnAdd = new Button();
            btnAdd.Text = "Добавить найденную вещь";
            btnAdd.Font = new Font(
                "Segoe UI",
                11);

            btnAdd.Location = new Point(75, 225);
            btnAdd.Size = new Size(230, 55);

            btnAdd.Click += BtnAdd_Click;

            Controls.Add(btnAdd);

            // Поиск

            btnSearch = new Button();
            btnSearch.Text = "Поиск";
            btnSearch.Font = new Font(
                "Segoe UI",
                11);

            btnSearch.Location = new Point(345, 225);
            btnSearch.Size = new Size(230, 55);

            btnSearch.Click += BtnSearch_Click;

            Controls.Add(btnSearch);

            // Выход

            btnExit = new Button();
            btnExit.Text = "Выход";
            btnExit.Font = new Font(
                "Segoe UI",
                11);

            btnExit.Location = new Point(220, 350);
            btnExit.Size = new Size(210, 50);

            btnExit.Click += BtnExit_Click;

            Controls.Add(btnExit);
        }

        private void BtnFound_Click(object sender, EventArgs e)
        {
            using FoundForm form = new FoundForm();
            form.ShowDialog();
        }

        private void BtnLost_Click(object sender, EventArgs e)
        {
            using LostForm form = new LostForm();
            form.ShowDialog();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using AddForm form = new AddForm();
            form.ShowDialog();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            using SearchForm form = new SearchForm();
            form.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы действительно хотите выйти из программы?",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}