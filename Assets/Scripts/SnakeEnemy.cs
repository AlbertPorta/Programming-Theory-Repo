using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : Enemy
{
    PlayerManager player;
    [SerializeField]
    private GameObject bigBallPrefab;

    protected override void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        Velocity = 1f;
        isMoving = false;
    }
    protected override void Death()
    {
        print(this.name + "Died");
        GameObject newBigBall = Instantiate(bigBallPrefab, transform.parent);
        EnemySpawner enemySpawner = transform.GetComponentInParent<EnemySpawner>();
        enemySpawner.AddEnemySpawned(newBigBall.GetComponent<Enemy>());
        enemySpawner.RemoveEnemySpawned(this);
        Destroy(this.gameObject);

    }
    protected override void SetTargetDir()    
    {
        if (isMoving == false )
        {
            
            Vector3 tempdir = new Vector3();
            if (player.targetPos.y >= transform.position.y)
            {
                tempdir.y = 1f;
                if (player.targetPos.x >= transform.position.x)
                {                    
                    tempdir.z = 1f;
                }
                else
                {
                    tempdir.x = -1f;
                }
            }
            else
            {
                tempdir.y = -1f;
                if (player.targetPos.x > transform.position.x)
                {
                    tempdir.x = 1f;
                }
                else if (player.targetPos.x <= transform.position.x)
                {
                    tempdir.z = -1f;
                }
                
            }            
            targetDir = tempdir;
        }        
    }    
}
