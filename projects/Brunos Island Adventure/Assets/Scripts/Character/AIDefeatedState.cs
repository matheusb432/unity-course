using UnityEngine;

namespace RPG.Character
{
    public class AIDefeatedState : IAIState
    {
        public void EnterState(EnemyController enemy)
        {
            var audioSourceCmp = enemy.GetComponent<AudioSource>();

            if (audioSourceCmp.clip == null)
                return;

            audioSourceCmp.Play();
        }

        public void UpdateState(EnemyController enemy) { }
    }
}
