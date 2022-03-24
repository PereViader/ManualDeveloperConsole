using ManualCheats.Core.EditorWidgets.Nop;

namespace ManualCheats.Core.EditorWidgets
{
    public class CheatEditorWidgetFactory
    {
        private readonly EditorWidgetConfiguration editorWidgetConfiguration;

        public CheatEditorWidgetFactory(EditorWidgetConfiguration editorWidgetConfiguration)
        {
            this.editorWidgetConfiguration = editorWidgetConfiguration;
        }

        public ICheatEditorWidget Create(ICheat cheat)
        {
            var type = cheat.GetType();
            var configuration = editorWidgetConfiguration.WidgetEntry.Find(x => x.predicate.Invoke(type));
            if (configuration == default)
            {
                return new NopCheatEditorWidget(cheat);
            }

            return configuration.createDelegate.Invoke(cheat);
        }
    }
}
