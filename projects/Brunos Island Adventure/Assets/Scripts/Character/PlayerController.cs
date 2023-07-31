using Assets.Scripts.Character;
using RPG.Core;
using RPG.Quests;
using RPG.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public Health healthCmp;
        public Combat combatCmp;
        public PotionInventory potionInventoryCmp;
        public CharacterStatsSO stats;
        private GameObject axeWeapon;
        private GameObject swordWeapon;

        public Weapons weapon = Weapons.Axe;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
            axeWeapon = GameObject.FindGameObjectWithTag(Constants.AXE_TAG);
            swordWeapon = GameObject.FindGameObjectWithTag(Constants.SWORD_TAG);
            potionInventoryCmp = GetComponent<PotionInventory>();
        }

        private void Start()
        {
            healthCmp.healthPoints = healthCmp.maxHealth = stats.health;
            combatCmp.damage = stats.damage;
            potionInventoryCmp.SetPotions(5);

            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePlayerPotions(potionInventoryCmp.Potions);

            SetWeapon();
        }

        private void OnEnable()
        {
            EventManager.OnReward += HandleReward;
        }

        private void OnDisable()
        {
            EventManager.OnReward -= HandleReward;
        }

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            var didUse = potionInventoryCmp.UsePotion();
            if (didUse)
            {
                healthCmp.Heal(50);
                EventManager.RaiseChangePlayerPotions(potionInventoryCmp.Potions);
            }
        }

        private void HandleReward(RewardSO reward)
        {
            // TODO change
            healthCmp.Heal(reward.bonusHealth);
            potionInventoryCmp.AddPotions(reward.bonusPotions);
            // NOTE updating scriptable objects values will persist after the game has been closed
            //stats.damage += reward.bonusDamage;
            combatCmp.damage += reward.bonusDamage;

            Debug.LogWarning($"new damage => {stats.damage}");
            if (reward.forceWeaponSwap)
            {
                weapon = reward.weapon;
                SetWeapon();
            }

            // TODO refactor to method to avoid duplication
            EventManager.RaiseChangePlayerPotions(potionInventoryCmp.Potions);
        }

        private void SetWeapon()
        {
            var isAxe = weapon == Weapons.Axe;

            axeWeapon.SetActive(isAxe);
            swordWeapon.SetActive(!isAxe);
        }
    }
}
