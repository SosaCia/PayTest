using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDayResources.Test.Pages
{
    internal class LoginPage
    {
        private IPage _page;
        private readonly ILocator _txtUserName;
        private readonly ILocator _txtPassword;
        private readonly ILocator _btnLogin;
        private readonly ILocator _configDropdown;

        public LoginPage(IPage page, ILocator txtUserName, ILocator txtPassword, ILocator btnLogin, ILocator configDropdown)
        {
            _page = page;
            _txtUserName = _page.Locator("#LoginInput_Email");
            _txtPassword = _page.Locator("#LoginInput_Password");
            _btnLogin = _page.Locator("text=Iniciar Sesión");
            _configDropdown = _page.Locator("#configDropdown");
        }


        public async Task Login(string userName, string password)
        {
            await _txtUserName.FillAsync(userName);
            await _txtPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
        }




    }
}
