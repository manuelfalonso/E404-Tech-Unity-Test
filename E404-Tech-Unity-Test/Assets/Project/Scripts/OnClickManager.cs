using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickManager : MonoBehaviour
{
    [SerializeField] private int _clicksMade = 0;
    [SerializeField] private int _clicksToDestroy = 0;
    [SerializeField] private int _clickPoints = 0;

    private TimedSelfDestruct _selfDestructScript;

    // Start is called before the first frame update
    void Start()
    {
        _selfDestructScript = GetComponent<TimedSelfDestruct>();
        GameManager.Instance.OnLosing.AddListener(DeactivateScript);
        GameManager.Instance.OnWinning.AddListener(DeactivateScript);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnLosing.RemoveListener(DeactivateScript);
            GameManager.Instance.OnWinning.RemoveListener(DeactivateScript);
        }
    }

    private void OnMouseOver()
    {
        if (!enabled)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _clicksMade++;
            if (_clicksMade >= _clicksToDestroy)
            {
                GameManager.Instance.IncreasePoints(_clickPoints);
                Destroy(gameObject);
            }
        }
    }

    // Call OnLosing or OnWinning event
    private void DeactivateScript()
    {
        this.enabled = false;
    }
}