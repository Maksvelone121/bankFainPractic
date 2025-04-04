using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace projectFBankO
{
    public partial class btnAddEmployee : Form
    {
        private string connectionString;

        public btnAddEmployee()
        {
            InitializeComponent();
            // Получаем строку подключения из App.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Метод вызывается при загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        // Метод загрузки сотрудников из БД и отображения в таблице
        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Employees";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке сотрудников: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployeeForm addForm = new AddEmployeeForm();
            addForm.FormClosed += (s, args) => LoadEmployees(); // обновим список после добавления
            addForm.ShowDialog();
        }
    }
}
