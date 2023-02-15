using System.Collections;
using UnityEngine;

/// <summary>
/// projectile that moves in a straight direction
/// </summary>
public class StraightProjectile : Projectile
{
    Vector3 direction;
    public void Init(Vector2 direction, int damage = 1, int pierce = 1, float speed = 1, float lifespan=1)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        this.pierce = pierce;
        this.speed = speed;
        this.lifespan = lifespan;
        base.Init();
    }
    protected override void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }
    protected override void OnDestroy()
    {
        //TODO
    }
    
}
