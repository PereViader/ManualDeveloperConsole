using ManualCheats.Core;
using ManualCheats.EditorPlugin.Widgets;
using ManualCheats.EditorPlugin.Widgets.Button;
using ManualCheats.EditorPlugin.Widgets.DropdownButton;
using ManualCheats.EditorPlugin.Widgets.NextPrevious;
using ManualCheats.EditorPlugin.Widgets.Toggle;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.EditorPlugin
{
    [CreateAssetMenu(menuName = "Manual Cheats/Default Editor Configuration")]
    public class DefaultEditorConfiguration : EditorConfiguration
    {
        public override EditorWidgetConfiguration Create()
        {
            var configuration = new EditorWidgetConfiguration
            {
                WidgetEntry = new List<(Predicate<Type> predicate, Func<ICheat, ICheatEditorWidget> createDelegate)>()
                {
                    CreateWidgetEntry<ButtonCheat>(x => new ButtonCheatEditorWidget(x)),
                    CreateWidgetEntry<ToggleCheat>(x => new ToggleCheatEditorWidget(x)),
                    CreateWidgetEntry<IDropdownButtonCheat>(new DropdownButtonCheatEditorWidgetFactory().Create),
                    CreateWidgetEntry<INextPreviousCheat>(new NextPreviousCheatEditorWidgetFactory().Create),
                }
            };

            return configuration;
        }

        public (Predicate<Type> predicate, Func<ICheat, ICheatEditorWidget> createDelegate) CreateWidgetEntry<TCheat>(
            Func<TCheat, ICheatEditorWidget> creationFunc
            )
            where TCheat : ICheat
        {
            return (typeof(TCheat).IsAssignableFrom, x => creationFunc.Invoke((TCheat)x));
        }
    }
}
