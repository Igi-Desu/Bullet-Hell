using UnityEngine.Events;
using UnityEngine;
/// <summary>
/// base class of weapons there should be one instance of each weapon
/// </summary>
[RequireComponent(typeof(Timerable))]
public abstract class Weapon : MonoBehaviour{
    ///Add new weapons here so it will be easier to check if we already have that weapon type
    public enum WeaponTypes{KnifeStorm,ThrowingKnives,Sword,ShootingBow};
    public abstract WeaponTypes WeaponType{
        get;
    }
    protected int damage;
    protected float cooldown;
    protected int pierce;
    protected float speed;
    protected int level=3;
    public int Level => level;
    /// <summary>
    /// returns whether weapon can still be upgraded or not
    /// </summary>
    public bool CanUpgrade => !(level==6);
    [SerializeField] protected static Transform parent;
    protected UnityAction upgradeAction;
    protected string[] whatUpgradeDo = new string[6];
    protected Timerable timer;
    protected void Start(){
        timer=GetComponent<Timerable>();
        // upgradeAction += Upgrade;
        // GameManager.Instance.AddMinuteEvent(upgradeAction);
        if (parent == null){
            parent = Player.Instance.transform.parent.Find("Projectiles");
        }
        GameManager.Instance.onGameOver.AddListener(onLose);
    }

    /// <summary>
    /// Spawn projectiles, for example 8 in every direction, burst of 10 in one direction, do as you please :3 
    /// </summary>
    abstract protected void SpawnProjectiles();
    /// <summary>
    /// what happens when we upgrade weapon
    /// </summary>
    virtual public void Upgrade(){
        ++level;
        //if we upgrade weapon and it has max level remove from avaiable weapons list
        if(!CanUpgrade){
            WeaponManager.Instance.avaiableWeapons.RemoveAll(x => x.WeaponType == WeaponType);
        }
    }
    /// <summary>
    /// get tooltip what new level do
    /// </summary>
    /// <param name="level">level of weapon</param>
    /// <returns>tooltip of upgrade</returns>
    public string GetUpgradeTip(int level){
        if(level==6)return "???";
        return whatUpgradeDo[level];
    }
    public void onAdd(){
        level=1;
    }
    public void onLose(){
        //Debug.Log("Wyłaczam bronie");
        this.enabled = false;
        timer.StopTimer();
    }
}
