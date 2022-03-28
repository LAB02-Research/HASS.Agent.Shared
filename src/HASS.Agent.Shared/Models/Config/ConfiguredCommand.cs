using System;
using System.Collections.Generic;
using HASS.Agent.Shared.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HASS.Agent.Shared.Models.Config
{
    /// <summary>
    /// Storable version of command objects
    /// </summary>
    public class ConfiguredCommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CommandType Type { get; set; }
        public Guid Id { get; set; } = Guid.Empty;
        public string Command { get; set; } = string.Empty;
        public byte KeyCode { get; set; }
        public bool RunAsLowIntegrity { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public List<string> Keys { get; set; } = new List<string>();
    }
}
