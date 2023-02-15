using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public enum ExperienceAmount {low,medium,high};
    public int experienceGiven;
    SpriteRenderer spriteRenderer;
    public void Init(ExperienceAmount orbType, int experience=0){
        spriteRenderer=GetComponent<SpriteRenderer>();
        switch(orbType){
            case ExperienceAmount.low:
                spriteRenderer.color=Color.cyan;
                experienceGiven=1;
                break;
            case ExperienceAmount.medium:
                spriteRenderer.color=Color.green;
                experienceGiven=3;
                break;
            case ExperienceAmount.high:
                spriteRenderer.color=Color.red;
                experienceGiven=10;
                break;
        }
        if(experience>0){
            experienceGiven=experience;
        }
        // LayerMask mask = LayerMask.GetMask("Exp");
        // Debug.Log((int)(mask));
        //TODO Ktos Merging exp orb for increased performance
        // var hit = Physics2D.CircleCast(transform.position,2f,Vector2.up,LayerMask.GetMask("Exp"));
        
        // if(hit.transform!=null){

        // try{    
        //     hit.transform.GetComponent<experienceOrb>().Merge(this);
        //     // Debug.Log("Merging.....");
        //     Destroy(gameObject);
        // }
        // catch{
        //     //do nothing.... //TODO for now
        //     // Debug.Log("hit doesn't contain experience orb component");
        // }
        // }
        
    }
    public void TryMerge(){

    }
    public void Merge(ExperienceOrb other){
        experienceGiven+=other.experienceGiven;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("col",other.transform.gameObject);
        if(other.transform.tag=="Player"){
           GivePlayerExp();
            return;
        }
        if(other.transform.tag=="ExperienceGatherer"){
            //remove collision start go to player cor
            GetComponent<CircleCollider2D>().enabled=false;
            StartCoroutine(FlyToPlayer());
        }
    }
    void GivePlayerExp(){
            LevelManager.Instance.GetExp(experienceGiven);
            Destroy(gameObject);
    }
    IEnumerator FlyToPlayer(){
        Transform playerPos= Player.Instance.transform;
        while(true){
            transform.position=Vector3.MoveTowards(transform.position,playerPos.position,10*Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if(Vector3.Magnitude(transform.position-playerPos.position)<0.5f){
                StopAllCoroutines();
                GivePlayerExp();
            }
        }
    }
}
