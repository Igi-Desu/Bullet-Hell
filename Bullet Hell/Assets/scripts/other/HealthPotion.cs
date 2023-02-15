using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healthRestored = 3;
    void Start(){
        PotionSpawner.Instance.currentPotions++;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("col",other.transform.gameObject);
        if(other.transform.tag=="Player"){
           RestoreHealth();
            return;
        }
        if(other.transform.tag=="ExperienceGatherer"){
            //remove collision start go to player cor
            GetComponent<CircleCollider2D>().enabled=false;
            StartCoroutine(FlyToPlayer());
        }
    }
    void RestoreHealth(){
            Player.Instance.RestoreHealth(healthRestored);
            Destroy(gameObject);
    }
    void Update(){
        if (Vector3.Magnitude(transform.position - Player.Instance.transform.position) > 70){
            Destroy(gameObject);
            Debug.Log("Usunalem odlegla potke");
        }
    }
    void OnDestroy() {
        PotionSpawner.Instance.currentPotions--;
    }
    IEnumerator FlyToPlayer(){
        Transform playerPos= Player.Instance.transform;
        while(true){
            transform.position=Vector3.MoveTowards(transform.position,playerPos.position,10*Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if(Vector3.Magnitude(transform.position-playerPos.position)<0.5f){
                StopAllCoroutines();
                RestoreHealth();
            }
        }
    }
}
