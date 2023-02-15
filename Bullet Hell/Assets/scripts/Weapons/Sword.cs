using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    // Start is called before the first frame update
    GameObject Slash;
    float sizeMult=1;
    public override WeaponTypes WeaponType => WeaponTypes.Sword;
    Transform playerPos;
    private void Awake(){
        whatUpgradeDo[0]="Slash enemies in half!";
        whatUpgradeDo[1]="Slashes are bigger and faster";
        whatUpgradeDo[2]="Slashes are bigger and faster";
        whatUpgradeDo[3]="Slashes are bigger and faster";
        whatUpgradeDo[4]="Slashes are bigger and faster";
        whatUpgradeDo[5]="Master of sword";
    }
    new void Start()
    {
        base.Start();
        playerPos=Player.Instance.transform;
        cooldown=2;
        sizeMult=1;
        damage=5;
        timer.Init(cooldown);
        timer.AddTimerEndEvent(SpawnProjectiles);
        timer.StartTimer();
        Slash=Resources.Load("Projectiles/Slash") as GameObject;
        
    }

    protected override void SpawnProjectiles(){
        Vector3 dir = PlayerMovement.Instance.FacingRight? Vector3.left*sizeMult:Vector3.right*sizeMult;
        var slash = Instantiate(Slash,playerPos.position+dir,Quaternion.identity);
        slash.transform.localScale= new Vector3(dir.x/Math.Abs(dir.x)*sizeMult,1*sizeMult,1);
        slash.GetComponent<Slash>().Attack(damage);
    }
    public override void Upgrade()
    {
        base.Upgrade();
        switch(level){
            case 1:
                cooldown=1.75f;
                sizeMult=1.1f;
                break;
            case 2:
                cooldown=1.5f;
                sizeMult=1.2f;
                break;
            case 3:
                cooldown=1.25f;
                sizeMult=1.3f;
                break;
            case 4:
                cooldown=1.0f;
                sizeMult=1.4f;
                break;
            case 5:
                cooldown=0.75f;
                sizeMult=1.5f;
                break;
            case 6:
                cooldown=0.25f;
                sizeMult=1.5f;
                break;


        }
        timer.UpdateTimer(cooldown);
    }
}
