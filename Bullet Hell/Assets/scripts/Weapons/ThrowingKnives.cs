using UnityEngine;

public class ThrowingKnives : Weapon{
    GameObject knife;
    public override WeaponTypes WeaponType => WeaponTypes.ThrowingKnives;
    float knifeLifeSpan = 3;
    Transform playerPos;
    private void Awake(){
        whatUpgradeDo[0]="Throws knives where you point!";
        whatUpgradeDo[1]="Faster and sharper!";
        whatUpgradeDo[2]="Faster and sharper!";
        whatUpgradeDo[3]="Faster and sharper!";
        whatUpgradeDo[4]="Faster and sharper!";
        whatUpgradeDo[5]="Faster and sharper!";
    }
    new protected void Start(){
        base.Start();
        cooldown=1;
        timer.Init(cooldown);
        timer.AddTimerEndEvent(SpawnProjectiles);
        timer.StartTimer();
        damage = 5;
        pierce = 1;
        speed = 7;
        knife = Resources.Load("Projectiles/Knife") as GameObject;
        playerPos = Player.Instance.gameObject.transform; 
    }

    protected override void SpawnProjectiles(){
        Vector2 v = IgorUtils.Vectors.GetMousePosNormalized();
        IgorUtils.Vectors.GetMousePos();
        var spawnedKnife = Instantiate(knife, playerPos.position,
        IgorUtils.Rotation.GetRotation(Vector2.right,v),parent);
        spawnedKnife.GetComponent<StraightProjectile>().Init(v, damage, pierce, speed,knifeLifeSpan);
    }
    public override void Upgrade()
    {
        //TODO mimo ze tu patrzysz to nadal nie sa porzadne, trzeba jakis balans zrobic ew. lekki rework broni
        //TODO żeby np rzucac po kazdym upgradzie o 1 noz wiecej
        //base stats
        // damage = 5;
        // pierce = 3;
        // speed = 3;
        //cooldown = 1
        base.Upgrade();
        switch(level){
            case 1:
            damage=8;
            cooldown-=0.1f;
            break;
            case 2:
            damage=10;
            cooldown=0.9f;
            break;
            case 3: 
            damage=12;
            cooldown=0.7f;
            break;
            case 4:
            damage=14;
            cooldown=0.5f;
            break;
            case 5:
            damage=18;
            cooldown=0.3f;
            break;
            case 6:
            damage=20;
            cooldown=0.1f;
            break;
        }
        timer.UpdateTimer(cooldown);
    }
}

