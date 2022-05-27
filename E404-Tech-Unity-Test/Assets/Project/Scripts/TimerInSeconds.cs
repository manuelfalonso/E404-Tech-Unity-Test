using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

/// <summary>
/// Timer Class using seconds
/// </summary>
public class TimerInSeconds : MonoBehaviour
{
    private float _seconds = 0;
    private bool _isRunning;

    public float Seconds
    {
        get { return _seconds; }
        private set
        {
            if (value < 0)
                value = 0;

            if (_secondsInput)
                _secondsInput.text = value.ToString();

            _seconds = value;
        }
    }

    // Event invoked when the timer reaches 0
    public UnityEvent OnTimerReached;

    [Header("Timer Settings")]

    [SerializeField]
    private int _secondsSet = 0;

    [Header("Timer Inputs")]

    [SerializeField]
    private TMP_InputField _secondsInput;
    [SerializeField]
    private TMP_InputField _minutesInput;

    [Header("Debug")]

    [SerializeField]
    private bool _enableLogging = false;

    #region Unity Events

    // Start is called before the first frame update
    void Start()
    {
        InitializeTime();
        InitializeInputFields();

        StartTimer();
    }

    #endregion

    #region Private Methods

    private void InitializeTime()
    {
        Seconds = _secondsSet;
    }

    private void InitializeInputFields()
    {
        // Initialize format, validation and On Value Changed Events
        if (_secondsInput)
        {
            _secondsInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _secondsInput.characterLimit = 2;
        }
        if (_minutesInput)
        {
            _minutesInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _minutesInput.characterLimit = 2;
        }
    }

    private IEnumerator RunTimer()
    {
        if (_enableLogging)
            Debug.Log(Seconds);

        while (!IsTimerReached())
        {
            TimeSpan time = TimeSpan.FromSeconds(_seconds);

            for (
                float remaingTime = _seconds;
                remaingTime > 0;
                remaingTime -= Time.deltaTime)
            {
                time = TimeSpan.FromSeconds(remaingTime);
                Seconds = time.Seconds;
                _minutesInput.text = time.Minutes.ToString();
                yield return null;
            }

            if (_enableLogging)
                Debug.Log(Seconds);

            // Check if final timer was reached
            if (IsTimerReached())
            {
                if (OnTimerReached.GetPersistentEventCount() != 0)
                    OnTimerReached.Invoke();

                StopTimer();
            }
        }
    }

    private bool IsTimerReached()
    {
        var secondsReached = Seconds == 0 ? true : false;
        return secondsReached;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Seconds setter. Cannot be called if the timer is running.
    /// </summary>
    /// <param name="value">Seconds</param>
    public void SetSeconds(string value)
    {
        if (_isRunning)
            return;

        Seconds = int.Parse(value);
        _secondsSet = int.Parse(value);
    }

    /// <summary>
    /// Start timer countdown and update UI
    /// </summary>
    public void StartTimer()
    {
        _isRunning = true;

        if (_enableLogging)
            Debug.Log("Timer Started");

        // Start timer
        StartCoroutine(RunTimer());
    }

    /// <summary>
    /// Stop timer countdown and update UI
    /// </summary>
    public void StopTimer()
    {
        _isRunning = false;

        if (_enableLogging)
            Debug.Log("Timer Stopped");

        // Stop timer
        StopAllCoroutines();
    }

    /// <summary>
    /// Restart timer countdown to initial set time and update UI
    /// </summary>
    public void RestartTimer()
    {
        _isRunning = false;

        if (_enableLogging)
            Debug.Log("Timer Restarted");

        // Stop timer
        StopAllCoroutines();

        // Set timer
        Seconds = _secondsSet;
    }

    #endregion
}
