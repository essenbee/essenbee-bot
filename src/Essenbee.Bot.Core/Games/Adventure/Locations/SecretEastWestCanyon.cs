using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SecretEastWestCanyon : AdventureLocation
    {
        public SecretEastWestCanyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SecretEastWestCanyon;
            Name = "Secret E-W Canyon";
            ShortDescription = "in secret east-west canyon";
            LongDescription = "in a secret canyon which here runs East-West. It crosses over a very tight canyon 15 feet below. " +
                "If you go down you may not be able to get back up.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.HallOfMountainKing, "east", "e"),
                new PlayerMove("", Location.SecretNorthEastCanyon, "west", "w"),
                // new PlayerMove("", Location., "down", "d"),
            };
        }
    }
}
