using Mvvm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private PrnSpr23B1Context _context;
        public EmployeeViewModel()
        {
            _context = new PrnSpr23B1Context();
            LoadData();
        }

        public List<Employee> Employees
        {
            get
            {
                return Employees;
            }
            set
            {
                if(Employees != value)
                {
                    Employees = value;
                    OnPropertyChanged(nameof(Employees));
                }
            }
        }

        private void LoadData()
        {
            Employees = _context.Employees.ToList();
        }
    }
}
