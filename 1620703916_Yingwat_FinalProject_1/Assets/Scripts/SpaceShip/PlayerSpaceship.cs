using System;
using Manager;
using UnityEngine;

namespace Spaceship
{
   public class PlayerSpaceship : BaseSpaceShip, IDamagable
   {
      public event Action OnExploded;

      private void Awake()
      {
         Debug.Assert(defaultBullet != null, "DefaultBullet cannot be null");
         Debug.Assert(gunPosition != null, "GunPosition cannot be null");

         audioSource = GetComponent<AudioSource>();
      }

      public void Init(int hp, float speed)
      {
         base.Init(hp, speed, defaultBullet);
      }

      public override void Fire()
      {
         SoundManager.Instance.Play(SoundManager.Sound.PlayerFire);
         var bullet = PoolManager.Instance.GetPooledObject(PoolManager.PoolObjectType.PlayerBullet);
         if (bullet)
         {
            bullet.transform.position = gunPosition.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().Init(Vector2.up);                
         }
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
         SoundManager.Instance.Play(SoundManager.Sound.PlayerExploded);
      }
   } 
}


