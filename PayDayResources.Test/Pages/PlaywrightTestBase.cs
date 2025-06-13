using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PayDayResources.Test
{
    public abstract class PlaywrightTestBase
    {
        // Renombrado para no chocar con el tipo estático
        protected IPlaywright _pw;
        protected IBrowser Browser { get; private set; }
        protected IBrowserContext Context { get; private set; }
        protected IPage Page { get; private set; }

        // 1️ Se ejecuta sólo una vez antes de todos los tests
        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            _pw = await Playwright.CreateAsync();
            Browser = await _pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,  // ves la UI
                SlowMo = 1000     // retrasa 250 ms cada acción
            });
        }

        // 2️ Se ejecuta antes de CADA test
        [SetUp]
        public async Task TestSetup()
        {
            var recordings = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "Media", "TestRecordings"
            );
            Directory.CreateDirectory(recordings);

            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                RecordVideoDir = recordings,
                RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 }
            });

            Page = await Context.NewPageAsync();
        }

        // 3️ Se ejecuta justo después de CADA test
        [TearDown]
        public async Task TestTearDown()
        {
            // Cierra el context para que se genere el vídeo
            await Context.CloseAsync();

            // Renombra el vídeo con el nombre del test
            var src = await Page.Video.PathAsync();
            var dest = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "Media", "TestRecordings",
                $"{TestContext.CurrentContext.Test.Name}.webm"
            );

            if (File.Exists(dest))
                File.Delete(dest);

            File.Move(src, dest);
        }

        // 4️ Se ejecuta al final, después de todos los tests
        [OneTimeTearDown]
        public async Task GlobalTearDown()
        {
            await Browser.CloseAsync();
            _pw.Dispose();
        }
    }
}
