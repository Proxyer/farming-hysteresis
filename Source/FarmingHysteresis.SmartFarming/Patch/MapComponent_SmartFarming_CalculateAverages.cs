using SmartFarming;

namespace FarmingHysteresis.SmartFarming.Patch;

/// <summary>
/// Captures the sow decision Smart Farming's own petty-jobs calculation just wrote to <see
/// cref="Zone_Growing.allowSow"/>, so <see cref="SmartFarmingIntegration.AllowSow"/> can replay
/// it between Smart Farming's own (much less frequent) recalculations - see <see
/// cref="SmartFarmingIntegration"/> for why this cooperation is needed at all.
/// </summary>
[HarmonyPatch(
    typeof(MapComponent_SmartFarming),
    nameof(MapComponent_SmartFarming.CalculateAverages)
)]
internal static class MapComponent_SmartFarming_CalculateAverages
{
    private static void Postfix(ref Zone_Growing zone) =>
        SmartFarmingIntegration.CacheAllowSow(zone, zone.allowSow);
}
