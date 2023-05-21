using Listener.Congifuration;
using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static TConfig GetConfigSection<TConfig>(this IConfiguration configuration) where TConfig : IConfigSection, new()
    {
        var instance = new TConfig();
        var typeName = typeof(TConfig).Name;
        configuration.GetSection(typeName).Bind(instance);

        return instance;
    }
}
