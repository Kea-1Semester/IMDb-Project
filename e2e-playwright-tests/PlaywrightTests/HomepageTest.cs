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
public class HomePage : PageTest
{
    /*Run Test with Browser:

    dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Headless=false Playwright.LaunchOptions.Channel=msedge
    */

    /*For easy tests writing run:

        powershell bin/Debug/net9.0/playwright.ps1 codegen
        or
        pwsh bin/Debug/net9.0/playwright.ps1 codegen
    */

    /*Test Template:

        [Test]
        public async Task TestTemplate()
        {
            await Page.GotoAsync("/");

        }
    */

    [SetUp]
    public async Task SetupTests()
    {
        var host = Environment.GetEnvironmentVariable("FRONTEND_HOST") ?? "http://localhost:3000";
        await Page.GotoAsync(host);
    }


    [Test]
    public async Task TestTemplate()
    {
         // check if the page exists
        await Expect(Page).ToHaveTitleAsync(new Regex("imdb-frontend"));
    }

    [Test]
    public async Task Has_Home_Button()
    {
        //await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Home" })).ToBeVisibleAsync();

        // get type button and has text Home
        var homeButton = Page.Locator("button", new PageLocatorOptions { HasTextString = "Home" });
        await Expect(homeButton).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_Titles_Has_Attributes()
    {  
        var card = Page.Locator("div.chakra-card__root").First;

        await Expect(card).ToBeVisibleAsync();
        await Expect(card.Locator("h2")).ToHaveCountAsync(1);
        await Expect(card.Locator("p")).ToHaveCountAsync(2);
    }
    
    [Test]
    public async Task Page_Navigate_To_Page()
    {

        var Page1Button = Page.GetByRole(AriaRole.Button, new() { Name = "page 1" });
        var Page2Button = Page.GetByRole(AriaRole.Button, new() { Name = "page 2" });
          
        await Expect(Page1Button).ToBeVisibleAsync();
        await Expect(Page2Button).ToBeVisibleAsync();

        // ----- Page 1 -----
        await Page1Button.ClickAsync();

        var firstCardPage1 = Page.Locator("div.chakra-card__root").First;

        var page1CardText = (await firstCardPage1.InnerTextAsync()).Trim();

        Assert.That(page1CardText, Is.Not.Empty, "First card on page 1 is empty");

        // ----- Page 2 -----
        await Page2Button.ClickAsync(); 

        var firstCardPage2 = Page.Locator("div.chakra-card__root").First;
        var page2CardText = (await firstCardPage2.InnerTextAsync()).Trim();

         Assert.That(page2CardText, Is.Not.Empty, "First card on page 2 is empty");

        // ----- Assert -----
        Assert.That(page1CardText, Is.Not.EqualTo(page2CardText), "First card on page 1 and page 2 should be diffrent");
    }
}