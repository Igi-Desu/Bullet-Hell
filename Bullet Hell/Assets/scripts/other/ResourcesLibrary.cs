using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesLibrary
{
    public static GameObject skeleton =Resources.Load("Enemies/Skeleton") as GameObject;
    public static GameObject lowExpOrb =Resources.Load("Other/Exp") as GameObject;
    //!WeaponIcons WIP
    //TODO ktos jezeli ktos ma czas i checi sprawdzic czemu na razie to nie dziala zapraszam
    //TODO wykorzystywane sa w leveluphud w funkcji CreateButton, i w przyszlosci beda w hudzie
    /// <summary>
    /// Dictionary containing icons of all weapons currently in game 
    /// </summary>
    /// <returns></returns>
    public static Dictionary<Weapon.WeaponTypes,Sprite> WeaponIcons= new(){
        {Weapon.WeaponTypes.KnifeStorm,Resources.Load<Sprite>("Weapons/Icons/knife")},
        {Weapon.WeaponTypes.ThrowingKnives,Resources.Load<Sprite>("Weapons/Icons/knife")},
        {Weapon.WeaponTypes.Sword,Resources.Load<Sprite>("Weapons/Icons/sword")},
        {Weapon.WeaponTypes.ShootingBow,Resources.Load<Sprite>("Weapons/Icons/bow")}
    };
    /// <summary>
    /// List of all weapons currently avaiable in game
    /// </summary>
    public static List<GameObject> allWeapons= new List<GameObject>(){
        Resources.Load("Weapons/EightWay") as GameObject,
        Resources.Load("Weapons/ThrowingKnives") as GameObject,
        Resources.Load("Weapons/Sword") as GameObject,
        Resources.Load("Weapons/ShootingBow") as GameObject
    };
    /// <summary>
    /// Dictionary of base tooltips used when we are given option to pick new weapon
    /// </summary>
    /// <returns></returns>
    public static Dictionary<Weapon.WeaponTypes,string> BaseTooltips = new(){
        {Weapon.WeaponTypes.KnifeStorm,"Throws multiple knives"},
        {Weapon.WeaponTypes.ThrowingKnives,"Throws knives in mouse direction"},
        {Weapon.WeaponTypes.Sword, "Powerful slash hitting all enemies in range"},
        {Weapon.WeaponTypes.ShootingBow,"Shoots arrows"}
    };
}
