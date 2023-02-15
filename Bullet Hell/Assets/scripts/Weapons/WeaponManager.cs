using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
/// <summary>
/// manages all the weapons that player has
/// </summary>
public class WeaponManager : Singleton<WeaponManager>
{
    //holds a list of all weapons
    public const int maxNumberOfWeapons=3;
    List<Weapon> currentWeapons = new List<Weapon>();
    /// <summary>
    /// List of avaiable weapons to upgrade
    /// </summary>
    public List<Weapon> avaiableWeapons = new();
    /// <summary>
    /// Finds and returns weapon of given tag, if weapon not found returns null
    /// </summary>
    /// <param name="type">weapon type</param>
    /// <returns>null or reference to weapon currently held</returns>
    public Weapon GetWeaponByTag(Weapon.WeaponTypes type){
        return currentWeapons.Find(x => x.WeaponType==type);
    }
    /// <summary>
    /// Returns how many weapons does player currently have
    /// </summary>
    public int WeaponCount => currentWeapons.Count;
    
    /// <summary>
    /// checks if weapon is already in players inventory
    /// </summary>
    /// <returns>returns weapon searched for or null</returns>
    public Weapon WeaponInInventory(Weapon.WeaponTypes type){
        foreach(var weap in currentWeapons){
            if(weap.WeaponType==type){
                return weap;
            }
        }
        return null;
    }

    void Start(){
        // GameObject weap = Resources.Load("Weapons/ThrowingKnives") as GameObject;
        foreach(var weap in ResourcesLibrary.allWeapons){
            avaiableWeapons.Add(weap.GetComponent<Weapon>());
        }
        GameObject weap1 = Resources.Load("Weapons/ThrowingKnives") as GameObject;
        AddWeapon(weap1);
    }

    /// <summary>
    /// upgrades all weapons, that player has
    /// </summary>
    public void UpgradeAllWeapons(){
        foreach(Weapon weap in currentWeapons){
            weap.Upgrade();
        }
        
    }
    /// <summary>
    /// Add weapon to player arsenal, weapon won't be added if it's over the weapon limit
    /// </summary>
    /// <param name="weap">weapon to give it must have Weapon script attached</param>
    /// <returns>weapon component of newly added weapon</returns>
    public Weapon AddWeapon(GameObject weap){
        if(WeaponCount==maxNumberOfWeapons){
            Debug.Log("Weapon not added, reached limit of 6");
            return null;
        }
        if(weap.GetComponent<Weapon>()==null){
            Debug.LogError("You are trying to add something as a weapon that isn't");
            return null;
        }
        GameObject addedWeapon = Player.Instance.AddWeapon(weap);
        var script = addedWeapon.GetComponent<Weapon>();
        currentWeapons.Add(script);
        //remove all weapons that are not in inventory if we reached limit
        //so that they won't be taken in consideration while  finding new ones to upgrade
        if(WeaponCount==maxNumberOfWeapons){
            avaiableWeapons.RemoveAll(x => !WeaponInInventory(x.WeaponType));
        }
        script.onAdd();
        return script;
    }
}
