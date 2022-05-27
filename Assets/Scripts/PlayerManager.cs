using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Character
{
    private InputManager input;
    public int Lives { get; set; }
    public int Score { get; set; }
    protected override void Death()
    {
        print(this.name + "Died");
        Destroy(this.gameObject);        
    }

    protected override void Move()
    {
        if (isMoving == false )
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
            transform.position = new Vector3(currentPos.x, currentPos.y + addYParabola, currentPos.z) ;

            if (deltaTime >= 1 )
            {   
                if (CheckIfFalling())
                {
                    if (deltaTime > 2)
                    {
                        Death();//Start DeatCorroutine
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
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1))
        {
            isFalling = false;
            if (hit.collider.CompareTag("Grid"))
            {
                hit.collider.gameObject.GetComponent<GridBehaviour>().IncreaseTimesPressed();
            }
        }
        else
        {
            isFalling = true;            
        }
        return isFalling;
    }    

    // Start is called before the first frame update
    void Start()
    {
        characterName = "player";
        input = gameObject.AddComponent<InputManager>();
        Velocity = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetDir();
        Move();   
              
    }

    protected override void SetTargetDir()
    {
        if (isMoving == false)
        {
            targetDir = input.GetDirection();
        }
    }   
}
