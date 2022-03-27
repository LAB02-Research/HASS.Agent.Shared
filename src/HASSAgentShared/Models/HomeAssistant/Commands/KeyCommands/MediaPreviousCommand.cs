namespace HASSAgent.Shared.Models.HomeAssistant.Commands.KeyCommands
{
    /// <summary>
    /// Simulates a 'previous' mediakey press
    /// </summary>
    public class MediaPreviousCommand : KeyCommand
    {
        public MediaPreviousCommand(string name = "Previous", string id = default) : base(VK_MEDIA_PREV_TRACK, name ?? "Previous", id) { }
    }
}
