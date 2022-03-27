using System;
using System.Diagnostics;
using System.Globalization;
using HASSAgent.Shared.Functions;
using Serilog;

namespace HASSAgent.Shared.Models.HomeAssistant.Sensors
{
    /// <summary>
    /// Sensor containing the current value of the provided performance counter
    /// </summary>
    public class PerformanceCounterSensor : AbstractSingleValueSensor
    {
        protected PerformanceCounter Counter = null;

        public string CategoryName;
        public string CounterName;
        public string InstanceName;

        public PerformanceCounterSensor(string categoryName, string counterName, string instanceName, int? updateInterval = null, string name = "performancecountersensor", string id = default) : base(name ?? "performancecountersensor", updateInterval ?? 10, id)
        {
            CategoryName = categoryName;
            CounterName = counterName;
            InstanceName = instanceName;

            Counter = PerformanceCounters.GetSingleInstanceCounter(categoryName, counterName);
            if (Counter == null)
            {
                Log.Error("[PERFMON] Counter not found: {cat}\\{name}\\{inst}", categoryName, counterName, instanceName);
                return;
            }

            Counter.InstanceName = instanceName;

            try
            {
                Counter.NextValue();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "[PERFMON] Error retrieving counter value for {cat}\\{name}\\{inst}: {msg}", categoryName, counterName, instanceName, ex.Message);
            }
        }

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
                State_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{Name}/state",
                Availability_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/availability"
            });
        }
        
        public override string GetState()
        {
            if (Counter == null) return string.Empty;
            try
            {
                return Math.Round(Counter.NextValue()).ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Log.Error("[PERFMON] Error retrieving counter value for {cat}\\{name}\\{inst}: {msg}", CategoryName, CounterName, InstanceName, ex.Message);
                return string.Empty;
            }
        }
    }
}
