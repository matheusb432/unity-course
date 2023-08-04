using RPG.Character;
using RPG.Quests;
using RPG.Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        public static bool IsUiOpen = false;

        private readonly List<string> sceneEnemyIds = new();
        private readonly List<GameObject> enemiesAlive = new();

        private PlayerInput playerInputCmp;

        private void Awake()
        {
            playerInputCmp = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            var sceneEnemies = GameObject.FindGameObjectsWithTag(Consts.ENEMY_TAG);

            var defeatedEnemyIds = PlayerPrefsUtil.GetStrings(SaveConsts.ENEMIES_DEFEATED);

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
            EventManager.OnCutsceneUpdated += HandleCutsceneUpdated;
        }

        private void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlePortalEnter;
            EventManager.OnCutsceneUpdated -= HandleCutsceneUpdated;
        }

        private void HandlePortalEnter(Collider player, int nextSceneIndex)
        {
            var playerControllerCmp = player.GetComponent<PlayerController>();

            // NOTE Saving player data in PlayerPrefs when entering a portal
            PlayerPrefs.SetFloat(SaveConsts.HEALTH, playerControllerCmp.healthCmp.healthPoints);
            PlayerPrefs.SetInt(SaveConsts.POTIONS, playerControllerCmp.potionInventoryCmp.Potions);
            PlayerPrefs.SetFloat(SaveConsts.DAMAGE, playerControllerCmp.combatCmp.damage);
            PlayerPrefs.SetInt(SaveConsts.WEAPON, (int)playerControllerCmp.weapon);
            PlayerPrefs.SetInt(SaveConsts.SCENE_INDEX, nextSceneIndex);

            enemiesAlive.AddRange(GameObject.FindGameObjectsWithTag(Consts.ENEMY_TAG));

            sceneEnemyIds.ForEach(SaveDefeatedEnemies);

            var inventoryCmp = player.GetComponent<Inventory>();

            inventoryCmp.items.ForEach(SaveQuestItem);

            var npcs = GameObject.FindGameObjectsWithTag(Consts.NPC_WITH_QUEST_TAG).ToList();

            npcs.ForEach(SaveNpcItem);
        }

        private void SaveDefeatedEnemies(string enemyId)
        {
            bool isAlive = enemiesAlive.Any(
                x => x.GetComponent<EnemyController>().enemyId == enemyId
            );

            if (isAlive)
                return;

            // ! getting and saving player prefs on each iteration seems _really_ slow
            List<string> enemiesDefeated = PlayerPrefsUtil.GetStrings(SaveConsts.ENEMIES_DEFEATED);
            enemiesDefeated.Add(enemyId);

            PlayerPrefsUtil.SetStrings(SaveConsts.ENEMIES_DEFEATED, enemiesDefeated);
        }

        public void SaveQuestItem(QuestItemSO item)
        {
            List<string> playerItems = PlayerPrefsUtil.GetStrings(SaveConsts.PLAYER_ITEMS);
            playerItems.Add(item.itemName);

            PlayerPrefsUtil.SetStrings(SaveConsts.PLAYER_ITEMS, playerItems);
        }

        private void SaveNpcItem(GameObject npc)
        {
            var npcControllerCmp = npc.GetComponent<NpcController>();

            if (!npcControllerCmp.hasQuestItem)
                return;

            var npcItems = PlayerPrefsUtil.GetStrings(SaveConsts.NPC_ITEMS);

            npcItems.Add(npcControllerCmp.desiredQuestItem.itemName);

            PlayerPrefsUtil.SetStrings(SaveConsts.NPC_ITEMS, npcItems);
        }

        private void HandleCutsceneUpdated(bool isPlaying)
        {
            // ? Could switch action map to a cutscene map, to enable skipping cutscenes
            playerInputCmp.enabled = !isPlaying;
        }
    }
}
