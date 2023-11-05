using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Q2.Hubs;
using Q2.Models;

namespace Q2.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly PRN_Spr23_B1Context _context;
        private readonly IHubContext<EmployeeHub> _employeeHub;

        public DeleteModel(PRN_Spr23_B1Context context, IHubContext<EmployeeHub> hub)
        {
            _context = context;
            _employeeHub = hub;
        }

        public IActionResult OnGet(int? id)
        {
            Employee? employee = _context.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);    
                _context.SaveChanges();
                _employeeHub.Clients.All.SendAsync("DeleteEmployee", id);
            }
            return RedirectToPage("./Index");
        }
    }
}
