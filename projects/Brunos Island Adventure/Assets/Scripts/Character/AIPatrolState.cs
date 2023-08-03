namespace RPG.Character
{
    public class AIPatrolState : IAIState
    {
        public void EnterState(EnemyController enemy)
        {
            enemy.patrolCmp.ResetTimers();
        }

        public void UpdateState(EnemyController enemy)
        {
            if (enemy.IsPlayerInChaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            var oldPosition = enemy.patrolCmp.GetNextPosition();

            enemy.patrolCmp.CalculateNextPosition();
            var currentPosition = enemy.transform.position;
            var newPosition = enemy.patrolCmp.GetNextPosition();
            var offset = newPosition - currentPosition;

            enemy.movementCmp.MoveAgentByOffset(offset);

            // NOTE getting a position 2% farther than the destination to have accurate rotations
            // ? This is necessary since the NPCs can pause in their pathing
            var fartherOutPosition = enemy.patrolCmp.GetFartherOutPosition();
            var newForwardVector = fartherOutPosition - currentPosition;
            newForwardVector.y = 0;

            enemy.movementCmp.Rotate(newForwardVector);

            if (oldPosition == newPosition)
            {
                enemy.movementCmp.isMoving = false;
            }
        }
    }
}
