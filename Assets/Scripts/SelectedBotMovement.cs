using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedBotMovement : MonoBehaviour
{
    private EventSystem eventSystem;
    public Vector3 targetPos, targetDir, currentPos, offsetPos;
    private float addYParabola;
    public float deltaTime;
    public bool isMoving;
    private float speedYo = 4.9f;
    private float g = 9.8f;

    public float Velocity { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        Velocity = 1.5f;
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetDir();
        Move();
    }
    void SetTargetDir()
    {
        if (isMoving == false && eventSystem.currentSelectedGameObject != null)
        {
            targetPos.y = eventSystem.currentSelectedGameObject.transform.position.y + offsetPos.y;

            if (targetPos.y > currentPos.y + 0.1f)
            {
                targetDir = new Vector3(-1f, 1f, 1f);
            }
            else if (targetPos.y < currentPos.y-0.1f)
            {
                targetDir = new Vector3(1f, -1f, -1f);
            }
            else
            {
                targetDir = Vector3.zero;
            }                 
        }
    }
    void Move()
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
                transform.position = targetPos;
                isMoving = false;
            }
        }
        else
        {
            isMoving = false;
        }
    }
}
