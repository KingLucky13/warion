using LearnGame.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LearnGame.CompositionRoot;
using LearnGame.Timer;

namespace LearnGame
{
    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {  get; private set; }

        public event Action Win;
        public event Action Loss;

        public PlayerCharacterView Player {  get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set; }

        public TimerUI Timer { get; private set; }

        [SerializeField]
        private CharacterCompositionRoot _playerCompositionRoot;

        [SerializeField]
        private List<CharacterCompositionRoot> _enemiesCompositionRoot;


        protected void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            ITimer timer = new UnityTimer();

            Player =(PlayerCharacterView) _playerCompositionRoot.Compose(timer);
            Enemies = new List<EnemyCharacterView>(_enemiesCompositionRoot.Count);
            foreach (var enemyRoot in _enemiesCompositionRoot)
            {
                Enemies.Add((EnemyCharacterView)enemyRoot.Compose(timer));
            }

            Player.Dead += OnPlayerDead;

            foreach (var enemy in Enemies)
                enemy.Dead += OnEnemyDead;

            Timer = FindObjectOfType<TimerUI>();
            Timer.TimeEnd += PlayerLoose;
            Time.timeScale = 1f;

            _format = _remainingEnemiesText.text;
            _remainingEnemiesText.text = string.Format(_format, Enemies.Count);
        }

        protected void OnDestroy()
        {
            Player.Dead-= OnPlayerDead;
            foreach (var enemy in Enemies)
                enemy.Dead -= OnEnemyDead;

            Timer.TimeEnd -= PlayerLoose;
        }

        [SerializeField]
        private TextMeshProUGUI _remainingEnemiesText;
        private string _format;

        private void OnEnemyDead(BaseCharacterView sender)
        {
            var enemy=sender as EnemyCharacterView;
            Enemies.Remove(enemy);
            enemy.Dead -= OnEnemyDead;
            _remainingEnemiesText.text = string.Format(_format, Enemies.Count);

            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void OnPlayerDead(BaseCharacterView sender)
        {
            Player.Dead -= OnPlayerDead;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }

        private void PlayerLoose()
        {
            Timer.TimeEnd -= PlayerLoose;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }
    }
}