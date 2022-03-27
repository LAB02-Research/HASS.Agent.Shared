namespace HASSAgent.Shared.Models.HomeAssistant.Commands.KeyCommands
{
    /// <summary>
    /// Simulates a 'playpause' mediakey press
    /// </summary>
    public class MediaPlayPauseCommand : KeyCommand
    {
        public MediaPlayPauseCommand(string name = "PlayPause", string id = default) : base(VK_MEDIA_PLAY_PAUSE, name ?? "PlayPause", id) { }
    }
}
