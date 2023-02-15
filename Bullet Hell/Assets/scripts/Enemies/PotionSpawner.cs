using UnityEngine;

public class PotionSpawner : Singleton<PotionSpawner>
{
    public int maxPotions = 5;
    public int currentPotions = 0;
    //reference to player position
    Transform playerposition;
    [SerializeField] GameObject [] potionsToSpawn;
    GameObject PotionToSpawn => potionsToSpawn[Random.Range(0,potionsToSpawn.Length)];
    [Header("Spawn properties")]
        
    [Tooltip("number of potions to spawn per second")]
    [SerializeField] float spawnSpeed = 1;

    [Tooltip("How far from player shall they spawn")]
    [SerializeField] float spawnDistance;

    [Tooltip("Which object should be a parent of the spawned potions?")]
    [SerializeField] Transform parent;
    float timerBase;
    protected float timer;
    protected void Start()
    {
        playerposition = Player.Instance.transform;
        timerBase = 1 / spawnSpeed;
        timer = timerBase;
    }
    protected void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = timerBase;
            if(currentPotions >= maxPotions){ //guard clouse
                return;
            }
            Vector3 v = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //Instantiate(PotionToSpawn, parent ,playerposition.position + v.normalized * 
            //spawnDistance, PotionToSpawn.transform.rotation);
            Instantiate(PotionToSpawn,playerposition.position+v.normalized*spawnDistance,PotionToSpawn.transform.rotation,parent);
        }
    }
}
