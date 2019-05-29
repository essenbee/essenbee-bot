using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAdventureItem
    {
        IList<IAdventureItem> Contents { get; set; }
        IReadonlyAdventureGame Game { get; }
        IList<IInteraction> Interactions { get; set; }
        PlayerStatus GivesPlayerStatus { get; set; }
        bool IsActive { get; set; }
        bool IsContainer { get; set; }
        bool IsEndlessSupply { get; set; }
        bool IsLocked { get; set; }
        bool IsOpen { get; set; }
        bool IsPortable { get; set; }
        bool IsTransparent { get; set; }
        bool IsTreasure { get; set; }
        bool IsWeapon { get; set; }
        Item ItemId { get; set; }
        Item ItemIdToUnlock { get; set; }
        Item MustBeContainedIn { get; set; }
        Item PreventTakeItemId { get; set; }
        string PreventTakeText { get; set; }
        string Name { get; set; }
        List<string> Nouns { get; }
        string PluralName { get; set; }
        Guid UniqueId { get; }

        Dictionary<string, List<string>> PlayerItemState { get; set; }

        bool ContainerRequired();
        bool Interact(string verb, IAdventurePlayer player);
        bool IsMatch(string noun);
        bool HasState(IAdventurePlayer player, string state);
        void AddPlayerStatusCondition(IAdventurePlayer player, PlayerStatus status);
        void RemovePlayerStatusCondition(IAdventurePlayer player, PlayerStatus status);
    }
}