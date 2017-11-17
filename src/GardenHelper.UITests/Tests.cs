using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GardenHelper.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        [Ignore]
        public void Repl()
        {
            app.Repl();
            //app.Screenshot("First screen.");
        }

        [Test]
        public void TestRose()
        {
            // Verify we are on the main screen.
            app.WaitForElement("CameraButton");

            app.Screenshot("Take a Picture");

            // Pull a picture of a rose.
            app.Invoke("UITestSelectPicture", "rose");

            // Verify we are now on the ROSE description.
            app.WaitForElement("I think it\'s a Rose");

            app.Screenshot("Showing a Rose");

            // Go back.
            app.Back();

            // Should be back on main screen.
            app.WaitForElement("CameraButton");

            app.Screenshot("Back to Main Page");
        }
    }
}

