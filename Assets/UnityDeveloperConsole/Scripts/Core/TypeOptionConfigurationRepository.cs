using System;
using System.Collections.Generic;

namespace UnityDeveloperConsole.Core
{
    public class TypeOptionConfigurationRepository
    {
        private readonly IReadOnlyList<TypeOptionConfiguration> typeOptionConfigurations;

        public TypeOptionConfigurationRepository(IReadOnlyList<TypeOptionConfiguration> typeOptionConfigurations)
        {
            this.typeOptionConfigurations = typeOptionConfigurations;
        }

        public bool TryGet(Type type, out TypeOptionConfiguration typeOptionConfiguration)
        {
            foreach (var current in typeOptionConfigurations)
            {
                if (current.AcceptsTypePredicate(type))
                {
                    typeOptionConfiguration = current;
                    return true;
                }
            }

            typeOptionConfiguration = default;
            return false;
        }
    }
}
