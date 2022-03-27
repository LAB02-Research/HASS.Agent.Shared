﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace HASSAgent.Shared.Models.HomeAssistant.Commands
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class MultipleKeysCommand : AbstractCommand
    {
        public List<string> Keys { get; set; }

        public MultipleKeysCommand(List<string> keys, string name = "MultipleKeys", string id = default) : base(name ?? "MultipleKeys", id) => Keys = keys;

        public override DiscoveryConfigModel GetAutoDiscoveryConfig()
        {
            if (Variables.MqttManager == null) return null;

            var deviceConfig = Variables.MqttManager.GetDeviceConfigModel();
            if (deviceConfig == null) return null;

            return new CommandDiscoveryConfigModel
            {
                Name = Name,
                Unique_id = Id,
                Availability_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/sensor/{deviceConfig.Name}/availability",
                Command_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{ObjectId}/set",
                State_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{ObjectId}/state",
                Device = deviceConfig
            };
        }

        public override string GetState() => "OFF";

        public override void TurnOff()
        {
            //
        }

        public override async void TurnOn()
        {
            try
            {
                foreach (var key in Keys)
                {
                    SendKeys.SendWait(key);
                    SendKeys.Flush();
                    await Task.Delay(50);
                }
            }
            catch (Exception ex)
            {
                Log.Error("[COMMAND] Launching command '{name}' failed: {ex}", Name, ex.Message);
            }
        }
    }
}
