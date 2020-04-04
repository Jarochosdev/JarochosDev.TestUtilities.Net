using System;
using System.Reflection;

namespace JarochosDev.TestUtilities.Net.NetStandard
{
    public class Helper
    {
        public object RunProtectedMethod<T>(object objInstance, string methodName, params object[] parameters)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            return RunMethod(typeof(T), methodName, objInstance, parameters, eFlags);
        }

        private object RunMethod(Type instanceType, string methodName, object objInstance, object[] parameters, BindingFlags flags)
        {
            try
            {
                MethodInfo methodInfo = instanceType.GetMethod(methodName, flags);
                if (methodInfo == null)
                {
                    throw new ArgumentException("There is no method '" +  methodName + "' for type '" + instanceType.ToString() + "'.");
                }

                object result = methodInfo.Invoke(objInstance, parameters);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}