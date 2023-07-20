using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Character
{
    public class AIReturnState : AIBaseState
    {
        Vector3 targetPosition;

        public override void EnterState(EnemyController enemy)
        {
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
        }
    }
}
