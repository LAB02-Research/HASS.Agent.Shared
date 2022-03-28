namespace HASS.Agent.Shared.Enums
{
    /// <summary>
    /// Contains the user's Windows notification states
    /// </summary>
    public enum UserNotificationState
    {
        NotPresent = 1,
        Busy = 2,
        RunningDirect3dFullScreen = 3,
        PresentationMode = 4,
        AcceptsNotifications = 5,
        QuietTime = 6,
        RunningWindowsStoreApp = 7
    }
}