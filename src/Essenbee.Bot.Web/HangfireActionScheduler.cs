using System.Collections.Generic;
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

        public void Schedule(IScheduledAction action)
        {
            Log.Information($"Scheduling {action.Name} with Hangfire server...");

            switch (action)
            {
               case DelayedMessage delayedMsg:
                    var msg = delayedMsg.Message;
                    var chnl = delayedMsg.Channel;

                    foreach (var chatClient in _chatClients)
                    {
                        if (!string.IsNullOrWhiteSpace(chnl))
                        {
                            BackgroundJob.Schedule(() => chatClient.PostMessage(chnl, msg), delayedMsg.Delay);
                        }
                        else
                        {
                            BackgroundJob.Schedule(() => chatClient.PostMessage(msg), delayedMsg.Delay);
                        }
                    }
                    break;

                case RepeatingMessage repeatingMsg:
                    var message = repeatingMsg.Message;
                    var channel = repeatingMsg.Channel;

                    foreach (var chatClient in _chatClients)
                    {
                        if (!string.IsNullOrWhiteSpace(channel))
                        {
                            RecurringJob.AddOrUpdate(
                            repeatingMsg.Name,
                            () => chatClient.PostMessage(channel, message),
                            Cron.MinuteInterval(repeatingMsg.IntervalInMinutes));
                        }
                        else
                        {
                            RecurringJob.AddOrUpdate(
                            repeatingMsg.Name,
                            () => chatClient.PostMessage(message),
                            Cron.MinuteInterval(repeatingMsg.IntervalInMinutes));
                        }
                    }
                    break;
            }
        }
    }
}
