using RPG.Character;
using RPG.Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Quests;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        private readonly List<string> sceneEnemyIds = new();
        private readonly List<GameObject> enemiesAlive = new();

        private void Start()
        {
            var sceneEnemies = GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG);

            var defeatedEnemyIds = PlayerPrefsUtil.GetString(SaveConstants.ENEMIES_DEFEATED);

            // ? The course solution checked if the enemy was defeated in the EnemyController
            foreach (var enemy in sceneEnemies)
            {
                var enemyId = enemy.GetComponent<EnemyController>().enemyId;
                var isDefeated = defeatedEnemyIds.Contains(enemyId);

                if (isDefeated)
                    Destroy(enemy);
                else
                    sceneEnemyIds.Add(enemyId);
            }
        }

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

            enemiesAlive.AddRange(GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG));

            sceneEnemyIds.ForEach(SaveDefeatedEnemies);

            var inventoryCmp = player.GetComponent<Inventory>();

            inventoryCmp.items.ForEach(SaveQuestItem);

            var npcs = GameObject.FindGameObjectsWithTag(Constants.NPC_WITH_QUEST_TAG).ToList();

            npcs.ForEach(SaveNpcItem);
        }

        private void SaveDefeatedEnemies(string enemyId)
        {
            // TODO - benchmark and try to optimize
            bool isAlive = enemiesAlive.Any(
                x => x.GetComponent<EnemyController>().enemyId == enemyId
            );

            // * Course solution
            //enemiesAlive.ForEach((x) =>
            //{
            //    if (x.GetComponent<EnemyController>().enemyId == enemyId) isAlive = true;
            //});

            if (isAlive)
                return;

            // TODO - benchmark and try to optimize, getting and saving player prefs on each iteration seems _really_ slow
            List<string> enemiesDefeated = PlayerPrefsUtil.GetString(
                SaveConstants.ENEMIES_DEFEATED
            );
            enemiesDefeated.Add(enemyId);

            PlayerPrefsUtil.SetString(SaveConstants.ENEMIES_DEFEATED, enemiesDefeated);
        }

        public void SaveQuestItem(QuestItemSO item)
        {
            // TODO refactor to HashSet<T> to guarantee there's no duplicates
            List<string> playerItems = PlayerPrefsUtil.GetString(SaveConstants.PLAYER_ITEMS);
            playerItems.Add(item.itemName);

            PlayerPrefsUtil.SetString(SaveConstants.PLAYER_ITEMS, playerItems);
        }

        private void SaveNpcItem(GameObject npc)
        {
            var npcControllerCmp = npc.GetComponent<NpcController>();

            if (!npcControllerCmp.hasQuestItem)
                return;

            var npcItems = PlayerPrefsUtil.GetString(SaveConstants.NPC_ITEMS);

            npcItems.Add(npcControllerCmp.desiredQuestItem.itemName);

            PlayerPrefsUtil.SetString(SaveConstants.NPC_ITEMS, npcItems);
        }
    }
}
