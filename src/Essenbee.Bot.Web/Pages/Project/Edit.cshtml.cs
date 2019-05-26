using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Data;
using Serilog;

namespace Essenbee.Bot.Web.Pages.Project
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;

        public EditModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public ProjectText ProjectText { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                ProjectText = _repository.Single<ProjectText>(DataItemPolicy<ProjectText>.ById(id.Value));
            }
            catch (Exception ex)
            {
                Log.Error($"Project.Edit.OnGet(): {ex.Message} - {ex.StackTrace}");
            }

            if (ProjectText == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _repository.Update(ProjectText);
            }
            catch (Exception ex)
            {
                Log.Error($"Project.Edit.OnPost(): {ex.Message} - {ex.StackTrace}");
            }

            return RedirectToPage("/Index");
        }

        private bool ProjectTextExists(Guid id)
        {
            return _repository.List<ProjectText>(DataItemPolicy<Core.Data.ProjectText>.ById(id)).Any();
        }
    }
}
