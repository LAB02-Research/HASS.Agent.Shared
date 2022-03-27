﻿namespace HASSAgent.Shared.Models.HomeAssistant.Commands
{
    /// <summary>
    /// Internal commands (executed within the HASS.Agent platform)
    /// </summary>
    public class InternalCommand : AbstractCommand
    {
        public string State { get; protected set; }
        public string CommandConfig { get; protected set; }

        public InternalCommand(string name = "Internal", string commandConfig = "", string id = default) : base(name ?? "Internal", id)
        {
            State = "OFF";
            CommandConfig = commandConfig;
        }

        public override void TurnOn()
        {
            State = "ON";

            // to be overriden

            State = "OFF";
        }
        
        public override DiscoveryConfigModel GetAutoDiscoveryConfig()
        {
            if (Variables.MqttManager == null) return null;

            var deviceConfig = Variables.MqttManager.GetDeviceConfigModel();
            if (deviceConfig == null) return null;

            return new CommandDiscoveryConfigModel()
            {
                Name = Name,
                Unique_id = Id,
                Availability_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/sensor/{deviceConfig.Name}/availability",
                Command_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{ObjectId}/set",
                State_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{ObjectId}/state",
                Device = deviceConfig,
            };
        }

        public override string GetState() => State;

        public override void TurnOff()
        {
            //
        }
    }
}
