using RimTestRedux;

namespace FarmingHysteresis.Tests;

[HotSwappable]
[TestSuite]
internal static class HysteresisModeExtensionsAsStringTests
{
    [Test]
    public static void SowingReturnsNonEmptyString()
    {
        var result = HysteresisMode.Sowing.AsString();
        Assert.That(result).Is.Not.Null();
        Assert.That(result).Is.Not.EqualTo("");
    }

    [Test]
    public static void HarvestingReturnsNonEmptyString()
    {
        var result = HysteresisMode.Harvesting.AsString();
        Assert.That(result).Is.Not.Null();
        Assert.That(result).Is.Not.EqualTo("");
    }

    [Test]
    public static void SowingAndHarvestingReturnsNonEmptyString()
    {
        var result = HysteresisMode.SowingAndHarvesting.AsString();
        Assert.That(result).Is.Not.Null();
        Assert.That(result).Is.Not.EqualTo("");
    }

    [Test]
    [ShouldThrow(typeof(InvalidOperationException))]
    public static void UncoveredHysteresisModeThrows() => _ = ((HysteresisMode)99).AsString();
}

[HotSwappable]
[TestSuite]
internal static class HysteresisModeExtensionsControlsTests
{
    [Test]
    public static void SowingControlsSowingOnly()
    {
        Assert.That(HysteresisMode.Sowing.ControlsSowing()).Is.True();
        Assert.That(HysteresisMode.Sowing.ControlsHarvesting()).Is.False();
    }

    [Test]
    public static void HarvestingControlsHarvestingOnly()
    {
        Assert.That(HysteresisMode.Harvesting.ControlsSowing()).Is.False();
        Assert.That(HysteresisMode.Harvesting.ControlsHarvesting()).Is.True();
    }

    [Test]
    public static void SowingAndHarvestingControlsBoth()
    {
        Assert.That(HysteresisMode.SowingAndHarvesting.ControlsSowing()).Is.True();
        Assert.That(HysteresisMode.SowingAndHarvesting.ControlsHarvesting()).Is.True();
    }
}
