using UnityEngine;
using Unity.Mathematics;
[RequireComponent(typeof(Knockback))]
public class BaseEnemy : MonoBehaviour, IDamagable, IDamager
{
    // Start is called before the first frame update
    [Header("Enemy properties")]
    [SerializeField] float speed;
    [SerializeField] int hp = 2;
    [SerializeField] int dmg = 2;
    public int Dmg => dmg;
    protected Transform Target;//here put player transform
    protected Rigidbody2D rb;
    protected bool facingright = false;
    public bool can_move = true;
    protected float rotation_speed = 20;


    public static int enemyNumber=0;
    protected void Start()
    {
        if(enemyNumber>=GameManager.ENEMYLIMIT){
            enemyNumber++;
            Destroy(gameObject);
        }
        Target = Player.Instance.transform;
        rb = GetComponent<Rigidbody2D>();
        hp+= (GameManager.Instance.CurrentMinutes+1)*hp/2;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {   
        CheckDistance();
        if (can_move == false)
            return;
        transform.rotation=Quaternion.Euler(0,0,transform.rotation.eulerAngles.z+rotation_speed*Time.fixedDeltaTime);
        if (math.abs(transform.rotation.eulerAngles.z) >= 12)
            rotation_speed*=-1;
        // transform.position= Vector3.MoveTowards(transform.position,Target.position,speed*Time.deltaTime);
        rb.MovePosition(Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime));
        if (!facingright && transform.position.x < Target.transform.position.x)
        {
            Flip();
        }
        else if (facingright && transform.position.x > Target.transform.position.x)
        {
            Flip();
        }
    }
    protected void Flip()
    {
        transform.localScale = new(-transform.localScale.x, transform.localScale.y, 1);
        facingright = !facingright;
    }
    private void CheckDistance(){
        if((transform.position-Target.position).magnitude>15){
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int dmg)
    {   
        GetComponent<Knockback>().PlayFeedback(Player.Instance.gameObject, dmg);
        hp -= dmg;
        if (hp <= 0)
        {
            DropLoot();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.transform.tag=="Enemy")return;
       var damager = other.GetComponent<IDamager>();
       if(damager==null){
            return;
       }
       damager.DealDamage(GetComponent<IDamagable>());
    }
    virtual protected void DropLoot(){
        var orb = Instantiate(ResourcesLibrary.lowExpOrb,transform.position,Quaternion.identity).GetComponent<ExperienceOrb>();
        orb.Init(ExperienceOrb.ExperienceAmount.low);
    }
    public void DealDamage(IDamagable damagable){
        damagable.TakeDamage(dmg);
    }
    void OnDestroy()
    {
        enemyNumber--;
    }
}
