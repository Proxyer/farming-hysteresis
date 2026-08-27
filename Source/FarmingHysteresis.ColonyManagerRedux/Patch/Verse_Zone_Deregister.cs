using ColonyManagerRedux;

namespace FarmingHysteresis.ColonyManagerRedux.Patch;

/// <summary>
/// Mirrors <c>ColonyManagerRedux</c>'s own <c>Verse_AreaManager_NotifyEveryoneAreaRemoved</c>
/// patch, but for zones.
/// </summary>
[HarmonyPatch(typeof(Zone), nameof(Zone.Deregister))]
internal static class Verse_Zone_Deregister
{
    private static void Postfix(Zone __instance)
    {
        if (__instance.Map is not { } map)
        {
            return;
        }

        foreach (var job in Manager.For(map).JobTracker.JobsOfType<ManagerJob_FarmingHysteresis>())
        {
            job.Notify_ZoneRemoved(__instance);
        }
    }
}
