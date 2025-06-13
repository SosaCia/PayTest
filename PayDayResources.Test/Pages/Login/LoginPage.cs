using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDayResources.Test.Pages.Login
{
    internal class LoginPage
    {
        private readonly IPage _page;
        public LoginPage(IPage page) => _page = page;

        private ILocator _txtUserName => _page.Locator("#LoginInput_Email");
        private ILocator _txtPassword => _page.Locator("#LoginInput_Password");
        private ILocator _btnLogin => _page.Locator("text=Iniciar Sesión");
        private ILocator _errorSummary => _page.Locator("div.validation-summary-errors li");
        private ILocator _configDropdown => _page.Locator("#configDropdown");

        /// <summary>
        /// Teclea el usuario y contraseña con un delay “humano” y hace click en Iniciar Sesión.
        /// </summary>
        public async Task Login(string user, string pass, int delayMs = 100)
        {
            await _txtUserName.TypeAsync(user, new LocatorTypeOptions { Delay = delayMs });
            await _txtPassword.TypeAsync(pass, new LocatorTypeOptions { Delay = delayMs });
            await _btnLogin.ClickAsync();
        }

        /// <summary>
        /// Devuelve true si ya aparece el dropdown de configuración.
        /// </summary>
        public Task<bool> IsLoggedIn() =>
            _configDropdown.IsVisibleAsync();

        /// <summary>
        /// Si el login falla, obtiene el texto del primer mensaje de error.
        /// </summary>
        public Task<string> GetErrorMessage() =>
            _errorSummary.InnerTextAsync();
    }
}

