using System;
using EnemyShip;
using Spaceship;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceship playerSpaceship;
        [SerializeField] private EnemySpaceship enemySpaceship;
        [SerializeField] private EnemySpaceship enemySpaceship2;
        [SerializeField] private EnemySpaceship enemySpaceship3;
        [SerializeField] private BossSpaceship  bossSpaceship;
        public event Action OnRestarted;
        [SerializeField] private int playerSpaceshipHp;
        [SerializeField] private int playerSpaceshipMoveSpeed;
        [SerializeField] private int enemySpaceshipHp;
        [SerializeField] private int enemySpaceshipMoveSpeed;
        [SerializeField] private int bossSpaceshipHp;
        [SerializeField] private int bossSpaceshipMoveSpeed;
        

        private PlayerSpaceship spawnedPlayerShip;
        
        public static GameManager Instance { get; private set; }
        
        private void Awake()
        {
            Debug.Assert(playerSpaceship != null,"PlayerSpaceship cannot be null");
            Debug.Assert(enemySpaceship != null,"EnemySpaceship cannot be null");
            Debug.Assert(playerSpaceshipHp > 0,"PlayerSpaceshipHp cannot be null");
            Debug.Assert(playerSpaceshipMoveSpeed > 0,"PlayerSpaceshipMoveSpeed cannot be null");
            Debug.Assert(enemySpaceshipHp > 0,"EnemySpaceshipHp cannot be null");
            Debug.Assert(enemySpaceshipMoveSpeed > 0,"EnemySpaceshipMoveSpeed cannot be null");
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void StartGame()
        {
            SpawnPlayerSpaceship();
            SpawnEnemySpaceship();
        }
        
        public void NextLevelGame()
        {
            SpawnPlayerSpaceship();
            SpawnBossSpaceship();
        }

        private void SpawnPlayerSpaceship()
        {
            spawnedPlayerShip = Instantiate(playerSpaceship);
            spawnedPlayerShip.Init(playerSpaceshipHp, playerSpaceshipMoveSpeed);
            spawnedPlayerShip.OnExploded += OnPlayerSpaceshipExploded;
        }

        private void OnPlayerSpaceshipExploded()
        {
            UIManager.Instance.OnEndDialog();
            DestroyRemainingShips();
        }
        
        private void SpawnEnemySpaceship()
        {
            var spawnedEnemyShip = Instantiate(enemySpaceship);
            spawnedEnemyShip.Init(enemySpaceshipHp,enemySpaceshipMoveSpeed);
            spawnedEnemyShip.OnExploded += OnEnemySpaceshipExploded;
            
            var spawnedEnemyShip2 = Instantiate(enemySpaceship2);
            spawnedEnemyShip2.Init(enemySpaceshipHp,enemySpaceshipMoveSpeed);
            spawnedEnemyShip2.OnExploded += OnEnemySpaceshipExploded;
            
            var spawnedEnemyShip3 = Instantiate(enemySpaceship3);
            spawnedEnemyShip3.Init(enemySpaceshipHp,enemySpaceshipMoveSpeed);
            spawnedEnemyShip3.OnExploded += OnEnemySpaceshipExploded;
            
            var enemyController = spawnedEnemyShip.GetComponent<EnemyController>();
            enemyController.Init(spawnedPlayerShip);
        }

        private void OnEnemySpaceshipExploded()
        {
            ScoreManager.Instance.UpdateScore(1);
            if (ScoreManager.Instance.GetScore() == 3)
            {
                ShowscoreEnemySpaceshipExploded();
            }
        }

        public void ShowscoreEnemySpaceshipExploded()
        {
            UIManager.Instance.OnNextlevel();
            DestroyRemainingShips();
        }
        
        private void SpawnBossSpaceship()
        {
            var spawnedBossShip = Instantiate(bossSpaceship);
            spawnedBossShip.Init(bossSpaceshipHp,bossSpaceshipMoveSpeed);
            spawnedBossShip.OnExploded += OnBossSpaceshipExploded;

            /*var bossController = spawnedBossShip.GetComponent<BossController>();
            bossController.Init(spawnedPlayerShip);*/
        }
        
        private void OnBossSpaceshipExploded()
        {
            ScoreManager.Instance.UpdateScore(5);
            UIManager.Instance.OnEndDialog();
            DestroyRemainingShips();
        }

        /*private void Restart()
        {
            
            //menuDialog.gameObject.SetActive(true);
            //showDialog.gameObject.SetActive(false);
            OnRestarted?.Invoke();
        }*/

        private void DestroyRemainingShips()
        {
            /*if (enemySpaceshipHp == 0)
            {
                var remainingEnemies = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in remainingEnemies)
                {
                    Destroy(player);
                }
            }

            if (playerSpaceshipHp == 0)
            {
                var remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in remainingEnemies)
                {
                    Destroy(enemy);
                }
            }*/
            var remainingPlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in remainingPlayers)
            {
                Destroy(player);
            }
            
            var remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in remainingEnemies)
            {
                Destroy(enemy);
            }
            
            var remainingBoss = GameObject.FindGameObjectsWithTag("Boss");
            foreach (var boss in remainingBoss)
            {
                Destroy(boss);
            }
        }
    }
}
