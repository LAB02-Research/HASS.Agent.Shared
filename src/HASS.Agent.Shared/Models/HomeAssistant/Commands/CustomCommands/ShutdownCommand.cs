namespace HASS.Agent.Shared.Models.HomeAssistant.Commands.CustomCommands
{
    /// <summary>
    /// Command to shutdown the machine
    /// </summary>
    public class ShutdownCommand : CustomCommand
    {
        public ShutdownCommand(string name = "Shutdown", string id = default) : base("shutdown /s", false, name ?? "Shutdown", id) => State = "OFF";
    }
}
