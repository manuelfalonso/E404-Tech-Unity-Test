using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 3D Area Object Spawner with interval
/// WARINING!: This script doesnt work with rotation of the spawn area
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class ObjectCreatorArea3D : MonoBehaviour
{
	[Header("Object creation")]

	[Tooltip("The object to spawn " +
		"WARNING: take if from the Project panel, NOT the Scene/Hierarchy!")]
	public List<GameObject> prefabToSpawn;
	public DifficultySO difficulty;

	[Header("Other options")]

	[Tooltip("Configure the spawning pattern")]
	public bool spawnOnStart = true;

	private bool _isSpawnStreak = false;
	private int _spawnStreakQuantity = 0;

	private BoxCollider _boxCollider;
	private GameObject _spawnStreakPrefab = null;

    #region Unity Events

    void Start()
	{
		_boxCollider = GetComponent<BoxCollider>();
		// Make the collider not affect physics
		_boxCollider.isTrigger = true;

		if (spawnOnStart)
			StartSpawningObjects();
	}

    private void OnEnable()
    {
        ItemClickManager.OnStreakSpawn += ItemSpawnStreakHandler;
	}

	private void OnDisable()
    {
		ItemClickManager.OnStreakSpawn -= ItemSpawnStreakHandler;
	}

    #endregion

    #region Private Methods

    /// <summary>
    /// This will spawn an object, and then wait some time, then spawn another...
    /// </summary>
    private IEnumerator SpawnObject()
	{
		while (true)
		{
			// Random quantity to spawn
			var spawnCount = Random.Range(difficulty.minimumObjectAtSameSpawns, difficulty.maximumObjectAtSameSpawns);

			for (int i = 0; i < spawnCount; i++)
			{
				// If there is a spawn streak, spawn the spawn streak Prefab
				if (_isSpawnStreak)
				{
					_spawnStreakQuantity--;

					if (_spawnStreakQuantity >= 0)
						Spawn(_spawnStreakPrefab);
					else
						_isSpawnStreak = false;
				}
				// If not select a random object from the List of Prefabs
				else
				{
					// Select one prefab depending of difficult chances
					var nextPrefab = RandomPrefabBaseOnDifficultyChances();
					// Spawn next Prefab
					Spawn(nextPrefab);
				}
			}

			// Random time between spawns
			var timeToNextSpawn = Random.Range(difficulty.minimumTimeBetweenSpawns, difficulty.maximumTimeBetweenSpawns);
			// Wait for some time before spawning another object
			yield return new WaitForSeconds(timeToNextSpawn);
		}
	}

	/// <summary>
	/// Spawn the prefab in a random position inside the Box Collider
	/// </summary>
	/// <param name="prefabToSpawn">Prefab to Spawn</param>
    private void Spawn(GameObject prefabToSpawn)
    {
		// Create some random numbers
		float randomX = Random.Range(
			-_boxCollider.size.x, _boxCollider.size.x) * .5f;
		float randomY = Random.Range(
			-_boxCollider.size.y, _boxCollider.size.y) * .5f;
		float randomZ = Random.Range(
			-_boxCollider.size.z, _boxCollider.size.z) * .5f;

		// Generate the new object
		GameObject newObject = Instantiate(prefabToSpawn);
		newObject.transform.position = new Vector3(
			randomX + transform.position.x,
			randomY + transform.position.y,
			randomZ + transform.position.z);
    }

	/// <summary>
	/// Choose the Prefab from a list based on their probabilities chances
	/// </summary>
	/// <returns></returns>
	private GameObject RandomPrefabBaseOnDifficultyChances()
	{
		GameObject prefabToReturn = null;

		var totalChances = 0;
        foreach (var item in difficulty.spawnChances)
        {
			totalChances += item.chances;
		}

		var randomChances = Random.Range(0, totalChances);
		var previousChances = 0;

		for (int i = 0; i < difficulty.spawnChances.Length; i++)
        {
            if (randomChances >= previousChances && randomChances <= (previousChances + difficulty.spawnChances[i].chances))
            {
				prefabToReturn = difficulty.spawnChances[i].prefab;
				break;
            }
            else
				previousChances += difficulty.spawnChances[i].chances;
        }
		return prefabToReturn;
	}

	private void ItemSpawnStreakHandler(SpawnItemInfo itemInfo)
    {
		// If it has spawn streak update local variables to make the Spawn Streak
		if (itemInfo._hasSpawnStreak)
		{
			_isSpawnStreak = true;
			_spawnStreakQuantity = itemInfo._streakQuantity;
			_spawnStreakPrefab = itemInfo._prefabStreak;
		}
	}

    #endregion

    #region Public Methods

    /// <summary>
    /// Start spawning objects with the time interval set
    /// </summary>
    public void StartSpawningObjects()
    {
		StartCoroutine(SpawnObject());
	}

	/// <summary>
	/// Stop spawning objects
	/// </summary>
	public void StopSpawningObjects()
    {
		StopAllCoroutines();
    }

    #endregion
}
