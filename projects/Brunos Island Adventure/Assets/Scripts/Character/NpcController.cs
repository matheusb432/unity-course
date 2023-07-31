using RPG.Core;
using RPG.Quests;
using RPG.Util;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class NpcController : MonoBehaviour
    {
        public QuestItemSO desiredQuestItem;

        public TextAsset inkJson;
        private Canvas canvasCmp;
        private Reward rewardCmp;

        private Inventory playerInventory;

        public bool hasQuestItem = false;

        public bool AlreadyCompletedQuest => hasQuestItem;

        private void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
            rewardCmp = GetComponent<Reward>();
            // ! course solution had the player inventory retrieval on CheckPlayerForQuestItem instead
            playerInventory = GameObject
                .FindWithTag(Constants.PLAYER_TAG)
                .GetComponent<Inventory>();
        }

        // NOTE OnTriggerEnter is called when the collider enters the trigger area, which would be the player being near the NPC
        private void OnTriggerEnter()
        {
            canvasCmp.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            canvasCmp.enabled = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            var isNearPlayer = canvasCmp.enabled;
            if (!context.performed || !isNearPlayer)
                return;

            if (inkJson == null)
            {
                Debug.LogWarning("add an ink file to the npc.");
                return;
            }

            // NOTE `gameObject` is a MonoBehavior field that contains a reference to this game object
            EventManager.RaiseOpenDialogue(inkJson, gameObject);
        }

        public bool CheckPlayerForQuestItem()
        {
            if (hasQuestItem)
                return true;

            hasQuestItem = playerInventory.HasItem(desiredQuestItem);

            if (rewardCmp != null && hasQuestItem)
                rewardCmp.SendReward();

            return hasQuestItem;
        }

        /* // ? Alternative solution to get distance from player
        private bool IsNearObject(Vector3 self, Vector3 other)
        {
            // ? Calculates distance via the square root of the sum of the squares of the differences between corresponding coordinates
            var distance = Vector3.Distance(self, other);

            return distance < 1.35f;
        }
        */
    }
}
