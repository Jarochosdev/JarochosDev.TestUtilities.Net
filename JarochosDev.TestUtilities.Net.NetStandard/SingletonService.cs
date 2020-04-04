using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;

namespace JarochosDev.TestUtilities.Net.NetStandard
{
    internal class SingletonService : ISingletonService
    {
        Dictionary<Type, Mock> _singletonServices = new Dictionary<Type, Mock>();
        public void CreateMockSingleton<TSingleton, TInstance>(string instanceName = "_instance") where TSingleton : TInstance
        {
            if (_singletonServices.ContainsKey(typeof(TSingleton)))
            {
                throw new Exception("The mock singleton service was already created.");
            }

            FieldInfo fieldInfoInstance = typeof(TSingleton).GetField(instanceName, BindingFlags.Static | BindingFlags.NonPublic);

            var constructor = typeof(TSingleton).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);

            var objects = new object[] { };

            if (constructor != null)
            {
                var parameterInfos = constructor.GetParameters();
                objects = new object[parameterInfos.Length];
                for (int x = 0; x < parameterInfos.Length; x++)
                {
                    objects[x] = null;
                }
            }

            Mock mock = (Mock)Activator.CreateInstance(typeof(Mock<>).MakeGenericType(typeof(TInstance)), objects);
            fieldInfoInstance.SetValue(null, mock.Object);
            _singletonServices.Add(typeof(TSingleton), mock);
        }

        public Mock GetMockSingleton<TSingleton>()
        {
            if (!_singletonServices.ContainsKey(typeof(TSingleton)))
            {
                throw new Exception("The mock singleton service has not been created.");
            }

            return _singletonServices[typeof(TSingleton)];
        }

        public void ClearMockSingleton<TSingleton, TInstance>(string instanceName = "_instance")
        {
            FieldInfo fieldInfoInstance = typeof(TSingleton).GetField(instanceName, BindingFlags.Static | BindingFlags.NonPublic);
            Mock mock = (Mock)Activator.CreateInstance(typeof(Mock<>).MakeGenericType(typeof(TInstance)), null);
            fieldInfoInstance.SetValue(null, null);
        }
    }

    public interface ISingletonService
    {
        void CreateMockSingleton<TSingleton, TInstance>(string instanceName = "_instance") where TSingleton : TInstance;
        Mock GetMockSingleton<TSingleton>();
        void ClearMockSingleton<TSingleton, TInstance>(string instanceName = "_instance");
    }
}