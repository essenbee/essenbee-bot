using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Data;
using Essenbee.Bot.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Serilog;

namespace Essenbee.Bot.Web.Pages.TimedMessage
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository _repository;

        public DetailsModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public List<SelectListItem> Statuses { get; set; }

        public Core.Data.TimedMessage TimedMessage { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Statuses = new List<SelectListItem>
            {
                new SelectListItem {Text = "Active", Value = "0"},
                new SelectListItem {Text = "Draft", Value = "2"},
                new SelectListItem {Text = "Disabled", Value = "2"}
            };

            try
            {
                TimedMessage = _repository.Single<Core.Data.TimedMessage>(DataItemPolicy<Core.Data.TimedMessage>.ById(id.Value));
            }
            catch (Exception ex)
            {
                Log.Error($"TimedMessage.Details.OnGet(): {ex.Message} - {ex.StackTrace}");
            }

            if (TimedMessage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
