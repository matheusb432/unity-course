using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;

        public static event UnityAction<int> OnChangePlayerPotions;

        // NOTE Raise event == Emit event
        public static void RaiseChangePlayerHealth(float newHealth) =>
            OnChangePlayerHealth?.Invoke(newHealth);

        public static void RaiseChangePlayerPotions(int newPotions) =>
            OnChangePlayerPotions?.Invoke(newPotions);
    }
}
