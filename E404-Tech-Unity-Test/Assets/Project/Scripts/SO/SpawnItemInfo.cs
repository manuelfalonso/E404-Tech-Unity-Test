using UnityEngine;

/// <summary>
/// Scriptable object to define info of the spawning object
/// </summary>
[CreateAssetMenu(fileName = "New Item Info", menuName = "Item Info", order = 51)]
public class SpawnItemInfo : ScriptableObject
{
    public string _name;
    public int _clicksToPoints = 1;
    public int _pointsOnClick = 0;
    public int _pointsNoClick = 0;
    public bool _hasSpawnStreak = false;
    public int _streakQuantity = 0;
    public GameObject _prefabStreak;
}
