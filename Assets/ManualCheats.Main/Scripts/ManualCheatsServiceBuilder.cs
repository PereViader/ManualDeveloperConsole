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

            service.Inject(new ManualTypeCheatConfigurationStore(typeCheatConfigurations));

            return service;
        }
    }
}
