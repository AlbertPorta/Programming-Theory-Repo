using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemy : Enemy
{
    // Start is called before the first frame update
    protected override void SetTargetDir()    
    {
        if (isMoving == false && isReborn == false)
        {
            int r = Random.Range(0, 2);
            switch (r)
            { 
                case 0:
                    targetDir = new Vector3(0, -1, -1);
                    break;
                case 1:
                    targetDir = new Vector3(1, -1, 0);
                    break;
                default:
                    targetDir = Vector3.zero;
                    break;
            }
        }
        else if (isReborn == true)
        {
            if (startPos.x < 0)
            {
                targetDir = new Vector3(1, -1, 0);
            }
            else
            {
                targetDir = new Vector3(0, -1, -1);
            }
                        
        }
    }    
}
