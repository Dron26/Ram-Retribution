using System;
using System.Collections.Generic;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class ModulesContainer
    {
        private readonly Dictionary<Type, object> _modules = new Dictionary<Type, object>();

        public void Register<T>(T module)
        {
            Type type = typeof(T);

            if (!_modules.TryAdd(type, module))
            {
                throw new ArgumentException($"The requested module {type} is already registered." +
                                            $"Multiple registrations are not supported");
            }
        }

        public T Get<T>()
        {
            Type type = typeof(T);

            if (_modules.TryGetValue(type, out var module))
                return (T)module;

            throw new ArgumentException($"The requested module {type} was not found." +
                                        $"Make sure that the data type you are trying to retrieve " +
                                        $"has been registered before");
        }
    }
}