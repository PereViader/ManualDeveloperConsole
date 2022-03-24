namespace ManualCheats.Core
{
    public interface INextPreviousCheat : ICheat
    {
        TReturn Accept<TArg, TReturn>(INextPreviousCheatVisitor<TArg, TReturn> visitor, TArg arg);
    }
}
