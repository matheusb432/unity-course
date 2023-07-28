using Assets.Scripts.Character;
using RPG.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public Health healthCmp;
        public Combat combatCmp;
        public Inventory inventoryCmp;
        public CharacterStatsSO stats;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
            inventoryCmp = GetComponent<Inventory>();
        }

        private void Start()
        {
            healthCmp.healthPoints = healthCmp.maxHealth = stats.health;
            combatCmp.damage = stats.damage;
            inventoryCmp.SetPotions(5);

            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePlayerPotions(inventoryCmp.Potions);
        }

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            var didUse = inventoryCmp.UsePotion();
            if (didUse)
            {
                healthCmp.Heal(50);
                EventManager.RaiseChangePlayerPotions(inventoryCmp.Potions);
            }
        }
    }
}
