using System;
using System.Collections.Generic;
using System.Text;

namespace HASSAgent.Shared.Models.Config.Service
{
    public class ServiceSettings
    {
        public ServiceSettings()
        {
            //
        }

        public string AuthId { get; set; } = string.Empty;

        public string DeviceName { get; set; } = $"{Environment.MachineName}-satellite";

        public string CustomExecutorName { get; set; } = string.Empty;
        public string CustomExecutorBinary { get; set; } = string.Empty;

        public int DisconnectedGracePeriodSeconds { get; set; } = 60;
    }
}
