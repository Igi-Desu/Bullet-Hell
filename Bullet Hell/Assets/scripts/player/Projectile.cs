using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IDamager
{
    // Start is called before the first frame update
    protected int damage;
    protected int pierce;
    protected float speed;
    protected float lifespan;
    public void Init(){
        if(lifespan>0){
            StartCoroutine(LifeSpanTimer());
        }
        else{
            Destroy(this.gameObject);
        }
    }

    protected abstract void FixedUpdate();

    // what projectile should do when it hits something, do damage, inflict some effect

    //TODO think about better possibility of collision between enemies, projectiles and obstacles DONE UWU
    public void DealDamage(IDamagable damagable){
        damagable.TakeDamage(damage);
        pierce--;
        if(pierce==0){
            Destroy(gameObject);
        }
    }
    //some animation to be played, or maybe explosion
    protected abstract void OnDestroy();

    IEnumerator LifeSpanTimer(){
        while(lifespan>0){
            yield return new WaitForSeconds(0.1f);
            lifespan-=0.1f;
        }
        Destroy(gameObject);
    }
}
