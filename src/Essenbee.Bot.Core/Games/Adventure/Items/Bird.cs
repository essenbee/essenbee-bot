using System.Linq;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Bird : AdventureItem
    {
        internal Bird(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Bird;
            Name = "little *bird* singing cheerfully";
            PluralName = "little *birds* singing cheerfully";
            IsPortable = true;
            IsActive = true;
            MustBeContainedIn = Item.Cage;

            var free = new ItemInteraction(Game, "use", "free", "release");
            free.RegisteredInteractions.Add(new Display("You open the cage and the bird flies out."));
            free.RegisteredInteractions.Add(new RemoveFromInventory());
            free.RegisteredInteractions.Add(new AddToLocation(this));
            Interactions.Add(free);
        }

        public override bool Interact(string verb, AdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("free"))
                {
                    if (!player.Inventory.GetItems().Any(x => x.ItemId.Equals(Item.Cage)))
                    {
                        var msg = new Display("You are not carrying a caged bird!");
                        msg.Do(player);
                        return true;
                    }

                    var cage = player.Inventory.GetItems().First(x => x.ItemId.Equals(Item.Cage));

                    if (cage.Contents.All(c => c.ItemId != Item.Bird))
                    {
                        var msg = new Display("There is no bird in the cage for you to free!");
                        msg.Do(player);
                        return true;
                    }

                    var snake = GetSnakeIfPresent(player);

                    if (snake != null)
                    {
                        SnakeKilled(interaction, snake);
                    }
                }

                foreach (var action in interaction.RegisteredInteractions)
                {
                    action.Do(player, this);
                }

                return true;
            }

            return false;
        }

        private AdventureItem GetSnakeIfPresent(AdventurePlayer player) => 
            player.CurrentLocation.Items.FirstOrDefault(i => i.ItemId.Equals(Item.Snake));

        private void SnakeKilled(IInteraction interaction, AdventureItem snake)
        {
            interaction.RegisteredInteractions.Add(new Display("The bird spots the snake, and attacks it..."));
            interaction.RegisteredInteractions.Add(new RemoveFromLocation(snake));
            interaction.RegisteredInteractions.Add(
                new AddToLocation(ItemFactory.GetInstance(Game, Item.DeadSnake)));
            interaction.RegisteredInteractions.Add(new Display("After a furious battle, the snake lies dead on the floor!"));
            //interaction.RegisteredInteractions.Add(new AddMoves(new List<PlayerMove>
            //{
            //    { new PlayerMove(string.Empty, ) },
            //}, Game, Location.HallOfTheMountainKing));
        }
    }
}
