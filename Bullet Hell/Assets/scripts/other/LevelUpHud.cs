using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
//Igor
public class LevelUpHud : Singleton<LevelUpHud>
{
    Canvas canv;
    [SerializeField] Button[] currentButtons;
    void Start()
    {
        canv = GetComponent<Canvas>();
        canv.enabled = false;
    }


    /// <summary>
    /// Defines what happens should also happen when player upgrades one of the weapons
    /// </summary>
    private void OnClickUpgradeButton()
    {
        Time.timeScale = 1;
        canv.enabled = false;
        LevelManager.Instance.CheckIfLevelUp();
    }
    /// <summary>
    /// ShowUp upgrade HUD and check current upgrades status
    /// </summary>
    public void ShowUp()
    {
            foreach(var butt in currentButtons){
                butt.gameObject.SetActive(false);
            }
            List<int> randomNumbers = new();
            int smaller= math.min(currentButtons.Length,WeaponManager.Instance.avaiableWeapons.Count);
            if(smaller==0){
                Debug.Log("Nothing to upgrade");
                return;
            }
            while(randomNumbers.Count!=smaller){
                int rand=UnityEngine.Random.Range(0,WeaponManager.Instance.avaiableWeapons.Count);
                if(!randomNumbers.Contains(rand)){
                    randomNumbers.Add(rand);
                }
            }
           for(int i=0; i<smaller; i++){
                GameObject weapon = WeaponManager.Instance.avaiableWeapons[randomNumbers[i]].gameObject;
                Debug.Log(weapon.GetComponent<Weapon>().WeaponType);
                currentButtons[i].gameObject.SetActive(CreateButton(currentButtons[i],weapon));
           }
            canv.enabled=true;
            Time.timeScale=0;
    }

    /// <summary>
    /// creates button, need some changes so less spaghetti
    /// </summary>
    /// <param name="button"></param>
    /// <returns>true if succed false if not</returns>
    private bool CreateButton(Button button, GameObject weapon)
    {
        if (weapon == null || weapon.GetComponent<Weapon>() == null)
        {
            Debug.LogError("Trying to add null weapon to button, or something that is not weapon");
            return false;
        }
        //check if weapon is already in player inventory
        var check = WeaponManager.Instance.WeaponInInventory(weapon.GetComponent<Weapon>().WeaponType);
        Weapon weapScript = null;
        button.onClick.RemoveAllListeners();
        var butScript = button.GetComponent<UpgradeButton>();
        if (check == null)//if the weapon is new
        {
            button.onClick.AddListener(delegate { WeaponManager.Instance.AddWeapon(weapon); });
            weapScript = weapon.GetComponent<Weapon>();
            butScript.toolTip.text = ResourcesLibrary.BaseTooltips[weapScript.WeaponType];
            butScript.level.text = "lv. 1";
        }
        else//if we already have it
        {
            button.onClick.AddListener(check.Upgrade);
            weapScript = check;
            butScript.toolTip.text = weapScript.GetUpgradeTip(weapScript.Level);
            butScript.level.text = "lv. " + (weapScript.Level + 1).ToString();
        }
        butScript.icon.sprite =ResourcesLibrary.WeaponIcons[weapScript.WeaponType];
        butScript.itemType.text = weapScript.WeaponType.ToString();
        button.onClick.AddListener(OnClickUpgradeButton);
        return true;
    }
}
