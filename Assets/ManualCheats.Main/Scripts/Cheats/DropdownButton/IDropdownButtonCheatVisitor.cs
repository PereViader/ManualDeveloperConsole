namespace ManualCheats.Core
{
    public interface IDropdownButtonCheatVisitor<TArg, TReturn>
    {
        TReturn Visit<T>(DropdownButtonCheat<T> dropdownButtonCheat, TArg arg);
    }
}
