using System;
using System.Collections;
using System.Collections.Generic;
using Spaceship;
using UnityEngine;

namespace EnemyShip
{
    public class EnemyController : MonoBehaviour
    {
        /*[SerializeField] private Transform playerTransform;
        [SerializeField] private float enemyShipSpeed = 5;
        [SerializeField] private Renderer playerRenderer;

        private float chasingThresholdDistance = 1.0f;
        private Renderer enemyRenderer;

        private void Awake()
        {
            enemyRenderer = GetComponent<Renderer>();
        }

        void Update()
        {
            MoveToPlayer();
        }

        private void OnDrawGizmos()
        {
            CollisionDebug();
        }

        private void CollisionDebug()
        {
            if (enemyRenderer != null && playerRenderer != null)
            {
                if (intersectAABB(enemyRenderer.bounds,playerRenderer.bounds))
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                
                Gizmos.DrawWireCube(enemyRenderer.bounds.center,2*enemyRenderer.bounds.extents);
                Gizmos.DrawWireCube(playerRenderer.bounds.center,2*enemyRenderer.bounds.extents);
            }
        }

        private bool intersectAABB(Bounds a, Bounds b)
        {
            return (a.min.x <= b.max.x && a.max.x >= b.min.x) && 
                   (a.min.y <= b.max.y && a.max.y >= b.min.y);
        }

        private void MoveToPlayer()
        {
            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = playerTransform.position;
            enemyPosition.z = playerPosition.z; // ensure there is no 3D rotation by aligning Z position
            
            Vector3 vectorToTarget = playerPosition - enemyPosition; // vector from this object towards the target location
            Vector3 directionToTarget = vectorToTarget.normalized;
            Vector3 velocity = directionToTarget * enemyShipSpeed;
                       
            float distanceToTarget = vectorToTarget.magnitude;

            if (distanceToTarget > chasingThresholdDistance)
            {
                transform.Translate(velocity * Time.deltaTime);
            }

        }*/
        [SerializeField] private EnemySpaceship enemySpaceship;
        [SerializeField] private float chasingThresholdDistance;
        
        [SerializeField] private PlayerSpaceship spawnedPlayerSpaceship;

        public void Init(PlayerSpaceship playerSpaceship)
        {
            spawnedPlayerSpaceship = playerSpaceship;
        }

        private void Update()
        {
            MoveToPlayer();
            enemySpaceship.Fire();
        }

        private void MoveToPlayer()
        {
            var distanceToPlayer = Vector2.Distance(spawnedPlayerSpaceship.transform.position, transform.position);
            Debug.Log(distanceToPlayer);

            if (distanceToPlayer < chasingThresholdDistance)
            {
                var direction = (Vector2) (spawnedPlayerSpaceship.transform.position - transform.position);
                direction.Normalize();
                var distance = direction * enemySpaceship.Speed * Time.deltaTime;
                gameObject.transform.Translate(distance);
            }
        }
    }    
}


