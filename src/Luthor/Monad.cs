namespace Luthor;

public delegate Out<TValue, TInp> Mondad<TValue, TInp>(TInp inp);

public static class Monad
{
    public static Mondad<TValue, TInp> Zero<TValue, TInp>() => Out.Zero<TValue, TInp>;

    public static Mondad<TValue, TInp> Result<TValue, TInp>(TValue value) =>
        input => new(value, true, input, input);

    public static Mondad<RValue, TInp> Bind<TInp, LValue, RValue>(
        this Mondad<LValue, TInp> mLeft,
        Func<LValue, Mondad<RValue, TInp>> continuation)
    {
        ArgumentNullException.ThrowIfNull(mLeft);
        ArgumentNullException.ThrowIfNull(continuation);

        return input =>
        {
            var oLeft = mLeft(input);
            if (!oLeft.HasValue)
            {
                return Out.Zero<RValue, TInp>(input);
            }

            var mRight = continuation(oLeft.Value);
            var oRight = mRight(oLeft.Remainder);

            return oRight.HasValue
                ? oRight
                : Out.Zero<RValue, TInp>(input);
        };
    }

    public static Mondad<TValue, TInp>[] Plus<TValue, TInp>(this Mondad<TValue, TInp> left, Mondad<TValue, TInp> right)
        => [left, right];
}

public static class Out
{
    public static Out<TValue, TInp> Zero<TValue, TInp>(TInp input) => new(default!, false, input, input);
    public static Out<TValue, TInp> Value<TValue, TInp>(TValue value, TInp input) => new(value, true, input, input);
}

public readonly struct Out<TValue, TInp>(
    TValue value,
    bool hasValue,
    TInp input,
    TInp remainder)
{
    public readonly TValue Value = value;
    public readonly bool HasValue = hasValue;
    public readonly TInp Input = input;
    public readonly TInp Remainder = remainder;
}
