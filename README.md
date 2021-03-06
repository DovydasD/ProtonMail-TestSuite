# ProtonMail ReadMe TestSuite
Description:
This is a test suite for the Label/Folder settings page of the ProtonMails Beta webpage. The test suite consists of six tests that need to be run in order for all of them to work properly. The order is required because when testing, after the first time the tests are run, logging in is mandatory and logging in with every test would become time-consuming. The first test is the "Login" test. The test enters correct login credentials, logs-in and closes down the pop-up which appears the first time the website is opened. The next two tests create a new Folder and a new Label with the appropriate information entered. After they are created, the fourth test sees if the notification toggle is working properly and changes its state after being pressed. The fifth test tries to edit one of the elements and see if it works properly. The sixth and last test deletes both the Folder and Label elements and closes the driver.

Bugs:
When writing this test suite I have found a few bugs.
1. The first bug is that sometimes, after some actions an overlay appears over the page. This overlay prevents the user from interacting with anything present on the website until the website is reloaded. To test the website without this issue interfering, I wrote an "IsElementPresent" method which reloads the website if the overlay is detected. I commented the method out, for the completed test suite. 

2. The next bug is about the Notification toggle button. I found that if you toggle the notification element and press the Edit button in too fast a sequence, the Notification toggle doesn't get toggled and returns to its prior state.
