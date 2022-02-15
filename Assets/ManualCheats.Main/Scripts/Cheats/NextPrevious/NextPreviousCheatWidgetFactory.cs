namespace ManualCheats.Core
{
    public class NextPreviousCheatWidgetFactory : INextPreviousCheatVisitor<NextPreviousCheatWidgetReferences, ICheatWidget>
    {
        public ICheatWidget Create(INextPreviousCheat cheat, NextPreviousCheatWidgetReferences nextPreviousCheatWidgetReferences)
        {
            var widget = cheat.Accept(this, nextPreviousCheatWidgetReferences);
            return widget;
        }

        public ICheatWidget Visit<T>(NextPreviousCheat<T> cheat, NextPreviousCheatWidgetReferences references)
        {
            var widget = new NextPreviousCheatWidget<T>(cheat, references);
            widget.Initialize();
            return widget;
        }
    }
}
