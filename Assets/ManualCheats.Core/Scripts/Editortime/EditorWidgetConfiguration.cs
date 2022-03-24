using System;
using System.Collections.Generic;

namespace ManualCheats.Core.EditorWidgets
{
    public class EditorWidgetConfiguration
    {
        public List<(Predicate<Type> predicate, Func<ICheat, ICheatEditorWidget> createDelegate)> WidgetEntry { get; set; }
    }
}
