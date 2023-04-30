using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Car;

public class Bullet : Projectile
{

    public override int Damage => 10;
    public float speed = 20f;
    public float lifeTime = 5f;
}
