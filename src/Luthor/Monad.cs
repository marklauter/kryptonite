namespace Luthor;

public delegate Out<TValue, TInp> M<TInp, TValue>(TInp inp);

public static class Monad
{
    public static M<TInp, TValue> Result<TInp, TValue>(TValue value) =>
        inp => new(value, inp);

    public static M<TInp, RValue> Bind<TInp, LValue, RValue>(
        this M<TInp, LValue> mLeft,
        Func<LValue, M<TInp, RValue>> continuation)
    {
        ArgumentNullException.ThrowIfNull(mLeft);
        ArgumentNullException.ThrowIfNull(continuation);

        return inp =>
        {
            var oLeft = mLeft(inp);
            if (oLeft.Value is null)
            {
                return new(default, inp);
            }

            var oRight =
                continuation(oLeft.Value)(oLeft.Remainder);

            return oRight.Value is not null
                ? oRight
                : new(default, inp);
        };
    }
}

public struct Out<TValue, TInp>(TValue? value, TInp remainder)
{
    public TValue? Value = value;
    public TInp Remainder = remainder;
}
