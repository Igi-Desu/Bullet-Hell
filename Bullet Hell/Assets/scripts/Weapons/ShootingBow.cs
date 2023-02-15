using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBow : Weapon
{
    public override WeaponTypes WeaponType => WeaponTypes.ShootingBow;
    GameObject arrows;
    Transform playerPos;
    List<Vector2> directions = new();
    float arrowLifeSpan = 8;

    private void Awake(){
        whatUpgradeDo[0]="Shoots arrows!";
        whatUpgradeDo[1]="Faster and sharper!";
        whatUpgradeDo[2]="Shots 2 arrows!";
        whatUpgradeDo[3]="Faster and sharper!";
        whatUpgradeDo[4]="Faster and sharper!";
        whatUpgradeDo[5]="Shots 3 arrows!";
    }
    new protected void Start(){
        directions.Add(Vector2.right);
        base.Start();
        cooldown=3;
        timer.Init(cooldown);
        timer.AddTimerEndEvent(SpawnProjectiles);
        timer.StartTimer();
        damage = 3;
        pierce = 15;
        speed = 6;
        arrows = Resources.Load("Projectiles/arrow") as GameObject;
        playerPos = Player.Instance.gameObject.transform; 
    }
    protected override void SpawnProjectiles()
    {

        if (!PlayerMovement.Instance.getFacingright())
        {   
            foreach(Vector2 direction in directions)
            {
                var arrow = Instantiate(arrows,playerPos.position, Quaternion.identity);
                arrow.GetComponent<StraightProjectile>().Init(direction, damage, pierce, speed,arrowLifeSpan);
            }
        }
        else
        {
            foreach(Vector2 direction in directions)
            {
                var temp = new Vector2(-direction.x,direction.y);
                var arrow = Instantiate(arrows,playerPos.position, IgorUtils.Rotation.GetRotation(Vector2.right,temp));
                arrow.GetComponent<StraightProjectile>().Init(temp, damage, pierce, speed,arrowLifeSpan);
            }
        }
    }
    public override void Upgrade()
    {
        base.Upgrade();
        switch(level){
            case 1:
                damage=5;
                cooldown=2.8f;
                break;
            case 2:
                damage=7;
                cooldown=2.6f;
                break;
            case 3:
                damage=9;
                cooldown=2.4f;
                var slightlyDown = IgorUtils.Vectors.RotateVector(Vector2.right,-10);
                directions.Add(slightlyDown);
                break;
            case 4:
                damage=11;
                cooldown=2.2f;
                break;
            case 5:
                damage=13;
                cooldown=2.0f;
                break;
            case 6:
                damage=15;
                cooldown=1.8f;
                var slightlyUp = IgorUtils.Vectors.RotateVector(Vector2.right,10);
                directions.Add(slightlyUp);
                break;
        }
        timer.UpdateTimer(cooldown);
    }
}