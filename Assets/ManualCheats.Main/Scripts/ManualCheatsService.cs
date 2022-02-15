using System;
using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.Core
{
    public class ManualCheatsService : MonoBehaviour, IManualCheatsService
    {
        public GameObject canvasGameObject;
        public ManualCheatsWidgetContainerController containerController;

        private ManualTypeCheatConfigurationStore manualTypeCheatConfigurationStore;

        private readonly Dictionary<ICheat, ICheatWidget> cheatWidgets = new Dictionary<ICheat, ICheatWidget>();
        private readonly Dictionary<ICheat, string> cheatCategories = new Dictionary<ICheat, string>();


        public bool IsVisible { get; set; }

        public void Inject(ManualTypeCheatConfigurationStore manualTypeCheatConfigurationStore)
        {
            this.manualTypeCheatConfigurationStore = manualTypeCheatConfigurationStore;
        }

        public void AddCheat(string category, ICheat cheat)
        {
            if (cheatWidgets.ContainsKey(cheat))
            {
                throw new InvalidOperationException("Cheat is already registered");
            }

            var type = cheat.GetType();
            if (!manualTypeCheatConfigurationStore.TryGet(type, out var configuration))
            {
                throw new InvalidOperationException($"No cheat configuration was found for type {type.FullName}");
            }

            var cheatWidget = configuration.CreateCheatWidgetDelegate.Invoke(cheat);

            containerController.Add(category, cheatWidget);

            cheatWidgets.Add(cheat, cheatWidget);
            cheatCategories.Add(cheat, category);
        }

        public void RemoveCheat(ICheat cheat)
        {
            if (!cheatWidgets.TryGetValue(cheat, out var cheatWidget))
            {
                throw new InvalidOperationException("Cheat was not registered");
            }

            var type = cheat.GetType();
            if (!manualTypeCheatConfigurationStore.TryGet(type, out var configuration))
            {
                throw new InvalidOperationException($"No cheat configuration was found for type {type.FullName}");
            }

            if (!cheatCategories.TryGetValue(cheat, out var category))
            {
                throw new InvalidOperationException("Could not get cheat category for cheat");
            }

            cheatWidget.Deactivate();

            containerController.Remove(category, cheatWidget);

            configuration.DisposeCheatWidgetDelegate.Invoke(cheatWidget);
            cheatWidgets.Remove(cheat);
            cheatCategories.Remove(cheat);
        }

        public void Show()
        {
            canvasGameObject.SetActive(true);

            foreach (var cheat in cheatWidgets)
            {
                cheat.Value.Activate();
            }

            IsVisible = true;
        }

        public void Hide()
        {
            foreach (var cheat in cheatWidgets)
            {
                cheat.Value.Deactivate();
            }

            canvasGameObject.SetActive(false);

            IsVisible = false;
        }
    }
}
