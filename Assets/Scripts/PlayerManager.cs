using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Character
{
    private InputManager input;
    Vector3 targetdir;
    Vector3 targetPos;
    Vector3 currentPos;
    Vector3 nextPos;
    private Vector3 currentDir;


    //campos para parabola
    readonly float g = 9.8f;
    readonly float speedYo = 4.9f;
    float addYParabola;
    public float deltaTime;
    
    
    bool isFalling;
    private bool isFloor;

    public override void Death()
    {
        print("Ups!!!!!!");
        Destroy(this);        
    }

    public override void Move()
    {
        if (isMoving == false)
        {            
            isMoving = true;
            currentPos = transform.position;
            targetdir = input.GetDirection();
            targetPos = currentPos + targetdir;
            addYParabola = 0;
            deltaTime = 0;
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));            
        }     
        
        if (transform.position != targetPos && isMoving)
        {
            
            deltaTime += Velocity* Time.deltaTime;
            addYParabola = speedYo * deltaTime - ((g * deltaTime * deltaTime) / 2);      
            currentPos += targetdir *Velocity* Time.deltaTime;
            transform.position = new Vector3(currentPos.x,currentPos.y + addYParabola, currentPos.z) ;


            if (deltaTime >= 1 )
            {   
                if (CheckIfFalling())
                {
                    print("ups");
                    return;
                }             
                transform.position = targetPos;
            }
            
        }
        else
        {
            isMoving = false;
        }        
    }

    private bool CheckIfFalling()
    {
        //Esta tocando suelo?
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1))
        {
            isFalling = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            isFalling = true;
        }
        return isFalling;
    }

    public override void SetVelocity()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        input = gameObject.AddComponent<InputManager>();
        Velocity = 2f;
    }

    // Update is called once per frame
    void Update()
    {        
        Move();   
              
    }
}
