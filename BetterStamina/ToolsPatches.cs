using BetterStamina;
using HarmonyLib;
using System.Diagnostics;

internal static class ToolsPatches
{
    [HarmonyPatch(typeof(Player), "UseStamina")]
    [HarmonyPrefix]
    private static bool UseStamina_Prefix(ref float v)
    {
        if (Player.m_localPlayer.GetCurrentWeapon() != null)
        {
            string name = Player.m_localPlayer.GetCurrentWeapon().m_shared.m_name;
            string callingMethodName = new StackFrame(2).GetMethod().Name;

            if (callingMethodName.Contains("Repair") || callingMethodName.Contains("UpdatePlacement"))
            {
                if (BetterStaminaPlugin.removeToolStaminaCost.Value)
                {
                    return false;
                }
            }
        }

        return true;
    }
}