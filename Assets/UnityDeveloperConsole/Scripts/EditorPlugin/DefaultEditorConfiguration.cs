using System;
using System.Collections.Generic;
using UnityDeveloperConsole.Core;
using UnityDeveloperConsole.EditorPlugin.Widgets;
using UnityDeveloperConsole.EditorPlugin.Widgets.Button;
using UnityDeveloperConsole.EditorPlugin.Widgets.DropdownButton;
using UnityDeveloperConsole.EditorPlugin.Widgets.NextPrevious;
using UnityDeveloperConsole.EditorPlugin.Widgets.Toggle;
using UnityEngine;

namespace UnityDeveloperConsole.EditorPlugin
{
    [CreateAssetMenu(menuName = "Unity Developer Console/Default Editor Configuration")]
    public class DefaultEditorConfiguration : EditorConfiguration
    {
        public override EditorWidgetConfiguration Create()
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

        public (Predicate<Type> predicate, Func<IOption, IOptionEditorWidget> createDelegate) CreateWidgetEntry<TOption>(
            Func<TOption, IOptionEditorWidget> creationFunc
            )
            where TOption : IOption
        {
            return (typeof(TOption).IsAssignableFrom, x => creationFunc.Invoke((TOption)x));
        }
    }
}
