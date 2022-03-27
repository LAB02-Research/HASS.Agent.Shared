namespace HASSAgent.Shared.Models.HomeAssistant.Commands.CustomCommands
{
    /// <summary>
    /// Command to restart the machine
    /// </summary>
    public class RestartCommand : CustomCommand
    {
        public RestartCommand(string name = "Restart", string id = default) : base("shutdown /r", false, name ?? "Restart", id) => State = "OFF";
    }
}
