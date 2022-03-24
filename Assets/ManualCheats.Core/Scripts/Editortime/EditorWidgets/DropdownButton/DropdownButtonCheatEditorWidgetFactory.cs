namespace ManualCheats.Core.EditorWidgets.DropdownButton
{
    public class DropdownButtonCheatEditorWidgetFactory : IDropdownButtonCheatVisitor<object, ICheatEditorWidget>
    {
        public ICheatEditorWidget Create(IDropdownButtonCheat cheat)
        {
            return cheat.Accept(this, null);
        }

        public ICheatEditorWidget Visit<T>(DropdownButtonCheat<T> dropdownButtonCheat, object arg)
        {
            return new DropdownButtonCheatEditorWidget<T>(dropdownButtonCheat);
        }
    }
}
