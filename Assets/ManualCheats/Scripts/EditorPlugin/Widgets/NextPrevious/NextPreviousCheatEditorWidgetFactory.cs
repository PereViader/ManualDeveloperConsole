using ManualCheats.Core;

namespace ManualCheats.EditorPlugin.Widgets.NextPrevious
{
    public class NextPreviousCheatEditorWidgetFactory : INextPreviousCheatVisitor<object, ICheatEditorWidget>
    {
        public ICheatEditorWidget Create(INextPreviousCheat cheat)
        {
            return cheat.Accept(this, null);
        }

        public ICheatEditorWidget Visit<T>(NextPreviousCheat<T> nextPreviousCheatEditorWidget, object arg)
        {
            return new NextPreviousCheatEditorWidget<T>(nextPreviousCheatEditorWidget);
        }
    }
}
