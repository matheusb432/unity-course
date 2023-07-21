using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Quest
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

            animatorCmp.SetBool("IsShaking", false);
            isOpened = true;
        }
    }
}
