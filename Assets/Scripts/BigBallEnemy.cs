using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigBallEnemy : BallEnemy    
{
    public GameObject snakePrefab;
    private bool isMetamorfosi;

    protected override void Move()
    {
        CheckIfMetamorfosi();
        base.Move();
    }

    private void CheckIfMetamorfosi()
    {
        if (transform.position.y == 1 )
        {
            isMetamorfosi = true;           
        }
    }

    private void Metaforfosi()
    {
        GameObject newSnake = Instantiate(snakePrefab, transform.position, Quaternion.identity, transform.parent);
        EnemySpawner enemySpawner = transform.GetComponentInParent<EnemySpawner>();
        enemySpawner.AddEnemySpawned(newSnake.GetComponent<Enemy>());
        enemySpawner.RemoveEnemySpawned(this);
        Destroy(this.gameObject);
    }
    protected override void Update()
    {
        if (isMetamorfosi)
        {
            deltaTime += Time.deltaTime;
            if (deltaTime >= 4.5)
            {
                Metaforfosi();
            }
        }
        else
            base.Update();
    }
}
