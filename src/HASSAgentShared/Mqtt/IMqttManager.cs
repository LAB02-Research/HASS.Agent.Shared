using System.Threading.Tasks;
using HASSAgent.Shared.Enums;
using HASSAgent.Shared.Models.HomeAssistant;
using HASSAgent.Shared.Models.HomeAssistant.Commands;
using MQTTnet;

namespace HASSAgent.Shared.Mqtt
{
    /// <summary>
    /// HASS.Agent MQTT Managers interface
    /// </summary>
    public interface IMqttManager
    {
        bool IsConnected();
        void Initialize();
        void CreateDeviceConfigModel();
        Task PublishAsync(MqttApplicationMessage message);
        Task AnnounceAutoDiscoveryConfigAsync(AbstractDiscoverable discoverable, string domain, bool clearConfig = false);
        MqttStatus GetStatus();
        Task AnnounceAvailabilityAsync(bool offline = false);
        Task ClearDeviceConfigAsync();
        void Disconnect();
        Task SubscribeAsync(AbstractCommand command);
        Task UnubscribeAsync(AbstractCommand command);
        string MqttDiscoveryPrefix();
        DeviceConfigModel GetDeviceConfigModel();
        void ReloadConfiguration();
    }
}
