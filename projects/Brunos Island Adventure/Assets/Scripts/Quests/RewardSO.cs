using RPG.Character;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Reward", menuName = "RPG/Reward", order = 2)]
    public class RewardSO : ScriptableObject
    {
        public float bonusHealth = 0;
        public float bonusDamage = 0;
        public int bonusPotions = 0;
        public bool forceWeaponSwap = false;
        public Weapons weapon = Weapons.Sword;
    }
}
