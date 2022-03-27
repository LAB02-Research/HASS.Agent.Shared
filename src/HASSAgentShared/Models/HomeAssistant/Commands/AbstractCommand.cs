using System;
using System.Threading.Tasks;
using MQTTnet;
using Serilog;

namespace HASSAgent.Shared.Models.HomeAssistant.Commands
{
    /// <summary>
    /// Abstract command from which all commands are derived
    /// </summary>
    public abstract class AbstractCommand : AbstractDiscoverable
    {
        public int UpdateIntervalSeconds => 1;
        public DateTime? LastUpdated { get; set; }
        public string PreviousPublishedState { get; set; } = string.Empty;
        public override string Domain => "switch";

        protected AbstractCommand(string name, string id = default)
        {
            Id = id == null || id == Guid.Empty.ToString() ? Guid.NewGuid().ToString() : id;
            Name = name;
        }

        protected CommandDiscoveryConfigModel AutoDiscoveryConfigModel;
        protected CommandDiscoveryConfigModel SetAutoDiscoveryConfigModel(CommandDiscoveryConfigModel config)
        {
            AutoDiscoveryConfigModel = config;
            return config;
        }

        public override void ClearAutoDiscoveryConfig() => AutoDiscoveryConfigModel = null;

        public abstract string GetState();
        
        public async Task PublishStateAsync(bool respectChecks = true)
        {
            try
            {
                if (Variables.MqttManager == null) return;

                var state = GetState();

                if (respectChecks)
                {
                    if (LastUpdated.HasValue && LastUpdated.Value.AddSeconds(UpdateIntervalSeconds) > DateTime.Now) return;
                    if (PreviousPublishedState == state) return;
                }

                var autoDiscoConfig = GetAutoDiscoveryConfig();
                if (autoDiscoConfig == null) return;

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(autoDiscoConfig.State_topic)
                    .WithPayload(state)
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();

                await Variables.MqttManager.PublishAsync(message);

                PreviousPublishedState = state;
                LastUpdated = DateTime.Now;
            }
            catch (Exception ex)
            {
                Log.Fatal("[COMMAND] [{name}] Error publishing state: {err}", Name, ex.Message);
            }
        }

        public async Task PublishAutoDiscoveryConfigAsync()
        {
            if (Variables.MqttManager == null) return;
            await Variables.MqttManager.AnnounceAutoDiscoveryConfigAsync(this, Domain);
        }

        public async Task UnPublishAutoDiscoveryConfigAsync()
        {
            if (Variables.MqttManager == null) return;
            await Variables.MqttManager.AnnounceAutoDiscoveryConfigAsync(this, Domain, true);
        }

        public abstract void TurnOn();
        public abstract void TurnOff();
    }
}
