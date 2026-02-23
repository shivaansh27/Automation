using EcommerceAutomationFramework.Models;
using Microsoft.Extensions.Configuration;

namespace EcommerceAutomationFramework.Utilities;

public static class ConfigReader
{
    private static readonly Lazy<IConfigurationRoot> ConfigurationRoot = new(() =>
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("Config/testdata.json", optional: false, reloadOnChange: false)
            .Build());

    private static readonly Lazy<FrameworkConfig> FrameworkConfig = new(() =>
        ConfigurationRoot.Value.Get<FrameworkConfig>() ?? new FrameworkConfig());

    public static FrameworkConfig GetFrameworkConfig() => FrameworkConfig.Value;

    public static string GetBaseUrl()
    {
        var config = GetFrameworkConfig();
        return config.Environments.TryGetValue(config.Environment, out var env)
            ? env.BaseUrl
            : string.Empty;
    }

    public static T GetSection<T>(string sectionName) where T : new()
    {
        var section = ConfigurationRoot.Value.GetSection(sectionName);
        return section.Exists() ? section.Get<T>() ?? new T() : new T();
    }
}
