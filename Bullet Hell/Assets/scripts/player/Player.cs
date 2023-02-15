using System.Collections;
using UnityEngine;

public class Player : Singleton<Player>, IDamagable
{
    public float LifeSpanMultiplier => lifeSpanMultiplier;
    private float lifeSpanMultiplier = 1;
    public PlayerMovement PlayerMovement => playerMovement;
    [SerializeField] GameObject deathAnimation;
    [SerializeField] private PlayerMovement playerMovement;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject healthBar;

    private int baseHp = 10;
    public int Hp => hp;
    private int hp;

    private bool godMode = false;

    private void Start()
    {
        hp = baseHp;
        GameManager.Instance.onGameOver.AddListener(Lose);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(1);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("uwu",other.gameObject);
        //if (godMode){
            //return;
        //}
        IDamager damager = other.gameObject.GetComponent<IDamager>();
        if(damager==null){
            return;
        }
        damager.DealDamage(GetComponent<IDamagable>());
    }

    public void TakeDamage(int amount){
        if(godMode)return;
        hp -= amount;
        if (hp <= 0){
            Debug.Log("HP ponizej lub rowne 0");
            GameManager.Instance.Lose();
            return;
        }
        StartCoroutine(iFrame(1));
        Debug.Log("hp = " + hp);
        healthBar.transform.localScale = new Vector3((float)hp / baseHp, 1, 1);
    }
    public void Lose(){
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        gameObject.transform.Find("OrbGatherer").gameObject.SetActive(false);
        gameObject.transform.Find("pasekzdrowia").gameObject.SetActive(false);
        gameObject.transform.Find("LevelManager").gameObject.SetActive(false);
        Instantiate(deathAnimation, transform.position,Quaternion.identity);
    }

    IEnumerator iFrame(int sec)
    {
        var cor = StartCoroutine(Miganie());
        godMode = true;
        yield return new WaitForSeconds(sec);
        godMode = false;
        StopCoroutine(cor);
        spriteRenderer.enabled = true;

    }

    IEnumerator Miganie(){
        while (true){
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }

    public GameObject AddWeapon(GameObject Weapon){
        var weaponHolder = transform.parent.Find("Weapons").gameObject.transform;
        return Instantiate(Weapon,Vector3.zero,Quaternion.identity,weaponHolder);
    }
    public void RestoreHealth(int amount){
        hp += amount;
        if(hp > baseHp){
            hp = baseHp;
        }
        healthBar.transform.localScale = new Vector3((float)hp / baseHp, 1, 1);
    }
}
