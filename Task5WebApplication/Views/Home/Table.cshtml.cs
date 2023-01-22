using Htmx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Task5WebApplication.Views.Home
{
    public class TableModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Cursor { get; set; } = 11;

        private readonly ILogger<TableModel> _logger;

        public TableModel(ILogger<TableModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Request.IsHtmx()
                ? Partial("_Cards", this)
                : Page();
        }
    }
}
