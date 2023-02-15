using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour {

    protected BaseEnemy baseEnemy;
   protected Rigidbody2D rb;
   private float delay = 0.16f;
   //public UnityEvent OnBegin, OnDone;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        baseEnemy=GetComponent<BaseEnemy>();
    }
    public void PlayFeedback(GameObject sender , float strenght)
    {
        StopAllCoroutines();
        //OnBegin?.Invoke();
        baseEnemy.can_move = false;
        Vector2 direction = (transform.position-sender.transform.position).normalized;
        rb.AddForce(direction*strenght,ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
   private IEnumerator Reset() 
    {
        yield return new WaitForSeconds(delay);
        rb.velocity= Vector3.zero;
        baseEnemy.can_move = true;
        //OnDone?.Invoke();
    }
   
};

