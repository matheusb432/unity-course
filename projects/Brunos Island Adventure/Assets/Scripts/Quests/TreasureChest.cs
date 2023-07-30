using RPG.Core;
using RPG.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Quests
{
    public class TreasureChest : MonoBehaviour
    {
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
            if (!isInteractable || isOpened)
                return;

            animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            isOpened = true;

            EventManager.RaiseTreasureChestOpen();
        }
    }
}
