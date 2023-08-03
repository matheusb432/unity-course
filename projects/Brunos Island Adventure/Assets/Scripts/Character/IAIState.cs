namespace RPG.Character
{
    public interface IAIState
    {
        void EnterState(EnemyController enemy);

        void UpdateState(EnemyController enemy);
    }
}
