using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;
using DotNetEnv;

namespace E2E.Playwright.Tests;

[NonParallelizable]
[TestFixture]
[Category("E2ETest")]
public class AuthorizationPages : PageTest
{
    [SetUp]
    public async Task SetupTests()
    {
        await Page.GotoAsync("/");
    }

    [Test]
    public async Task Test_Login()
    {
        var loginButton = Page.Locator("button", new PageLocatorOptions { HasTextString = "Log In " });
        await Expect(loginButton).ToBeVisibleAsync();

        // click login button
        await loginButton.ClickAsync();

        // check login page
        await Expect(Page).ToHaveURLAsync(new Regex(".*login"));
        await Expect(Page).ToHaveTitleAsync(new Regex("Log in \\| imdb-app"));

        //---------------

        var mailInput = Environment.GetEnvironmentVariable("TEST_USER_EMAIL") ?? "";
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).FillAsync(mailInput);

        await Page.GetByText("Password *").ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("TestPassword123!");


        var submitButton = Page.Locator("button.c04b3dedf.c837aaff8.cea427a68.cfb672822.cdda8fe19", new PageLocatorOptions { HasTextString = "Continue" });

        await Expect(submitButton).ToBeVisibleAsync();
        await submitButton.ClickAsync();

        await Page.WaitForTimeoutAsync(5000);

        //---------------

        await Expect(Page).ToHaveTitleAsync(new Regex("imdb-frontend"));

        var logoutButton = Page.GetByRole(AriaRole.Button, new() { Name = "Log Out" });
        await Expect(logoutButton).ToBeVisibleAsync();
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        Env.TraversePath().Load();
        return new BrowserNewContextOptions
        {
            ColorScheme = ColorScheme.Dark,
            ViewportSize = new() { Width = 1280, Height = 720 },
            BaseURL = Environment.GetEnvironmentVariable("FRONTEND_HOST") ?? "http://localhost:3000",
        };
    }
}