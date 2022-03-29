using System;
using System.Collections.Generic;
using UnityDeveloperConsole.Core;
using UnityDeveloperConsole.EditorPlugin.Widgets;
using UnityDeveloperConsole.EditorPlugin.Widgets.Button;
using UnityDeveloperConsole.EditorPlugin.Widgets.DropdownButton;
using UnityDeveloperConsole.EditorPlugin.Widgets.NextPrevious;
using UnityDeveloperConsole.EditorPlugin.Widgets.Toggle;

namespace UnityDeveloperConsole.EditorPlugin
{
    public class DefaultEditorConfiguration : EditorConfiguration
    {
        public override EditorWidgetConfiguration Create()
        {
            return CreateDefault();
        }

        public static EditorWidgetConfiguration CreateDefault()
        {
            var configuration = new EditorWidgetConfiguration
            {
                WidgetEntry = new List<(Predicate<Type> predicate, Func<IOption, IOptionEditorWidget> createDelegate)>()
                {
                    CreateWidgetEntry<ButtonOption>(x => new ButtonOptionEditorWidget(x)),
                    CreateWidgetEntry<ToggleOption>(x => new ToggleOptionEditorWidget(x)),
                    CreateWidgetEntry<IDropdownButtonOption>(new DropdownButtonOptionEditorWidgetFactory().Create),
                    CreateWidgetEntry<INextPreviousOption>(new NextPreviousOptionEditorWidgetFactory().Create),
                }
            };

            return configuration;
        }

        public static (Predicate<Type> predicate, Func<IOption, IOptionEditorWidget> createDelegate) CreateWidgetEntry<TOption>(
            Func<TOption, IOptionEditorWidget> creationFunc
            )
            where TOption : IOption
        {
            return (typeof(TOption).IsAssignableFrom, x => creationFunc.Invoke((TOption)x));
        }
    }
}
