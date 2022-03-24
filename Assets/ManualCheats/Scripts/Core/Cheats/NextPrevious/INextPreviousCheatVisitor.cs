namespace ManualCheats.Core
{
    public interface INextPreviousCheatVisitor<TArg, TReturn>
    {
        TReturn Visit<T>(NextPreviousCheat<T> cheat, TArg arg);
    }
}
