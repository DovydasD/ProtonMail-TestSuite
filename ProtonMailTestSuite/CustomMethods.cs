using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;

namespace ProtonMail_FolderLabel
{
    static class CustomMethods
    {
        public static bool isElementPresent(this IWebDriver driver, By by) //Checks if an element is present in the website without throwing an exception, for if statements
        {
            int timeout = 10000;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));

            wait.IgnoreExceptionTypes(new Type[] { typeof(WebDriverException) });

            wait.Until(drvr => {
                try
                {
                    drvr.FindElement(by);
                    return true;
                }
                catch (NotFoundException)
                {
                    return false;
                }
                catch (System.TimeoutException)
                {
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            return true;
        }

        public static void EnterText(IWebDriver driver, string element, string value, string elementType) // Custom method for entering text into input fields
        {
            switch (elementType)
            {
                case "Id":
                    driver.FindElement(By.Id(element)).SendKeys(value);
                    break;
                case "Name":
                    driver.FindElement(By.Name(element)).SendKeys(value);
                    break;
                default:
                    driver.FindElement(By.Id(element)).SendKeys(value);
                    break;
            }
        }

        public static void Click(IWebDriver driver, string element, string elementType) // Custom method for clicking on elements
        {
            switch (elementType)
            {
                case "Id":
                    driver.FindElement(By.Id(element)).Click();
                    break;
                case "Name":
                    driver.FindElement(By.Name(element)).Click();
                    break;
                case "CSS":
                    driver.FindElement(By.CssSelector(element)).Click();
                    break;
                case "XPath":
                    driver.FindElement(By.XPath(element)).Click();
                    break;
                case "ClassName":
                    driver.FindElement(By.ClassName(element)).Click();
                    break;
                default:
                    driver.FindElement(By.Id(element)).Click();
                    break;
            }
        }

        public static void SelectDropDown(IWebDriver driver, string element, string value, string elementType) // Custom method for selecting dropdows and their populated elements
        {
            switch (elementType)
            {
                case "Id":
                    driver.FindElement(By.Id(element)).SendKeys(value);
                    break;
                case "Name":
                    driver.FindElement(By.Name(element)).SendKeys(value);
                    break;
                default:
                    driver.FindElement(By.Id(element)).SendKeys(value);
                    break;
            }
        }
    }
}
