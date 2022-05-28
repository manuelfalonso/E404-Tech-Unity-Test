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

	[Header("Other options")]

	[Tooltip("Configure the spawning pattern")]
	public float spawnInterval = 1f;
	public bool spawnOnStart = true;

	private BoxCollider _boxCollider;

	private bool _isSpawnStreak = false;
	private int _spawnStreakQuantity = 0;
	private GameObject _spawnStreakPrefab = null;

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
        ItemClickManager.testAction += ItemSpawnStreakHandler;
	}

	private void OnDisable()
    {
		ItemClickManager.testAction -= ItemSpawnStreakHandler;
	}

	/// <summary>
	/// This will spawn an object, and then wait some time, then spawn another...
	/// </summary>
	private IEnumerator SpawnObject()
	{
		while (true)
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
				// Select one prefab from the list
				var nextPrefab = prefabToSpawn[Random.Range(0, prefabToSpawn.Count - 1)];
				// Spawn next Prefab
				Spawn(nextPrefab);
            }

			// Wait for some time before spawning another object
			yield return new WaitForSeconds(spawnInterval);
		}
	}

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
}
