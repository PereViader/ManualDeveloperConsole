namespace UnityDeveloperConsole.Core
{
    public class DropdownButtonOptionRuntimeWidgetFactory : IDropdownButtonOptionVisitor<DropwdownButtonOptionRuntimeWidgetReferences, IOptionRuntimeWidget>
    {
        public IOptionRuntimeWidget Create(IDropdownButtonOption cheat, DropwdownButtonOptionRuntimeWidgetReferences references)
        {
            var widget = cheat.Accept(this, references);
            return widget;
        }

        public IOptionRuntimeWidget Visit<T>(DropdownButtonOption<T> dropdownButtonCheat, DropwdownButtonOptionRuntimeWidgetReferences references)
        {
            var widget = new DropdownButtonOptionRuntimeWidget<T>(references, dropdownButtonCheat);
            widget.Initialize();
            return widget;
        }
    }
}
