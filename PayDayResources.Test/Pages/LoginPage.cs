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
        public LoginPage(IPage page) => _page = page;
        private ILocator _lnkLogin => _page.Locator("text=Login");
        private ILocator _txtUserName => _page.Locator("#LoginInput_Email");
        private ILocator _txtPassword => _page.Locator("#LoginInput_Password");
        private ILocator _btnLogin => _page.Locator("text=Iniciar Sesión");
        private ILocator _configDropdown => _page.Locator("#configDropdown");


        public async Task ClickLogin()
        {
            await _page.RunAndWaitForNavigationAsync(async () =>
            {
                await _lnkLogin.ClickAsync();
            }, new PageRunAndWaitForNavigationOptions
            { 
                UrlString = "**/Login"
            });
        }


        public async Task Login(string userName, string password)
        {
            await _txtUserName.FillAsync(userName);
            await _txtPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
        }

        public async Task<bool> IsEmployeeDetailsExists() => await _configDropdown.IsVisibleAsync();


    }
}
 