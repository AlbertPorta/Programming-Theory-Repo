using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected bool isReborn;
    [SerializeField]
    protected Vector3[] randomStartPositions;
    [SerializeField]
    protected bool enemyBounce;
    protected Vector3 lastPos;
    protected LayerMask characterMask;

    protected override void ResetToStartPos()
    {
        SetStartPos(StartRandomPos());
        transform.position = startPos ;
    }
    protected override void SetStartPos(Vector3 startpos)
    {
        startPos = startpos ;
    }
    protected Vector3 StartRandomPos()
    {
        int tempInt = Random.Range(0, randomStartPositions.Length);
        return randomStartPositions[tempInt];
    }
    protected override void Death()
    {
        print( this.name +  "Died");
        isReborn = true;
        deltaTime = 0;
        ResetToStartPos();
        
    }

    protected override void Move()
    {
        if (isMoving == false )
        {
            isMoving = true;
            isReborn = false;
            currentPos = transform.position;
            lastPos = currentPos;
            targetPos = currentPos + targetDir;
            addYParabola = 0;
            deltaTime = 0;
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        }

        deltaTime += Velocity * Time.deltaTime;
        if (transform.position != targetPos && isMoving && isReborn == false)
        {
            ChechEnemyBounces();

            addYParabola = speedYo * deltaTime - ((g * deltaTime * deltaTime) / 2);
            currentPos += targetDir * Velocity * Time.deltaTime;
            transform.position = new Vector3(currentPos.x, currentPos.y + addYParabola, currentPos.z);

            if (deltaTime >= 1)
            {
                CheckIfFalling();
            }
        }
        else
        {
            if (deltaTime >1.5f)
            {
                isMoving = false;
            }            
        }
    }

    private void ChechEnemyBounces()
    {
        
        //Esta chocando con algo?
        RaycastHit hit;
        enemyBounce = Physics.Raycast(transform.position, targetDir, out hit, 0.7f,characterMask);
        if (enemyBounce)
        {
            targetPos = lastPos;
            targetDir = (targetPos - currentPos) / (1 - deltaTime);
            enemyBounce = false;
        }
    }

    private void CheckIfFalling()
    {
        bool isFalling;
        //Esta tocando suelo?
        RaycastHit hit;

        isFalling = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1);
            
        if (isFalling)
        {
            if (deltaTime > 3)
            {
                Death();
            }
        }
        else
        {
            transform.position = targetPos;
        }


    }
    protected override void SetTargetDir()
    {
        if (isMoving == false)
        {
            targetDir = Vector3.zero;
        }
    }

    protected virtual void Start()
    {
        characterMask = LayerMask.GetMask("Characters");
        Velocity = 1f;
        ResetToStartPos();
        isReborn = true;
        isMoving = true;
    }
    protected virtual void Update()
    {
        if (IsPaused == false)
        {
            SetTargetDir();
            Move();
        }
        
    }
}

