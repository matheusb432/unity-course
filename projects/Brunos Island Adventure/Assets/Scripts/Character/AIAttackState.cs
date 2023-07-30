using RPG.Character;

namespace Assets.Scripts.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.player == null || enemy.hasUIOpened)
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
