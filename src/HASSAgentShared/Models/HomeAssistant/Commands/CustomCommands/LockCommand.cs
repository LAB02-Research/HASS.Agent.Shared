﻿namespace HASSAgent.Shared.Models.HomeAssistant.Commands.CustomCommands
{
    /// <summary>
    /// Command to lock the current Windows session
    /// </summary>
    public class LockCommand : CustomCommand
    {
        public LockCommand(string name = "Lock", string id = default) : base("Rundll32.exe user32.dll,LockWorkStation", false, name ?? "Lock", id) => State = "OFF";
    }
}