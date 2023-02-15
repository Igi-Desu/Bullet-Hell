using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private int currentLevel;
    /// <summary>
    /// Get current Player level
    /// </summary>
    public int CurrentLevel=> currentLevel;
    private int currentExp;
    /// <summary>
    /// Get current exp player has on level
    /// </summary>
    public int CurrentExp=>currentExp;

    private int expToNextLevel=10;
    /// <summary>
    /// get required amount of exp to next level
    /// </summary>
    public int ExpToNextLevel => expToNextLevel;
    [SerializeField]private Bar experienceBar;
    /// <summary>
    /// Gives player given amount of exp
    /// </summary>
    /// <param name="amount">amount of exp</param>
    public void GetExp(int amount){
        currentExp+=amount;
        experienceBar.ResizeBar((float)currentExp/expToNextLevel);
        if(currentExp>=expToNextLevel){
            LevelUp();
        }
    }
    /// <summary>
    /// Called when player levels up
    /// </summary>
    private void LevelUp(){
        currentLevel++;
        currentExp = (currentExp==expToNextLevel)? 0 : (currentExp-expToNextLevel);
        expToNextLevel+=10;
        experienceBar.ResizeBar((float)currentExp/expToNextLevel);
        //TODO Ktos Jakis system ktory by zwiekszal madrze expa potrzebnego do kolejnego levela
        //TODO żeby nie było jego za dużo ani za mało 
        //show up HUD that gives player option to upgrade weapon of choice
        LevelUpHud.Instance.ShowUp();
    }
    /// <summary>
    /// checks whether player should gain new level
    /// this function helps chaining level ups
    /// </summary>
    public void CheckIfLevelUp(){
        if(currentExp<expToNextLevel){
            return;
        }
        else{
            //lower current exp by the amount needed to level up
            currentExp-=expToNextLevel;
            LevelUp();
        }

    }

}
