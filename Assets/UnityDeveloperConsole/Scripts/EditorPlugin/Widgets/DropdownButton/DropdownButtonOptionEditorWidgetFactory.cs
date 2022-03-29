using UnityDeveloperConsole.Core;

namespace UnityDeveloperConsole.EditorPlugin.Widgets.DropdownButton
{
    public class DropdownButtonOptionEditorWidgetFactory : IDropdownButtonOptionVisitor<object, IOptionEditorWidget>
    {
        public IOptionEditorWidget Create(IDropdownButtonOption option)
        {
            return option.Accept(this, null);
        }

        public IOptionEditorWidget Visit<T>(DropdownButtonOption<T> dropdownButtonOption, object arg)
        {
            return new DropdownButtonOptionEditorWidget<T>(dropdownButtonOption);
        }
    }
}
