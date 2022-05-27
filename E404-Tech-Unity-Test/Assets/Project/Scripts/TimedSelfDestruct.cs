using UnityEngine;

/// <summary>
/// Simple class to be attached to a gameobject 
/// that should be destroyed after certain time
/// </summary>
public class TimedSelfDestruct : MonoBehaviour
{
	[Tooltip("After this time, the object will be destroyed")]
	public float timeToDestruction;

	void Start()
	{
		Invoke("DestroyMe", timeToDestruction);
	}

	// This function will destroy this object
	void DestroyMe()
	{
		Destroy(gameObject);
	}
}
