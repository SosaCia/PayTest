using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PayDayResources.Test.Pages.Login
{
    public class LoginTest : PlaywrightTestBase
    {
        private LoginPage _login;

        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync("http://10.10.24.122:8888/Login");
            _login = new LoginPage(Page);
        }

        [Test]
        public async Task Login()
        {

            await _login.Login("jsieiro@payday.com.pa", "PayDay2021*");
            Assert.That(await _login.IsLoggedIn(), Is.True,
                        "El menú de configuración debería estar visible");
        }

        [Test]
        public async Task LoginFailed()
        {
            await _login.Login("jsieiro@payday.com.pa", "PayDay2023*");
            var error = await _login.GetErrorMessage();
            Assert.That(error, Does.Contain("Error al iniciar sesión"),
                        "Debería mostrar 'Error al iniciar sesión'");
        }

        [Test]
        public async Task LoginSuccessObjectIdNotFound()
        {
            await _login.Login("jsieiro@payday.com.pa", "PayDay2021*");
            Assert.That(await Page.Locator("#configDropdown1").IsVisibleAsync(),
                        Is.False, "El menú de configuración NO debería estar visible");
        }



    }
}
