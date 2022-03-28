using HASS.Agent.Shared.Mqtt;

namespace HASS.Agent.Shared
{
    /// <summary>
    /// Base class, used for initialization and configuration
    /// </summary>
    public static class AgentSharedBase
    {
        /// <summary>
        /// Set bindings for internal functions and variables
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="mqttManager"></param>
        public static void Initialize(string deviceName, IMqttManager mqttManager)
        {
            Variables.DeviceName = deviceName;
            Variables.MqttManager = mqttManager;
        }

        /// <summary>
        /// Set bindings for internal functions and variables
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="mqttManager"></param>
        /// <param name="customExecutorBinary"></param>
        public static void Initialize(string deviceName, IMqttManager mqttManager, string customExecutorBinary)
        {
            Variables.DeviceName = deviceName;
            Variables.MqttManager = mqttManager;
            Variables.CustomExecutorBinary = customExecutorBinary;
        }

        /// <summary>
        /// Set bindings for internal functions and variables
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="customExecutorBinary"></param>
        public static void Initialize(string deviceName, string customExecutorBinary)
        {
            Variables.DeviceName = deviceName;
            Variables.CustomExecutorBinary = customExecutorBinary;
        }

        /// <summary>
        /// Set bindings for internal functions and variables
        /// </summary>
        /// <param name="deviceName"></param>
        public static void Initialize(string deviceName)
        {
            Variables.DeviceName = deviceName;
        }

        /// <summary>
        /// (Re)sets the custom executor binary
        /// </summary>
        /// <param name="customExecutorBinary"></param>
        public static void SetCustomExecutorBinary(string customExecutorBinary) => Variables.CustomExecutorBinary = customExecutorBinary;

        /// <summary>
        /// (Re)sets the device name
        /// </summary>
        /// <param name="deviceName"></param>
        public static void SetDeviceName(string deviceName) => Variables.DeviceName = deviceName;

        /// <summary>
        /// (Re)sets the MQTT manager
        /// </summary>
        /// <param name="mqttManager"></param>
        public static void SetMqttManager(IMqttManager mqttManager) => Variables.MqttManager = mqttManager;
    }
}
