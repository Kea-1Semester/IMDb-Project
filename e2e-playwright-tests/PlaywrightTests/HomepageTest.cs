using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

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
        var host = /*Environment.GetEnvironmentVariable("FRONTEND_HOST") ??*/ "http://localhost:3000";
        await Page.GotoAsync(host);
    }

    [Test]
    public async Task Has_Home_Button()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Home" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_Has_Login()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Log In" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_Has_Register()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Register" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task Page_Titles_Has_Attributes()
    {  
        var card = Page.Locator("div.chakra-card__root").First;

        await Expect(card).ToBeVisibleAsync();
        await Expect(card.Locator("h2")).ToHaveCountAsync(1);
        await Expect(card.Locator("p")).ToHaveCountAsync(2);
    }
    
    // [Test]
    // public async Task Page_Navigate_To_Page()
    // {
    //     await Page.GotoAsync("/");
        
    // }

    // [Test]
    // public async Task Page_Contains_Items_Or_Less()
    // {
        
    //     await Page.GotoAsync("/");
        
    // }
}
