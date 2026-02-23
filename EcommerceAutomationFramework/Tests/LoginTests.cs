using EcommerceAutomationFramework.API;
using EcommerceAutomationFramework.Models;
using EcommerceAutomationFramework.Pages;
using EcommerceAutomationFramework.Utilities;
using NUnit.Framework.Interfaces;
using RestSharp;

namespace EcommerceAutomationFramework.Tests;

[TestFixture]
[NonParallelizable]
public class LoginTests : BaseTest
{
    [Test]
    [Retry(2)]
    public void User_Login_Should_Succeed()
    {
        var credentials = ConfigReader.GetSection<CredentialData>("Credentials");
        var loginPage = new LoginPage(Driver);

        loginPage.Open(BaseUrl);
        loginPage.Login(credentials.Email, credentials.Password);

        if (!loginPage.IsLoggedIn())
        {
            loginPage.Open(BaseUrl);
            loginPage.Login(credentials.Email, credentials.Password);
        }

        Assert.That(loginPage.IsLoggedIn(), Is.True, $"Login failed: {loginPage.GetLoginErrorMessage()} | Url: {Driver.Url}");
    }

    [Test]
    [Retry(1)]
    public async Task Login_UI_Then_Validate_User_Via_API()
    {
        var credentials = ConfigReader.GetSection<CredentialData>("Credentials");
        var config = ConfigReader.GetFrameworkConfig();

        var loginPage = new LoginPage(Driver);
        loginPage.Open(BaseUrl);
        loginPage.Login(credentials.Email, credentials.Password);

        if (!loginPage.IsLoggedIn())
        {
            loginPage.Open(BaseUrl);
            loginPage.Login(credentials.Email, credentials.Password);
        }

        Assert.That(
            loginPage.IsLoggedIn(),
            Is.True,
            $"UI login failed: {loginPage.GetLoginErrorMessage()} | Url: {Driver.Url}");
        var userLabel = loginPage.GetLoggedInUserText();

        var userApi = new UserApi(config.Api.BaseUrl);
        RestResponse verifyLoginResponse = await userApi.VerifyLoginAsync(credentials.Email, credentials.Password);
        var userDetails = await userApi.GetUserByEmailAsync(credentials.Email);

        Assert.Multiple(() =>
        {
            Assert.That(verifyLoginResponse.IsSuccessful, Is.True, "Verify login API call failed.");
            Assert.That(userDetails, Is.Not.Null, "User details API response is empty.");
            Assert.That(userDetails!.email, Is.EqualTo(credentials.Email), "Email mismatch between UI and API.");
            Assert.That(userLabel.ToLowerInvariant(), Does.Contain((userDetails.name ?? string.Empty).ToLowerInvariant()).Or.Contain("logged in as"), "User data mismatch.");
        });
    }
}
