using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public bool IsComplete { get; private set; }    
    [SerializeField]
    int timesForComplete;
    int timesPressed = 0;
    Color[] colors ;
    MeshRenderer mesh;
    
    
    private void Start()
    {
        colors = new Color[1 + timesForComplete];
        Color tempColor = Color.blue;
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = tempColor;
            tempColor.g += 0.5f; 
        }
        mesh = GetComponent<MeshRenderer>();
        //mesh.material.color = colors[0];
    }
    public void IncreaseTimesPressed()
    {        
        timesPressed++;

        if (timesForComplete <= timesPressed)
        {
            timesPressed = timesForComplete;
            IsComplete = true;
        }
        mesh.material.color = colors[timesPressed];


    }
    public void DecreaseTimesPressed()
    {
        timesPressed--;
        IsComplete = false;

        if (timesPressed <= 0)
        {
            timesPressed = 0;
        }
        
        mesh.material.color = colors[timesPressed];

    }
    public void SetTimesForComplete(int timesFC)
    {
        timesForComplete = timesFC;
    }
}
