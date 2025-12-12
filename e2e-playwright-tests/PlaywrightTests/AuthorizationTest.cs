using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace E2E.Playwright.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[Category("E2ETest")]
public class AuthorizationPages : PageTest
{
    // [Setup]
    // public async Task Setup()
    // {
    //     var host = Environment.GetEnvironmentVariable("FRONTEND_HOST") ?? "http://localhost:3000";
    //     await Page.GotoAsync(host);
    // }

    // [Test]
    // public async Task Has_Home_Button()
    // {   
    //     var host = Environment.GetEnvironmentVariable("FRONTEND_HOST") ?? "http://localhost:3000";
    //     await Page.GotoAsync(host);
    //     await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Home" })).ToBeVisibleAsync();
    // }
}