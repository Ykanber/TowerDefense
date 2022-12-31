using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level", menuName ="ScriptableObjects/Level")]
public class Level : ScriptableObject
{

    [SerializeField]
    int startGold;

    //For monsters to Move
    [SerializeField]
    Vector2[] path;

    // Waves on this level
    [SerializeField]
    Wave[] waves;

    // Tower Prefabs
    [SerializeField]
    GameObject[] towerPrefabs;

    //Tower Locations
    [SerializeField]
    List<Vector2> towerLocations;

    public int StartGold { get => startGold;}

    public Vector2[] Path { get => path;}
    public Wave[] Waves { get => waves;}

    public List<Vector2> TowerLocations { get => towerLocations;}
    public GameObject[] TowerPrefabs { get => towerPrefabs;}
    
}
