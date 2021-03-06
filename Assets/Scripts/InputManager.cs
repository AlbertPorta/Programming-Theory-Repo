using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private bool isExitButton;
    public Vector3 GetDirection()
    {
        if (horizontal == 0 || vertical == 0)
        {
            return Vector3.zero;
        }
        else
        {
            if (horizontal == 1 && vertical == 1)
            {
                return new Vector3(0, 1, 1);
            }
            else if (horizontal == 1 && vertical == -1)
            {
                return new Vector3(1, -1, 0);
            }
            else if (horizontal == -1 && vertical == 1)
            {
                return new Vector3(-1, 1, 0);
            }
            else if (horizontal == -1 && vertical == -1)
            {
                return new Vector3(0, -1, -1);
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    private void LateUpdate()
    {
        CheckInput();
    }
}
