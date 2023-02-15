using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour, IDamager
{
    SpriteRenderer spriteRenderer;
    float speedIn=10;
    float speedOut=5f;
    int dmg=0;
    public void Attack(int dmg){
        this.dmg=dmg;
        var hits = Physics2D.CircleCastAll(transform.position,1.5f*Math.Abs(transform.localScale.x),Vector2.zero);
        Debug.Log(hits.Length);
        foreach(var hit in hits){
            if(hit.transform.tag=="Player")continue;
            var damagable = hit.transform.GetComponent<IDamagable>();
            if(damagable==null)continue;
            DealDamage(damagable);
        }
        StartCoroutine(SlashFadeInOut());
    }
    public void DealDamage(IDamagable damagable){
        damagable.TakeDamage(dmg);
    }
    IEnumerator SlashFadeInOut(){
        spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.color= new Color(1,1,1,0);
        while(spriteRenderer.color.a<=1){
            spriteRenderer.color= new Color(1,1,1,spriteRenderer.color.a+speedIn*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        while(spriteRenderer.color.a>=0){
             spriteRenderer.color= new Color(1,1,1,spriteRenderer.color.a-speedOut*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
