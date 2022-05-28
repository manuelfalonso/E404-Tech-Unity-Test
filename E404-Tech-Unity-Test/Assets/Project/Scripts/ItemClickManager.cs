using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class ItemClickManager : MonoBehaviour
{
    public static event Action<SpawnItemInfo> testAction;
    
    public SpawnItemInfo _itemInfo;

    private int _clicksMade = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            if (_clicksMade >= _itemInfo._clicksToPoints)
            {
                if (_itemInfo._hasSpawnStreak)
                    testAction?.Invoke(_itemInfo);

                GameManager.Instance.IncreasePoints(_itemInfo._pointsOnClick);
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
