using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
	public class LongWindingCorridor : AdventureLocation
	{
		public LongWindingCorridor(IReadonlyAdventureGame game) : base(game)
		{
			LocationId = Location.LongWindingCorridor;
			Name = "Long Winding Corridor";
			ShortDescription = "a long winding corridor";
			LongDescription = "a long winding corridor sloping out of sight in both directions.";
			Items = new List<IAdventureItem>();
			ValidMoves = new List<IPlayerMove> {
				new PlayerMove(string.Empty, Location.LowRoom, "northeast", "ne"),
				new PlayerMove(string.Empty, Location.SouthWestOfChasm, "southwest", "sw", "up", "u"),
			};
		}
	}
}
