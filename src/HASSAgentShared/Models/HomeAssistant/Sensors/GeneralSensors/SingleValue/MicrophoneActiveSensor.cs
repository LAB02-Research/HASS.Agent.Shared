﻿using System.Linq;
using Microsoft.Win32;

namespace HASSAgent.Shared.Models.HomeAssistant.Sensors.GeneralSensors.SingleValue
{
    /// <summary>
    /// Sensor indicating whether the mic is in use
    /// </summary>
    public class MicrophoneActiveSensor : AbstractSingleValueSensor
    {
        public override string Domain => "binary_sensor";
        public MicrophoneActiveSensor(int? updateInterval = null, string name = "microphoneactive", string id = default) : base(name ?? "microphoneactive", updateInterval ?? 10, id) { }

        public override string GetState() => IsMicrophoneInUse() ? "ON" : "OFF";

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

        private static bool IsMicrophoneInUse()
        {
            const string regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\microphone";
            bool inUse;

            // first local machine
            using (var key = Registry.LocalMachine.OpenSubKey(regKey))
            {
                inUse = CheckRegForMicrophoneInUse(key);
                if (inUse) return true;
            }

            // then current user
            using (var key = Registry.CurrentUser.OpenSubKey(regKey))
            {
                inUse = CheckRegForMicrophoneInUse(key);
                if (inUse) return true;
            }

            // nope
            return false;
        }

        private static bool CheckRegForMicrophoneInUse(RegistryKey key)
        {
            if (key == null) return false;

            foreach (var subKeyName in key.GetSubKeyNames())
            {
                // NonPackaged has multiple subkeys
                if (subKeyName == "NonPackaged")
                {
                    using var nonpackagedkey = key.OpenSubKey(subKeyName);
                    if (nonpackagedkey == null) continue;

                    foreach (var nonpackagedSubKeyName in nonpackagedkey.GetSubKeyNames())
                    {
                        using var subKey = nonpackagedkey.OpenSubKey(nonpackagedSubKeyName);
                        if (subKey == null || !subKey.GetValueNames().Contains("LastUsedTimeStop")) continue;

                        var endTime = subKey.GetValue("LastUsedTimeStop") is long
                            ? (long)(subKey.GetValue("LastUsedTimeStop") ?? -1)
                            : -1;

                        if (endTime <= 0) return true;
                    }
                }
                else
                {
                    using var subKey = key.OpenSubKey(subKeyName);
                    if (subKey == null || !subKey.GetValueNames().Contains("LastUsedTimeStop")) continue;

                    var endTime = subKey.GetValue("LastUsedTimeStop") is long ? (long)(subKey.GetValue("LastUsedTimeStop") ?? -1) : -1;
                    if (endTime <= 0) return true;
                }
            }

            return false;
        }
    }
}
