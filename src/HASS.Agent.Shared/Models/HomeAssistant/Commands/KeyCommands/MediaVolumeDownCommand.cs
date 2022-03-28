namespace HASS.Agent.Shared.Models.HomeAssistant.Commands.KeyCommands
{
    /// <summary>
    /// Simulates a 'volume down' mediakey press
    /// </summary>
    public class MediaVolumeDownCommand : KeyCommand
    {
        public MediaVolumeDownCommand(string name = "VolumeDown", string id = default) : base(VK_VOLUME_DOWN, name ?? "VolumeDown", id) { }
    }
}
