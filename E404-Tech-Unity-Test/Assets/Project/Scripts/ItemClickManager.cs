using System;
using UnityEngine;

/// <summary>
/// This script manage the gameplay of clicked items
/// </summary>
public class ItemClickManager : MonoBehaviour
{
    public static event Action<SpawnItemInfo> OnStreakSpawn;
    
    public SpawnItemInfo _itemInfo;

    private int _clicksMade = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnLosing.AddListener(DeactivateScript);
        GameManager.Instance.OnWinning.AddListener(DeactivateScript);
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

        HandleMouseClick();
    }

    /// <summary>
    /// Check clicks, count them, check streak spawn, play sound and destroy object
    /// </summary>
    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clicksMade++;
            if (_clicksMade >= _itemInfo._clicksToPoints)
            {
                if (_itemInfo._hasSpawnStreak)
                    OnStreakSpawn?.Invoke(_itemInfo);

                AudioSource.PlayClipAtPoint(_itemInfo.ClickSound, transform.position);

                GameManager.Instance.IncreasePoints(_itemInfo._pointsOnClick);
                Destroy(gameObject);
            }
        }
    }

    // Call OnLosing or OnWinning event
    private void DeactivateScript()
    {
        enabled = false;
    }
}
