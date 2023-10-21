using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Enemy> enemysForThisLv;
    private List<Enemy> enemysSpawned;
    private List<int> enemysPosSpawn;
    float timeSpawning;
    [SerializeField]
    float spawnfreq = 3f;
    bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        InitSpawner();

    }

    private void InitSpawner()
    {
        enemysSpawned = new List<Enemy>();
        enemysPosSpawn = new List<int>();
        for (int i = 0; i < enemysForThisLv.Count; i++)
        {
            int randNum = Random.Range(0,enemysForThisLv.Count);
            while (enemysPosSpawn.Contains(randNum))
            {
                randNum = Random.Range(0, enemysForThisLv.Count);
            }
            enemysPosSpawn.Add(randNum);
            timeSpawning = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            SpawnRandomEnemy();
        }
    }
    public void SetLevelEnemys(List<Enemy> levelEnemys)
    {
        enemysForThisLv = levelEnemys;
    }
    public void AddEnemySpawned(Enemy enemyToAdd)
    {
        enemysSpawned.Add(enemyToAdd);
    }
    public void RemoveEnemySpawned(Enemy enemyToRemove)
    {
        enemysSpawned.Remove(enemyToRemove);
    }
    public void PauseEnemys()
    {
        isPaused = true;
        if (enemysSpawned != null)
        {
            foreach (Enemy enemy in enemysSpawned)
            {
                enemy.IsPaused = true;
            }
        }
        
    }
    public void UnPauseEnemys()
    {
        isPaused = false;
        if (enemysSpawned != null)
        {
            foreach (Enemy enemy in enemysSpawned)
            {
                enemy.IsPaused = false;
            }
        }
    }
    public void RestartEnemys()
    {
        foreach (Enemy enemy in enemysSpawned)
        {
            Destroy(enemy.gameObject);
        }
        InitSpawner();
    }
    void SpawnRandomEnemy()
    {
        if (enemysPosSpawn.Count > 0)
        {
            timeSpawning +=  Time.deltaTime;
            if (timeSpawning > spawnfreq )
            {                
                Enemy tempEnemy = Instantiate(enemysForThisLv[enemysPosSpawn[0]],this.transform);
                AddEnemySpawned(tempEnemy);
                enemysPosSpawn.RemoveAt(0);
                timeSpawning = 0;
            }
        }
        
        
    }
}
