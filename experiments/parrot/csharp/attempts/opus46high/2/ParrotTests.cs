using Xunit;

namespace Parrot;

public class ParrotTests
{
    [Fact]
    public void SpeedOfEuropeanParrot()
    {
        var parrot = new Parrot(ParrotTypeEnum.EUROPEAN, 0, 0, false);
        Assert.Equal(12.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfAfricanParrotWithOneCoconut()
    {
        var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 1, 0, false);
        Assert.Equal(3.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfAfricanParrotWithTwoCoconuts()
    {
        var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 2, 0, false);
        Assert.Equal(0.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfAfricanParrotWithNoCoconuts()
    {
        var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 0, 0, false);
        Assert.Equal(12.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNailed()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 0, true);
        Assert.Equal(0.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNailedWithVoltage()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 1.5, true);
        Assert.Equal(0.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNotNailed()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 1.5, false);
        Assert.Equal(18.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNotNailedHighVoltage()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 4, false);
        Assert.Equal(24.0, parrot.GetSpeed());
    }

    [Fact]
    public void CryOfEuropeanParrot()
    {
        var parrot = new Parrot(ParrotTypeEnum.EUROPEAN, 0, 0, false);
        Assert.Equal("Sqoork!", parrot.GetCry());
    }

    [Fact]
    public void CryOfAfricanParrot()
    {
        var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 2, 0, false);
        Assert.Equal("Sqaark!", parrot.GetCry());
    }

    [Fact]
    public void CryOfNorwegianBlueParrotHighVoltage()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 4, false);
        Assert.Equal("Bzzzzzz", parrot.GetCry());
    }

    [Fact]
    public void CryOfNorwegianBlueParrotNoVoltage()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 0, false);
        Assert.Equal("...", parrot.GetCry());
    }
}
