using FarmingHysteresis.Extensions;
using RimTestRedux;

namespace FarmingHysteresis.Tests;

[HotSwappable]
[TestSuite]
internal static class PlantToGrowSettableExtensionsComputeCanEverYieldHarvestTests
{
    [Test]
    public static void FalseWhenNoValidPlantTypes()
    {
        var result = PlantToGrowSettableExtensions.ComputeCanEverYieldHarvest([]);

        Assert.That(result).Is.False();
    }

    // Regression guard for plant pots: their sowTag only matches purely decorative plants (e.g.
    // roses), so every valid plant type has a null harvestedThingDef.
    [Test]
    public static void FalseWhenEveryValidPlantTypeIsDecorative()
    {
        var rose = new ThingDef
        {
            defName = "Rose",
            plant = new PlantProperties { harvestedThingDef = null },
        };

        var result = PlantToGrowSettableExtensions.ComputeCanEverYieldHarvest([rose]);

        Assert.That(result).Is.False();
    }

    [Test]
    public static void TrueWhenAnyValidPlantTypeHasHarvestedThingDef()
    {
        var rose = new ThingDef
        {
            defName = "Rose",
            plant = new PlantProperties { harvestedThingDef = null },
        };
        var rice = new ThingDef
        {
            defName = "Rice",
            plant = new PlantProperties
            {
                harvestedThingDef = new ThingDef { defName = "RiceHarvested" },
            },
        };

        var result = PlantToGrowSettableExtensions.ComputeCanEverYieldHarvest([rose, rice]);

        Assert.That(result).Is.True();
    }
}
