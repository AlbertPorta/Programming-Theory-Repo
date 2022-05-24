using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string name;
    private float velocity;
    public Vector3 offsetPos;
    public bool isMoving;

    public float Velocity { get => velocity; set => velocity = value; }

    public abstract void Move();
    public abstract void SetVelocity();
    public abstract void Death();
        
    
}
