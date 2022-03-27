namespace HASSAgent.Shared.Models.HomeAssistant.Commands.CustomCommands
{
    /// <summary>
    /// Command to log off the current Windows session
    /// </summary>
    public class LogOffCommand : CustomCommand
    {
        public LogOffCommand(string name = "LogOff", string id = default) : base("shutdown /l", false, name ?? "LogOff", id) => State = "OFF";
    }
}
