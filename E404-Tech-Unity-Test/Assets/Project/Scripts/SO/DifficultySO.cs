using System;
using UnityEngine;

/// <summary>
/// Scriptable Object to define the Difficulty behaviour
/// </summary>
[CreateAssetMenu(fileName = "New Difficulty", menuName = "Difficulty", order = 51)]

public class DifficultySO : ScriptableObject
{
    [Serializable]
    public struct SpawnChances
    {
        public GameObject prefab;
        public int chances;
    }

    public new string name;
    [Tooltip("Chance for type of object to spawn")]
    public SpawnChances[] spawnChances;
    [Tooltip("Minimum time between spawns")]
    public float minimumTimeBetweenSpawns = 0f;
    [Tooltip("Maximum time between spawns")]
    public float maximumTimeBetweenSpawns = 0f;
    [Tooltip("Minimum objects at the same spawn")]
    public int minimumObjectAtSameSpawns = 0;
    [Tooltip("Maximum objects at the same spawn")]
    public int maximumObjectAtSameSpawns = 0;
}
