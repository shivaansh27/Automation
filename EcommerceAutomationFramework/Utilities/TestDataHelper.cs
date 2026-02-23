namespace EcommerceAutomationFramework.Utilities;

public static class TestDataHelper
{
    public static string GetUniqueEmail(string emailTemplate)
    {
        if (string.IsNullOrWhiteSpace(emailTemplate))
        {
            throw new ArgumentException("Email template cannot be null or empty.", nameof(emailTemplate));
        }

        var token = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

        if (emailTemplate.Contains("${random}", StringComparison.OrdinalIgnoreCase))
        {
            return emailTemplate.Replace("${random}", token, StringComparison.OrdinalIgnoreCase);
        }

        var atIndex = emailTemplate.IndexOf('@');
        if (atIndex <= 0)
        {
            return $"{emailTemplate}_{token}";
        }

        var localPart = emailTemplate[..atIndex];
        var domainPart = emailTemplate[atIndex..];
        return $"{localPart}_{token}{domainPart}";
    }
}
