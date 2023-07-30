using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "QuestItem", menuName = "RPG/Quest Item", order = 1)]
    public class QuestItemSO : ScriptableObject
    {
        [Tooltip("must be unique")]
        public string itemName;
    }
}
