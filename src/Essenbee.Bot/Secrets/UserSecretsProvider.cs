using Microsoft.Extensions.Options;

namespace Essenbee.Bot
{
    public class UserSecretsProvider
    {
        private readonly UserSecrets _secrets;

        public UserSecretsProvider(IOptions<UserSecrets> secrets)
        {
            _secrets = secrets.Value;
        }
    }
}
