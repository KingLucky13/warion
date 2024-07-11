using LearnGame.Enemy.States;
using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyAiControllerView : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        public EnemyAiControllerModel Model { get; private set; }

        protected void Awake()
        {
            var player=GameManager.Instance.Player;

            var enemyDirectionController=GetComponent<EnemyDirectionController>();

            var navMesher = new NavMesher(transform);

            _target = new EnemyTarget(transform,_viewRadius,player);
            _stateMachine = new EnemyStateMachine(enemyDirectionController,navMesher,_target);

            Model = new EnemyAiControllerModel(_target, _stateMachine);

        }

        protected void Update()
        {
           Model.AiUpdate();
        }
    }
}