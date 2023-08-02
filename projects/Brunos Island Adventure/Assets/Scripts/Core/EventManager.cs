using RPG.Quests;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePlayerPotions;
        public static event UnityAction<TextAsset, GameObject> OnOpenDialogue;
        public static event UnityAction<QuestItemSO, bool> OnTreasureChestOpen;
        public static event UnityAction<bool> OnToggleUI;
        public static event UnityAction<RewardSO> OnReward;
        public static event UnityAction<Collider, int> OnPortalEnter;
        public static event UnityAction<bool> OnCutsceneUpdated;
        public static event UnityAction OnVictory;
        public static event UnityAction OnGameOver;

        // NOTE Raise event == Emit event
        public static void RaiseChangePlayerHealth(float newHealth) =>
            OnChangePlayerHealth?.Invoke(newHealth);

        public static void RaiseChangePlayerPotions(int newPotions) =>
            OnChangePlayerPotions?.Invoke(newPotions);

        public static void RaiseOpenDialogue(TextAsset inkJson, GameObject npc) =>
            OnOpenDialogue?.Invoke(inkJson, npc);

        public static void RaiseTreasureChestOpen(QuestItemSO item, bool showUi) =>
            OnTreasureChestOpen?.Invoke(item, showUi);

        public static void RaiseToggleUI(bool isOpened) => OnToggleUI?.Invoke(isOpened);

        public static void RaiseReward(RewardSO reward) => OnReward?.Invoke(reward);

        // TODO save player data on portal enter
        public static void RaisePortalEnter(Collider player, int sceneIndex) =>
            OnPortalEnter?.Invoke(player, sceneIndex);

        public static void RaiseCutsceneUpdated(bool isPlaying) =>
            OnCutsceneUpdated?.Invoke(isPlaying);

        public static void RaiseVictory() => OnVictory?.Invoke();

        public static void RaiseGameOver() => OnGameOver?.Invoke();
    }
}
