using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Web.Pages.TimedMessage
{
    public class CreateModel : PageModel
    {
        private readonly IRepository _repository;

        public CreateModel(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Core.Data.TimedMessage TimedMessage { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Create<Core.Data.TimedMessage>(TimedMessage);

            return RedirectToPage("./Index");
        }
    }
}