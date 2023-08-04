using RPG.Character;
using RPG.Core;

namespace Assets.Scripts.Character
{
    public sealed class AIAttackState : IAIState
    {
        public void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();
        }

        public void UpdateState(EnemyController enemy)
        {
            if (enemy.player == null || GameManager.IsUiOpen)
            {
                enemy.combatCmp.CancelAttack();
                return;
            }

            if (!enemy.IsPlayerInAttackRange)
            {
                enemy.SwitchState(enemy.chaseState);
                enemy.combatCmp.CancelAttack();
                return;
            }

            enemy.combatCmp.StartAttack();
            // ? Enemy will track the player while attacking
            enemy.transform.LookAt(enemy.player.transform);
        }
    }
}
