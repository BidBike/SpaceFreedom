using UnityEngine;

namespace Spaceship
{
    public abstract class BaseSpaceShip : MonoBehaviour
    {
        [SerializeField] protected Bullet defaultBullet;
        [SerializeField] protected Transform gunPosition;

        protected AudioSource audioSource;
        
        public int Hp { get; protected set; }
        public float Speed { get; protected set; }
        public Bullet Bullet { get; protected set; }

        protected void Init(int hp, float speed, Bullet bullet)
        {
            Hp = hp;
            Speed = speed;
            Bullet = bullet;
        }

        public abstract void Fire();
    }

}

