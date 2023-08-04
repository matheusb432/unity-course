using RPG.Core;
using RPG.Quests;
using RPG.Util;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public sealed class PlayerController : MonoBehaviour
    {
        [NonSerialized]
        public Health healthCmp;

        [NonSerialized]
        public Combat combatCmp;

        [NonSerialized]
        public Movement movementCmp;

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
            movementCmp = GetComponent<Movement>();
            axeWeapon = GameObject.FindGameObjectWithTag(Consts.AXE_TAG);
            swordWeapon = GameObject.FindGameObjectWithTag(Consts.SWORD_TAG);
            potionInventoryCmp = GetComponent<PotionInventory>();
        }

        private void Start()
        {
            var hasSavedData = PlayerPrefs.HasKey(SaveConsts.HEALTH);
            if (hasSavedData)
            {
                healthCmp.maxHealth = stats.health;
                healthCmp.healthPoints = PlayerPrefs.GetFloat(SaveConsts.HEALTH);
                combatCmp.damage = PlayerPrefs.GetFloat(SaveConsts.DAMAGE);
                potionInventoryCmp.SetPotions(PlayerPrefs.GetInt(SaveConsts.POTIONS));
                weapon = (Weapons)PlayerPrefs.GetInt(SaveConsts.WEAPON);

                var agentCmp = GetComponent<NavMeshAgent>();
                // NOTE loads first object of specified type, is slower than GetComponent so it shouldn't be used if possible
                // ? is only used here for demo purposes, ideally a tag should be used to get the portal instead
                var portalCmp = FindObjectOfType<Portal>();

                agentCmp.Warp(portalCmp.spawnPoint.position);
                transform.rotation = portalCmp.spawnPoint.rotation;
            }
            else
            {
                healthCmp.healthPoints = healthCmp.maxHealth = stats.health;
                combatCmp.damage = stats.damage;
                potionInventoryCmp.SetPotions(5);
            }

            movementCmp.UpdateAgentSpeed(stats.runSpeed, false);

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
            healthCmp.Heal(reward.bonusHealth);
            potionInventoryCmp.AddPotions(reward.bonusPotions);
            // NOTE updating scriptable objects values will persist after the game has been closed
            //stats.damage += reward.bonusDamage;
            combatCmp.damage += reward.bonusDamage;

            if (reward.forceWeaponSwap)
            {
                weapon = reward.weapon;
                SetWeapon();
            }

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
