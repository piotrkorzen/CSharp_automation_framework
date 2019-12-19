using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System;

namespace Se_csharp
{

    public class BasePage
    {

        public IWebDriver _driver = new ChromeDriver("C:\\Users\\RGHT46\\source\\repos\\Se_csharp\\Se_csharp\\");

        private static By Locator(string value)
        {
            if (value.StartsWith("//") || value.StartsWith("/"))
            {
                return By.XPath(value);
            }
            else if (value.StartsWith("#"))
            {
                return By.CssSelector(value);
            }
            else
            {
                return By.Id(value);
            }
        }

        public void GoToIndex(string url)
        {
            _driver.Manage().Window.Maximize();
            _driver.Url = url;

        }

        public void Quit()
        {
            _driver.Quit();
        }

        public void Click(string value, int seconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

            try
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(Locator(value)));
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Locator(value)));
                element.Click();
            }
        }

        public void Set(string value, string sendValue = null, int seconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

            if (value.Contains("input") || value.Contains("textarea"))
            {
                if (sendValue != null && sendValue != "uncheck")
                {
                    IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Locator(value)));
                    element.Clear();
                    element.SendKeys(sendValue);
                }
                else
                {
                    IWebElement checkbox = _driver.FindElement(Locator(value));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

                    try
                    {
                        if (!checkbox.Selected)
                        {
                            checkbox.Click();
                        }
                        else if (checkbox.Selected && sendValue == "uncheck")
                        {
                            checkbox.Click();
                        }
                    }
                    catch (ElementNotVisibleException)
                    {
                        js.ExecuteScript("arguments[0].click();", checkbox);
                    }
                    catch (InvalidElementStateException)
                    {
                        js.ExecuteScript("arguments[0].click();", checkbox);
                    }
                }
            }
            if (value.Contains("select"))
            {
                IWebElement selectList = _driver.FindElement(Locator(value));
                SelectElement select = new SelectElement(selectList);

                try
                {
                    select.SelectByText(sendValue);
                }
                catch (NoSuchElementException)
                {
                    try
                    {
                        select.SelectByValue(sendValue);

                    }
                    catch (NoSuchElementException)
                    {
                        select.SelectByIndex(Convert.ToInt32(sendValue));
                    }
                }
            }
        }

        public bool VerifyElementPresent(string value, int seconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Locator(value)));
            return element.Displayed;
        }

        public bool VerifyElementNotPresent(string value, int seconds = 20)
        {
            //IWebElement condition = SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(Locator(value));
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(Locator(value)));
            if (element)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteScript(string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(script);
        }

        public void SwitchToWindow(int window)
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[window]);
        }
    }
}
