using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace E2E.Playwright.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[Category("E2ETest")]
public class AuthorizationPages : PageTest
{
    [SetUp]
    public async Task SetupTests()
    {
        var host = /*Environment.GetEnvironmentVariable("FRONTEND_HOST") ??*/ "http://localhost:3000";
        await Page.GotoAsync(host);
    }

    [Test]
    public async Task Page_Tests()
    {
        // login button
        var loginButton = Page.Locator("button", new PageLocatorOptions { HasTextString = "Log In " });
        await Expect(loginButton).ToBeVisibleAsync();

        // click login button
        await loginButton.ClickAsync();

        // check login page
        await Expect(Page).ToHaveURLAsync(new Regex(".*login"));
        await Expect(Page).ToHaveTitleAsync(new Regex("Log in \\| imdb-app"));

        // Signup button with a tager
        var signUpButton = Page.Locator("a", new PageLocatorOptions { HasTextString = "Sign up" });
        await Expect(signUpButton).ToBeVisibleAsync();


        // // click signup button
        await signUpButton.ClickAsync();
        // check signup page
        await Expect(Page).ToHaveURLAsync(new Regex(".*signup"));
        await Expect(Page).ToHaveTitleAsync(new Regex("Sign up \\| imdb-app"));

        // insert email
        var mailInput = Environment.GetEnvironmentVariable("TEST_USER_EMAIL") ?? "";
        var emailInput = Page.Locator("input[name='email']");
        await emailInput.FillAsync(mailInput);
        // insert password
        var passwordInput = Page.Locator("input[name='password']");
        await passwordInput.FillAsync("TestPassword123!");

        // click submit button with class is c04b3dedf c837aaff8 cea427a68 cfb672822 c056bbc2e with text continue
        var submitButton = Page.Locator("button.c04b3dedf.c837aaff8.cea427a68.cfb672822.c056bbc2e", new PageLocatorOptions { HasTextString = "Continue" });
        await submitButton.ClickAsync();

        //await Page.WaitForTimeoutAsync(5000);

        // check redirect to home page
        await Expect(Page).ToHaveURLAsync(new Regex(".*/"));
        
        // logout button
        var logoutButton = Page.Locator("button", new PageLocatorOptions { HasTextString = "Log Out" });
        await Expect(logoutButton).ToBeVisibleAsync();
        // click logout button
        await logoutButton.ClickAsync();
        // check redirect to home page
        await Expect(Page).ToHaveURLAsync(new Regex(".*/"));
        // check login button is visible again
        await Expect(loginButton).ToBeVisibleAsync();
    }
}