using RPG.Core;
using RPG.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Quests
{
    public class TreasureChest : MonoBehaviour
    {
        [SerializeField]
        private QuestItemSO questItem;

        public Animator animatorCmp;

        [SerializeField]
        private bool isInteractable = false;

        [SerializeField]
        private bool isOpened = false;

        private void Start()
        {
            if (PlayerPrefs.HasKey(SaveConsts.PLAYER_ITEMS))
            {
                var playerItems = PlayerPrefsUtil.GetString(SaveConsts.PLAYER_ITEMS);

                playerItems.ForEach(CheckItem);
            }
        }

        private void OnTriggerEnter()
        {
            isInteractable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !isInteractable || isOpened)
                return;

            // TODO refactor to method
            EventManager.RaiseTreasureChestOpen(questItem, true);
            isOpened = true;
            animatorCmp.SetBool(Consts.IS_SHAKING_ANIMATOR_PARAM, false);

            var audioSourceCmp = GetComponent<AudioSource>();
            if (audioSourceCmp.clip != null)
                audioSourceCmp.Play();
        }

        private void CheckItem(string itemName)
        {
            if (itemName != questItem.itemName)
                return;

            EventManager.RaiseTreasureChestOpen(questItem, false);
            isOpened = true;
            animatorCmp.SetBool(Consts.IS_SHAKING_ANIMATOR_PARAM, false);
        }
    }
}
