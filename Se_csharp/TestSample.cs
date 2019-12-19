using NUnit.Framework;


namespace Se_csharp
{
    class Guru99Demo
    {
        BasePage _driver = new BasePage();

        [SetUp]
        public void StartBrowser()
        {
            _driver.GoToIndex("http://automationpractice.com/index.php?id_category=3&controller=category");
        }

        [Test]
        public void Test()
        {
            _driver.ExecuteScript("window.open();");
            _driver.SwitchToWindow(1);
            _driver.GoToIndex("https://onet.pl");
        }

        [TearDown]
        public void closeBrowser()
        {
            _driver.Quit();
        }

    }
}