using UnityEngine;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.OnPortalEnter += HandlePortalEnter;
        }

        private void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlePortalEnter;
        }

        private void HandlePortalEnter(Collider player, int nextSceneIndex)
        {
            print("portal entered");
        }
    }
}
