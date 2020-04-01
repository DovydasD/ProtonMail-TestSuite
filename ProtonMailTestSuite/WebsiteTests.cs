using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace ProtonMail_FolderLabel
{
    class WebsiteTests
    {
        string title = "Test Date: " + DateTime.Now;

        IWebDriver driver = new ChromeDriver(); // Creating the Chrome driver

        static void Main(string[] args)
        {
        }

        [SetUp]
        public void Initialize()
        {
            driver.Navigate().GoToUrl("https://beta.protonmail.com/settings/labels"); // Navigating to the labels page of the settings
        }

        [Test]
        [Order(1)]
        public void Login() // Log-in Test
        {
            int timeout = 10000;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));

            if (CustomMethods.isElementPresent(driver, By.Id("login_btn"))) // Checkin if first time launched, if so, log-in
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
                CustomMethods.EnterText(driver, "username", "DovydasD@protonmail.com", "Id");
                CustomMethods.EnterText(driver, "password", "bX&3B8R%Gx7R", "Id");
                CustomMethods.Click(driver, "login_btn", "Id");
            }

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".pm-button"))); // Checks if a pop-up appears and closes it, ussualy after the first time launching
            if (driver.FindElement(By.CssSelector(".pm-button")).Displayed == true)
                CustomMethods.Click(driver, ".pm-button", "CSS");

            Assert.AreEqual("https://beta.protonmail.com/inbox", driver.Url); // Asserts if log-in was succesfull and the current page is the home page
        }

        [Test]
        [Order(2)]
        public void AddFolder() // Folder creation test
        {
            int timeout = 10000;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".pm-button--primary"))); // Checks if the "Add ..." button is visible and clicks it
            CustomMethods.Click(driver, ".pm-button--primary", "CSS");

            CustomMethods.EnterText(driver, "accountName", "Folder " + title, "Id"); // Enters a title for the Folder
            CustomMethods.Click(driver, "(//input[@name='paletteColor'])[1]", "XPath"); // Selects the first color for the Folder

            CustomMethods.Click(driver, ".pm-modalFooter > .pm-button--primary", "CSS"); // Creates the folder

            //Assert.AreEqual(title, driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div/div[2]/div/main/div/section/table/tbody/tr/td[2]/div")).GetAttribute("value"));
        }

        [Test]
        [Order(3)]
        public void AddLabel()
        {
            int timeout = 10000;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));

            //if (CustomMethods.isElementPresent(driver, By.XPath("/html/body/div[7]"))) // One bug causes an overlay to appear on the page which makes everything unclickable until website realod. Checks if the bug appeared and reloads the website
            //    driver.Navigate().Refresh();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".pm-button--primary"))); // Checks if the "Add ..." button is visible and clicks it
            CustomMethods.Click(driver, ".pm-button--primary:nth-child(2)", "CSS");

            CustomMethods.EnterText(driver, "accountName", "Label " + title, "Id"); // Enters a title for the Label
            CustomMethods.Click(driver, "(//input[@name='paletteColor'])[1]", "XPath"); // Selects the first color for the Label

            CustomMethods.Click(driver, ".pm-modalFooter > .pm-button--primary", "CSS"); // Creates the Label
        }

        [Test]
        [Order(4)]
        public void notificationCheck()
        {
            int timeout = 10000;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));

            //if (CustomMethods.isElementPresent(driver, By.XPath("/html/body/div[7]"))) // One bug causes an overlay to appear on the page which makes everything unclickable until website realod. Checks if the bug appeared and reloads the website
            //    driver.Navigate().Refresh();

            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("pm-toggle-label-text"))); // Checks if the Notification toggle button is visible and toggles it
            CustomMethods.Click(driver, "pm-toggle-label", "ClassName");

            Assert.AreEqual("true", driver.FindElement(By.ClassName("pm-toggle-checkbox")).GetAttribute("aria-busy")); // Asserts if the toggle has entered the changing state

            driver.Navigate().Refresh(); // Refreshes the website to ensure the toggle has finished entering its other state

            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("pm-toggle-label"))); // Checks if the Notification toggle button is visible
            Assert.AreEqual("false", driver.FindElement(By.ClassName("pm-toggle-checkbox")).GetAttribute("aria-busy")); // Asserts if the toggle state has returned thus ensuring that the toggle has actually toggled
        }

        [Test]
        [Order(5)]
        public void Edit()
        {
            int timeout = 10000;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("tr:nth-child(1) .pm-button:nth-child(1)"))); // Checks if the Edit button is visible
            CustomMethods.Click(driver, "tr:nth-child(1) .pm-button:nth-child(1)", "CSS"); // Clicks the Edit button

            CustomMethods.Click(driver, "accountName", "Id"); // Clicks on the title text field
            CustomMethods.EnterText(driver, "accountName", Keys.Control + "a", "Id"); // Deletes the existing text
            CustomMethods.EnterText(driver, "accountName", Keys.Delete, "Id");

            CustomMethods.EnterText(driver, "accountName", title + " Edited", "Id"); // Enter the edited title
            CustomMethods.Click(driver, "(//input[@name='paletteColor'])[19]", "XPath"); // Selects a different color

            CustomMethods.Click(driver, ".pm-modalFooter > .pm-button--primary", "CSS"); // Completes editing
        }

        [Test]
        [Order(6)]
        public void Delete()
        {
            int timeout = 10000;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));

            //if (CustomMethods.isElementPresent(driver, By.XPath("/html/body/div[7]"))) // One bug causes an overlay to appear on the page which makes everything unclickable until website realod. Checks if the bug appeared and reloads the website
            //    driver.Navigate().Refresh();

            //IWebElement entryOne = driver.FindElement(By.CssSelector("orderableTableHeader:nth-child(1)"));
            //IWebElement entryTwo = driver.FindElement(By.CssSelector("orderableTableHeader:nth-child(2)"));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div/div[2]/div/main/div/section/table/tbody/tr[1]/td[4]/div/button[2]"))); // Checks if the Dropdown next to the Edit buttons is visible, clicks on it and selects the "Delete" element
            CustomMethods.Click(driver, "/html/body/div[1]/div[2]/div/div/div[2]/div/main/div/section/table/tbody/tr[1]/td[4]/div/button[2]", "XPath");
            CustomMethods.Click(driver, "/html/body/div[8]/div/ul/li/button", "XPath");

            CustomMethods.Click(driver, ".pm-modalFooter > .pm-button--primary", "CSS"); // Confirms the pop-up

            //Assert.AreEqual(driver.FindElement(By.CssSelector("orderableTableHeader:nth-child(1)")), entryTwo);

            //if (CustomMethods.isElementPresent(driver, By.XPath("/html/body/div[7]"))) // One bug causes an overlay to appear on the page which makes everything unclickable until website realod. Checks if the bug appeared and reloads the website
            //    driver.Navigate().Refresh();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div/div[2]/div/main/div/section/table/tbody/tr/td[4]/div/button[2]"))); // Checks if the Dropdown next to the Edit buttons is visible, clicks on it and selects the "Delete" element
            CustomMethods.Click(driver, "/html/body/div[1]/div[2]/div/div/div[2]/div/main/div/section/table/tbody/tr/td[4]/div/button[2]", "XPath");
            CustomMethods.Click(driver, "/html/body/div[7]/div/ul/li/button", "XPath");

            CustomMethods.Click(driver, ".pm-modalFooter > .pm-button--primary", "CSS"); // Confirms the pop-up

            //Assert.AreEqual(driver.FindElement(By.CssSelector("orderableTableHeader:nth-child(1)")), entryOne);

            driver.Close(); // Closes the tests as this is the last one
        }

        //[TearDown] // TearDown after every test would log-out the tester
        //public void Cleanup()
        //{
        //    driver.Close();
        //}
    }
}
