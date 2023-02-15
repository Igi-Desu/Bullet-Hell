using UnityEngine;

public class DebugEnemySpawner : EnemySpawner
{
    // Start is called before the first frame update
    [SerializeField] int enemylimit;
    int spawnedEnemies = 0;

    new void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    new void Update()
    {
        if (spawnedEnemies >= enemylimit) return;
        base.Update();
        spawnedEnemies++;

    }
}
