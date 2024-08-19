using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaceship
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rigidbody2D;

        public void Init(Vector2 direction)
        {
            Move(direction);
        }

        private void Awake()
        {
            Debug.Assert(rigidbody2D != null,"Rigibody cannot be null");
        }

        private void Move(Vector2 direction)
        {
            rigidbody2D.velocity = direction * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("ObjectDestroyer"))
            {
                gameObject.SetActive(false);
                return;
            }
            
            var target = other.gameObject.GetComponent<IDamagable>();
            target?.TakeHit(damage);
        }
    }
}


