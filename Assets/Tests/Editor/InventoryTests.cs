using NUnit.Framework;

public class InventoryTests
{
    [Test]
    public void TryAddLog_ReturnsFalseAfterSixteenSuccessfulAdds()
    {
        Inventory inventory = new Inventory();

        for (int i = 0; i < 16; i++)
        {
            Assert.That(inventory.TryAddLog(), Is.True, $"Add attempt {i + 1} should succeed.");
        }

        Assert.That(inventory.TryAddLog(), Is.False, "The 17th add attempt should fail.");
        Assert.That(inventory.LogsCount, Is.EqualTo(16));
    }
}
