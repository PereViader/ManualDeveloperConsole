namespace UnityDeveloperConsole.Core
{
    public interface IDropdownButtonOptionVisitor<TArg, TReturn>
    {
        TReturn Visit<T>(DropdownButtonOption<T> dropdownButtonCheat, TArg arg);
    }
}
