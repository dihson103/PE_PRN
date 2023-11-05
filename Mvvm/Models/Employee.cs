using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Mvvm.Models
{
    public partial class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int employeeId;
        public int EmployeeId
        {
            get { return employeeId; }
            set
            {
                if (employeeId != value)
                {
                    employeeId = value;
                    OnPropertyChanged(nameof(EmployeeId));
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        private int? departmentId;
        public int? DepartmentId
        {
            get { return departmentId; }
            set
            {
                if (departmentId != value)
                {
                    departmentId = value;
                    OnPropertyChanged(nameof(DepartmentId));
                }
            }
        }

        private string? title;
        public string? Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string? titleOfCourtesy;
        public string? TitleOfCourtesy
        {
            get { return titleOfCourtesy; }
            set
            {
                if (titleOfCourtesy != value)
                {
                    titleOfCourtesy = value;
                    OnPropertyChanged(nameof(TitleOfCourtesy));
                }
            }
        }

        private DateTime? birthDate;
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
                }
            }
        }

        private DateTime? hireDate;
        public DateTime? HireDate
        {
            get { return hireDate; }
            set
            {
                if (hireDate != value)
                {
                    hireDate = value;
                    OnPropertyChanged(nameof(HireDate));
                }
            }
        }

        private string? address;
        public string? Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
