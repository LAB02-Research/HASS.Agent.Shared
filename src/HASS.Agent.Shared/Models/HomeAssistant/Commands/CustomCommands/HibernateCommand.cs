﻿namespace HASS.Agent.Shared.Models.HomeAssistant.Commands.CustomCommands
{
    /// <summary>
    /// Command to put Windows in hibernation
    /// </summary>
    public class HibernateCommand : CustomCommand
    {
        public HibernateCommand(string name = "Hibernate", string id = default) : base("shutdown /h", false, name ?? "Hibernate", id) => State = "OFF";
    }
}