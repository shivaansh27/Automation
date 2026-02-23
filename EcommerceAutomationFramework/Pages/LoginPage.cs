using OpenQA.Selenium;

namespace EcommerceAutomationFramework.Pages;

public class LoginPage : BasePage
{
    private static readonly By LoginEmail = By.CssSelector("input[data-qa='login-email']");
    private static readonly By LoginPassword = By.CssSelector("input[data-qa='login-password']");
    private static readonly By LoginButton = By.CssSelector("button[data-qa='login-button']");
    private static readonly By LoggedInUser = By.XPath("//a[contains(normalize-space(),'Logged in as')]");
    private static readonly By LogoutLink = By.CssSelector("a[href='/logout']");
    private static readonly By LoginErrorMessage = By.XPath("//form[@action='/login']//p");

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public void Open(string baseUrl)
    {
        Driver.Navigate().GoToUrl($"{baseUrl}/login");
    }

    public void Login(string email, string password)
    {
        Type(LoginEmail, email);
        Type(LoginPassword, password);
        var button = Wait.WaitForClickable(LoginButton);
        button.Click();
        WaitForLoginResult();

        if (!IsLoggedIn() && string.IsNullOrWhiteSpace(GetLoginErrorMessage()) && Driver.Url.Contains("/login", StringComparison.OrdinalIgnoreCase))
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", button);
            WaitForLoginResult();
        }

        if (!IsLoggedIn() && string.IsNullOrWhiteSpace(GetLoginErrorMessage()) && Driver.Url.Contains("/login", StringComparison.OrdinalIgnoreCase))
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript(
                "const e=document.querySelector(\"input[data-qa='login-email']\");" +
                "const p=document.querySelector(\"input[data-qa='login-password']\");" +
                "const b=document.querySelector(\"button[data-qa='login-button']\");" +
                "if(e&&p&&b){e.value=arguments[0];p.value=arguments[1];e.dispatchEvent(new Event('input',{bubbles:true}));p.dispatchEvent(new Event('input',{bubbles:true}));b.click();}",
                email,
                password);
            WaitForLoginResult();
        }
    }

    public string GetLoggedInUserText()
    {
        if (Driver.FindElements(LoggedInUser).Count > 0)
        {
            return Driver.FindElement(LoggedInUser).Text;
        }

        if (Driver.FindElements(LogoutLink).Count > 0)
        {
            return "Logged in";
        }

        var error = GetLoginErrorMessage();
        return string.IsNullOrWhiteSpace(error) ? string.Empty : error;
    }

    public bool IsLoggedIn()
    {
        return Driver.FindElements(LoggedInUser).Count > 0 || Driver.FindElements(LogoutLink).Count > 0;
    }

    public string GetLoginErrorMessage()
    {
        var elements = Driver.FindElements(LoginErrorMessage);
        if (elements.Count > 0)
        {
            return elements[0].Text;
        }

        var genericErrors = Driver.FindElements(By.XPath("//p[contains(translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'incorrect') or contains(translate(normalize-space(.), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'invalid')]"));
        return genericErrors.Count > 0 ? genericErrors[0].Text : string.Empty;
    }

    private void WaitForLoginResult()
    {
        var endTime = DateTime.UtcNow.AddSeconds(20);

        while (DateTime.UtcNow < endTime)
        {
            if (IsLoggedIn() || !string.IsNullOrWhiteSpace(GetLoginErrorMessage()))
            {
                return;
            }

            Thread.Sleep(250);
        }
    }
}
