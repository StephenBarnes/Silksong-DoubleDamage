using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace Silksong.DoubleDamage
{
    [BepInPlugin("stebars.silksong.double-damage", "Double Damage", "1.0.0")]
    public sealed class DoubleDamagePlugin : BaseUnityPlugin
    {
        internal static ManualLogSource LogSrc;
        internal static ConfigEntry<float> DamageMult;

        private void Awake()
        {
            LogSrc = Logger;
            DamageMult = Config.Bind("General", "PlayerDamageMultiplier", 2f, "Multiplier for all hero damage");

            var harmony = new Harmony("stebars.silksong.double-damage");
            harmony.PatchAll();

            LogSrc.LogInfo($"Double Damage loaded. Multiplier = {DamageMult.Value}");
        }
    }

    // ---- Harmony patch ----
    // Target: private void HealthManager.TakeDamage(HitInstance hitInstance)
    [HarmonyPatch(typeof(HealthManager), "TakeDamage")]
    internal static class Patch_HealthManager_TakeDamage
    {
        // Runs before the original, lets us tweak the HitInstance that's about to be applied.
        private static void Prefix(ref HitInstance hitInstance)
        {
            // Only touch damage originating from the hero/player.
            if (!hitInstance.IsHeroDamage)
                return;

            // Multiply on top of whatever other mods/game applied.
            hitInstance.Multiplier *= DoubleDamagePlugin.DamageMult.Value;

            // Optional debug line (comment out if noisy)
            DoubleDamagePlugin.LogSrc?.LogDebug($"Applied hero damage mult: {DoubleDamagePlugin.DamageMult.Value} -> final Multiplier={hitInstance.Multiplier}");
        }
    }
}
