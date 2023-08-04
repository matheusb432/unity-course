using UnityEngine;

namespace RPG.Character
{
    // ? This is different than the course solution for handling potions
    public sealed class PotionInventory : MonoBehaviour
    {
        public int Potions { get; private set; }

        public bool SetPotions(int potions)
        {
            if (potions < 0)
                return false;

            Potions = potions;
            return true;
        }

        public void AddPotions(int potions) => Potions += potions;

        public bool UsePotion() => SetPotions(Potions - 1);
    }
}
