using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace projectFBankO
{
    public partial class AddEmployeeForm : Form
    {
        private string connectionString;

        public AddEmployeeForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            string position = txtPosition.Text;
            decimal baseSalary = decimal.Parse(txtBaseSalary.Text);
            decimal bonus = decimal.Parse(txtBonus.Text);
            decimal deductions = decimal.Parse(txtDeductions.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employees (FullName, Position, BaseSalary, Bonus, Deductions) " +
                               "VALUES (@FullName, @Position, @BaseSalary, @Bonus, @Deductions)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
                cmd.Parameters.AddWithValue("@Bonus", bonus);
                cmd.Parameters.AddWithValue("@Deductions", deductions);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Сотрудник добавлен!");
                this.Close(); // Закрыть форму после добавления
            }
        }
    }
}
