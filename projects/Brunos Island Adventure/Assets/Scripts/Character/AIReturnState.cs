using UnityEngine;

namespace RPG.Character
{
    public class AIReturnState : AIBaseState
    {
        private Vector3 targetPosition;

        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.UpdateAgentSpeed(enemy.stats.walkSpeed);

            targetPosition =
                enemy.patrolCmp != null
                    ? enemy.patrolCmp.GetNextPosition()
                    : enemy.originalPosition;

            enemy.movementCmp.MoveAgentByDestination(targetPosition);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.IsPlayerInChaseRange)
            {
                // TODO is it good practice to transition states inside states themselves?
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            // TODO refactor to reduce nesting
            if (enemy.movementCmp.ReachedDestination())
            {
                if (enemy.patrolCmp != null)
                {
                    enemy.SwitchState(enemy.patrolState);
                    return;
                }
            }
        }
    }
}
