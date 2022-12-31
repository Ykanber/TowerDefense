using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    //Scriptable Object
    public Level level;

    // for death trigger
    public static bool death;


    static GameObject gameOverUI;

    //Managers
    [HideInInspector] public TowerManager towerManager;
    [HideInInspector] public SpawnManager spawnManager;



    void Awake()
    {
        SaveSystem.saveOnNextLevelScreen = false; 
        towerManager = GetComponent<TowerManager>();
        spawnManager = GetComponent<SpawnManager>();

        GetScriptableObjectData();
        InitializeStaticVariables();
        if (SaveSystem.isLoad)
        {
            LoadGame();
        }
        else
        {
            towerManager.emptyTowerLocations = new List<Vector2>(level.TowerLocations);
            towerManager.towers = new List<Tower>();
        }
        GetGameOverUI();
        SaveSystem.SavePlayer(this); // save at the beginning of the level
        spawnManager.StartSpawn(); // starts spawning
    }

    /// <summary>
    /// Loads the data from the save data.
    /// </summary>
    void LoadGame()
    {
        SaveSystem.isLoad = false;
        PlayerData data = SaveSystem.LoadGame();
        if (data != null)
        {
            towerManager.emptyTowerLocations = new List<Vector2>(); 
            towerManager.towers = new List<Tower>();

            GoldManager.SetGoldCount(data.gold);
            SpawnManager.maxMonsterCount += data.deadMonsterCount;
            SpawnManager.diedMonsterCount = data.deadMonsterCount;
            UI.SetScoreCount(data.deadMonsterCount);
            spawnManager.waveIndex = data.WaveIndex;
            foreach (var item in data.emptyTurretPositions)
            {
                towerManager.emptyTowerLocations.Add(new Vector2(item[0], item[1]));
            }
            for (int i = 0; i < data.turretPositions.Count; i++) // spawns turrets at regarding damage and range levels
            {
                Vector2 pos = new Vector2(data.turretPositions[i][0], data.turretPositions[i][1]);
                int damageLevel = data.damageLevels[i];
                int rangeLevel = data.rangeLevels[i];
                towerManager.towers.Add(towerManager.SpawnTurret(pos, damageLevel, rangeLevel));
            }
        }
    }

    void InitializeStaticVariables()
    {
        spawnManager.waveIndex = 0;
        SpawnManager.maxMonsterCount = 0;
        SpawnManager.diedMonsterCount = 0;
        death = false;
        Time.timeScale = 1;
    }


    /// <summary>
    /// Get Level Data
    /// </summary>
    void GetScriptableObjectData()
    {
        SpawnManager.path = level.Path;
        towerManager.towerPrefabs = level.TowerPrefabs;
        spawnManager.maxWaveCount = level.Waves.Length;
        GoldManager.SetGoldCount(level.StartGold);
        spawnManager.waves = level.Waves;
    }

    void GetGameOverUI()
    {
        gameOverUI = FindObjectOfType<GameOverUI>().gameObject;
        gameOverUI.SetActive(false);
    }

    /// <summary>
    /// EnemyPassedTheEnd
    /// </summary>
    public static void DeathTriggerIsTriggered()
    {
        Time.timeScale = 0;
        death = true;
        gameOverUI.SetActive(true);
    }

    public static int LevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}