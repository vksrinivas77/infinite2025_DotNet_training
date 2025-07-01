using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CURD_OP
{
    public partial class MainForm : Form
    {
        private List<Employee> employees = new List<Employee>();
        private MobilePhone phone = new MobilePhone();

        public MainForm()
        {
            InitializeComponent();
            phone.OnRing += PlayRingtone;
            phone.OnRing += ShowScreen;
            phone.OnRing += StartVibration;
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvEmployees.DataSource = null;
            dgvEmployees.DataSource = employees;
            lblStatus.Text = $"Total Employees: {employees.Count}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var emp = new Employee
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    Department = txtDepartment.Text,
                    Salary = double.Parse(txtSalary.Text)
                };
                if (employees.Any(x => x.Id == emp.Id))
                    throw new Exception("ID already exists.");
                employees.Add(emp);
                RefreshGrid();
                lblStatus.Text = "Added successfully!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(txtId.Text);
                var emp = employees.FirstOrDefault(x => x.Id == id)
                          ?? throw new Exception("Employee not found.");
                emp.Name = txtName.Text;
                emp.Department = txtDepartment.Text;
                emp.Salary = double.Parse(txtSalary.Text);
                RefreshGrid();
                lblStatus.Text = "Updated successfully!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(txtId.Text);
                var emp = employees.FirstOrDefault(x => x.Id == id)
                          ?? throw new Exception("Employee not found.");
                employees.Remove(emp);
                RefreshGrid();
                lblStatus.Text = "Deleted successfully!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(txtId.Text);
                var emp = employees.FirstOrDefault(x => x.Id == id)
                          ?? throw new Exception("Employee not found.");
                dgvEmployees.ClearSelection();
                foreach (DataGridViewRow row in dgvEmployees.Rows)
                    if (((Employee)row.DataBoundItem).Id == id)
                        row.Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search");
            }
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow?.DataBoundItem is Employee emp)
            {
                txtId.Text = emp.Id.ToString();
                txtName.Text = emp.Name;
                txtDepartment.Text = emp.Department;
                txtSalary.Text = emp.Salary.ToString();
            }
        }
        

        private void btnSimulateCall_Click(object sender, EventArgs e)
        {
            phone.ReceiveCall();
        }

        private void PlayRingtone() => MessageBox.Show("Playing ringtone...");
        private void ShowScreen() => MessageBox.Show("Displaying caller info...");
        private void StartVibration() => MessageBox.Show("Phone is vibrating...");
    }
}
