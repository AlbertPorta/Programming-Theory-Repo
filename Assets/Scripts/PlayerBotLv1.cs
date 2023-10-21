using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBotLv1 : PlayerManager
{
    private void Update()
    {
        if (IsPaused == false)
        {
            SetTargetDir();
            Move();
        }
    }
    protected override void SetTargetDir()
    {
        if (isMoving == false && transform.position == new Vector3(0, 3, 0))
        {
            targetDir = new Vector3(0, -1, -1);
        }
        else if (isMoving == false && transform.position == new Vector3(0, 2, -1))
        {
            targetDir = new Vector3(1, -1, 0 );

        }
        else if (isMoving == false && transform.position == new Vector3(1, 1, -1))
        {
            targetDir = new Vector3(0, 1, 1);
        }
        else if (isMoving == false && transform.position == new Vector3(1, 2, 0))
        {
            targetDir = new Vector3(-1, 1, 0);
        }
    }   
}
