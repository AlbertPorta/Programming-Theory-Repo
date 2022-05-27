using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public float velocity;
    public Vector3 offsetPos;
    protected bool isMoving;
    public Vector3 currentPos;
    protected Vector3 targetDir;
    protected Vector3 targetPos;

    //campos para parabola
    protected readonly float g = 9.8f;
    protected readonly float speedYo = 4.9f;
    public float addYParabola;
    public float deltaTime;

    public float Velocity { get => velocity; set => velocity = value; }

    protected abstract void Move();    
    protected abstract void Death();
    protected abstract void SetTargetDir();
        
    
}
