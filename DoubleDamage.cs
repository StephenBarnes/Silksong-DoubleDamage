using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;

namespace Silksong.DoubleDamage
{
    private ConfigEntry<float> _mult;

    [BepInPlugin("stebars.silksong.double-damage", "Double Damage", "1.0.0")]
    public sealed class DoubleDamagePlugin : BaseUnityPlugin
    {
        internal static ManualLogSource LogSrc;

        private void Awake()
        {
            _mult = Config.Bind("General", "PlayerDamageMultiplier", 2f, "Multiplier for all hero damage");
            LogSrc = Logger;
            var harmony = new Harmony("stebars.silksong.double-damage");
            harmony.PatchAll();
            LogSrc.LogInfo("Double Damage loaded: all hero damage will be doubled.");
        }
    }

    // ---- Harmony patch ----
    // This targets the *private* method HealthManager.TakeDamage(HitInstance)
    [HarmonyPatch(typeof(HealthManager), "TakeDamage")]
    internal static class Patch_HealthManager_TakeDamage
    {
        // Prefix runs before the original. We modify the HitInstance in-place.
        static void Prefix(ref HitInstance hitInstance)
        {
            // Only touch outgoing player damage.
            if (!hitInstance.IsHeroDamage)
                return;

            // If mods or the game have already set a multiplier, multiply on top.
            hitInstance.Multiplier *= DoubleDamagePlugin.Instance._mult.Value;

            // Optional: comment the next line out if you don’t want log spam.
            DoubleDamagePlugin.LogSrc?.LogDebug($"Doubled hero damage. New Multiplier={hitInstance.Multiplier}");
        }
    }
}
