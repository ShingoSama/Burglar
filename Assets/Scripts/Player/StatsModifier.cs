public enum StatsModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class StatsModifier
{
    public readonly float Value;
    public readonly StatsModType Type;
    public readonly int Order;
    public readonly object Source;
    public StatsModifier(float value,  StatsModType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }
    public StatsModifier(float value, StatsModType type) : this(value, type, (int)type) { }
    public StatsModifier(float value, StatsModType type, int order) : this(value, type, order, null) { }
    public StatsModifier(float value, StatsModType type, object source) : this(value, type, (int)type, source) { }
}
