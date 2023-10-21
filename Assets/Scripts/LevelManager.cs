using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private InputManager input;
    public bool inputPause;
    [SerializeField]
    GameObject gridPrefab;
    GridBehaviour grid;
    [SerializeField]
    int nºcolumnas =7;
    [SerializeField]
    int nºfilas = 7;
    [SerializeField]
    int nºlineas = 7;
    [SerializeField]
    int timesToPressGrids = 1;
    List<GridBehaviour> levelGrids;


    [SerializeField]
    GameObject levelText;
    [SerializeField]
    GameObject pauseText;
    public bool IsInit { get; private set; }
    public bool IsLvPaused { get; private set; }
    [SerializeField]
    public Vector3 startPlayerPos;
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    GameObject playerBotPrefab;
    PlayerManager player;
    PlayerBotLv1 playerBot;
    [SerializeField]
    List<Enemy> levelEnemys;
    private GameObject enemySpawnerGO;
    private EnemySpawner enemySpawner;

    
    

    // Start is called before the first frame update
    void Start()
    {
        input = gameObject.AddComponent<InputManager>();
        LevelGuide();
    }
    private void Update()
    {
        inputPause = input.isExitButton;
        CheckLevelPause();

    }

    private void LevelGuide()
    {
        levelGrids = new List<GridBehaviour>();        
        grid = gridPrefab.GetComponent<GridBehaviour>();
        grid.SetTimesForComplete(timesToPressGrids);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (i + j + k == 2 && i != 2 && k != 2)
                    {
                        GameObject tempGO = Instantiate(gridPrefab, new Vector3(k, j, -i), Quaternion.identity);
                        GridBehaviour newGrid = tempGO.GetComponent<GridBehaviour>();
                        levelGrids.Add(newGrid);
                    }                    
                }
            }
        }
        playerBot = Instantiate(playerBotPrefab,new Vector3(0, 3, 0), Quaternion.identity).GetComponent<PlayerBotLv1>();

    }
    private void Init()
    {
        levelGrids = new List<GridBehaviour>();        
        grid = gridPrefab.GetComponent<GridBehaviour>();
        grid.SetTimesForComplete(timesToPressGrids);        
        ConstructLevel();
        enemySpawnerGO = new GameObject("Enemy Spawner");
        enemySpawner = enemySpawnerGO.AddComponent<EnemySpawner>();
        enemySpawner.SetLevelEnemys(levelEnemys);        
        player = Instantiate(playerPrefab, startPlayerPos, Quaternion.identity).GetComponent<PlayerManager>();
        
    }
    IEnumerator InitLevelCorroutine()
    {        
        playerBot.IsPaused = true;
        yield return new WaitForSeconds(2);
        Destroy(playerBot.gameObject);
        foreach (var item in levelGrids)
        {
            Destroy(item.gameObject);
        }
        levelGrids.Clear();
        Init();
        LevelPause();
        yield return new WaitForSeconds(2);
        levelText.SetActive(false);
        LevelUnPause();
        IsInit = true;
    }    
    void LevelComplete()
    {        
            //GameManager.LevelUp or similar
    }
    void CheckLevelPause()
    {
        if (IsInit)
        {
            bool tempBool = input.isExitButton;
            if (IsLvPaused == false && tempBool == true)
            {
                IsLvPaused = true;
                pauseText.gameObject.SetActive(true);
                LevelPause();
            }
            else if (IsLvPaused == true && tempBool == false)
            {
                IsLvPaused = false;
                pauseText.gameObject.SetActive(false);
                LevelUnPause();
            }
        }
        
    }
    public void LevelPause() 
    {
        enemySpawner.PauseEnemys();
        player.IsPaused = true;
    }
    public void LevelUnPause()
    {
        enemySpawner.UnPauseEnemys();
        player.IsPaused = false;
    }

    public void RestartLv()
    {
        enemySpawner.RestartEnemys();
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
        if (IsInit)
        {
            LevelComplete();
        }
        else
        {            
            StartCoroutine("InitLevelCorroutine");       
        }
    }
    private void ConstructLevel()
    {        
        GameObject level = this.gameObject;
        level.transform.position = Vector3.zero;
        for (int i = 0; i < nºcolumnas; i++)
        {
            for (int j = 0; j  < nºfilas; j++)
            {
                for (int k = 0;  k < nºlineas; k++)
                {
                    if (i + j + k == nºlineas - 1)
                    {
                        GameObject tempGO = Instantiate(gridPrefab, new Vector3(k, j, -i), Quaternion.identity, level.transform);
                        GridBehaviour newGrid = tempGO.GetComponent<GridBehaviour>();                        
                        levelGrids.Add(newGrid);
                    }
                }
            }
        }
    }    
}
