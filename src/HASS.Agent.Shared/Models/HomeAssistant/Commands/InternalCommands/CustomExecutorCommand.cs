using System;
using System.Diagnostics;
using System.IO;
using Serilog;

namespace HASS.Agent.Shared.Models.HomeAssistant.Commands.InternalCommands
{
    /// <summary>
    /// Command to perform an action through the configured custom executor
    /// </summary>
    public class CustomExecutorCommand : InternalCommand
    {
        public CustomExecutorCommand(string name = "CustomExecutor", string command = "", string id = default) : base(name ?? "CustomExecutor", command, id)
        {
            CommandConfig = command;
            State = "OFF";
        }

        public override void TurnOn()
        {
            State = "ON";

            try
            {
                // is there a custom executor provided?
                if (string.IsNullOrEmpty(Variables.CustomExecutorBinary))
                {
                    Log.Warning("[CUSTOMEXECUTOR] No custom executor provided, unable to execute");
                    return;
                }

                // does the binary still exist?
                if (!File.Exists(Variables.CustomExecutorBinary))
                {
                    Log.Error("[CUSTOMEXECUTOR] Provided custom executor not found: {file}", Variables.CustomExecutorBinary);
                    return;
                }

                // all good, launch
                using var process = new Process();
                var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = Variables.CustomExecutorBinary,
                    Arguments = CommandConfig
                };

                process.StartInfo = startInfo;
                var start = process.Start();

                // check if the start went ok
                if (!start) Log.Error("[CUSTOMEXECUTOR] Unable to start executing command: {command}", CommandConfig);

                // yep, done
            }
            catch (Exception ex)
            {
                Log.Error("[CUSTOMEXECUTOR] Error while processing custom executor: {err}", ex.Message);
            }
            finally
            {
                State = "OFF";
            }
        }
    }
}
