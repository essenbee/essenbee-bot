using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Essenbee.Bot.Web.Pages.TimedMessage
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public IList<Core.Data.TimedMessage> TimedMessages { get;set; }

        public void OnGetAsync()
        {
            TimedMessages = _repository.List<Core.Data.TimedMessage>();
        }
    }
}
