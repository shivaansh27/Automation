namespace EcommerceAutomationFramework.Models;

public class FrameworkConfig
{
    public string Environment { get; set; } = "dev";
    public int TimeoutSeconds { get; set; } = 20;
    public string Browser { get; set; } = "chrome";
    public ApiConfig Api { get; set; } = new();
    public Dictionary<string, EnvironmentConfig> Environments { get; set; } = new();
}

public class ApiConfig
{
    public string BaseUrl { get; set; } = string.Empty;
}

public class EnvironmentConfig
{
    public string BaseUrl { get; set; } = string.Empty;
}

public class CredentialData
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class SearchData
{
    public string Keyword { get; set; } = string.Empty;
}
