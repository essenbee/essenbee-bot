using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System;
using Serilog;

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

            try
            {
                StartupMessage = _repository.List<Core.Data.StartupMessage>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error($"StartupMessage.Create.OnGet(): {ex.Message} - {ex.StackTrace}");
            }

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
                try
                {
                    _repository.Create<Core.Data.StartupMessage>(StartupMessage);
                }
                catch (Exception ex)
                {
                    Log.Error($"StartupMessage.Create.OnPost(): {ex.Message} - {ex.StackTrace}");
                }

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}