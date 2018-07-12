using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Web.Pages.StartupMessage
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
            Statuses = new List<SelectListItem>
            {
                new SelectListItem {Text = "Active", Value = "0"},
                new SelectListItem {Text = "Draft", Value = "1"},
                new SelectListItem {Text = "Disabled", Value = "2"}
            };

            StartupMessage = _repository.List<Core.Data.StartupMessage>().FirstOrDefault();

            return Page();
        }

        [BindProperty]
        public List<SelectListItem> Statuses { get; set; }

        [BindProperty]
        public Core.Data.StartupMessage StartupMessage { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (StartupMessage is null)
            {
                _repository.Create<Core.Data.StartupMessage>(StartupMessage);

                return RedirectToPage("/Admin");
            }

            return Page();
        }
    }
}