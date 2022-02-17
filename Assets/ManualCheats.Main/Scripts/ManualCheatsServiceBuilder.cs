using System.Collections.Generic;
using UnityEngine;

namespace ManualCheats.Core
{
    public class ManualCheatsServiceBuilder : IManualCheatsServiceBuilder
    {
        private readonly List<TypeCheatConfiguration> typeCheatConfigurations = new List<TypeCheatConfiguration>();

        public IManualCheatsServiceBuilder AddCheat(TypeCheatConfiguration typeCheatConfiguration)
        {
            typeCheatConfigurations.Add(typeCheatConfiguration);
            return this;
        }

        public IManualCheatsService Build(ManualCheatsConfiguration manualCheatsConfiguration)
        {
            var service = GameObject.Instantiate(manualCheatsConfiguration.manualCheatsServicePrefab);
            GameObject.DontDestroyOnLoad(service);

            service.gameObject.SetActive(false);

            var store = new ManualTypeCheatConfigurationStore(typeCheatConfigurations);
            var repository = new CheatEntryRepository();
            var allState = new DisplayingAllState(
                store,
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
