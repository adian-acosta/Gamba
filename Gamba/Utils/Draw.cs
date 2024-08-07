using Dalamud.Interface.Colors;
using ImGuiNET;
using System.ComponentModel.DataAnnotations;

namespace Gamba.Utils;

public static class Draw
{
    public static void AddButton()
    {
        ImGui.PushStyleColor(ImGuiCol.Button, 0xFF000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, 0xDD000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, 0xAA000000 | 0x005A5A5A);

        if (Helpers.IsTargeting())
        {
            if (ImGui.Button("Add Player")) Player.AddPlayer(Helpers.GetTargetName());
        }
        else
        {
            ImGui.Button("Add Player");
        }

        ImGui.PopStyleColor(3);
    }

    public static void DisplayNames()
    {
        for (var i = 0; i < Player.GetAllPlayers().Count; i++)
        {
            var player = Player.GetAllPlayers()[i];
            ImGui.TextColored(ImGuiColors.DalamudViolet, player.Name);
            ImGui.SameLine();
            RemoveButton(player.Name, i);
            if (Service.Configuration.Blackjack)
            {
                ImGui.SameLine();
                BlackjackButton(player.Name, i);
            }

            BetInput(player, i);
            ImGui.Indent(20f);
            Payout(player.Name, i);
            ImGui.SameLine();
            CopyButton(player.Name, i);
            ImGui.Unindent(20f);
            ImGui.Spacing();
            ImGui.Spacing();
            ImGui.Spacing();
            ImGui.Separator();
        }
    }

    private static void BetInput(Player player, int i)
    {
        var bet = player.Bet;
        Helpers.InputInt($"Bet##{i}", ref bet);
        if (Service.Configuration.Blackjack)
        {
            //
        }

        player.Bet = bet;
    }

    public static void RemoveButton(string name, int index)
    {
        ImGui.PushStyleColor(ImGuiCol.Button, 0xFF000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, 0xDD000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, 0xAA000000 | 0x005A5A5A);
        if (ImGui.Button($"Remove##{index}")) Player.RemovePlayer(name);
        ImGui.PopStyleColor(3);
    }

    public static void RemoveAllPlayersButton()
    {
        ImGui.PushStyleColor(ImGuiCol.Button, 0xFF000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, 0xDD000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, 0xAA000000 | 0x005A5A5A);

        if (Player.GetAllPlayers().Count > 0)
        {
            if (ImGui.Button("Remove All Players")) Player.RemoveAllPlayers();
        }
        else
        {
            ImGui.Button("Remove All Players");
        }

        ImGui.PopStyleColor(3);
    }

    public static void CopyButton(string name, int i)
    {
        ImGui.PushStyleColor(ImGuiCol.Button, 0xFF000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, 0xDD000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, 0xAA000000 | 0x005A5A5A);

        if (ImGui.Button($"Copy Payout to Clipboard##{i}"))
        {
            var bet = Player.GetPlayerBet(name);
            if (Service.Configuration.Blackjack && Player.PlayerBlackjack(name))
                bet = (int)(bet * Service.Configuration.BlackjackPayoutFactor) + bet;
            else
                bet *= 2;
            ImGui.SetClipboardText(bet.ToString());
        }

        ImGui.PopStyleColor(3);
    }

    public static void BlackjackButton(string name, int i)
    {
        ImGui.PushStyleColor(ImGuiCol.Button, 0xFF000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, 0xDD000000 | 0x005A5A5A);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, 0xAA000000 | 0x005A5A5A);

        var player = Player.GetPlayer(name);

        if (player != null && player.Bet > 0)
        {
            if (ImGui.Button($"Has Blackjack##{i}")) player.Blackjack = !player.Blackjack;
        }
        else
        {
            ImGui.Button($"Has Blackjack##{i}");
        }

        ImGui.PopStyleColor(3);
    }

    public static void Payout(string name, int i)
    {
        ImGui.Text("Payout: ");
        ImGui.SameLine();

        if (Service.Configuration.Blackjack && Player.PlayerBlackjack(name))
        {
            var bet = Player.GetPlayerBet(name);
            bet = (int)(bet * Service.Configuration.BlackjackPayoutFactor) + bet;
            ImGui.Text(bet.ToString("N0"));
        }
        else
        {
            var bet = Player.GetPlayerBet(name) * 2;
            ImGui.Text(bet.ToString("N0"));
        }
    }
}