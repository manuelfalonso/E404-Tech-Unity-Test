using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private int _points = 0;
    public int Points
    {
        get { return _points; }
        private set
        {
            _points = value;
            if (_points < 0)
                _points = 0;

            // Update UI
            if (_pointsText)
                _pointsText.text = _points.ToString();
        } 
    }

    public UnityEvent OnWinning;
    public UnityEvent OnLosing;

    [SerializeField] private TimerInSeconds _timerScript;

    [SerializeField] private int _pointsToWin = 100;
    [SerializeField] private int _seconds = 0;
    [SerializeField] private int _secondsToLose = 120;

    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _secondsText;
    [SerializeField] private ObjectCreatorArea3D _spawner;
    [SerializeField] private GameObject _pauseMenu;

    private bool _isPlaying;

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
            _pointsText.text = Points.ToString();
        if (_secondsText)
            _secondsText.text = _seconds.ToString();

        _timerScript.SetSeconds(_secondsToLose.ToString());
        TimedSelfDestruct.OnDestroy.AddListener(IncreasePoints);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPauseMenu();
    }

    private void CheckPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPlaying)
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            if (_pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    private void OnDestroy()
    {
        TimedSelfDestruct.OnDestroy.RemoveListener(IncreasePoints);

        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void IncreasePoints(int pointsToIncrease)
    {
        //Debug.Log("Clicked");
        Points += pointsToIncrease;

        // Update UI
        if (_pointsText)
            _pointsText.text = _points.ToString();

        if (Points >= _pointsToWin)
        {
            OnWinning.Invoke();
        }
    }

    public void TimerReached()
    {
        OnLosing.Invoke();
    }

    public void SetDifficulty(DifficultySO difficulty)
    {
        _spawner.difficulty = difficulty;
    }

    public void SetIsPlaying(bool isPlaying)
    {
        _isPlaying = isPlaying;
    }
}
