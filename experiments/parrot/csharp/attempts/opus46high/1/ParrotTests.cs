using Xunit;

namespace Parrot;

public class ParrotTests
{
    [Fact]
    public void SpeedOfEuropeanParrot()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.EUROPEAN, 0, 0, false);
        Assert.Equal(12.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfAfricanParrotWithOneCoconut()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.AFRICAN, 1, 0, false);
        Assert.Equal(3.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfAfricanParrotWithTwoCoconuts()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.AFRICAN, 2, 0, false);
        Assert.Equal(0.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfAfricanParrotWithNoCoconuts()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.AFRICAN, 0, 0, false);
        Assert.Equal(12.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNailed()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 0, true);
        Assert.Equal(0.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNailedWithVoltage()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 1.5, true);
        Assert.Equal(0.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNotNailed()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 1.5, false);
        Assert.Equal(18.0, parrot.GetSpeed());
    }

    [Fact]
    public void SpeedOfNorwegianBlueParrotNotNailedHighVoltage()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 4, false);
        Assert.Equal(24.0, parrot.GetSpeed());
    }

    [Fact]
    public void CryOfEuropeanParrot()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.EUROPEAN, 0, 0, false);
        Assert.Equal("Sqoork!", parrot.GetCry());
    }

    [Fact]
    public void CryOfAfricanParrot()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.AFRICAN, 2, 0, false);
        Assert.Equal("Sqaark!", parrot.GetCry());
    }

    [Fact]
    public void CryOfNorwegianBlueParrotHighVoltage()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 4, false);
        Assert.Equal("Bzzzzzz", parrot.GetCry());
    }

    [Fact]
    public void CryOfNorwegianBlueParrotNoVoltage()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 0, false);
        Assert.Equal("...", parrot.GetCry());
    }

    [Fact]
    public void SpeedOfExoticParrot()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.EXOTIC);
        Assert.Equal(4.0, parrot.GetSpeed());
    }

    [Fact]
    public void CryOfExoticParrot()
    {
        var parrot = Parrot.Create(ParrotTypeEnum.EXOTIC);
        Assert.Equal("squaaaawk-exotic", parrot.GetCry());
    }
}
