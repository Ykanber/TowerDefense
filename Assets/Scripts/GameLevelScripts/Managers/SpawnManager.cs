using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public int waveIndex;
    [HideInInspector] public static int maxMonsterCount;
    [HideInInspector] public int maxWaveCount;

    public static int diedMonsterCount;

    [HideInInspector] public Wave[] waves;
    public static Vector2[] path;

    Vector2 startPoint;

    //Audio Manager
    AudioManager audioManager;
    GameManager gameManager;

    //Level passed UI
    static GameObject levelPassedUI;

    // For wave Handling
    [SerializeField] GameObject waveIncreaseButton;

    private void Awake()
    {
        levelPassedUI = FindObjectOfType<LevelPassedUI>().gameObject;
        levelPassedUI.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = GetComponent<GameManager>();
    }

    public void StartSpawn()
    {
        startPoint = path[0] + new Vector2(0.5f, 0.5f);
        FindMaxMonsterCount();
        UI.SetWaveDisplay(waveIndex, maxWaveCount);
        StartCoroutine(SpawnRoutine());
    }
    public void IncreaseWave()
    {
        audioManager.PlayNextWave();
        waveIncreaseButton.SetActive(false);
        FindMaxMonsterCount();
        UI.SetWaveDisplay(waveIndex, maxWaveCount);
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        float time = 0;
        float cooldown = 0;
        List<GameObject> enemies = new List<GameObject>(waves[waveIndex].Enemies);
        List<int> enemyCount = new List<int>(waves[waveIndex].EnemyCount);
        List<float> enemyCooldown = new List<float>(waves[waveIndex].EnemyCooldown);
        while (enemyCount.Count != 0)   // SpawnLoop
        {
            time += Time.deltaTime;
            // time is up
            if (time > cooldown)
            {
                int random = UnityEngine.Random.Range(0, enemies.Count);
                SpawnEnemy(enemies[random], startPoint);
                cooldown += enemyCooldown[random];
                enemyCount[random]--;
                if (enemyCount[random] == 0)    // If 1 enemy type is over then remove it from the spawn loop
                {
                    enemies.RemoveAt(random);
                    enemyCount.RemoveAt(random);
                    enemyCooldown.RemoveAt(random);
                }
            }
            yield return null;
        }

        while (diedMonsterCount != maxMonsterCount)
        {
            yield return null;
        }
        if (waveIndex != waves.Length - 1)   // if current wave is over
        {
            waveIndex += 1;
            SaveSystem.SavePlayer(gameManager);
            waveIncreaseButton.SetActive(true);
        }
        else
        {   // if current level is over
            SaveSystem.saveOnNextLevelScreen = true;
            audioManager.PlayLevelPassed();
            levelPassedUI.SetActive(true);
        }
    }


    void SpawnEnemy(GameObject enemy, Vector2 pos)
    {
        Instantiate(enemy, pos, Quaternion.identity);
    }

    // To understand if all enemies are dead at the and of the level
    void FindMaxMonsterCount()
    {
        foreach (int item in waves[waveIndex].EnemyCount)
        {
            maxMonsterCount += item;
        }
    }
    public static void IncreaseDeadMonsterCount()
    {
        diedMonsterCount++;
        UI.SetScoreCount(diedMonsterCount);
    }

    public static Vector2[] ReturnPath()
    {
        return path;
    }
}
