using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaceship
{
    public interface IDamagable
    {
        event Action OnExploded;
        void TakeHit(int damage);
        void Explode();
    }
}

