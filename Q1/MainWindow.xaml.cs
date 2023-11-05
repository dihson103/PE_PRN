using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Q1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
    }

    public partial class MainWindow : Window
    {
        private PRN_Spr23_B1Context _context;
        public MainWindow()
        {
            _context = new PRN_Spr23_B1Context();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
            LoadDepartments();
            LoadTitleOfCourtesy();
            cbDepartment.SelectedIndex = 0;
            cbTitleOfCourtesy.SelectedIndex = 0;
        }

        private void LoadDepartments()
        {
            cbDepartment.ItemsSource = _context.Departments.ToList();
        }

        private void LoadTitleOfCourtesy()
        {
            cbTitleOfCourtesy.ItemsSource = new List<string>() {"Dr.", "Mr.", "Mrs.", "Ms."};
        }

        private void LoadEmployees()
        {
            lvEmployees.ItemsSource = _context.Employees
                .Include(e => e.Department)
                .ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtEmployeeId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtTitle.Clear();
            dtBirthDate.SelectedDate = null;
            cbDepartment.SelectedIndex = 0;
            cbTitleOfCourtesy.SelectedIndex = 0;
        }

       

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string departmentIdString = cbDepartment.SelectedValue.ToString();
            int deparmentId = int.Parse(departmentIdString);
            string title = txtTitle.Text;
            string titleOfCourtesy = cbTitleOfCourtesy.SelectedValue.ToString();
            DateTime? birthDate = dtBirthDate.SelectedDate;

            Employee employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                DepartmentId = deparmentId,
                Title = title,
                TitleOfCourtesy= titleOfCourtesy,
                BirthDate = birthDate
            };
            _context.Add(employee);
            _context.SaveChanges();
            LoadEmployees();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtEmployeeId.Text, out int employeeId))
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

                if (employee != null)
                {
                    employee.FirstName = txtFirstName.Text;
                    employee.LastName = txtLastName.Text;

                    if (int.TryParse(cbDepartment.SelectedValue?.ToString(), out int departmentId))
                        employee.DepartmentId = departmentId;

                    employee.Title = txtTitle.Text;
                    employee.TitleOfCourtesy = cbTitleOfCourtesy.SelectedValue?.ToString();

                    if (dtBirthDate.SelectedDate.HasValue)
                        employee.BirthDate = dtBirthDate.SelectedDate.Value;

                    try
                    {
                        _context.SaveChanges();
                        LoadEmployees();
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception, show an error message, log the error, etc.
                        MessageBox.Show($"Error updating employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Employee not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid Employee ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".json";
            saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                List<EmployeeDto> employees = _context.Employees.Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }).ToList();
                string jsonContent = JsonSerializer.Serialize(employees, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(saveFileDialog.FileName, jsonContent);
                MessageBox.Show("Save file ok");
            }
        }
    }
}
