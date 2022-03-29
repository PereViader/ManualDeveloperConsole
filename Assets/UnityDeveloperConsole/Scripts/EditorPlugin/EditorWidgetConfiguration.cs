using System;
using System.Collections.Generic;
using UnityDeveloperConsole.Core;
using UnityDeveloperConsole.EditorPlugin.Widgets;

namespace UnityDeveloperConsole.EditorPlugin
{
    public class EditorWidgetConfiguration
    {
        public List<(Predicate<Type> predicate, Func<IOption, IOptionEditorWidget> createDelegate)> WidgetEntry { get; set; }
    }
}
