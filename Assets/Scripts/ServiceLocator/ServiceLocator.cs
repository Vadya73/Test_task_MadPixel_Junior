using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> services = new();

    public static void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (!services.TryAdd(type, service))
        {
            throw new Exception($"Сервис {type.Name} уже зарегистрирован!");
        }
    }

    public static T GetService<T>()
    {
        var type = typeof(T);
        if (services.TryGetValue(type, out var service))
        {
            return (T)service;
        }
        throw new Exception($"Сервис {type.Name} не зарегистрирован!");
    }

    public static void UnregisterService<T>()
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            services.Remove(type);
        }
    }
}