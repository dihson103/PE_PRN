using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Q2.Hubs;
using Q2.Models;

namespace Q2.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly PRN_Spr23_B1Context _context;
        private readonly IHubContext<EmployeeHub> _hub;

        public EditModel(PRN_Spr23_B1Context context, IHubContext<EmployeeHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public void OnGet(int? id)
        {
            Employee = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
        }

        public IActionResult OnPost()
        {
            _context.Update(Employee);
            _context.SaveChanges();
            _hub.Clients.All.SendAsync("UpdateEmployee", Employee);
            return RedirectToPage("./Index");
        }
    }
}
