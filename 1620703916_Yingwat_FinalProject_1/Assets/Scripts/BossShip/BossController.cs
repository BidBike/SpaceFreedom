using System.Collections;
using System.Collections.Generic;
using Spaceship;
using UnityEngine;

public class BossController : MonoBehaviour
{
        [SerializeField] private BossSpaceship bossSpaceship;
        [SerializeField] private float chasingThresholdDistance;
        
        [SerializeField] private PlayerSpaceship spawnedPlayerSpaceship;

        public void Init(PlayerSpaceship playerSpaceship)
        {
            spawnedPlayerSpaceship = playerSpaceship;
        }

        private void Update()
        {
            MoveToPlayer();
            bossSpaceship.Fire();
        }

        private void MoveToPlayer()
        {
            var distanceToPlayer = Vector2.Distance(spawnedPlayerSpaceship.transform.position, transform.position);
            Debug.Log(distanceToPlayer);

            if (distanceToPlayer < chasingThresholdDistance)
            {
                var direction = (Vector2) (spawnedPlayerSpaceship.transform.position - transform.position);
                direction.Normalize();
                var distance = direction * bossSpaceship.Speed * Time.deltaTime;
                gameObject.transform.Translate(distance);
            }
        }
}
