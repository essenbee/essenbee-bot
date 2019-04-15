using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SecretNorthSouthCanyon : AdventureLocation
    {

        public SecretNorthSouthCanyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SecretNorthSouthCanyon;
            Name = "secret canyon";
            ShortDescription = "in a secret canyon which exits to the north and south";
            LongDescription = "in a secret canyon which exits to the north and south.";
        }
    }
}
