using System;

namespace Parrot;

public enum ParrotTypeEnum
{
    EUROPEAN,
    AFRICAN,
    NORWEGIAN_BLUE
}

public class Parrot
{
    private readonly ParrotTypeEnum _type;
    private readonly int _numberOfCoconuts;
    private readonly double _voltage;
    private readonly bool _isNailed;

    public Parrot(ParrotTypeEnum type, int numberOfCoconuts, double voltage, bool isNailed)
    {
        _type = type;
        _numberOfCoconuts = numberOfCoconuts;
        _voltage = voltage;
        _isNailed = isNailed;
    }

    public double GetSpeed()
    {
        switch (_type)
        {
            case ParrotTypeEnum.EUROPEAN:
                return GetBaseSpeed();
            case ParrotTypeEnum.AFRICAN:
                return Math.Max(0, GetBaseSpeed() - GetLoadFactor() * _numberOfCoconuts);
            case ParrotTypeEnum.NORWEGIAN_BLUE:
                return _isNailed ? 0 : GetBaseSpeed(_voltage);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public string GetCry()
    {
        switch (_type)
        {
            case ParrotTypeEnum.EUROPEAN:
                return "Sqoork!";
            case ParrotTypeEnum.AFRICAN:
                return "Sqaark!";
            case ParrotTypeEnum.NORWEGIAN_BLUE:
                return _voltage > 0 ? "Bzzzzzz" : "...";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private double GetBaseSpeed(double voltage)
    {
        return Math.Min(24.0, voltage * GetBaseSpeed());
    }

    private double GetLoadFactor()
    {
        return 9.0;
    }

    private double GetBaseSpeed()
    {
        return 12.0;
    }
}
