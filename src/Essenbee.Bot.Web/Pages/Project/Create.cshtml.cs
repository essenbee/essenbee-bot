using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Interfaces;
using System.Linq;
using System;
using Serilog;
using Essenbee.Bot.Core.Data;

namespace Essenbee.Bot.Web.Pages.Project
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
            try
            {
                ProjectText = _repository.List<ProjectText>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error($"Project.Create.OnGet(): {ex.Message} - {ex.StackTrace}");
            }

            return Page();
        }

        [BindProperty]
        public ProjectText ProjectText { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ProjectText is null)
            {
                try
                {
                    _repository.Create<ProjectText>(ProjectText);
                }
                catch (Exception ex)
                {
                    Log.Error($"Project.Create.OnPost(): {ex.Message} - {ex.StackTrace}");
                }

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}