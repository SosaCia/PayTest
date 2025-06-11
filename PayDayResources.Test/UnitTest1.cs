using Microsoft.Playwright;
using System.Net.Http.Headers;

namespace PayDayResources.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            //Playwright
            using var playwright = await Playwright.CreateAsync();
            //Browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 500,    // retrasa cada acción 250 ms
            });

            //context de grabación de video
            // Construye la ruta bin\Debug\net9.0\Videos\TestRecordings
            var recordingsDir = AppDomain.CurrentDomain.BaseDirectory;
            var recordingsFolder = Path.Combine(recordingsDir, "Media", "TestRecordings");
            Directory.CreateDirectory(recordingsFolder);

            // Crea un contexto que graba vídeo en esa carpeta
            var recording = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                RecordVideoDir = recordingsFolder,
                RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 }
            });

            //Page
            var page = await recording.NewPageAsync();

            //1. ir a la siguiente URL
            await page.GotoAsync("http://10.10.24.122:8888/Login");

            //2. Rellenar credenciales
            await page.FillAsync("#LoginInput_Email", "jsieiro@payday.com.pa");
            await page.FillAsync("#LoginInput_Password", "PayDay2021*");

            //3. Click en Iniciar Sesión (sin esperar nada más)
            var btnLogin = page.Locator("text=Iniciar Sesión");
            await btnLogin.ClickAsync();

            //4. Espero a que no queden fetches pendientes (o que la nueva vista haya terminado de renderizar)
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            //5. Chequeo inmediato de existencia: QuerySelectorAsync devuelve null si no lo encuentra.
            var configDropdown = page.Locator("#configDropdown");
            bool exists = await configDropdown.IsVisibleAsync();
            Assert.That(exists, Is.True, "El menú de configuración debería estar visible");

            //6. Tomar captura
            var screenshots = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media", "Screenshots");
            Directory.CreateDirectory(screenshots);
            var screenshotPath = Path.Combine(screenshots, "LogInTest.jpg");
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath,
                FullPage = true
            });

            await recording.CloseAsync();

            var originalPath = await page.Video.PathAsync();

            var renamedPath = Path.Combine(recordingsFolder, "LogInTest.webm");

            if (File.Exists(renamedPath))
                File.Delete(renamedPath);

            File.Move(originalPath, renamedPath);

        }
    }
}
