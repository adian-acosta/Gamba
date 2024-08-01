using Dalamud.Game.Command;
using Dalamud.Plugin;
using Gamba.Windows;

namespace Gamba;

public class Plugin : IDalamudPlugin
{
    private const string CmdName = "/gamba";

    public Plugin(IDalamudPluginInterface pluginInterface)
    {
        Service.Initialize(pluginInterface);
        Service.Configuration = Configuration.Load();
        Service.PluginInterface.UiBuilder.Draw += Service.WindowSystem.Draw;
        Service.PluginInterface.UiBuilder.OpenMainUi += OnOpenMainUi;

        MainWindow = new MainWindow(this);

        Service.Commands.AddHandler(CmdName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Open the gamba window"
        });
    }

    private MainWindow MainWindow { get; }

    public void Dispose()
    {
        Service.Configuration?.Save();

        Service.Commands.RemoveHandler(CmdName);
        Service.PluginInterface.UiBuilder.Draw -= Service.WindowSystem.Draw;
        Service.PluginInterface.UiBuilder.OpenMainUi -= OnOpenMainUi;
    }

    public void OnCommand(string command, string args)
    {
        OnOpenMainUi();
    }

    private void OnOpenMainUi()
    {
        MainWindow.Toggle();
    }
}