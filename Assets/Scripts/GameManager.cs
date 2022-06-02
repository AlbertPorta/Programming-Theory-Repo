using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject lvManagerGO;
    LevelManager lvManager;
    // Start is called before the first frame update
    void Start()
    {
        StartLevel();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartLevel()
    {
        lvManagerGO = new GameObject("Level Manager");
        lvManager = lvManagerGO.AddComponent<LevelManager>();
        PlayerManager player = Instantiate(playerPrefab).GetComponent<PlayerManager>();
        
    }
}
