using RPG.Quests;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;

        public static event UnityAction<int> OnChangePlayerPotions;

        public static event UnityAction<TextAsset> OnOpenDialogue;

        public static event UnityAction<QuestItemSO> OnTreasureChestOpen;

        public static event UnityAction<bool> OnToggleUI;

        // NOTE Raise event == Emit event
        public static void RaiseChangePlayerHealth(float newHealth) =>
            OnChangePlayerHealth?.Invoke(newHealth);

        public static void RaiseChangePlayerPotions(int newPotions) =>
            OnChangePlayerPotions?.Invoke(newPotions);

        public static void RaiseOpenDialogue(TextAsset inkJson) => OnOpenDialogue?.Invoke(inkJson);

        public static void RaiseTreasureChestOpen(QuestItemSO item) =>
            OnTreasureChestOpen?.Invoke(item);

        public static void RaiseToggleUI(bool isOpened) => OnToggleUI?.Invoke(isOpened);
    }
}
