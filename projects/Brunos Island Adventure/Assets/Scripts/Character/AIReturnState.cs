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
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.MoveAgentByDestination(enemy.originalPosition);
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
