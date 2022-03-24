using System;
using System.Collections.Generic;
using UnityEditor;

namespace ManualCheats.Core.EditorWidgets
{
    public class ManualCheatsEditorService
    {
        public static ManualCheatsEditorService Instance { get; private set; }

        private readonly CheatEditorWidgetFactory widgetFactory;
        private readonly Dictionary<ICheat, ICheatEditorWidget> widgets = new Dictionary<ICheat, ICheatEditorWidget>();

        private ManualCheatsEditorService(CheatEditorWidgetFactory widgetFactory)
        {
            this.widgetFactory = widgetFactory;
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            var serviceConfigurations = AssetDatabaseUtils.FindAssetsOfType<EditorConfiguration>();
            if (serviceConfigurations.Count == 0)
            {
                UnityEngine.Debug.LogError("Manual Cheats editor configuration was not found, please create the asset using the Create Asset menu");
                return;
            }

            if (serviceConfigurations.Count > 1)
            {
                UnityEngine.Debug.LogError($"There should be only one asset of type {nameof(EditorConfiguration)}");
                return;
            }

            var serviceConfiguration = serviceConfigurations[0];

            var widgetConfiguration = serviceConfiguration.Create();
            Instance = new ManualCheatsEditorService(new CheatEditorWidgetFactory(widgetConfiguration));
            EditorApplication.playModeStateChanged += Instance.EditorApplication_playModeStateChanged;
            CheatEvents.OnCheatAdded += Instance.AddCheat;
            CheatEvents.OnCheatRemoved += Instance.RemoveCheat;
        }

        public void Register()
        {

        }

        private void EditorApplication_playModeStateChanged(PlayModeStateChange change)
        {
            if (change != PlayModeStateChange.ExitingPlayMode)
            {
                return;
            }

            widgets.Clear();
            ManualCheatsEditorWindow.OnGuiActions.Clear();
        }

        public void AddCheat(string category, ICheat cheat)
        {
            if (widgets.ContainsKey(cheat))
            {
                throw new InvalidOperationException("Cheat is already registered");
            }

            ICheatEditorWidget widget = widgetFactory.Create(cheat);

            widget.Activate();

            widgets.Add(cheat, widget);
            ManualCheatsEditorWindow.OnGuiActions.Add(widget.OnGUI);
        }

        public void RemoveCheat(ICheat cheat)
        {
            if (!widgets.TryGetValue(cheat, out var widget))
            {
                throw new InvalidOperationException("Cheat was not registered");
            }

            widget.Deactivate();

            widgets.Remove(cheat);
            ManualCheatsEditorWindow.OnGuiActions.Remove(widget.OnGUI);
        }
    }
}
