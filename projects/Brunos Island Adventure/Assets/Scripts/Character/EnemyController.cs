using RPG.Util;
using UnityEngine;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        private GameObject player;

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        private void Awake()
        {
            // NOTE FindWithTag() can be slow and should be used carefully
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
        }
    }
}
