namespace UnityDeveloperConsole.Core
{
    public interface INextPreviousOption : IOption
    {
        TReturn Accept<TArg, TReturn>(INextPreviousOptionVisitor<TArg, TReturn> visitor, TArg arg);
    }
}
