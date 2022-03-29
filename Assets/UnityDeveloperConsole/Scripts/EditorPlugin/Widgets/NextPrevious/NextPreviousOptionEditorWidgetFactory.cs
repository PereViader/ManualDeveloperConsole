using UnityDeveloperConsole.Core;

namespace UnityDeveloperConsole.EditorPlugin.Widgets.NextPrevious
{
    public class NextPreviousOptionEditorWidgetFactory : INextPreviousOptionVisitor<object, IOptionEditorWidget>
    {
        public IOptionEditorWidget Create(INextPreviousOption option)
        {
            return option.Accept(this, null);
        }

        public IOptionEditorWidget Visit<T>(NextPreviousOption<T> nextPreviousOptionEditorWidget, object arg)
        {
            return new NextPreviousOptionEditorWidget<T>(nextPreviousOptionEditorWidget);
        }
    }
}
