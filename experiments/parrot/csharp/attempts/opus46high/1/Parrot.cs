using System;

namespace Parrot;

public abstract class Parrot
{
    public abstract double GetSpeed();
    public abstract string GetCry();

    public static Parrot Create(ParrotTypeEnum type, int numberOfCoconuts = 0, double voltage = 0, bool isNailed = false)
    {
        return type switch
        {
            ParrotTypeEnum.EUROPEAN => new EuropeanParrot(),
            ParrotTypeEnum.AFRICAN => new AfricanParrot(numberOfCoconuts),
            ParrotTypeEnum.NORWEGIAN_BLUE => new NorwegianBlueParrot(voltage, isNailed),
            ParrotTypeEnum.EXOTIC => new ExoticParrot(),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}

public enum ParrotTypeEnum
{
    EUROPEAN,
    AFRICAN,
    NORWEGIAN_BLUE,
    EXOTIC
}

public class EuropeanParrot : Parrot
{
    public override double GetSpeed() => 12.0;
    public override string GetCry() => "Sqoork!";
}

public class AfricanParrot : Parrot
{
    private readonly int _numberOfCoconuts;

    public AfricanParrot(int numberOfCoconuts)
    {
        _numberOfCoconuts = numberOfCoconuts;
    }

    public override double GetSpeed() => Math.Max(0, 12.0 - 9.0 * _numberOfCoconuts);
    public override string GetCry() => "Sqaark!";
}

public class NorwegianBlueParrot : Parrot
{
    private readonly double _voltage;
    private readonly bool _isNailed;

    public NorwegianBlueParrot(double voltage, bool isNailed)
    {
        _voltage = voltage;
        _isNailed = isNailed;
    }

    public override double GetSpeed() => _isNailed ? 0 : Math.Min(24.0, _voltage * 12.0);
    public override string GetCry() => _voltage > 0 ? "Bzzzzzz" : "...";
}

public class ExoticParrot : Parrot
{
    public override double GetSpeed() => 4.0;
    public override string GetCry() => "squaaaawk-exotic";
}
