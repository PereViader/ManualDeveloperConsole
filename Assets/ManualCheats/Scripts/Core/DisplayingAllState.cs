using System;
using System.Collections.Generic;
using System.Linq;

namespace ManualCheats.Core
{
    public class DisplayingAllState : IState
    {
        private readonly ManualTypeCheatConfigurationStore manualTypeCheatConfigurationStore;
        private readonly ManualCheatsWidgetContainerController containerController;
        private readonly CheatEntryRepository cheatEntryRepository;

        private readonly Dictionary<CheatEntry, ICheatRuntimeWidget> cheatWidgets = new Dictionary<CheatEntry, ICheatRuntimeWidget>();

        public DisplayingAllState(
            ManualTypeCheatConfigurationStore manualTypeCheatConfigurationStore,
            ManualCheatsWidgetContainerController containerController,
            CheatEntryRepository cheatEntryRepository)
        {
            this.manualTypeCheatConfigurationStore = manualTypeCheatConfigurationStore;
            this.containerController = containerController;
            this.cheatEntryRepository = cheatEntryRepository;
        }

        public bool Contains(CheatEntry cheatEntry)
        {
            return cheatWidgets.ContainsKey(cheatEntry);
        }

        public void OnEnter()
        {
            foreach (var cheatEntry in cheatEntryRepository.GetAll())
            {
                OnCheatAdded(cheatEntry);
            }
        }

        public void OnExit()
        {
            RemoveAll();
        }

        public void OnCheatAdded(CheatEntry cheatEntry)
        {
            var type = cheatEntry.Cheat.GetType();
            if (!manualTypeCheatConfigurationStore.TryGet(type, out var configuration))
            {
                throw new InvalidOperationException($"No cheat configuration was found for type {type.FullName}");
            }

            var cheatWidget = configuration.CreateCheatWidgetDelegate.Invoke(cheatEntry.Cheat);

            cheatWidgets.Add(cheatEntry, cheatWidget);
            containerController.Add(cheatEntry.Category, cheatWidget);
            cheatWidget.Activate();
        }

        public void OnCheatRemoved(CheatEntry cheatEntry)
        {
            var type = cheatEntry.Cheat.GetType();
            if (!manualTypeCheatConfigurationStore.TryGet(type, out var configuration))
            {
                throw new InvalidOperationException($"No cheat configuration was found for type {type.FullName}");
            }

            if (!cheatWidgets.TryGetValue(cheatEntry, out var cheatWidget))
            {
                throw new InvalidOperationException("Cheat was not registered");
            }
            cheatWidgets.Remove(cheatEntry);

            cheatWidget.Deactivate();
            containerController.Remove(cheatEntry.Category, cheatWidget);
            configuration.DisposeCheatWidgetDelegate.Invoke(cheatWidget);
        }

        public void RemoveAll()
        {
            foreach (var cheatEntry in cheatWidgets.Keys.ToArray())
            {
                OnCheatRemoved(cheatEntry);
            }
        }

        public void Activate()
        {
            foreach (var cheat in cheatWidgets)
            {
                cheat.Value.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (var cheat in cheatWidgets)
            {
                cheat.Value.Deactivate();
            }
        }
    }
}
