using UnityEngine;

namespace RPG.Character
{
    // ? This is different than the course solution for handling potions
    public class PotionInventory : MonoBehaviour
    {
        public int Potions { get; private set; }

        public bool SetPotions(int potions)
        {
            if (potions < 0)
                return false;

            Potions = potions;
            return true;
        }

        public bool UsePotion() => SetPotions(Potions - 1);
    }
}
