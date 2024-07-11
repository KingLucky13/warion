using LearnGame.Enemy.States;

namespace LearnGame.Enemy
{
    public class EnemyAiControllerModel
    {
        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        public EnemyAiControllerModel(EnemyTarget target, EnemyStateMachine stateMachine)
        {
            _target = target;
            _stateMachine = stateMachine;
        }

        public void AiUpdate()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}