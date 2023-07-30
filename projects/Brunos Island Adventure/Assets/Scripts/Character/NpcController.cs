using RPG.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class NpcController : MonoBehaviour
    {
        public TextAsset inkJson;
        public Canvas canvasCmp;

        private void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
        }

        // NOTE OnTriggerEnter is called when the collider enters the trigger area, which would be the player being near the NPC
        private void OnTriggerEnter()
        {
            print("called!");
            canvasCmp.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            print("called2!");
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

            print("talking with npc");
            EventManager.RaiseOpenDialogue(inkJson);
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
