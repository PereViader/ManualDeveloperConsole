using System;
using System.Collections.Generic;
using UnityDeveloperConsole.Core;
using UnityDeveloperConsole.EditorPlugin.Utils;
using UnityDeveloperConsole.EditorPlugin.Widgets;
using UnityEditor;

namespace UnityDeveloperConsole.EditorPlugin
{
    public class UnityDeveloperConsoleEditorService
    {
        public static UnityDeveloperConsoleEditorService Instance { get; private set; }

        private readonly OptionEditorWidgetFactory widgetFactory;
        private readonly Dictionary<IOption, IOptionEditorWidget> optionEditorWidgets = new Dictionary<IOption, IOptionEditorWidget>();

        private UnityDeveloperConsoleEditorService(OptionEditorWidgetFactory widgetFactory)
        {
            this.widgetFactory = widgetFactory;
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            var serviceConfigurations = AssetDatabaseUtils.FindAssetsOfType<EditorConfiguration>();
            if (serviceConfigurations.Count == 0)
            {
                UnityEngine.Debug.LogError("Unity Developer Console editor configuration was not found, please create the asset using the Create Asset menu");
                return;
            }

            if (serviceConfigurations.Count > 1)
            {
                UnityEngine.Debug.LogError($"There should be only one asset of type {nameof(EditorConfiguration)}");
                return;
            }

            var serviceConfiguration = serviceConfigurations[0];

            var widgetConfiguration = serviceConfiguration.Create();
            Instance = new UnityDeveloperConsoleEditorService(new OptionEditorWidgetFactory(widgetConfiguration));
            EditorApplication.playModeStateChanged += Instance.EditorApplication_playModeStateChanged;
            OptionEvents.OnOptionAdded += Instance.AddOption;
            OptionEvents.OnOptionRemoved += Instance.RemoveOption;
        }

        private void EditorApplication_playModeStateChanged(PlayModeStateChange change)
        {
            if (change != PlayModeStateChange.ExitingPlayMode)
            {
                return;
            }

            optionEditorWidgets.Clear();
            UnityDeveloperConsoleEditorWindow.OnGuiActions.Clear();
        }

        public void AddOption(string category, IOption option)
        {
            if (optionEditorWidgets.ContainsKey(option))
            {
                throw new InvalidOperationException("Option is already registered");
            }

            IOptionEditorWidget widget = widgetFactory.Create(option);

            widget.Activate();

            optionEditorWidgets.Add(option, widget);
            UnityDeveloperConsoleEditorWindow.OnGuiActions.Add(widget.OnGUI);
        }

        public void RemoveOption(IOption option)
        {
            if (!optionEditorWidgets.TryGetValue(option, out var widget))
            {
                throw new InvalidOperationException("Option was not registered");
            }

            widget.Deactivate();

            optionEditorWidgets.Remove(option);
            UnityDeveloperConsoleEditorWindow.OnGuiActions.Remove(widget.OnGUI);
        }
    }
}
