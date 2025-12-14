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
public class TitleDetail : PageTest
{
    [SetUp]
    public async Task SetupTests()
    {
        await Page.GotoAsync("/");
        var card = Page.Locator("div.chakra-card__root").First;
        await card.ClickAsync();
    }

    [Test]
    public async Task Title_Details_Has_HomeButton()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Home" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task Title_Details_Has_TestTitle()
    {
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Title Details" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task Title_Details_Has_BackButton()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Back" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task Title_Details()
    {
        // Page Fields
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("PrimaryTitle:");
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("OriginalTitle:");
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("IsAdult:");
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("StartYear:");
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("EndYear:");
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("RuntimeMinutes:");
        await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("Genres:");
    }

    [Test]
    public async Task TestTitle_Is_String()
    {
        await Expect(Page.GetByRole(AriaRole.Main)).ToHaveTextAsync(new Regex(@".*PrimaryTitle:\s*.+.*"));
    }

    [Test]
    public async Task TestIsAdult_Is_Bool_YesNo()
    {
        await Expect(Page.GetByRole(AriaRole.Main)).ToHaveTextAsync(new Regex(@".*IsAdult:\s*(Yes|No)(?=[StartYear]).*"));
    }

    [Test]
    public async Task TestStartYear_Is_4Digits()
    {
        await Expect(Page.GetByRole(AriaRole.Main)).ToHaveTextAsync(new Regex(@".*StartYear:\s*\d{4}.*"));
    }

    [Test]
    public async Task TestEndYear_Is_4Digits_Or_Null()
    {
        await Expect(Page.GetByRole(AriaRole.Main)).ToHaveTextAsync(new Regex(@".*EndYear:\s*(\d{4}|-).*"));
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