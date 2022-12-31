using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Generate Tower Variables
    [HideInInspector] public List<Vector2> emptyTowerLocations;
    [HideInInspector] public List<Tower> towers;


    [HideInInspector] public GameObject[] towerPrefabs;


    AudioManager audioManager;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void BuyTurret()
    {
        if (emptyTowerLocations.Count != 0 && !GameManager.death && GoldManager.GetGoldCount() >= 50)
        {
            audioManager.PlayGenerateTower();
            GoldManager.DecreaseGold(50);
            int random = UnityEngine.Random.Range(0, emptyTowerLocations.Count);
            towers.Add(Instantiate(towerPrefabs[0], emptyTowerLocations[random], Quaternion.identity).GetComponent<Tower>());
            emptyTowerLocations.RemoveAt(random);
        }
    }

    /// <summary>
    /// For Loading The Game
    /// </summary>
    public Tower SpawnTurret(Vector2 pos, int damageLevel, int rangeLevel)
    {
        GameObject instantiatedObject = Instantiate(towerPrefabs[0], pos, Quaternion.identity);
        Tower tower = instantiatedObject.GetComponent<Tower>();
        Scorpion scorpion = tower.Scorpion;
        for (int i = 0; i < damageLevel; i++)
        {
            scorpion.IncreaseDamage();
        }
        for (int i = 0; i < rangeLevel - 2; i++)
        {
            scorpion.IncreaseRange();
        }
        return tower;
    }
}
