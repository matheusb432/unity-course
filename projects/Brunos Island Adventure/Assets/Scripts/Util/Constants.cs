namespace RPG.Util
{
    // TODO refactor to `Consts` so it's less verbose
    public static class Constants
    {
        public const string PLAYER_TAG = "Player";
        public const string IS_SHAKING_ANIMATOR_PARAM = "IsShaking";
        public const string ENEMY_TAG = "Enemy";
        public const string NPC_DIALOGUE_ICON_TAG = "NpcDialogueIcon";
        public const string CAMERA_TAG = "MainCamera";
        public const string GAME_MANAGER_TAG = "GameManager";
        public const string SWORD_TAG = "Sword";
        public const string AXE_TAG = "Axe";

        public const string SPEED_ANIMATOR_PARAM = "speed";
        public const string ATTACK_ANIMATOR_PARAM = "attack";
        public const string DEFEATED_ANIMATOR_PARAM = "defeated";

        public const int MAIN_MENU_SCENE_IDX = 0;
        public const int ISLAND_SCENE_IDX = 1;
        public const int DUNGEON_SCENE_IDX = 2;

        public const string GAMEPLAY_ACTION_MAP = "Gameplay";
        public const string UI_ACTION_MAP = "UI";
    }

    public static class UIConstants
    {
        public const string MAIN_MENU_NAME = "main-menu";

        public const string PLAYER_INFO_NAME = "player-info";
        public const string HEALTH_LABEL_NAME = "health-label";
        public const string POTIONS_LABEL_NAME = "potions-label";

        public const string QUEST_ITEM_NAME = "quest-item";
        public const string QUEST_ITEM_LABEL_NAME = "quest-item-label";
        public const string QUEST_ITEM_ICON_NAME = "quest-item-icon";
    }

    public static class SaveConstants
    {
        public const string HEALTH = "Health";
        public const string POTIONS = "Potions";
        public const string DAMAGE = "Damage";
        public const string WEAPON = "Weapon";
        public const string SCENE_INDEX = "SceneIndex";
    }
}
