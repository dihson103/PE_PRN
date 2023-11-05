using System;
using System.Collections.Generic;

namespace Mvvm.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public string? DepartmentType { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
