using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    // Enemy Prefabs
    [SerializeField]
    List<GameObject> enemies;
    
    // Enemy Count For Each Prefab
    [SerializeField]
    List<int> enemyCount;

    // Cooldown afterr enemy spawned
    [SerializeField] List<float> enemyCooldown;

    public List<GameObject> Enemies { get => enemies;}
    public List<int> EnemyCount { get => enemyCount;}
    public List<float> EnemyCooldown { get => enemyCooldown;}
}
