using ManualCheats.Core;
using ManualCheats.EditorPlugin.Widgets;
using System;
using System.Collections.Generic;

namespace ManualCheats.EditorPlugin
{
    public class EditorWidgetConfiguration
    {
        public List<(Predicate<Type> predicate, Func<ICheat, ICheatEditorWidget> createDelegate)> WidgetEntry { get; set; }
    }
}
