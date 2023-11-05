using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Q2.Hubs;
using Q2.Models;

namespace Q2.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly PRN_Spr23_B1Context _context;
        private readonly IHubContext<EmployeeHub> _hub;

        public CreateModel(PRN_Spr23_B1Context context, IHubContext<EmployeeHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _context.Employees.Add(Employee);
            _context.SaveChanges();
            _hub.Clients.All.SendAsync("CreatedNewEmployee", Employee);
            return RedirectToPage("./Index");
        }
    }
}
