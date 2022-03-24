namespace ManualCheats.Core
{
    public class NextPreviousCheatRuntimeWidgetFactory : INextPreviousCheatVisitor<NextPreviousCheatRuntimeWidgetReferences, ICheatRuntimeWidget>
    {
        public ICheatRuntimeWidget Create(INextPreviousCheat cheat, NextPreviousCheatRuntimeWidgetReferences nextPreviousCheatWidgetReferences)
        {
            var widget = cheat.Accept(this, nextPreviousCheatWidgetReferences);
            return widget;
        }

        public ICheatRuntimeWidget Visit<T>(NextPreviousCheat<T> cheat, NextPreviousCheatRuntimeWidgetReferences references)
        {
            var widget = new NextPreviousCheatRuntimeWidget<T>(cheat, references);
            widget.Initialize();
            return widget;
        }
    }
}
