namespace HASS.Agent.Shared.Enums
{
    /// <summary>
    /// Contains the state changes that can occur in a system
    /// </summary>
    public enum SystemStateEvent
    {
        HassAgentStarted,
        HassAgentSatelliteServiceStarted,
        Logoff,
        SystemShutdown,
        Resume,
        Suspend,
        ConsoleConnect,
        ConsoleDisconnect,
        RemoteConnect,
        RemoteDisconnect,
        SessionLock,
        SessionLogoff,
        SessionLogon,
        SessionRemoteControl,
        SessionUnlock
    }
}