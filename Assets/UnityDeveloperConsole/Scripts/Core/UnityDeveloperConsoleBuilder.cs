using System.Collections.Generic;
using UnityEngine;

namespace UnityDeveloperConsole.Core
{
    public class UnityDeveloperConsoleBuilder : IUnityDeveloperConsoleBuilder
    {
        private readonly List<TypeOptionConfiguration> typeCheatConfigurations = new List<TypeOptionConfiguration>();

        public IUnityDeveloperConsoleBuilder AddOption(TypeOptionConfiguration typeCheatConfiguration)
        {
            typeCheatConfigurations.Add(typeCheatConfiguration);
            return this;
        }

        public IUnityDeveloperConsole Build(UnityDeveloperConsoleRuntimeConfiguration runtimeConfiguration)
        {
            var service = GameObject.Instantiate(runtimeConfiguration.unityDeveloperConsole);
            GameObject.DontDestroyOnLoad(service);

            service.gameObject.SetActive(false);

            var typeCheatConfigurationRepository = new TypeOptionConfigurationRepository(typeCheatConfigurations);
            var repository = new OptionEntryRepository();
            var allState = new DisplayingAllState(
                typeCheatConfigurationRepository,
                service.containerController,
                repository);

            var searchState = new SearchingState(
                allState,
                repository);

            service.Inject(
                repository,
                allState,
                searchState);

            return service;
        }
    }
}
