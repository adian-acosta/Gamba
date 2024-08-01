using Dalamud.Interface.Components;
using ImGuiNET;

namespace Gamba.Utils;

internal static class Helpers
{
    // Check if the player is targeting an object
    public static bool IsTargeting()
    {
        return Service.TargetManager?.Target is not null;
    }

    // Get the name of the object the player is targeting
    public static string GetTargetName()
    {
        return Service.TargetManager?.Target?.Name.ToString() ?? "No Target";
    }

    // Draw a checkbox with optional help text
    public static void Checkbox(string label, ref bool refValue, string helpText = "")
    {
        ImGui.Checkbox($"{label}", ref refValue);

        if (helpText != string.Empty)
            ImGuiComponents.HelpMarker(helpText);
    }

    // Draw an input field for an integer with optional help text
    public static void InputInt(string label, ref int refValue, string helpText = "")
    {
        ImGui.InputInt($"{label}", ref refValue);

        if (helpText != string.Empty)
            ImGuiComponents.HelpMarker(helpText);
    }

    // Draw an input field for a double with optional help text
    public static void InputFloat(string label, ref float refValue, string helpText = "")
    {
        ImGui.InputFloat($"{label}", ref refValue, 0.5f);

        if (helpText != string.Empty)
            ImGuiComponents.HelpMarker(helpText);
    }

    // None of these are used in the current codebase, but if a use is found then uncomment them
    /*
    // Get the first name from a full name
    public static string GetFirstName(string fullName)
    {
        return fullName.Split(' ')[0];
    }

    // Get the name of the plugin user
    public static unsafe string GetUsersName()
    {
        return PlayerState.Instance()->CharacterNameString;
    }


    public static bool IsPlayer()
    {
        return Service.TargetManager.Target.GetType().ToString() == "Dalamud.Game.ClientState.Objects.SubKinds.PlayerCharacter";
    }

    public static bool IsUserInParty()
    {
        // don't know if the second condition is necessary, but I have it just in case
        // the user is in an alliance raid and maybe that co
        return Service.PartyList.Count > 1 && Service.PartyList.Count <= 8;
    }
    */
}