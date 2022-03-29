namespace UnityDeveloperConsole.Core
{
    public class NextPreviousCheatRuntimeWidgetFactory : INextPreviousOptionVisitor<NextPreviousOptionRuntimeWidgetReferences, IOptionRuntimeWidget>
    {
        public IOptionRuntimeWidget Create(INextPreviousOption cheat, NextPreviousOptionRuntimeWidgetReferences nextPreviousCheatWidgetReferences)
        {
            var widget = cheat.Accept(this, nextPreviousCheatWidgetReferences);
            return widget;
        }

        public IOptionRuntimeWidget Visit<T>(NextPreviousOption<T> cheat, NextPreviousOptionRuntimeWidgetReferences references)
        {
            var widget = new NextPreviousOptionRuntimeWidget<T>(cheat, references);
            widget.Initialize();
            return widget;
        }
    }
}
