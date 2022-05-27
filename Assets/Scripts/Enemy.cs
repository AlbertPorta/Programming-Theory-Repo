using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{    
    protected override void Death()
    {
        print( this.name +  "Died");
        Destroy(this.gameObject);
    }

    protected override void Move()
    {
        if (isMoving == false)
        {
            isMoving = true;
            currentPos = transform.position;
            targetPos = currentPos + targetDir;
            addYParabola = 0;
            deltaTime = 0;
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        }

        if (transform.position != targetPos && isMoving)
        {
            deltaTime += Velocity * Time.deltaTime;
            addYParabola = speedYo * deltaTime - ((g * deltaTime * deltaTime) / 2);
            currentPos += targetDir * Velocity * Time.deltaTime;
            transform.position = new Vector3(currentPos.x, currentPos.y + addYParabola, currentPos.z);

            if (deltaTime >= 1)
            {
                if (CheckIfFalling())
                {
                    if (deltaTime > 2)
                    {
                        Death();
                    }                    
                }
                else
                {
                    transform.position = targetPos;
                }
            }
        }
        else
        {
            isMoving = false;
        }
    }

    private bool CheckIfFalling()
    {
        bool isFalling;
        //Esta tocando suelo?
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1))
        {
            isFalling = false;            
        }
        else
        {
            isFalling = true;
        }
        return isFalling;
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
        Velocity = 1f;
    }
    protected virtual void Update()
    {
        SetTargetDir();
        Move();
    }
}

