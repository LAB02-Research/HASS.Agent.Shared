namespace HASSAgent.Shared.Models.HomeAssistant.Commands.KeyCommands
{
    /// <summary>
    /// Simulates a 'volume up' mediakey press
    /// </summary>
    public class MediaVolumeUpCommand : KeyCommand
    {
        public MediaVolumeUpCommand(string name = "VolumeUp", string id = default) : base(VK_VOLUME_UP, name ?? "VolumeUp", id) { }
    }
}
