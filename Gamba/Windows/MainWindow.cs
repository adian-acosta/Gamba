using System;
using Dalamud.Interface.Windowing;
using Gamba.Utils;
using ImGuiNET;

namespace Gamba.Windows;

public class MainWindow : Window, IDisposable
{
    private readonly Plugin Plugin;

    public MainWindow(Plugin plugin) : base("Gamba")
    {
        Service.WindowSystem.AddWindow(this);
        Flags |= ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize;

        Plugin = plugin;
    }

    public void Dispose()
    {
        Service.Configuration.Save();
        Service.WindowSystem.RemoveWindow(this);
    }

    public override void Draw()
    {
        if (!IsOpen) return;

        ImGui.Text("- Target the player you want to add.");
        Helpers.Checkbox("Blackjack", ref Service.Configuration.Blackjack,
            "- Enable this if the game you are playing is blackjack");
        if (Service.Configuration.Blackjack)
            Helpers.InputFloat("Payout Factor", ref Service.Configuration.BlackjackPayoutFactor,
                "- The factor by which the payout is multiplied by when a player gets blackjack." +
                "\n\n - For example, if the payout factor is 1.5 (3:2) and a player bets 100 gil, they will receive 250 gil in total if they get blackjack rather than the standard 200 gil." +
                "\n\n- The default value is 1.5.");
        ImGui.Separator();
        ImGui.Spacing();
        ImGui.Spacing();

        Utils.Draw.AddButton();
        ImGui.SameLine();
        Utils.Draw.RemoveAllPlayersButton();

        ImGui.Separator();
        ImGui.Spacing();
        ImGui.Spacing();

        Utils.Draw.DisplayNames();
    }
}