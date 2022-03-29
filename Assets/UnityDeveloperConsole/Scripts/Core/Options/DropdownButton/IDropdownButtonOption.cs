namespace UnityDeveloperConsole.Core
{
    public interface IDropdownButtonOption : IOption
    {
        TReturn Accept<TArg, TReturn>(IDropdownButtonOptionVisitor<TArg, TReturn> visitor, TArg arg);
    }
}
