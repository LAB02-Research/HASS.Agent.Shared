﻿using System.Linq;
using HASSAgent.Shared.Functions;

namespace HASSAgent.Shared.Models.HomeAssistant.Sensors.GeneralSensors.SingleValue
{
    /// <summary>
    /// Sensor indicated whether the provided windowname is currently open (doesn't need focus)
    /// </summary>
    public class NamedWindowSensor : AbstractSingleValueSensor
    {
        public override string Domain => "binary_sensor";
        public string WindowName { get; protected set; }

        public NamedWindowSensor(string windowName, string name = "namedwindow", int? updateInterval = 10, string id = default) : base(name ?? "namedwindow", updateInterval ?? 30, id) => WindowName = windowName;

        public override DiscoveryConfigModel GetAutoDiscoveryConfig()
        {
            if (Variables.MqttManager == null) return null;

            var deviceConfig = Variables.MqttManager.GetDeviceConfigModel();
            if (deviceConfig == null) return null;

            return AutoDiscoveryConfigModel ?? SetAutoDiscoveryConfigModel(new SensorDiscoveryConfigModel()
            {
                Name = Name,
                Unique_id = Id,
                Device = deviceConfig,
                State_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{ObjectId}/state",
                Availability_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/sensor/{deviceConfig.Name}/availability"
            });
        }

        public override string GetState()
        {
            var windowNames = SharedHelperFunctions.GetOpenWindows().Values;
            return windowNames.Any(v => v.ToUpper().Contains(WindowName.ToUpper())) ? "ON" : "OFF";
        }
    }
}