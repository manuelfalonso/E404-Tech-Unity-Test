using UnityEngine;

/// <summary>
/// Limit frame rate per second to the current monitor refresh rate
/// </summary>
public static class FrameRateLimiter
{
    [RuntimeInitializeOnLoadMethod]
    public static void LimitFrameRate()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}
