using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int gold;
    public int deadMonsterCount;
    public int LevelIndex;
    public int WaveIndex;

    public List<int[]> turretPositions;
    public List<int[]> emptyTurretPositions;

    public List<int> damageLevels;
    public List<int> rangeLevels;

    public PlayerData(GameManager gameManager)
    {
        emptyTurretPositions = new List<int[]>();
        turretPositions = new List<int[]>();
        damageLevels = new List<int>();
        rangeLevels = new List<int>();


        gold = GoldManager.GetGoldCount();
        deadMonsterCount = SpawnManager.diedMonsterCount;
        LevelIndex = GameManager.LevelIndex();
        WaveIndex = gameManager.spawnManager.waveIndex;
        foreach (var item in gameManager.towerManager.emptyTowerLocations)
        {
            emptyTurretPositions.Add(new int[] { (int)item.x,(int)item.y});
        }
        foreach (var item in gameManager.towerManager.towers)
        {
            turretPositions.Add(new int[] { (int)item.transform.position.x, (int)item.transform.position.y });
            damageLevels.Add(item.Scorpion.DamageLevel);
            rangeLevels.Add(item.Scorpion.Range);
        }

    }
}
