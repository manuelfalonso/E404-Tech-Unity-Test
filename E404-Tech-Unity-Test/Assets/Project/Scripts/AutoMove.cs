using UnityEngine;

/// <summary>
/// Constant movement the direction selected.
/// </summary>
public class AutoMove : MonoBehaviour
{
    [Tooltip("These are the forces that will push the object every frame " +
    "don't forget they can be negative too!")]
    public Vector3 direction = new Vector3(1f, 0f, 0f);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(direction);
    }
}
