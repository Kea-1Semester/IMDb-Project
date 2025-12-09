using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace E2E.Playwright.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[Category("E2ETest")]
public class E2eTest : PageTest
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

    [Test]
    public async Task TestHomePage()
    {
        var host = Environment.GetEnvironmentVariable("FRONTEND_HOST") ?? "http://localhost:3000";
        await Page.GotoAsync(host);
        await Expect(Page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Welcome to Sample0");
    }
}
