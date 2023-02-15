using UnityEngine;
[RequireComponent(typeof(Timerable))]

public class SlimeEnemy : BaseEnemy
{
    static GameObject Slime_projectile;
    protected Timerable timer;
    new void Start()
    {
        base.Start();
        Slime_projectile=Resources.Load("Projectiles/Slime_projectile") as GameObject;
        timer=GetComponent<Timerable>();
        timer.Init(0.5f,0);
        timer.AddTimerEndEvent(Shoot);
    }
    new void FixedUpdate()
    {
        if(Vector3.Magnitude(Target.position-transform.position) > 5)
        {
            timer.StopTimer();
            base.FixedUpdate();
        } 
        else
        {   
            timer.StartTimer();
        }       
            
    }
    protected void Shoot()
    {
         if (!facingright && transform.position.x < Target.transform.position.x)
        {
            Flip();
        }
        else if (facingright && transform.position.x > Target.transform.position.x)
        {
            Flip();
        }
        Vector3 v = Target.position-transform.position;
        var pocisk =  Instantiate(Slime_projectile, transform.position,Quaternion.Euler(0, 0, v.y < 0 ? -Vector2.Angle(Vector2.right, v) : Vector2.Angle(Vector2.right, v)));
        pocisk.GetComponent<StraightProjectile>().Init(v, 1, pierce:1, 2,5);
    }
    protected override void DropLoot(){
        var orb = Instantiate(ResourcesLibrary.lowExpOrb,transform.position,Quaternion.identity).GetComponent<ExperienceOrb>();
        orb.Init(ExperienceOrb.ExperienceAmount.medium);
    }
}