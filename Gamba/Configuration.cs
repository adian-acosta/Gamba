using System;
using Dalamud.Configuration;

namespace Gamba;

[Serializable]
public class Configuration : IPluginConfiguration
{
    // Version of the configuration file. Increase this if you want to invalidate the current config.
    public int Version { get; set; } = 1;

    public bool Blackjack = false;
    public float BlackjackPayoutFactor = 1.5f;

    // Save configuration to the plugin interface
    public void Save()
    {
        Service.PluginInterface.SavePluginConfig(this);
    }

    // Load configuration, create a new one if it doesn't exist
    public static Configuration Load()
    {
        if (Service.PluginInterface.GetPluginConfig() is Configuration config)
            return config;

        config = new Configuration();
        config.Save();
        return config;
    }
}