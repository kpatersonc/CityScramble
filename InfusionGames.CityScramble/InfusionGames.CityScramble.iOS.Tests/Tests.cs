using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;

namespace InfusionGames.CityScramble.iOS.Tests
{
    [TestFixture]
    public class Tests
    {
        iOSApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp
                .iOS
                .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
    }
}

