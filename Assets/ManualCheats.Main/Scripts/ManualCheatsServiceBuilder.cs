using System;
using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.Core
{
    public class ManualCheatsServiceBuilder : IManualCheatsServiceBuilder
    {
        private readonly Dictionary<Type, CreateCheatWidgetDelegate> createCheatWidgetDelegates = new Dictionary<Type, CreateCheatWidgetDelegate>();
        private readonly Dictionary<Type, DisposeCheatWidgetDelegate> disposeCheatWidgetDelegates = new Dictionary<Type, DisposeCheatWidgetDelegate>();

        public IManualCheatsServiceBuilder AddCheat<TCheat, TCheatWidget>(
            Func<TCheat, TCheatWidget> createWidget,
            Action<TCheatWidget> disposeWidget)
            where TCheat : ICheat
            where TCheatWidget : ICheatWidget<TCheat>
        {
            var cheatType = typeof(TCheat);
            createCheatWidgetDelegates.Add(cheatType, x => createWidget.Invoke((TCheat)x));
            disposeCheatWidgetDelegates.Add(cheatType, x => disposeWidget.Invoke((TCheatWidget)x));

            return this;
        }

        public IManualCheatsService Build(ManualCheatsConfiguration manualCheatsConfiguration)
        {
            var service = GameObject.Instantiate(manualCheatsConfiguration.manualCheatsServicePrefab);
            GameObject.DontDestroyOnLoad(service);

            service.gameObject.SetActive(false);

            service.Inject(
                createCheatWidgetDelegates,
                disposeCheatWidgetDelegates);

            return service;
        }
    }
}
