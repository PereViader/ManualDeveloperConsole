namespace UnityDeveloperConsole.Core
{
    public interface INextPreviousOptionVisitor<TArg, TReturn>
    {
        TReturn Visit<T>(NextPreviousOption<T> cheat, TArg arg);
    }
}
