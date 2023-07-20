using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public Health healthCmp;
        public Combat combatCmp;
        public CharacterStatsSO stats;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
        }

        private void Start()
        {
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;
        }
    }
}
