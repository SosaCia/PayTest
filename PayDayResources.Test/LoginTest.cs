using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PayDayResources.Test
{
    public class LoginTest : PlaywrightTestBase
    {
        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync("http://10.10.24.122:8888/Login");
        }

        [Test]
        public async Task Login()
        {
            // 1. Navega
            //await Page.GotoAsync("http://10.10.24.122:8888/Login");

            await Page.FillAsync("#LoginInput_Email", "jsieiro@payday.com.pa");
            await Page.FillAsync("#LoginInput_Password", "PayDay2021*");

            var btnLogin = Page.Locator("button", new PageLocatorOptions { HasTextString = "Iniciar Sesión" });
            await btnLogin.ClickAsync();

            bool visible = await Page.Locator("#configDropdown").IsVisibleAsync();
            Assert.That(visible, Is.True, "El menú de configuración debería estar visible");
        }

        [Test]
        public async Task LoginFailed()
        {
            await Page.FillAsync("#LoginInput_Email", "jsieiro@payday.com.pa");
            await Page.FillAsync("#LoginInput_Password", "PayDay2023*");

            var btnLogin = Page.Locator("button", new PageLocatorOptions { HasTextString = "Iniciar Sesión" });
            await btnLogin.ClickAsync();

            var summary = Page.Locator("div.validation-summary-errors");
            await summary.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // 5a) Con NUnit puro, compruebas el texto:
            var text = await summary.InnerTextAsync();
            Assert.That(text, Does.Contain("Error al iniciar sesión"),
                        "Debería mostrar 'Error al iniciar sesión' en el resumen de validación.");
        }

        [Test]
        public async Task LoginSuccessObjectIdNotFound()
        {
            await Page.FillAsync("#LoginInput_Email", "jsieiro@payday.com.pa");
            await Page.FillAsync("#LoginInput_Password", "PayDay2021*");

            var btnLogin = Page.Locator("button", new PageLocatorOptions { HasTextString = "Iniciar Sesión" });
            await btnLogin.ClickAsync();

            bool visible = await Page.Locator("#configDropdown1").IsVisibleAsync();
            Assert.That(visible, Is.False, "El menú de configuración NO debería estar visible");
        }



    }
}
