using System.Collections.Generic;
using System.Linq;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Hangfire;
using Serilog;

namespace Essenbee.Bot.Web
{
    public class HangfireActionScheduler : IActionScheduler
    {
        private readonly IList<IChatClient> _chatClients;
        
        public HangfireActionScheduler(IList<IChatClient> chatClients)
        {
            _chatClients = chatClients;
        }

        public void AddAction(IScheduledAction action)
        {
            Log.Information($"Scheduling {action.Name}.with Hangfire server...");

            switch (action)
            {
               case DelayedMessage delayedMsg:
                    var msg = delayedMsg.Message;
                    var chnl = delayedMsg.Channel;
                    BackgroundJob.Schedule(() => _chatClients.Single().PostMessage(chnl, msg), delayedMsg.Delay);
                    break;

                case RepeatingMessage repeatingMsg:
                    var message = repeatingMsg.Message;
                    var channel = repeatingMsg.Channel;
                    RecurringJob.AddOrUpdate(
                        repeatingMsg.Name,
                        () => _chatClients.Single().PostMessage(channel, message),
                        Cron.MinuteInterval(repeatingMsg.IntervalInMinutes));
                    break;
            }
        }
    }
}
