using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Models;
using GraphQL.Client;
using GraphQL.Common.Exceptions;
using GraphQL.Common.Request;
using GraphQL.Common.Response;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Essenbee.Bot.Clients.GraphQL
{
    public class BotGraphClient: IBotClient
    {
        private readonly GraphQLClient _client;

        public BotGraphClient(IConfiguration config)
        {
            var endPoint = config["GraphQLEndpoint"];
            _client = new GraphQLClient(endPoint);
        }

        public async Task<ProjectTextModel> GetProjectText()
        {
            var query = new GraphQLRequest {
                Query = @"query projectQuery
                        {   projectText
                            { text }
                        }",
            };

            var response = await _client.PostAsync(query);

            if (response.Errors is null)
            {
                return response.GetDataFieldAs<ProjectTextModel>("projectText");
            }

            var error = response.Errors.First();
            throw new GraphQLException(new GraphQLError { Message = $"{error.Message}" });
        }

        public async Task<List<TimedMessageModel>> GetTimedMessages()
        {
            var query = new GraphQLRequest {
                Query = @"query repeatingMessagesQuery
                        { repeatedMessages
                            {  message delay }
                        }",
            };

            var response = await _client.PostAsync(query);

            if (response.Errors is null)
            {
                return response.GetDataFieldAs<List<TimedMessageModel>>("repeatedMessages");
            }

            var error = response.Errors.First();
            throw new GraphQLException(new GraphQLError { Message = $"{error.Message}" });
        }
    }
}
