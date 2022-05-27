using UnityEngine;
using UnityEngine.Events;

public class  IntUnityEvent : UnityEvent<int>
{

}
/// <summary>
/// Simple class to be attached to a gameobject 
/// that should be destroyed after certain time
/// </summary>
public class TimedSelfDestruct : MonoBehaviour
{
	[Tooltip("After this time, the object will be destroyed")]
	public float timeToDestruction;
	[Tooltip("Unity Event invoked when the gameobject is destroyed")]
	public static IntUnityEvent OnDestroy = new IntUnityEvent();
	[Tooltip("Enable/Disable OnDestroy Event")]
	public bool isOnDestroyEventEnable = true;

	[SerializeField] private int _notClickedPoints;

	void Start()
	{
		Invoke("DestroyMe", timeToDestruction);
	}

	// This function will destroy this object
	void DestroyMe()
	{
        if (OnDestroy != null && isOnDestroyEventEnable)
			OnDestroy.Invoke(_notClickedPoints);

		Destroy(gameObject);
	}
}
