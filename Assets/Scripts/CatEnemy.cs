using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEnemy : Enemy
{
    int changeDirFreq = 2;
    int counter = 0;
    protected override void SetTargetDir()
    {
        if (isMoving == false && isReborn == false)
        {
            if (changeDirFreq > counter)
            {
                counter++;
                int r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        targetDir = new Vector3(0, 1, 1);
                        break;
                    case 1:
                        targetDir = new Vector3(-1, 1, 0);
                        break;
                    default:
                        targetDir = Vector3.zero;
                        break;
                }
            }
            else
            {
                counter = 0;
                int r = Random.Range(0, 2);
                switch (r)
                {
                    case 0:
                        targetDir = new Vector3(1, -1, 0);
                        break;
                    case 1:
                        targetDir = new Vector3(0, -1, -1);
                        break;
                    default:
                        targetDir = Vector3.zero;
                        break;
                }
            }
        }
        else if (isReborn)
        {
            int r = Random.Range(0, 2);
            counter = 0;
            switch (r)
            {
                case 0:
                    targetDir = new Vector3(0, 1, 1);
                    break;
                case 1:
                    targetDir = new Vector3(-1, 1, 0);
                    break;
                default:
                    targetDir = Vector3.zero;
                    break;
            }
        }
    }
}
