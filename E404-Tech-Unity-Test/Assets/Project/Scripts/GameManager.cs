using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public UnityEvent OnWinning;
    public UnityEvent OnLosing;

    [SerializeField] private int _points = 0;
    [SerializeField] private int _pointsToWin = 100;
    [SerializeField] private int _seconds = 0;
    [SerializeField] private int _secondsToLose = 120;

    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _secondsText;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_pointsText)
            _pointsText.text = _points.ToString();
        if (_secondsText)
            _secondsText.text = _seconds.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void IncreasePoints(int pointsToIncrease)
    {
        Debug.Log("Click");
        _points += pointsToIncrease;

        // Update UI
        if (_pointsText)
            _pointsText.text = _points.ToString();

        if (_points >= _pointsToWin)
        {
            OnWinning.Invoke();
        }
    }
}
