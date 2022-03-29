using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityDeveloperConsole.Core
{
    public class DisplayingAllState : IState
    {
        private readonly TypeOptionConfigurationRepository typeOptionConfigurationRepository;
        private readonly UnityDeveloperConsoleWidgetContainerController containerController;
        private readonly OptionEntryRepository optionEntryRepository;

        private readonly Dictionary<OptionEntry, IOptionRuntimeWidget> optionWidgets = new Dictionary<OptionEntry, IOptionRuntimeWidget>();

        public DisplayingAllState(
            TypeOptionConfigurationRepository typeOptionConfigurationRepository,
            UnityDeveloperConsoleWidgetContainerController containerController,
            OptionEntryRepository optionEntryRepository)
        {
            this.typeOptionConfigurationRepository = typeOptionConfigurationRepository;
            this.containerController = containerController;
            this.optionEntryRepository = optionEntryRepository;
        }

        public bool Contains(OptionEntry optionEntry)
        {
            return optionWidgets.ContainsKey(optionEntry);
        }

        public void OnEnter()
        {
            foreach (var optionEntry in optionEntryRepository.GetAll())
            {
                OnOptionAdded(optionEntry);
            }
        }

        public void OnExit()
        {
            RemoveAll();
        }

        public void OnOptionAdded(OptionEntry optionEntry)
        {
            var type = optionEntry.Option.GetType();
            if (!typeOptionConfigurationRepository.TryGet(type, out var configuration))
            {
                throw new InvalidOperationException($"No option configuration was found for type {type.FullName}");
            }

            var optionWidget = configuration.CreateOptionWidgetDelegate.Invoke(optionEntry.Option);

            optionWidgets.Add(optionEntry, optionWidget);
            containerController.Add(optionEntry.Category, optionWidget);
            optionWidget.Activate();
        }

        public void OnOptionRemoved(OptionEntry optionEntry)
        {
            var type = optionEntry.Option.GetType();
            if (!typeOptionConfigurationRepository.TryGet(type, out var configuration))
            {
                throw new InvalidOperationException($"No option configuration was found for type {type.FullName}");
            }

            if (!optionWidgets.TryGetValue(optionEntry, out var optionWidget))
            {
                throw new InvalidOperationException("Option was not registered");
            }
            optionWidgets.Remove(optionEntry);

            optionWidget.Deactivate();
            containerController.Remove(optionEntry.Category, optionWidget);
            configuration.DisposeOptionWidgetDelegate.Invoke(optionWidget);
        }

        public void RemoveAll()
        {
            foreach (var optionEntry in optionWidgets.Keys.ToArray())
            {
                OnOptionRemoved(optionEntry);
            }
        }

        public void Activate()
        {
            foreach (var optionWidget in optionWidgets)
            {
                optionWidget.Value.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (var optionWidget in optionWidgets)
            {
                optionWidget.Value.Deactivate();
            }
        }
    }
}
