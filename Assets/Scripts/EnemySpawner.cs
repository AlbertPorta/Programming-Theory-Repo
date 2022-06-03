using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemysPrefabs;
    public List<Enemy> enemysForThisLv;
    public List<Enemy> enemysSpawned;
    public List<int> enemysPosSpawn;
    float timeSpawning;
    [SerializeField]
    float spawnfreq = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        InitSpawner();

    }

    private void InitSpawner()
    {
        enemysPosSpawn = new List<int>();
        for (int i = 0; i < enemysForThisLv.Count; i++)
        {
            enemysPosSpawn.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRandomEnemy();
    }
    public void AddEnemySpawned(Enemy enemyToAdd)
    {
        enemysSpawned.Add(enemyToAdd);
    }
    public void RemoveEnemySpawned(Enemy enemyToRemove)
    {
        enemysSpawned.Remove(enemyToRemove);
    }
    void PauseEnemys()
    {
        foreach (Enemy enemy in enemysSpawned)
        {
            enemy.IsPaused = true;
        }
    }
    void UnPauseEnemys()
    {
        foreach (Enemy enemy in enemysSpawned)
        {
            enemy.IsPaused = false;
        }
    }
    void SpawnRandomEnemy()
    {
        if (enemysPosSpawn.Count > 0)
        {
            timeSpawning +=  Time.deltaTime;
            if (timeSpawning > spawnfreq )
            {
                int randomNum = Random.Range(0, enemysPosSpawn.Count);
                Enemy tempEnemy = Instantiate(enemysForThisLv[randomNum],this.transform);
                AddEnemySpawned(tempEnemy);
                enemysPosSpawn.RemoveAt(randomNum);
                timeSpawning = 0;
            }
        }
        
        
    }
}
