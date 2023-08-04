using UnityEngine;

namespace RPG.Character
{
    public sealed class AIChaseState : IAIState
    {
        public void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.UpdateAgentSpeed(enemy.stats.runSpeed, false);
        }

        public void UpdateState(EnemyController enemy)
        {
            if (enemy.IsPlayerInAttackRange)
            {
                enemy.SwitchState(enemy.attackState);
                return;
            }
            if (!enemy.IsPlayerInChaseRange)
            {
                enemy.SwitchState(enemy.returnState);
                return;
            }

            ChasePlayer(enemy);

            Vector3 playerDirection = enemy.player.transform.position - enemy.transform.position;

            enemy.movementCmp.Rotate(playerDirection);
        }

        private void ChasePlayer(EnemyController enemy)
        {
            var playerPosition = enemy.player.transform.position;
            enemy.movementCmp.MoveAgentByDestination(playerPosition);
        }
    }
}
