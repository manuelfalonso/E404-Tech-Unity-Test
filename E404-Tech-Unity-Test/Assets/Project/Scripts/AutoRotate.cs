using UnityEngine;

/// <summary>
/// Simple rotation script in constant degrees
/// </summary>
public class AutoRotate : MonoBehaviour
{
    [SerializeField] private Vector3 _degreesToRotate = new Vector3(0f, 0f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_degreesToRotate);
    }
}
