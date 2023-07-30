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

        private void OnTriggerEnter()
        {
            isInteractable = true;
            print("player detected!");
        }

        private void OnTriggerExit(Collider other)
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !isInteractable || isOpened)
                return;
            EventManager.RaiseTreasureChestOpen(questItem);

            animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            isOpened = true;
        }
    }
}
