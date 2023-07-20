namespace RPG.Character
{
    public class AIPatrolState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolCmp.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.IsPlayerInChaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            enemy.patrolCmp.CalculateNextPosition();
            var currentPosition = enemy.transform.position;
            var newPosition = enemy.patrolCmp.GetNextPosition();
            var offset = newPosition - currentPosition;

            enemy.movementCmp.MoveAgentByOffset(offset);
        }
    }
}
