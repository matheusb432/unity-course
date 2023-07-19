using RPG.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("entering attack state...");
            enemy.movementCmp.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (!enemy.IsPlayerInAttackRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            Debug.LogWarning("attacking player!");
        }
    }
}
