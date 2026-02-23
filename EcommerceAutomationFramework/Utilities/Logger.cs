namespace EcommerceAutomationFramework.Utilities;

public static class Logger
{
    private static readonly object LockObject = new();

    public static void Info(string message) => Write("INFO", message);

    public static void Error(string message) => Write("ERROR", message);

    private static void Write(string level, string message)
    {
        var line = $"{DateTime.UtcNow:O} [{level}] {message}";

        lock (LockObject)
        {
            Directory.CreateDirectory("Logs");
            File.AppendAllText(Path.Combine("Logs", $"TestLog_{DateTime.UtcNow:yyyyMMdd}.log"), line + Environment.NewLine);
        }
    }
}
