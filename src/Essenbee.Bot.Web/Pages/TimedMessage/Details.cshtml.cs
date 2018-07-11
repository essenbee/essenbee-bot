using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Data;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Web.Pages.TimedMessage
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository _repository;

        public DetailsModel(IRepository repository)
        {
            _repository = repository;
        }

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
            return Page();
        }
    }
}
