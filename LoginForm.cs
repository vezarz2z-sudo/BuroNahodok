using System;
using System.Drawing;
using System.Windows.Forms;

namespace BuroNahodok
{
    public partial class LoginForm : Form
    {
        private Label lblTitle;
        private Label lblLogin;
        private Label lblPassword;

        private TextBox txtLogin;
        private TextBox txtPassword;

        private Button btnLogin;
        private Button btnExit;

        public LoginForm()
        {
            InitializeComponent();
            CreateInterface();
        }

        private void CreateInterface()
        {
            Text = "Бюро находок — Авторизация";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;
            ClientSize = new Size(500, 350);

            lblTitle = new Label();
            lblTitle.Text = "БЮРО НАХОДОК";
            lblTitle.Font = new Font(
                "Segoe UI",
                20,
                FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(135, 35);

            lblLogin = new Label();
            lblLogin.Text = "Логин:";
            lblLogin.Font = new Font(
                "Segoe UI",
                11);
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(80, 110);

            txtLogin = new TextBox();
            txtLogin.Font = new Font(
                "Segoe UI",
                11);
            txtLogin.Location = new Point(180, 105);
            txtLogin.Size = new Size(230, 30);

            lblPassword = new Label();
            lblPassword.Text = "Пароль:";
            lblPassword.Font = new Font(
                "Segoe UI",
                11);
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(80, 160);

            txtPassword = new TextBox();
            txtPassword.Font = new Font(
                "Segoe UI",
                11);
            txtPassword.Location = new Point(180, 155);
            txtPassword.Size = new Size(230, 30);
            txtPassword.PasswordChar = '*';

            btnLogin = new Button();
            btnLogin.Text = "Войти";
            btnLogin.Font = new Font(
                "Segoe UI",
                11);
            btnLogin.Location = new Point(180, 215);
            btnLogin.Size = new Size(110, 40);
            btnLogin.Click += BtnLogin_Click;

            btnExit = new Button();
            btnExit.Text = "Выход";
            btnExit.Font = new Font(
                "Segoe UI",
                11);
            btnExit.Location = new Point(300, 215);
            btnExit.Size = new Size(110, 40);
            btnExit.Click += BtnExit_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblLogin);
            Controls.Add(txtLogin);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnExit);

            AcceptButton = btnLogin;
        }

        private void BtnLogin_Click(
            object? sender,
            EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show(
                    "Введите логин.",
                    "Авторизация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtLogin.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Введите пароль.",
                    "Авторизация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            string role;

            bool userExists =
                Database.CheckUser(
                    login,
                    password,
                    out role);

            if (userExists)
            {
                MainForm mainForm = new MainForm();

                mainForm.Text =
                    "Бюро находок — Главное меню " +
                    $"({role})";

                mainForm.FormClosed +=
                    MainForm_FormClosed;

                Hide();

                mainForm.Show();
            }
            else
            {
                MessageBox.Show(
                    "Неверный логин или пароль!",
                    "Ошибка авторизации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void MainForm_FormClosed(
            object? sender,
            FormClosedEventArgs e)
        {
            Show();

            txtPassword.Clear();
            txtLogin.Focus();
        }

        private void BtnExit_Click(
            object? sender,
            EventArgs e)
        {
            Application.Exit();
        }
    }
}