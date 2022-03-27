using System;
using System.Collections.Generic;
using System.Text;
using HASSAgent.Shared.Enums;
using HASSAgent.Shared.Functions;

namespace HASSAgent.Shared.Extensions
{
    /// <summary>
    /// Extensions for HASS.Agent command objects
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Returns the name of the commandtype
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static string GetCommandName(this CommandType commandType)
        {
            var commandName = commandType.ToString().ToLower().Replace("command", "");
            return $"{SharedHelperFunctions.GetSafeConfiguredDeviceName()}_{commandName}";
        }

        /// <summary>
        /// Returns the name of the commandtype, based on the provided devicename
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public static string GetCommandName(this CommandType commandType, string deviceName)
        {
            var commandName = commandType.ToString().ToLower().Replace("command", "");
            return $"{SharedHelperFunctions.GetSafeValue(deviceName)}_{commandName}";
        }
    }
}
