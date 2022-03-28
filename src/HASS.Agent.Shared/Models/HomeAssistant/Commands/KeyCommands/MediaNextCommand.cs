namespace HASS.Agent.Shared.Models.HomeAssistant.Commands.KeyCommands
{
    /// <summary>
    /// Simulates a 'next' mediakey press
    /// </summary>
    public class MediaNextCommand : KeyCommand
    {
        public MediaNextCommand(string name = "Next", string id = default) : base(VK_MEDIA_NEXT_TRACK, name ?? "Next", id) { }
    }
}
