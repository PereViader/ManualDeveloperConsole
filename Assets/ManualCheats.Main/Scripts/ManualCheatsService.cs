using System;
using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.Core
{
    public delegate ICheatWidget CreateCheatWidgetDelegate(ICheat cheat);
    public delegate void DisposeCheatWidgetDelegate(ICheatWidget cheatWidget);

    public class ManualCheatsService : MonoBehaviour, IManualCheatsService
    {
        public GameObject canvasGameObject;
        public ManualCheatsWidgetContainerController containerController;

        private Dictionary<Type, CreateCheatWidgetDelegate> createCheatWidgetDelegates;
        private Dictionary<Type, DisposeCheatWidgetDelegate> disposeCheatWidgetDelegates;

        private readonly Dictionary<ICheat, ICheatWidget> cheatWidgets = new Dictionary<ICheat, ICheatWidget>();
        private readonly Dictionary<ICheat, string> cheatCategories = new Dictionary<ICheat, string>();


        public bool IsVisible { get; set; }

        public void Inject(
            Dictionary<Type, CreateCheatWidgetDelegate> createCheatWidgetDelegates,
            Dictionary<Type, DisposeCheatWidgetDelegate> disposeCheatWidgetDelegates)
        {
            this.createCheatWidgetDelegates = createCheatWidgetDelegates;
            this.disposeCheatWidgetDelegates = disposeCheatWidgetDelegates;
        }

        public void AddCheat<T>(string category, T cheat) where T : ICheat
        {
            if (cheatWidgets.ContainsKey(cheat))
            {
                throw new InvalidOperationException("Cheat is already registered");
            }

            var createCheatWidgetDelegate = createCheatWidgetDelegates[typeof(T)];
            var cheatWidget = createCheatWidgetDelegate.Invoke(cheat);

            containerController.Add(category, cheatWidget);

            cheatWidgets.Add(cheat, cheatWidget);
            cheatCategories.Add(cheat, category);
        }

        public void RemoveCheat<T>(T cheat) where T : ICheat
        {
            if (!cheatWidgets.TryGetValue(cheat, out var cheatWidget))
            {
                throw new InvalidOperationException("Cheat was not registered");
            }

            if (!disposeCheatWidgetDelegates.TryGetValue(typeof(T), out var disposeCheatWidgetDelegate))
            {
                throw new InvalidOperationException("There was no cheat widget disposer for the type");
            }

            if (!cheatCategories.TryGetValue(cheat, out var category))
            {
                throw new InvalidOperationException("Could not get cheat category for cheat");
            }

            cheatWidget.Deactivate();

            containerController.Remove(category, cheatWidget);

            disposeCheatWidgetDelegate.Invoke(cheatWidget);
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
