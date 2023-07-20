namespace RPG.Character
{
    public class AIChaseState : AIBaseState
    {
        public override void EnterState(EnemyController enemy) { }

        public override void UpdateState(EnemyController enemy)
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
        }

        private void ChasePlayer(EnemyController enemy)
        {
            var playerPosition = enemy.player.transform.position;
            enemy.movementCmp.MoveAgentByDestination(playerPosition);
        }
    }
}
