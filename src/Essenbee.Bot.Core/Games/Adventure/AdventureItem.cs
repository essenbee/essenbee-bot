﻿using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public abstract class AdventureItem : IAdventureItem
    {
        public Guid UniqueId { get; }
        public Item ItemId { get; set; }
        public List<string> Nouns { get; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool IsContainer { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public Item ItemIdToUnlock { get; set; } = Item.None;
        public Item MustBeContainedIn { get; set; } = Item.None;
        public bool IsCreature { get; set; }
        public bool IsPortable { get; set; }
        public bool IsEndlessSupply { get; set; }
        public bool IsTransparent { get; set; }
        public bool IsTreasure { get; set; }
        public bool IsWeapon { get; set; }
        public int Slots { get; set; } = 1;
        public IList<IAdventureItem> Contents { get; set; }
        public IList<IInteraction> Interactions { get; set; }
        public IReadonlyAdventureGame Game { get; }
        public Item PreventTakeItemId { get; set; } = Item.None;
        public string PreventTakeText { get; set; } = "";
        public Dictionary<string, List<string>> PlayerItemState { get; set; } = new Dictionary<string, List<string>>();
        public PlayerStatus GivesPlayerStatus { get; set; } = PlayerStatus.None;

        protected AdventureItem(IReadonlyAdventureGame game, params string[] nouns)
        {
            UniqueId = Guid.NewGuid();
            Game = game;
            Contents = new List<IAdventureItem>();
            Interactions = new List<IInteraction>();
            Nouns = nouns.ToList();
        }

        public bool IsMatch(string noun) => Nouns.Any(v => noun.Equals(v));
        
        public virtual bool RunItemEvents(IAdventurePlayer player, IAdventureItem item) => true;

        public bool ContainerRequired() => MustBeContainedIn != Item.None;

        public virtual bool Interact(string verb, IAdventurePlayer player)
        {
            return true;
        }

        public bool HasState(IAdventurePlayer player, string state)
        {
            if (PlayerItemState.ContainsKey(player.Id))
            {
                var states = PlayerItemState[player.Id];

                if (states.Contains(state))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddPlayerStatusCondition(IAdventurePlayer player, PlayerStatus status)
        {
            if (status != PlayerStatus.None &&
                !player.Statuses.Contains(status))
            {
                player.Statuses.Add(status);
            }
        }

        public void RemovePlayerStatusCondition(IAdventurePlayer player, PlayerStatus status)
        {
            if (status != PlayerStatus.None && 
                player.Statuses.Contains(status))
            {
                player.Statuses.Remove(status);
            }
        }

        public virtual void Give(IAdventurePlayer player, IAdventureItem itemInInventory, IAdventureItem recipient)
        {
            player.ChatClient.PostDirectMessage(player, $"I'm not sure why you are trying to give a {itemInInventory.Name} to a {recipient.Name}");
        }

        public virtual void Attack(IAdventurePlayer player, IAdventureItem target, IAdventureItem weapon = null)
        {
            player.ChatClient.PostDirectMessage(player, $"Why you are trying to attack a {target.Name}? That's not very productive!");
        }
    }
}
