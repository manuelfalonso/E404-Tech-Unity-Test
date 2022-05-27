using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickEvent : MonoBehaviour
{
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
        GameManager.Instance.OnLosing.RemoveListener(DeactivateScript);
        GameManager.Instance.OnWinning.RemoveListener(DeactivateScript);
    }

    private void OnMouseOver()
    {
        if (!enabled)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.IncreasePoints(1);
        }
    }

    // Call OnLosing or OnWinning event
    private void DeactivateScript()
    {
        this.enabled = false;
    }
}
