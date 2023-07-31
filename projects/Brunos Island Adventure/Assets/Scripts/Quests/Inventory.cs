using RPG.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Quests
{
    public class Inventory : MonoBehaviour
    {
        // NOTE The inventory is a list of items, maybe in a real game it'd be better for it to be a hashmap?
        public List<QuestItemSO> items = new();

        private void OnEnable()
        {
            EventManager.OnTreasureChestOpen += HandleTreasureChestOpen;
        }

        private void OnDisable()
        {
            EventManager.OnTreasureChestOpen -= HandleTreasureChestOpen;
        }

        private void HandleTreasureChestOpen(QuestItemSO item)
        {
            items.Add(item);
        }

        public bool HasItem(QuestItemSO desiredItem)
        {
            return items.Any(x => CompareItem(x, desiredItem));
        }

        private bool CompareItem(QuestItemSO item1, QuestItemSO item2) => item1.name == item2.name;
    }
}
