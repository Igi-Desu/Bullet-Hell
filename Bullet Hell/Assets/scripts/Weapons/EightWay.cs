using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightWay : Weapon
{
    public override WeaponTypes WeaponType => WeaponTypes.KnifeStorm;
    GameObject knife;
    //bool slant = false;
    float lifespan = 3;
    int numberOfKnives = 3;
    //List<Vector2> horizontalVectors = new();

    //List<Vector2> slantVectors = new();

    //List<Vector2> currList;
    private void Awake()
    {
        whatUpgradeDo[0] = "Throws multiple knives!";
        whatUpgradeDo[1] = "more knives!";
        whatUpgradeDo[2] = "more knives!";
        whatUpgradeDo[3] = "more knives!";
        whatUpgradeDo[4] = "more knives!";
        whatUpgradeDo[5] = "more knives!";
    }
    new void Start()
    {
        base.Start();
        //base stats for now 
        numberOfKnives = 3;
        cooldown = 3;
        pierce = 1;
        damage = 2;
        speed = 3;
        timer.Init(cooldown);
        timer.AddTimerEndEvent(SpawnProjectiles);
        timer.StartTimer();
        // horizontalVectors.Add(Vector2.up);
        // horizontalVectors.Add(Vector2.down);
        // horizontalVectors.Add(Vector2.right);
        // horizontalVectors.Add(Vector2.left);
        // slantVectors.Add(new Vector2(1, 1).normalized);
        // slantVectors.Add(new Vector2(1, -1).normalized);
        // slantVectors.Add(new Vector2(-1, 1).normalized);
        // slantVectors.Add(new Vector2(-1, -1).normalized);

        knife = Resources.Load("Projectiles/Knife") as GameObject;
    }

    protected override void SpawnProjectiles()
    {
        //slant = !slant;
        //currList = slant ? slantVectors : horizontalVectors;

        Vector2 v = Vector2.up;
        float angle = 360 / numberOfKnives;
        for (int i = 0; i < numberOfKnives; i++)
        {
            v = IgorUtils.Vectors.RotateVector(v, angle);
            var noz = Instantiate(knife, Player.Instance.transform.position,
            IgorUtils.Rotation.GetRotation(Vector2.right,v), parent);
            StraightProjectile script = noz.GetComponent<StraightProjectile>();
            // Debug.Log("speed = " + speed);
            script.Init(v, damage: damage, pierce: pierce, speed: speed, lifespan: lifespan);
        }
    }
    public override void Upgrade()
    {
        base.Upgrade();
        switch (level)
        {
            case 1:
                numberOfKnives = 3;
                cooldown = 3;
                pierce = 1;
                damage = 2;
                speed = 3;
                break;
            case 2:
                numberOfKnives = 5;
                cooldown = 2.8f;
                damage = 3;
                speed = 3.5f;
                break;
            case 3:
                numberOfKnives = 7;
                cooldown = 2.6f;
                pierce = 2;
                damage = 4;
                speed = 4;
                break;
            case 4:
                numberOfKnives = 9;
                cooldown = 2.4f;
                damage = 5;
                speed = 4.5f;
                break;
            case 5:
                numberOfKnives = 11;
                cooldown = 2.2f;
                damage = 6;
                speed = 5;
                break;
            case 6:
                numberOfKnives = 13;
                cooldown = 2.0f;
                pierce = 3;
                damage = 7;
                speed = 5;
                break;
        }
        timer.UpdateTimer(cooldown);
    }
}
