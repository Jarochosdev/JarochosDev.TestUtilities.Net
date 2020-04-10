using System;
using System.Reflection;

namespace JarochosDev.TestUtilities.Net.NetStandard
{
    public class Helper
    {
        public object RunProtectedMethod(object objInstance, string methodName, params object[] parameters)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            return RunMethod(objInstance.GetType(), methodName, objInstance, parameters, eFlags);
        }

        private object RunMethod(Type instanceType, string methodName, object objInstance, object[] parameters, BindingFlags flags)
        {
            try
            {
                MethodInfo methodInfo = instanceType.GetMethod(methodName, flags);
                if (methodInfo == null)
                {
                    throw new ArgumentException("There is no method '" + methodName + "' for type '" + instanceType.ToString() + "'.");
                }

                if (methodInfo.GetParameters().Length != 0 && parameters.Length != 0 && methodInfo.GetParameters().Length != parameters.Length)
                {
                    throw new ArgumentException("Parameter mist match for '" + methodName + "' for type '" + instanceType.ToString() + "'.");
                }

                if (methodInfo.GetParameters().Length != 0 && parameters.Length == 0)
                {
                    parameters = new object[methodInfo.GetParameters().Length];
                    for (int i = 0; i < methodInfo.GetParameters().Length; i++)
                    {
                        parameters[i] = null;
                    }
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