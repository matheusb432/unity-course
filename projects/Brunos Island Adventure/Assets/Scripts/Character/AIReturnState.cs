using UnityEngine;

namespace RPG.Character
{
    public class AIReturnState : IAIState
    {
        private Vector3 targetPosition;

        public void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.UpdateAgentSpeed(enemy.stats.walkSpeed, true);

            targetPosition =
                enemy.patrolCmp != null
                    ? enemy.patrolCmp.GetNextPosition()
                    : enemy.originalPosition;

            enemy.movementCmp.MoveAgentByDestination(targetPosition);
        }

        public void UpdateState(EnemyController enemy)
        {
            if (enemy.IsPlayerInChaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            if (enemy.movementCmp.ReachedDestination())
            {
                if (enemy.patrolCmp != null)
                {
                    enemy.SwitchState(enemy.patrolState);
                    return;
                }

                enemy.movementCmp.isMoving = false;
                enemy.movementCmp.ResetRotate();
                return;
            }

            Vector3 newForwardVector =
                enemy.patrolCmp != null
                    ? targetPosition - enemy.transform.position
                    : enemy.originalPosition - enemy.transform.position;

            newForwardVector.y = 0;

            enemy.movementCmp.Rotate(newForwardVector);
        }
    }
}
