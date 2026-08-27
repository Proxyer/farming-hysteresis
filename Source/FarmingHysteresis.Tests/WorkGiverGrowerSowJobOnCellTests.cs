using FarmingHysteresis.Patch;
using RimTestRedux;

namespace FarmingHysteresis.Tests;

// Regression guard: a protected leftover plant's cell must only ever have its cut job
// suppressed, never any other job JobOnCell might return (e.g. the eventual Sow job once the
// cell is actually clear) - see ManagerJob_FarmingHysteresis.ExecuteJobDataCoroutine for why
// this is a per-plant cut suppression rather than a whole-grower "allow sow" override.
[HotSwappable]
[TestSuite]
internal static class WorkGiverGrowerSowJobOnCellTests
{
    [Test]
    public static void CutJobOnProtectedLeftoverIsSuppressed() =>
        Assert
            .That(
                WorkGiver_GrowerSow_JobOnCell.ShouldSuppressCut(
                    jobDef: JobDefOf.CutPlant,
                    protectLeftoverFromCut: true
                )
            )
            .Is.True();

    [Test]
    public static void CutJobIsUntouchedWhenNothingIsProtected() =>
        Assert
            .That(
                WorkGiver_GrowerSow_JobOnCell.ShouldSuppressCut(
                    jobDef: JobDefOf.CutPlant,
                    protectLeftoverFromCut: false
                )
            )
            .Is.False();

    [Test]
    public static void NonCutJobIsNeverSuppressedEvenIfProtected() =>
        Assert
            .That(
                WorkGiver_GrowerSow_JobOnCell.ShouldSuppressCut(
                    jobDef: JobDefOf.Sow,
                    protectLeftoverFromCut: true
                )
            )
            .Is.False();
}
