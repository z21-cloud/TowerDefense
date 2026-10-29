using NUnit.Framework;
using TowerDefence.Core;

public class InventoryTests
{
    [Test]
    public void Constructor_InitializesSixteenCellsWithTwoReservedSlots()
    {
        Inventory inventory = new Inventory();

        Assert.That(inventory.CellsCount, Is.EqualTo(16));
        Assert.That(inventory.GetCell(0).Type, Is.EqualTo(CellType.Axe));
        Assert.That(inventory.GetCell(1).Type, Is.EqualTo(CellType.Shotgun));

        for (int i = 2; i < inventory.CellsCount; i++)
        {
            Assert.That(inventory.GetCell(i).Type, Is.EqualTo(CellType.Empty));
            Assert.That(inventory.GetCell(i).Count, Is.Zero);
        }
    }

    [Test]
    public void TryPickUp_AcceptsTwentyEightLogsAndRejectsTwentyNinth()
    {
        Inventory inventory = new Inventory();
        Pickable[] logs = new Pickable[29];

        for (int i = 0; i < logs.Length; i++)
        {
            logs[i] = new Pickable(PickableType.Log);
        }

        for (int i = 0; i < 28; i++)
        {
            Assert.That(inventory.TryPickUp(logs[i]), Is.True, $"Pickup {i + 1} should succeed.");
            Assert.That(logs[i].PickUpCount, Is.EqualTo(1));
        }

        Assert.That(inventory.LogsCount, Is.EqualTo(28));

        for (int i = 2; i < inventory.CellsCount; i++)
        {
            Assert.That(inventory.GetCell(i).Type, Is.EqualTo(CellType.Log));
            Assert.That(inventory.GetCell(i).Count, Is.EqualTo(2));
        }

        Assert.That(inventory.TryPickUp(logs[28]), Is.False);
        Assert.That(logs[28].PickUpCount, Is.Zero);
        Assert.That(inventory.LogsCount, Is.EqualTo(28));
    }

    [Test]
    public void TryPickUp_RejectsNonLogPickable()
    {
        Inventory inventory = new Inventory();
        Pickable otherPickable = new Pickable(PickableType.None);

        Assert.That(inventory.TryPickUp(otherPickable), Is.False);
        Assert.That(otherPickable.PickUpCount, Is.Zero);
        Assert.That(inventory.LogsCount, Is.Zero);
    }

    [Test]
    public void TryRemoveLog_DecreasesCountAndFreesCellWhenStackIsEmpty()
    {
        Inventory inventory = new Inventory();
        Pickable firstLog = new Pickable(PickableType.Log);
        Pickable secondLog = new Pickable(PickableType.Log);

        Assert.That(inventory.TryPickUp(firstLog), Is.True);
        Assert.That(inventory.TryPickUp(secondLog), Is.True);

        Assert.That(inventory.TryRemoveLog(), Is.True);
        Assert.That(inventory.LogsCount, Is.EqualTo(1));
        Assert.That(inventory.GetCell(2).Type, Is.EqualTo(CellType.Log));
        Assert.That(inventory.GetCell(2).Count, Is.EqualTo(1));

        Assert.That(inventory.TryRemoveLog(), Is.True);
        Assert.That(inventory.LogsCount, Is.Zero);
        Assert.That(inventory.GetCell(2).Type, Is.EqualTo(CellType.Empty));
        Assert.That(inventory.GetCell(2).Count, Is.Zero);
        Assert.That(inventory.TryRemoveLog(), Is.False);
    }

    private sealed class Pickable : IPickable
    {
        public PickableType Type { get; }
        public int PickUpCount { get; private set; }

        public Pickable(PickableType type)
        {
            Type = type;
        }

        public void PickUp()
        {
            PickUpCount++;
        }
    }
}
