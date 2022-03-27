namespace HASSAgent.Shared.Models.HomeAssistant.Commands.KeyCommands
{
    /// <summary>
    /// Simulates a 'mute' mediakey press
    /// </summary>
    public class MediaMuteCommand : KeyCommand
    {
        public MediaMuteCommand(string name = "Mute", string id = default) : base(VK_VOLUME_MUTE, name ?? "Mute", id) { }
    }
}
