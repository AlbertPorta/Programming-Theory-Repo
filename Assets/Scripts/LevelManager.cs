using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject gridGo;
    GridBehaviour grid;
    [SerializeField]
    int n�columnas =7;
    [SerializeField]
    int n�filas = 7;
    [SerializeField]
    int n�lineas = 7;
    [SerializeField]
    private int timesToPressGrids = 1;
    List<GridBehaviour> levelGrids;
    
    
    public bool IsInit { get; private set; }
    public Vector3 StartPlayerPos { get; private set; }
    
    

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        levelGrids = new List<GridBehaviour>();
        gridGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gridGo.tag = "Grid";
        grid = gridGo.AddComponent<GridBehaviour>();
        grid.SetTimesForComplete(timesToPressGrids);
        
        ConstructLevel();
        Destroy(gridGo);
        StartPlayerPos = new Vector3(0, 7, 0);
        IsInit = true;
    }

    private void Update()
    {
        LevelComplete();
    }
    void LevelComplete()
    {
        
            //GameManager.LevelUp or similar

    }
    public void CheckLvComplete()
    {
        foreach (GridBehaviour grid in levelGrids)
        {
            if (grid.IsComplete == false)
            {
                return;
            }
        }
        LevelComplete();
    }
    private void ConstructLevel()
    {        
        GameObject level = this.gameObject;
        level.transform.position = Vector3.zero;
        for (int i = 0; i < n�columnas; i++)
        {
            for (int j = 0; j  < n�filas; j++)
            {
                for (int k = 0;  k < n�lineas; k++)
                {
                    if (i + j + k == n�lineas - 1)
                    {

                        GameObject tempGO = Instantiate(gridGo, new Vector3(k, j, -i), Quaternion.identity, level.transform);
                        GridBehaviour newGrid = tempGO.GetComponent<GridBehaviour>();                        
                        levelGrids.Add(newGrid);
                    }
                }
            }
        }
    }    
}
