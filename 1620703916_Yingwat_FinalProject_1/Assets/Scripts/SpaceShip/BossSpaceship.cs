using System;
using System.Collections.Generic;
using Manager;
using Spaceship;
using UnityEngine;

namespace Spaceship
{
    public class BossSpaceship : BaseSpaceShip, IDamagable
    {
        public event Action OnExploded;
    
        [SerializeField] private double fireRate = 1;
        private float fireCounter = 0;
        public void Init(int hp, float speed)
        {
            base.Init(hp, speed, defaultBullet);
        }
    
        public void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
    
        public void TakeHit(int damage)
        {
            Hp -= damage;
    
            if (Hp > 0)
            {
                return;
            }
            Explode();
                
        }
        public void Explode()
        {
            Debug.Assert(Hp <=0,"HP is more than Zero.");
            gameObject.SetActive(false);
            Destroy(gameObject);
            OnExploded?.Invoke();
            SoundManager.Instance.Play(SoundManager.Sound.BossExploded);
        }
    
        public override void Fire()
        {
            fireCounter += Time.deltaTime;
            if (fireCounter >= fireRate)
            {
                SoundManager.Instance.Play(SoundManager.Sound.BossFire);
                var bullet = PoolManager.Instance.GetPooledObject(PoolManager.PoolObjectType.BossBullet);
                if (bullet)
                {
                    bullet.transform.position = gunPosition.position;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.SetActive(true);
                    bullet.GetComponent<Bullet>().Init(Vector2.down);    
                }
                fireCounter = 0;
            }
        }
    }
}

