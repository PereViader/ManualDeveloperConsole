namespace ManualCheats.Core
{
    public class DropdownButtonCheatWidgetFactory : IDropdownButtonCheatVisitor<DropwdownButtonCheatWidgetReferences, ICheatWidget>
    {
        public ICheatWidget Create(IDropdownButtonCheat cheat, DropwdownButtonCheatWidgetReferences references)
        {
            var widget = cheat.Accept(this, references);
            return widget;
        }

        public ICheatWidget Visit<T>(DropdownButtonCheat<T> dropdownButtonCheat, DropwdownButtonCheatWidgetReferences references)
        {
            var widget = new DropdownButtonCheatWidget<T>(references, dropdownButtonCheat);
            widget.Initialize();
            return widget;
        }
    }
}
