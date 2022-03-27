﻿using System.Diagnostics;
using HASSAgent.Shared.Functions;
using Serilog;

namespace HASSAgent.Shared.Models.HomeAssistant.Commands
{
    /// <summary>
    /// Command to perform an action through a console, either normal or with low integrity
    /// </summary>
    public class CustomCommand : AbstractCommand
    {
        public string Command { get; protected set; }
        public string State { get; protected set; }
        public bool RunAsLowIntegrity { get; protected set; }
        public Process Process { get; set; } = null;

        public CustomCommand(string command, bool runAsLowIntegrity, string name = "Custom", string id = default) : base(name ?? "Custom", id)
        {
            Command = command;
            RunAsLowIntegrity = runAsLowIntegrity;
            State = "OFF";
        }

        public override void TurnOn()
        {
            State = "ON";

            if (RunAsLowIntegrity) CommandLineManager.LaunchAsLowIntegrity(Command);
            else
            {
                var executed = CommandLineManager.ExecuteHeadless(Command);

                if (!executed)
                {
                    Log.Error("[COMMAND] Launching command '{name}' failed", Name);
                    State = "FAILED";
                    return;
                }
            }

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

        public override void TurnOff() => Process?.Kill();
    }
}
