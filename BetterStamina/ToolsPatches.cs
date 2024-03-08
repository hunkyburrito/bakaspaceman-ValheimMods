using BetterStamina;
using HarmonyLib;
using System.Diagnostics;

internal static class ToolsPatches
{
    [HarmonyPatch(typeof(Player), "UseStamina")]
    [HarmonyPrefix]
    private static bool UseStamina_Prefix(Player __instance, ref float v)
    {
        if (__instance.GetCurrentWeapon() != null)
        {
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