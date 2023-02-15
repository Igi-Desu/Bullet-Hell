using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //reference to player position
    Transform playerposition;
    [SerializeField] GameObject [] enemiesToSpawn;
    [SerializeField] GameObject bossEnemy;
    GameObject EnemyToSpawn => enemiesToSpawn[Random.Range(0,enemiesToSpawn.Length)];
    [Header("Spawn properties")]
        
    [Tooltip("number of enemies to spawn per second")]
    [SerializeField] float spawnSpeed = 2;
    public float SpawnSpeed => spawnSpeed;

    [Tooltip("How far from player shall they spawn")]
    [SerializeField] float spawnDistance;

    [Tooltip("Which object should be a parent of the spawned enemies?")]
    [SerializeField] Transform parent;
    float timerBase;
    protected float timer;
    protected void Start()
    {
        GameManager.Instance.AddMinuteEvent(UpdateSpawnSpeed);
        GameManager.Instance.AddMinuteEvent(SpawnBoss);
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
            Vector3 v = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //Instantiate(enemyToSpawn, parent ,playerposition.position + v.normalized * 
            //spawnDistance, enemyToSpawn.transform.rotation);
            Instantiate(EnemyToSpawn,playerposition.position+v.normalized*spawnDistance,EnemyToSpawn.transform.rotation,parent);
        }
    }
    protected void UpdateSpawnSpeed(){
        spawnSpeed = 2+GameManager.Instance.CurrentMinutes;
        timerBase=1/spawnSpeed;
    }
    protected void SpawnBoss(){
        Vector3 v = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Instantiate(bossEnemy,playerposition.position+v.normalized*spawnDistance,bossEnemy.transform.rotation,parent);
    }
}
