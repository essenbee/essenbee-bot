using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Essenbee.Bot.Web.Pages.TimedMessage
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;

        public EditModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public List<SelectListItem> Statuses { get; set; }

        [BindProperty]
        public Core.Data.TimedMessage TimedMessage { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimedMessage = _repository.Single<Core.Data.TimedMessage>(DataItemPolicy<Core.Data.TimedMessage>.ById(id.Value));

            if (TimedMessage == null)
            {
                return NotFound();
            }

            Statuses = new List<SelectListItem>
            {
                new SelectListItem {Text = "Active", Value = "0"},
                new SelectListItem {Text = "Draft", Value = "1"},
                new SelectListItem {Text = "Disabled", Value = "2"}
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Update<Core.Data.TimedMessage>(TimedMessage);

            return RedirectToPage("./Index");
        }

        private bool TimedMessageExists(Guid id)
        {
            return _repository.List<Core.Data.TimedMessage>(DataItemPolicy<Core.Data.TimedMessage>.ById(id)).Any();
        }
    }
}
