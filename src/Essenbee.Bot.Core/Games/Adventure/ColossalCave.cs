using System;
using System.Collections.Generic;
using System.Linq;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class ColossalCave
    {
        public Dictionary<Location, AdventureLocation> Build(AdventureGame game)
        {
            var dungeon = new Dictionary<Location, AdventureLocation>();

            try

            {
                var locations = GetType().Assembly.GetTypes().Where(t => typeof(AdventureLocation).IsAssignableFrom(t));

                foreach (var type in locations)
                {
                    if (type.Name == nameof(AdventureLocation)) continue;

                    var loc = Activator.CreateInstance(type, game) as AdventureLocation;
                    if (loc != null)
                    {
                        dungeon.Add(loc.LocationId, loc);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ": " + ex.StackTrace);
            }

            return dungeon;
        }
    }
}
