using RPG.Character;
using RPG.Util;
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
            var playerControllerCmp = player.GetComponent<PlayerController>();

            // NOTE Saving player data in PlayerPrefs when entering a portal
            PlayerPrefs.SetFloat(SaveConstants.HEALTH, playerControllerCmp.healthCmp.healthPoints);
            PlayerPrefs.SetInt(
                SaveConstants.POTIONS,
                playerControllerCmp.potionInventoryCmp.Potions
            );
            PlayerPrefs.SetFloat(SaveConstants.DAMAGE, playerControllerCmp.combatCmp.damage);
            PlayerPrefs.SetInt(SaveConstants.WEAPON, (int)playerControllerCmp.weapon);
            PlayerPrefs.SetInt(SaveConstants.SCENE_INDEX, nextSceneIndex);
        }
    }
}
