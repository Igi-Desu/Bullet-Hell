using UnityEngine;
[RequireComponent(typeof(Timerable))]

public class Boss: BaseEnemy
{
    static GameObject BossSlime_projectile;
    protected Timerable timer;
    float baseAngle=0;

    new void Start()
    {
        base.Start();
        BossSlime_projectile=Resources.Load("Projectiles/BossSlime_projectile") as GameObject;
        timer=GetComponent<Timerable>();
        timer.Init(0.5f,0);
        timer.AddTimerEndEvent(Shoot);
        timer.StartTimer();
    }
   
    protected void Shoot()
    {   int numberOfProj = 8;
        
         if (!facingright && transform.position.x < Target.transform.position.x)
        {
            Flip();
        }
        else if (facingright && transform.position.x > Target.transform.position.x)
        {
            Flip();
        }
       
        baseAngle+=5;
        float angle = 360 / numberOfProj;
        Vector2 v1 = IgorUtils.Vectors.RotateVector(Vector2.up, baseAngle);
        for (int i = 0; i < numberOfProj; i++)
        {
            v1 = IgorUtils.Vectors.RotateVector(v1, angle);
            var noz = Instantiate(BossSlime_projectile, transform.position,IgorUtils.Rotation.GetRotation(Vector2.right, v1));
            noz.GetComponent<StraightProjectile>().Init(v1, 1, pierce:1, 2,5); 
        }
    }
    override protected void DropLoot(){
        var orb = Instantiate(ResourcesLibrary.lowExpOrb,transform.position,Quaternion.identity).GetComponent<ExperienceOrb>();
        orb.Init(ExperienceOrb.ExperienceAmount.high);
    }
}