using UnityDeveloperConsole.Core;
using UnityDeveloperConsole.EditorPlugin.Widgets;
using UnityDeveloperConsole.EditorPlugin.Widgets.Nop;

namespace UnityDeveloperConsole.EditorPlugin
{
    public class OptionEditorWidgetFactory
    {
        private readonly EditorWidgetConfiguration editorWidgetConfiguration;

        public OptionEditorWidgetFactory(EditorWidgetConfiguration editorWidgetConfiguration)
        {
            this.editorWidgetConfiguration = editorWidgetConfiguration;
        }

        public IOptionEditorWidget Create(IOption option)
        {
            var type = option.GetType();
            var configuration = editorWidgetConfiguration.WidgetEntry.Find(x => x.predicate.Invoke(type));
            if (configuration == default)
            {
                return new NopOptionEditorWidget(option);
            }

            return configuration.createDelegate.Invoke(option);
        }
    }
}
