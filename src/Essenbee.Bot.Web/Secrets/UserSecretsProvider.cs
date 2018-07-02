using Microsoft.Extensions.Options;

namespace Essenbee.Bot.Web
{
    public class UserSecretsProvider
    {
        public UserSecrets Secrets { get; }

        public UserSecretsProvider(IOptions<UserSecrets> secrets)
        {
            Secrets = secrets.Value;
        }
    }
}
