using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestProject_Nunit
{
    public class Test01_ProductList
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Replace the path with the actual location of your ChromeDriver
            driver = new ChromeDriver(@"C:\path\to\chromedriver.exe");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("http://localhost:5000/products"); // Replace with your application url
        }

        [Test]
        public void Test_AddProductButton_Click()
        {
            // Find the Add Product button
            var addProductButton = driver.FindElement(By.CssSelector("button.btn.btn-primary"));

            // Click the button
            addProductButton.Click();

            // Verify that the navigation is successful (check for expected url or element)
            // Here, we are assuming the Add Product page has a specific title
            var title = driver.Title;
            Assert.IsTrue(title.Contains("Add Product"), "Navigation to Add Product page failed");

            // You can add additional assertions here to verify other aspects of the Add Product page

            // Cleanup (optional)
            driver.Quit();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
