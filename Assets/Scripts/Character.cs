using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public float velocity;
    public Vector3 offsetPos;
    public bool isMoving;
    public Vector3 startPos;
    public Vector3 currentPos;
    public Vector3 targetDir;
    public Vector3 targetPos;

    //campos para parabola
    protected readonly float g = 9.8f;
    protected float speedYo = 4.9f;
    protected float addYParabola;
    public float deltaTime;

    public float Velocity { get => velocity; set => velocity = value; }

    protected abstract void ResetToStartPos();

    protected abstract void SetStartPos(Vector3 pos);    
    protected abstract void Move();    
    protected abstract void Death();
    protected abstract void SetTargetDir();

        
    
}
