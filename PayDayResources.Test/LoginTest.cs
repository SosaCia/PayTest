using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PayDayResources.Test
{
    public class LoginTest : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync("http://10.10.24.122:8888/Login");
        }

        [Test]
        public async Task Login()
        {
            //Rellenar credenciales
            await Page.FillAsync("#LoginInput_Email", "jsieiro@payday.com.pa");
            await Page.FillAsync("#LoginInput_Password", "PayDay2021*");

            //Click en Iniciar Sesión (sin esperar nada más)
            var btnLogin = Page.Locator("button", new PageLocatorOptions { HasTextString = "Iniciar Sesión" });
            await btnLogin.ClickAsync();

            //Verificar la existencia de una opcion de la barra del menú que solo es visible cuando se está loggueado
            await Expect(Page.Locator("#configDropdown")).ToBeVisibleAsync();
        }
    }
}
