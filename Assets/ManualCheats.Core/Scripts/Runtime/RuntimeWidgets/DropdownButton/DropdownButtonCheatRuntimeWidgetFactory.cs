namespace ManualCheats.Core
{
    public class DropdownButtonCheatRuntimeWidgetFactory : IDropdownButtonCheatVisitor<DropwdownButtonCheatRuntimeWidgetReferences, ICheatRuntimeWidget>
    {
        public ICheatRuntimeWidget Create(IDropdownButtonCheat cheat, DropwdownButtonCheatRuntimeWidgetReferences references)
        {
            var widget = cheat.Accept(this, references);
            return widget;
        }

        public ICheatRuntimeWidget Visit<T>(DropdownButtonCheat<T> dropdownButtonCheat, DropwdownButtonCheatRuntimeWidgetReferences references)
        {
            var widget = new DropdownButtonCheatRuntimeWidget<T>(references, dropdownButtonCheat);
            widget.Initialize();
            return widget;
        }
    }
}
