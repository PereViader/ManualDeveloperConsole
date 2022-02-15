using System;
using System.Collections.Generic;

namespace ManualCheats.Core
{
    public class ManualTypeCheatConfigurationStore
    {
        private readonly IReadOnlyList<TypeCheatConfiguration> typeCheatConfigurations;

        public ManualTypeCheatConfigurationStore(IReadOnlyList<TypeCheatConfiguration> typeCheatConfigurations)
        {
            this.typeCheatConfigurations = typeCheatConfigurations;
        }

        public bool TryGet(Type type, out TypeCheatConfiguration typeCheatConfiguration)
        {
            foreach (var current in typeCheatConfigurations)
            {
                if (current.AcceptsTypePredicate(type))
                {
                    typeCheatConfiguration = current;
                    return true;
                }
            }

            typeCheatConfiguration = default;
            return false;
        }
    }
}
