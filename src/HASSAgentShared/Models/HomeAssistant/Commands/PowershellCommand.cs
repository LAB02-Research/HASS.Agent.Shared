using System.Diagnostics;
using HASSAgent.Shared.Functions;
using Serilog;

namespace HASSAgent.Shared.Models.HomeAssistant.Commands
{
    /// <summary>
    /// Command to perform an action or script through Powershell
    /// </summary>
    public class PowershellCommand : AbstractCommand
    {
        public string Command { get; protected set; }
        public string State { get; protected set; }
        public Process Process { get; set; }

        private readonly bool _isScript = false;
        private readonly string _descriptor = "command";

        public PowershellCommand(string command, string name = "Powershell", string id = default) : base(name ?? "Powershell", id)
        {
            Command = command;
            if (Command.ToLower().EndsWith(".ps1"))
            {
                _isScript = true;
                _descriptor = "script";
            }

            State = "OFF";
        }

        public override void TurnOn()
        {
            State = "ON";

            var executed = _isScript
                ? PowershellManager.ExecuteScriptHeadless(Command)
                : PowershellManager.ExecuteCommandHeadless(Command);

            if (!executed)
            {
                Log.Error("[COMMAND] Launching PS {descriptor} '{name}' failed", _descriptor, Name);
                State = "FAILED";
            }
            else State = "OFF";
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
