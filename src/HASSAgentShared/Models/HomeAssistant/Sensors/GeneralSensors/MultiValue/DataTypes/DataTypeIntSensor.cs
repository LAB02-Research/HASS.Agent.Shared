﻿namespace HASSAgent.Shared.Models.HomeAssistant.Sensors.GeneralSensors.MultiValue.DataTypes
{
    /// <summary>
    /// Generic int sensor
    /// </summary>
    public class DataTypeIntSensor : AbstractSingleValueSensor
    {
        private readonly string _deviceClass;
        private readonly string _unitOfMeasurement;
        private readonly string _icon;

        private int _value = 0;

        public DataTypeIntSensor(int? updateInterval, string name, string id, string deviceClass, string icon, string unitOfMeasurement, string multiValueSensorName) : base(name, updateInterval ?? 30, id)
        {
            TopicName = multiValueSensorName;

            _deviceClass = deviceClass;
            _unitOfMeasurement = unitOfMeasurement;
            _icon = icon;

            ObjectId = id;
        }

        public override DiscoveryConfigModel GetAutoDiscoveryConfig()
        {
            if (AutoDiscoveryConfigModel != null) return AutoDiscoveryConfigModel;

            if (Variables.MqttManager == null) return null;

            var deviceConfig = Variables.MqttManager.GetDeviceConfigModel();
            if (deviceConfig == null) return null;

            var model = new SensorDiscoveryConfigModel()
            {
                Name = Name,
                Unique_id = Id,
                Device = deviceConfig,
                State_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/{TopicName}/{ObjectId}/state",
                Availability_topic = $"{Variables.MqttManager.MqttDiscoveryPrefix()}/{Domain}/{deviceConfig.Name}/availability"
            };

            if (!string.IsNullOrWhiteSpace(_deviceClass)) model.Device_class = _deviceClass;
            if (!string.IsNullOrWhiteSpace(_unitOfMeasurement)) model.Unit_of_measurement = _unitOfMeasurement;
            if (!string.IsNullOrWhiteSpace(_icon)) model.Icon = _icon;

            return SetAutoDiscoveryConfigModel(model);
        }

        public void SetState(int value) => _value = value;

        public override string GetState() => _value.ToString();
    }
}
