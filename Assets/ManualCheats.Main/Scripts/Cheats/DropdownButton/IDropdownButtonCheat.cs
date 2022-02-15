namespace ManualCheats.Core
{
    public interface IDropdownButtonCheat : ICheat
    {
        TReturn Accept<TArg, TReturn>(IDropdownButtonCheatVisitor<TArg, TReturn> visitor, TArg arg);
    }
}
