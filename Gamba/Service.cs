using Dalamud.Game.ClientState.Objects;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace Gamba;

public class Service
{
    public const string Name = "Gamba";

    // Plugin services provided by Dalamud
    [PluginService] public static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] public static ICommandManager Commands { get; private set; } = null!;
    [PluginService] public static ITargetManager TargetManager { get; set; } = null!;
    // One of the methods in the Helpers class uses this service, but is unused. Uncomment if needed
    //[PluginService] public static IPartyList PartyList { get; private set; } = null!;

    // Configuration and window system
    public static Configuration Configuration { get; set; } = null!;
    public static WindowSystem WindowSystem { get; } = new(Name);

    // Initialize the plugin services
    public static void Initialize(IDalamudPluginInterface pluginInterface)
    {
        pluginInterface.Create<Service>();
    }
}