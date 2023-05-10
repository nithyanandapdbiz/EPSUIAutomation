using EPS.Automation.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace EPS.Automation
{
    /// <summary>
    /// This is the base class for all pages that responsible
    /// to handle the WebDriver functionality across pages
    /// </summary>
    public abstract class BasePage : BaseTestFixture
    {
        #region WebDriver Utilities

        private readonly int _waitTimeOut = Convert.ToInt32(
           ConfigurationManager.AppSettings["ElementFindTimeOutInSeconds"]);

        /// <summary>
        /// Get a string representing the current URL that the browser is looking at.
        /// </summary>
        protected String GetCurrentUrl
        {
            get { return WebDriver.Url; }
        }

        /// <summary>
        /// Get the title of the current page.
        /// </summary>
        public String GetPageTitle
        {
            get { return WebDriver.Title; }
        }

        /// <summary>
        /// Get Current Page Source
        /// </summary>
        /// <returns>Page Source</returns>
        protected String GetPageSource()
        {
            return WebDriver.PageSource;
        }

        #endregion WebDriver Utilities

        #region WebDriver Click

        /// <summary>
        /// Find and Click on the first WebElement using the given method.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <see cref="ClickAction">Click this element. If this causes a new page to load, this method will attempt to block until the page has loaded.
        /// At this point, you should discard all references to this element and any further operations performed on this element will throw a
        ///  StaleElementReferenceException unless you know the element and the page will still be present.
        ///  If click() causes a new page to be loaded via an event or is done by sending a native event then the method will *not* wait for
        /// it to be loaded and the caller should verify that a new page has been loaded.
        /// There are some preconditions for an element to be clicked. The element must be visible and it must have a height and width greater then 0.</see>
        /// <exception cref="StaleElementReferenceException">If the element no longer exists as initially defined.</exception>
        private void ClickOnButton(By by)
        {
            base.WaitForElement(by);
            WebDriver.FindElement(by).Click();
        }

        /// <summary>
        /// Click the element by locating Id.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        protected void ClickButtonById(String idAttributeValue)
        {
            ClickOnButton(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Click the element by locating Class Name.
        /// </summary>
        /// <param name="classNameAttributeValue">This is the Class Name Attribute Value.</param>
        protected void ClickButtonByClassName(String classNameAttributeValue)
        {
            ClickOnButton(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Click the element by locating partial link text.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">This is the Partial Link Text Attribute Value.</param>
        protected void ClickButtonByPartialLinkText(String partialLinkTextAttributeValue)
        {
            ClickOnButton(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Click the element by locating Css selector.
        /// </summary>
        /// <param name="cssSelectorValue">This is the Css Selector Value.</param>
        protected void ClickButtonByCssSelector(String cssSelectorValue)
        {
            ClickOnButton(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Click the element by locating link text.
        /// </summary>
        /// <param name="linkTextAttributeValue">This is the Link text Attribute Value.</param>
        protected void ClickButtonByLinkText(String linkTextAttributeValue)
        {
            ClickOnButton(By.LinkText(linkTextAttributeValue));
        }

        /// <summary>
        /// Click the element by locating XPath.
        /// </summary>
        /// <param name="xPathValue">This is the Xpath Attribute Value.</param>
        protected void ClickButtonByXPath(String xPathValue)
        {
            ClickOnButton(By.XPath(xPathValue));
        }

        /// <summary>
        /// Click the element by locating Name.
        /// </summary>
        /// <param name="nameAttributeValue">This is the Name Attribute Value.</param>
        protected void SelectButtonByName(String nameAttributeValue)
        {
            ClickOnButton(By.Name(nameAttributeValue));
        }

        /// <summary>
        /// Select the Radio Button element by locating it's Id.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        protected void SelectRadioButtonById(String idAttributeValue)
        {
            ClickOnButton(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Select the Radio Button element by locating it's Name.
        /// </summary>
        /// <param name="nameAttributeValue">This is the Name Attribute Value.</param>
        protected void SelectRadioButtonByName(String nameAttributeValue)
        {
            ClickOnButton(By.Name(nameAttributeValue));
        }

        /// <summary>
        /// Select the Radio Button element by locating it's Xpath.
        /// </summary>
        /// <param name="xPathValue">This is the XPath Value.</param>
        protected void SelectRadioButtonByXPath(String xPathValue)
        {
            ClickOnButton(By.XPath(xPathValue));
        }

        /// <summary>
        /// Select the Radio Button element by locating it's ClassName.
        /// </summary>
        /// <param name="classNameAttributeValue">This is the ClassName Attribute Value.</param>
        protected void SelectRadioButtonByClassName(String classNameAttributeValue)
        {
            ClickOnButton(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Select the Radio Button element by locating it's CssSelector Value.
        /// </summary>
        /// <param name="cssSelectorValue">This is the CssSelector Value.</param>
        protected void SelectRadioButtonByCssSelector(String cssSelectorValue)
        {
            ClickOnButton(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Select the Check Box element by locating it's Id.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        protected void SelectCheckBoxById(String idAttributeValue)
        {
            ClickOnButton(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Select the Check Box element by locating it's XPath.
        /// </summary>
        /// <param name="xPathValue"> This is Xpath attribute value.</param>
        protected void SelectCheckBoxByXPath(String xPathValue)
        {
            ClickOnButton(By.XPath(xPathValue));
        }

        /// <summary>
        /// Select the Check Box element by locating it's class name.
        /// </summary>
        /// <param name="classNameAttributeValue"> This is class name attribute value.</param>
        protected void SelectCheckBoxByClassName(String classNameAttributeValue)
        {
            ClickOnButton(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Select the Check Box element by locating it's Css Selector .
        /// </summary>
        /// <param name="cssSelectorValue"> This is Css Selector value.</param>
        protected void SelectCheckBoxByCssSelector(String cssSelectorValue)
        {
            ClickOnButton(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Select the Check Box element by locating it's Name .
        /// </summary>
        /// <param name="nameAttributeValue"> This is Name attribute value.</param>
        protected void SelectCheckBoxByName(String nameAttributeValue)
        {
            ClickOnButton(By.Name(nameAttributeValue));
        }

        /// <summary>
        /// Click the Image element by locating it's XPath.
        /// </summary>
        /// <param name="xPathValue">This is the Xpath Attribute Value.</param>
        protected void ClickImageByXPath(String xPathValue)
        {
            ClickOnButton(By.XPath(xPathValue));
        }

        /// <summary>
        /// Click the Image element by locating it's id.
        /// </summary>
        /// <param name="idAttributeValue">This is the id attribute value.</param>
        protected void ClickImageById(String idAttributeValue)
        {
            ClickOnButton(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Click the Image element by locating it's class name.
        /// </summary>
        /// <param name="classNameAttributeValue">This is the class name attribute value.</param>
        protected void ClickImageByClassName(String classNameAttributeValue)
        {
            ClickOnButton(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Click the Link element by locating it's Partial Link Text.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">This is the Link text Attribute Value.</param>
        protected void ClickLinkByPartialLinkText(String partialLinkTextAttributeValue)
        {
            ClickOnButton(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Click the Link element by locating it's Link Text.
        /// </summary>
        /// <param name="linkTextAttributeValue">This is the Link text Attribute Value.</param>
        protected void ClickLinkByLinkText(String linkTextAttributeValue)
        {
            ClickOnButton(By.LinkText(linkTextAttributeValue));
        }

        /// <summary>
        /// Click the Link element by locating it's CssSelector.
        /// </summary>
        /// <param name="cssSelectorValue">This is the CssSelector Attribute Value.</param>
        protected void ClickLinkByCssSelector(String cssSelectorValue)
        {
            ClickOnButton(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Click the Link element by locating it's XPath.
        /// </summary>
        /// <param name="xPathValue">This is the Xpath Attribute Value.</param>
        protected void ClickLinkByXPath(String xPathValue)
        {
            ClickOnButton(By.XPath(xPathValue));
        }

        /// <summary>
        /// Click the Link element by locating it's Id.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        protected void ClickLinkById(String idAttributeValue)
        {
            ClickOnButton(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Select the Tab element by locating it's Id.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        protected void SelectTabById(String idAttributeValue)
        {
            ClickOnButton(By.Id(idAttributeValue));
        }

        /// <summary>
        ///Select the Tab element by locating it's Class name.
        /// </summary>
        /// <param name="classNameAttributeValue">This is the Class Name Attribute Value.</param>
        protected void SelectTabByClassName(String classNameAttributeValue)
        {
            ClickOnButton(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Select the Tab element by locating it's Partial Link Text.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">This is the Link Text Attribute Value.</param>
        protected void SelectTabByPartialLinkText(String partialLinkTextAttributeValue)
        {
            ClickOnButton(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Select the Tab element by locating it's XPath.
        /// </summary>
        /// <param name="xPathValue">This is the XPath Value.</param>
        protected void SelectTabByXPath(String xPathValue)
        {
            ClickOnButton(By.XPath(xPathValue));
        }

        #endregion WebDriver Click

        #region WebDriver SendKeys

        /// <summary>
        /// Use this method to simulate typing into an element, which may set its value.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="textToFill">This is text fill in the textbox.</param>
        /// <see cref="SendKeys">This method to simulate typing into an element,
        /// which may set its value.</see>
        private void FillTextBox(By by, String textToFill)
        {
            base.WaitForElement(by);
            WebDriver.FindElement(by).SendKeys(textToFill);
        }

        /// <summary>
        /// Use this method to simulate typing into an element by id attribute
        /// value, which may set its value.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        /// <param name="textToFill">This is text to fill in the textbox.</param>
        protected void FillTextBoxById(String idAttributeValue, String textToFill)
        {
            FillTextBox(By.Id(idAttributeValue), textToFill);
        }

        /// <summary>
        /// Use this method to simulate typing into an element by class name attribute
        /// value, which may set its value.
        /// </summary>
        /// <param name="classNameAttributeValue">This is the Class name Attribute Value.</param>
        /// <param name="textToFill">This is text to fill in the textbox.</param>
        protected void FillTextBoxByClassName(String classNameAttributeValue, String textToFill)
        {
            FillTextBox(By.ClassName(classNameAttributeValue), textToFill);
        }

        /// <summary>
        /// Use this method to simulate typing into an element by xpath
        /// value, which may set its value.
        /// </summary>
        /// <param name="xPathValue">This is the Xpath Value.</param>
        /// <param name="textToFill">This is text to fill in the textbox.</param>
        protected void FillTextBoxByXPath(String xPathValue, String textToFill)
        {
            Thread.Sleep(5000);
            FillTextBox(By.XPath(xPathValue), textToFill);
        }

        /// <summary>
        /// Use this method to simulate typing into an element by CssSelector
        /// value, which may set its value.
        /// </summary>
        /// <param name="cssSelectorValue">This is the CssSelector Value.</param>
        /// <param name="textToFill">This is text to fill in the textbox.</param>
        protected void FillTextBoxByCssSelector(String cssSelectorValue, String textToFill)
        {
            FillTextBox(By.CssSelector(cssSelectorValue), textToFill);
        }

        /// <summary>
        /// Use this method to simulate typing into an element by Name
        /// value, which may set its value.
        /// </summary>
        /// <param name="nameAttributeValue">This is the Name Value.</param>
        /// <param name="textToFill">This is text to fill in the textbox.</param>
        protected void FillTextBoxByName(String nameAttributeValue, String textToFill)
        {
            FillTextBox(By.Name(nameAttributeValue), textToFill);
        }

        /// <summary>
        /// Use this method to simulate typing null value into an element,
        /// which may set its value.
        ///  </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <see cref="SendKeys">This method to simulate typing into an element, which may set its value.</see>
        protected void FillEmptyText(By by)
        {
            base.WaitForElement(by);
            WebDriver.FindElement(by).SendKeys(String.Empty);
        }

        /// <summary>
        /// Use this method to simulate typing null value into an element
        /// by Id attribute value, which may set its value.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        protected void FillEmptyTextById(String idAttributeValue)
        {
            FillEmptyText(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Use this method to simulate typing null value into an element by
        /// Xpath value, which may set its value.
        /// </summary>
        /// <param name="xPathValue">This is the XPath Value.</param>
        protected void FillEmptyTextByXPath(String xPathValue)
        {
            FillEmptyText(By.XPath(xPathValue));
        }

        /// <summary>
        ///Use this method to simulate typing null value into an element by
        /// Partial Link Text attribute value, which may set its value.
        /// </summary>
        /// <param name="partialLinkText">This is the Partial Link Text Attribute Value.</param>
        protected void FillEmptyTextByPartialLinkText(String partialLinkText)
        {
            FillEmptyText(By.PartialLinkText(partialLinkText));
        }

        /// <summary>
        ///Use this method to simulate typing null value into an element by
        /// Class Name attribute value, which may set its value.
        /// </summary>
        /// <param name="classNameAttributeValue">This is the Class Name Attribute Value.</param>
        protected void FillEmptyTextByClassName(String classNameAttributeValue)
        {
            FillEmptyText(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Use this method to simulate focus on element by typing null value into an element by
        /// Id attribute value, which may set its value.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id Attribute Value.</param>
        protected void FocusOnElementById(String idAttributeValue)
        {
            FillEmptyText(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Use this method to simulate focus on element by typing null value into an element by
        /// CssSelector attribute value, which may set its value.
        /// </summary>
        /// <param name="cssSelectorValue">This is the CssSelector Attribute Value.</param>
        protected void FocusOnElementByCssSelector(String cssSelectorValue)
        {
            FillEmptyText(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Use this method to simulate focus on element by typing null value into an element by
        /// Class name attribute value, which may set its value.
        /// </summary>
        /// <param name="classNameAttributeValue">This is the Class name Attribute Value.</param>
        protected void FocusOnElementByClassName(String classNameAttributeValue)
        {
            FillEmptyText(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Use this method to simulate focus on element by typing null value into an element by
        /// Partial Link text attribute value, which may set its value.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">This is the Partial Link text Attribute Value.</param>
        protected void FocusOnElementByPartialLinkText(String partialLinkTextAttributeValue)
        {
            FillEmptyText(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Use this method to simulate focus on element by typing null value into an element by
        /// Link text attribute value, which may set its value.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">This is the Partial Link text Attribute Value.</param>
        protected void FocusOnElementByLinkText(String linkTextAttributeValue)
        {
            FillEmptyText(By.LinkText(linkTextAttributeValue));
        }

        /// <summary>
        /// Use this method to simulate focus on element by typing null value into an element by
        /// XPath attribute value, which may set its value.
        /// </summary>
        /// <param name="xPathValue">This is the XPath Value.</param>
        protected void FocusOnElementByXPath(String xPathValue)
        {
            FillEmptyText(By.XPath(xPathValue));
        }

        /// <summary>
        /// Simulate focus on element by Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// </summary>
        /// <param name="iWebElement">Defines the interface through which the user controls elements on the page.</param>
        /// <remarks>This is working with google chome and other browsers.</remarks>
        protected void PerformFocusOnElementAction(IWebElement iWebElement)
        {
            PerformMoveToElementAction(iWebElement);
        }

        /// <summary>
        /// Simulate focus on element id by Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// </summary>
        /// <param name="idLocator">Defines the interface by locating element Id through which
        /// the user controls elements on the page.</param>
        /// <remarks>This is working with google chome and other browsers.</remarks>
        protected void PerformFocusOnElementActionById(string idLocator)
        {
            IWebElement idLoctorProperty = GetWebElementPropertiesById(idLocator);
            PerformFocusOnElementAction(idLoctorProperty);
        }

        /// <summary>
        /// Simulate focus on element xpath by Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// </summary>
        /// <param name="xPathLocator">Defines the interface by locating element xpath through which
        /// the user controls elements on the page.</param>
        /// <remarks>This is working with google chome and other browsers.</remarks>
        protected void PerformFocusOnElementActionByXPath(string xPathLocator)
        {
            IWebElement idLoctorProperty = GetWebElementPropertiesByXPath(xPathLocator);
            PerformFocusOnElementAction(idLoctorProperty);
        }

        /// <summary>
        /// Simulate focus on element xpath by Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// </summary>
        /// <param name="classNameLocator">Defines the interface by locating element class name through which
        /// the user controls elements on the page.</param>
        /// <remarks>This is working with google chome and other browsers.</remarks>
        protected void PerformFocusOnElementActionByClassName(string classNameLocator)
        {
            IWebElement idLoctorProperty = GetWebElementPropertiesByClassName(classNameLocator);
            PerformFocusOnElementAction(idLoctorProperty);
        }

        #endregion WebDriver SendKeys

        #region WebDriver SwitchToFrame

        /// <summary>
        /// Select a frame by its name or ID. Frames located by matching name attributes
        /// are always given precedence over those matched by ID.
        /// </summary>
        /// <param name="frameName">The name of the frame window, the id of the frame
        /// or iframe element, or the (zero-based) index.</param>
        /// <exception cref="NoSuchFrameException">If the frame cannot be found.</exception>
        protected void SwitchToIFrame(String frameName)
        {
            WebDriver.SwitchTo().Frame(frameName);
        }

        /// <summary>
        /// Select a frame using its previously located WebElement.
        /// </summary>
        /// <param name="webElement">The frame element to switch to.</param>
        /// <exception cref="NoSuchFrameException">If the given element is neither an IFRAME nor a FRAME element.</exception>
        /// <exception cref="StaleElementReferenceException">If the WebElement has gone stale.</exception>
        protected void SwitchToIFrameByWebElement(IWebElement webElement)
        {
            WebDriver.SwitchTo().Frame(webElement);
        }

        /// <summary>
        /// Select a frame by its name. Frames located by matching name attributes
        /// are always given precedence over those matched by ID.
        /// </summary>
        /// <param name="idAttributeValue">This is iFrame id atribute value.</param>
        protected void SwitchToIFrameByName(String nameAttributeValue)
        {
            base.WaitForElement(By.Name(nameAttributeValue));
            WebDriver.SwitchTo().Frame(nameAttributeValue);
        }

        /// <summary>
        ///  Select a frame by its (zero-based) index. That is, if a page has three frames,
        ///  the first frame would be at index "0", the second at index "1" and the third at index "2".
        /// Once the frame has been selected, all subsequent calls on the WebDriver interface are made to that frame.
        /// </summary>
        /// <param name="indexNumber"> (zero-based) index. </param>
        /// <exception cref="NoSuchFrameException">If the frame cannot be found.</exception>
        protected void SwitchToIFrameByIndex(int indexNumber)
        {
            WebDriver.SwitchTo().Frame(indexNumber);
        }

        /// <summary>
        /// Select a frame by its name or ID. Frames located by matching name attributes
        /// are always given precedence over those matched by ID.
        /// </summary>
        /// <param name="idAttributeValue">This is iFrame id atribute value.</param>
        protected void SwitchToIFrameById(String idAttributeValue)
        {
            base.WaitForElement(By.Id(idAttributeValue));
            WebDriver.SwitchTo().Frame(idAttributeValue);
        }

        /// <summary>
        /// Select a frame by its SOURCE. Frames located by matching SRC attributes
        /// </summary>
        /// <param name="nameAttributeValue">This is iFrame source attribute value.</param>
        protected void SwitchToIFrameBySource(String nameAttributeValue)
        {
            IEnumerable<IWebElement> frames = WebDriver.FindElements(By.TagName("iframe"));

            if (frames == null)
                return;

            foreach (IWebElement iframe in frames)
            {
                if (iframe.GetAttribute("src").Contains(nameAttributeValue))
                {
                    SwitchToIFrameByWebElement(iframe);
                    break;
                }
                else throw new Exception("Iframe " + nameAttributeValue + " do not  exist.");
            }
        }

        #endregion WebDriver SwitchToFrame

        #region WebDriver SwitchToWindow

        /// <summary>
        /// Selects either the first frame on the page, or the main document when a page contains iframes.
        /// </summary>
        /// <see cref="DefaultContent">This driver focused on the top window/first frame.</see>
        protected void SwitchToDefaultPageContent()
        {
            WebDriver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Switches to the element that currently has focus within the document currently "switched to",
        /// or the body element if this cannot be detected. This matches the semantics of
        /// calling "document.activeElement" in Javascript.
        /// </summary>
        /// <see cref="ActiveElement">The WebElement with focus, or the body element if no
        /// element with focus can be detected.</see>
        protected void SwitchToActivePageElement()
        {
            WebDriver.SwitchTo().ActiveElement();
        }

        /// <summary>
        /// Switch the focus of future commands for this driver to the
        /// parent window with the given empty string.
        /// </summary>
        /// <exception cref="NoSuchWindowException">If the window cannot be found.</exception>
        protected void SwitchToDefaultWindow()
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.First());
        }

        /// <summary>
        /// Send future commands to select a last frame or window.
        /// </summary>
        /// <see cref="List{T}">Return a set of window handles which can be used to iterate over all open windows
        ///  of this webdriver instance by passing them to #switchTo().window(String)</see>
        protected void SwitchToLastOpenedWindow()
        {
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.ToList().Last());
        }

        protected void SwitchToWindow(string windowName, int timeOut = -1)
        {
            bool windowFound = false;
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    List<String> allWindows = WebDriver.WindowHandles.ToList();
                    string winsname = "";

                    foreach (String wins in allWindows)
                    {
                        winsname = WebDriver.SwitchTo().Window(wins).Title;

                        if (winsname == windowName)
                        {
                            windowFound = true;
                            WebDriver.SwitchTo().Window(wins);
                            break;
                        }
                    }
                    if (windowFound) break;
                }
                stopWatch.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Switch the focus of future commands for this driver to the window specified
        /// </summary>
        /// <see cref="List{T}">Return a set of window handles which can be used to iterate over all open windows
        ///  of this webdriver instance by passing them to #switchTo().window(String)</see>
        /*protected void SwitchToWindow(string windowName)
        {
             WebDriver.SwitchTo().Window(WebDriver.WindowHandles.ToList().Find(x => {
                 return x == windowName;
             }));

            List<String> allWindows = WebDriver.WindowHandles.ToList();
            string winsname = "";
            foreach (String wins in allWindows)
            {
                winsname = WebDriver.SwitchTo().Window(wins).Title;

                if (winsname == windowName)
                {
                    WebDriver.SwitchTo().Window(wins);
                }
            }
        }

        //WebDriver.SwitchTo().Window(WebDriver.WindowHandles.ToList().Last()); */

        protected void SwitchToPopup()
        {
            try
            {
                List<String> AllWindows = WebDriver.WindowHandles.ToList();
                foreach (String Win in AllWindows)
                {
                    WebDriver.SwitchTo().Window(Win);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected void SwitchToPartialWindowTitle(string windowName, int timeOut = -1)
        {
            bool windowFound = false;
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    List<String> allWindows = WebDriver.WindowHandles.ToList();
                    string winsname = "";

                    foreach (String wins in allWindows)
                    {
                        winsname = WebDriver.SwitchTo().Window(wins).Title;

                        if (winsname.Contains(windowName))
                        {
                            windowFound = true;
                            WebDriver.SwitchTo().Window(wins);
                            break;
                        }
                    }
                    if (windowFound) break;
                }
                stopWatch.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion WebDriver SwitchToWindow

        #region WebDriver Get IWebElement

        /// <summary>
        /// Defines the interface through which the user controls elements on the page.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <see cref="FindElement">Finds the first IWebElement using the given method.</see>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element Properties.</returns>
        public IWebElement GetWebElementProperties(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by);
        }

        public IList<IWebElement> GetWebElementsProperties(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElements(by);
        }

        /// <summary>
        /// Get the value of a the given attribute of the element by specified locator attribute value.
        /// </summary>
        /// <param name="by">The locator to be used.</param>
        /// <param name="propertyName">The attribute whose value is to be returned.</param>
        /// <returns>The attribute's current value or null if the value is not set by locator attribute value.</returns>
        protected string GetWebElementPropertyValue(By by, string propertyName)
        {
            base.WaitForElement(by);
            IWebElement webElement = GetWebElementProperties(by);
            return webElement.GetAttribute(propertyName);
        }

        /// <summary>
        /// This method returns all the elements with the tagName in the context for the given XPath
        /// </summary>
        /// <param name="by"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private ICollection<IWebElement> GetWebElementsByXPathAndTagName(By by, string tagName)
        {
            var webElement = WebDriver.FindElement(by);
            return webElement.FindElements(By.TagName(tagName));
        }

        /// <summary>
        /// This method returns all the elements with the tagName in the context for the given XPath
        /// </summary>
        /// <param name="xPath"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        protected ICollection<IWebElement> GetWebElementsByXpathAndTagName(string xPath, string tagName)
        {
            return GetWebElementsByXPathAndTagName(By.XPath(xPath), tagName);
        }

        /// <summary>
        /// Defines the interface through which the user controls elements on the page by XPath value.
        /// </summary>
        /// <param name="xPathValue">Retrieves HTML Element by XPath.</param>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element properties by Xpath.</returns>
        protected IWebElement GetWebElementPropertiesByXPath(String xPathValue)
        {
            return GetWebElementProperties(By.XPath(xPathValue));
        }

        /// <summary>
        /// Defines the interface through which the user controls elements on the page by Partial Link text value.
        /// </summary>
        /// <param name="patialLinkTextAttributeValue">Retrieves HTML Element by Partial LinkText.</param>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element properties by Partial Link Text.</returns>
        protected IWebElement GetWebElementPropertiesByPartialLinkText(String patialLinkTextAttributeValue)
        {
            return GetWebElementProperties(By.PartialLinkText(patialLinkTextAttributeValue));
        }

        /// <summary>
        /// Defines the interface through which the user controls elements on the page by Id value.
        /// </summary>
        /// <param name="idAttributeValue">Retrieves HTML Element by Id.</param>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element properties by Id.</returns>
        protected IWebElement GetWebElementPropertiesById(String idAttributeValue)
        {
            return GetWebElementProperties(By.Id(idAttributeValue));
        }

        /// <summary>
        ///Defines the interface through which the user controls elements on the page by class name value.
        /// </summary>
        /// <param name="classNameAttributeValue">Retrieves HTML Element by Class Name.</param>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element properties by class name.</returns>
        protected IWebElement GetWebElementPropertiesByClassName(String classNameAttributeValue)
        {
            return GetWebElementProperties(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Defines the interface through which the user controls elements on the page by Css Selector value.
        /// </summary>
        /// <param name="cssSelectorValue">Retrieves HTML Element by CssSelector.</param>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element properties by Css Selector.</returns>
        protected IWebElement GetWebElementPropertiesByCssSelector(String cssSelectorValue)
        {
            return GetWebElementProperties(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Defines the interface through which the user controls elements on the page by Link text value.
        /// </summary>
        /// <param name="linkTextAttributeValue">Retrieves HTML Element by LinkText.</param>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element properties by Link Text.</returns>
        protected IWebElement GetWebElementPropertiesByLinkText(String linkTextAttributeValue)
        {
            return GetWebElementProperties(By.LinkText(linkTextAttributeValue));
        }

        /// <summary>
        /// Defines the interface through which the user controls elements on the page by Name Attribute value.
        /// </summary>
        /// <param name="nameAttributeValue">Retrieves HTML Element by Name.</param>
        /// <seealso cref="IWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</seealso>
        /// <returns>Web Element properties by Name Attribute.</returns>
        protected IWebElement GetWebElementPropertiesByName(String nameAttributeValue)
        {
            return GetWebElementProperties(By.Name(nameAttributeValue));
        }

        #endregion WebDriver Get IWebElement

        #region WebDriver Element Displayed

        /// <summary>
        /// Is this element displayed or not.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>Whether or not the element is displayed.</returns>
        /// <see cref="Displayed">Is this element displayed or not? This method avoids the
        /// problem of having to parse an element's "style" attribute.</see>
        private Boolean IsElementDisplayed(By by)
        {
            try
            {
                base.WaitForElement(by, 5);
                return WebDriver.FindElement(by).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  Is this element displayed or not by Partial LinkText Attribute Value.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">Retrieves HTML Element by Partial Link text attribute value.</param>
        /// <returns>Whether or not the element is displayed by Partial LinkText Attribute Value.</returns>
        protected Boolean IsElementDisplayedByPartialLinkText(string partialLinkTextAttributeValue)
        {
            return IsElementDisplayed(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Is this element displayed or not by Id Attribute Value.
        /// </summary>
        /// <param name="idAttributeValue">Retrieves HTML Element by Id attribute value.</param>
        /// <returns>Whether or not the element is displayed by Id Attribute Value.</returns>
        protected Boolean IsElementDisplayedById(string idAttributeValue)
        {
            return IsElementDisplayed(By.Id(idAttributeValue));
        }

        #endregion WebDriver Element Displayed

        #region WebDriver Element Enabled

        /// <summary>
        /// Is the element currently enabled or not.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>True if the element is enabled, false otherwise.</returns>
        /// <see cref="Enabled">Is the element currently enabled or not? This will generally return true
        /// for everything but disabled input elements.</see>
        private Boolean IsElementEnabled(By by)
        {
            try
            {
                base.WaitForElement(by, 10);
                return WebDriver.FindElement(by).Enabled;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Is the element currently enabled or not by locating id attribute value.
        /// </summary>
        /// <param name="idAttributeValue">Retrieves HTML Element by locating id attribute value.</param>
        /// <returns>True if the element is enabled, false otherwise by locating id attribute value.</returns>
        protected Boolean IsElementEnabledById(String idAttributeValue)
        {
            return IsElementEnabled(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Is the element currently enabled or not by locating id attribute value.
        /// </summary>
        /// <param name="xpathAttributeValue">Retrieves HTML Element by locating xpath attribute value.</param>
        /// <returns>True if the element is enabled, false otherwise by locating xpath attribute value.</returns>
        protected Boolean IsElementEnabledByXPath(String xpathAttributeValue)
        {
            return IsElementEnabled(By.XPath(xpathAttributeValue));
        }

        /// <summary>
        /// Is the element currently enabled or not by locating partial link text value.
        /// </summary>
        /// <param name="partialLinkTextValue">Retrieves HTML Element by locating partial link text value.</param>
        /// <returns>True if the element is enabled, false otherwise by locating partial link text value.</returns>
        protected Boolean IsElementEnabledByPartialLinkText(String partialLinkTextValue)
        {
            return IsElementEnabled(By.Id(partialLinkTextValue));
        }

        #endregion WebDriver Element Enabled

        #region WebDriver Submit

        /// <summary>
        /// If this current element is a form, or an element within a form,
        /// then this will be submitted to the remote server.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <see cref="Submit">If this current element is a form, or an element within a form,
        /// then this will be submitted to the remote server. If this causes the current page to change,
        ///  then this method will block until the new page is loaded.</see>
        /// <exception cref="NoSuchElementException">If the given element is not within a form</exception>
        private void SubmitButton(By by)
        {
            base.WaitForElement(by);
            WebDriver.FindElement(by).Submit();
        }

        /// <summary>
        /// If this current element by class name attribute value is a form, or an element within a form,
        /// then this will be submitted to the remote server.
        /// </summary>
        /// <param name="classNameAttributeValue">This is Class Name attribute value.</param>
        protected void SubmitButtonByClassName(String classNameAttributeValue)
        {
            SubmitButton(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// If this current element by Id attribute value is a form, or an element within a form,
        /// then this will be submitted to the remote server.
        /// </summary>
        /// <param name="idAttributeValue">This is Id attribute value.</param>
        protected void SubmitButtonById(String idAttributeValue)
        {
            SubmitButton(By.Id(idAttributeValue));
        }

        /// <summary>
        /// If this current element by CssSelector attribute value is a form, or an element within a form,
        /// then this will be submitted to the remote server.
        /// </summary>
        /// <param name="cssSelectorValue">This is CssSelector value.</param>
        protected void SubmitButtonByCssSelector(String cssSelectorValue)
        {
            SubmitButton(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// If this current element by XPath value is a form, or an element within a form,
        /// then this will be submitted to the remote server.
        /// </summary>
        /// <param name="xPathValue">This is XPath value.</param>
        protected void SubmitButtonByXPath(String xPathValue)
        {
            SubmitButton(By.XPath(xPathValue));
        }

        /// <summary>
        /// If this current element by Name attribute value is a form, or an element within a form,
        /// then this will be submitted to the remote server.
        /// </summary>
        /// <param name="nameAttributeValue">This is Name attribute value.</param>
        protected void SubmitButtonByName(String nameAttributeValue)
        {
            SubmitButton(By.Name(nameAttributeValue));
        }

        #endregion WebDriver Submit

        #region WebDriver Contains Text/Keyword

        /// <summary>
        ///  Indicating whether the specified text occurs within this string.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="containsTextToFind">Retrieves text to find in the specified string.</param>
        /// <returns>True if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        private Boolean IsElementContainsText(By by, string containsTextToFind)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).Text.Contains(containsTextToFind);
        }

        /// <summary>
        /// Indicating whether the specified text occurs within this string by id attribute of the element .
        /// </summary>
        /// <param name="idAttributeValue">Retrieves text from the id attribute value of HTML element.</param>
        /// <param name="containsTextToFind">Retrieves text to find in the specified string.</param>
        /// <returns>HTML Element Text Present Result</returns>
        protected Boolean IsElementContainsTextById(string idAttributeValue, string containsTextToFind)
        {
            return IsElementContainsText(By.Id(idAttributeValue), containsTextToFind);
        }

        /// <summary>
        ///  Indicating whether the specified inner text occurs within this string by attribute value of the element .
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="containsTextToFind">Retrieves text to find in the specified string.</param>
        /// <returns>True if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        private Boolean IsElementContainsInnerText(By by, string containsTextToFind)
        {
            //Inner Text Present for the Element
            Boolean isTextPresent = false;
            switch (Browser)
            {
                // This is for Internet Explorer Browser
                case InternetExplorer:
                    isTextPresent = WebDriver.FindElement(by).GetAttribute("innerText").Contains(containsTextToFind);
                    break;
                // This is for Chrome Browser
                case Chrome:
                    isTextPresent = WebDriver.FindElement(by).Text.Contains(containsTextToFind);
                    break;
                // This is for Firefox Browser
                case FireFox:
                    isTextPresent = WebDriver.FindElement(by).Text.Contains(containsTextToFind);
                    break;
            }
            return isTextPresent;
        }

        /// <summary>
        ///  Indicating whether the specified inner text occurs within this string by xpath of the element .
        /// </summary>
        /// <param name="xPathValue">Retrieves inner text from the xpath attribute value of HTML element.</param>
        /// <param name="containsTextToFind">Retrieves text to find in the specified string.</param>
        /// <returns>Inner Text Present Result</returns>
        protected Boolean IsElementContainsInnerTextByXPath(string xPathValue, string containsTextToFind)
        {
            return IsElementContainsInnerText(By.XPath(xPathValue), containsTextToFind);
        }

        /// <summary>
        ///  Indicating whether the specified inner text occurs within this string by id attribute of the element .
        /// </summary>
        /// <param name="idAttributeValue">Retrieves inner text from the id attribute value of HTML element.</param>
        /// <param name="containsTextToFind">Retrieves text to find in the specified string.</param>
        /// <returns>HTML Element Inner Text Present Result</returns>
        protected Boolean IsElementContainsInnerTextById(string idAttributeValue, string containsTextToFind)
        {
            return IsElementContainsInnerText(By.Id(idAttributeValue), containsTextToFind);
        }

        /// <summary>
        ///  Indicating whether the specified keyword text occurs within this string.
        /// </summary>
        /// <param name="keywordUrl">Retrieves keyword to find in the specified string.</param>
        /// <returns>True if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        protected Boolean IsUrlContainsTheKeyword(string keywordUrl)
        {
            return WebDriver.Url.Contains(keywordUrl);
        }

        #endregion WebDriver Contains Text/Keyword

        #region WebDriver GetAttribute

        /// <summary>
        /// Get the value of a the given title of the element.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned. </see>
        private String GetTitleAttributeValueByLocator(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("Title");
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating PartialLinkText attribute value.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue"> Retrieves The name of the attribute by locating
        /// PartialLinkText attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not
        /// set by locating PartialLinkText attribute value.</returns>
        protected String GetIdAttributeValueByPartialLinkText(String partialLinkTextAttributeValue)
        {
            return GetIDAttributeValue(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Get the ID attribute value of a web element.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned.

        private String GetIDAttributeValue(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("id");
        }

        /// <summary>
        /// Get the value of a href from a web element
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned. </see>
        private String GetHrefAttributeValueByLocator(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("href");
        }

        /// <summary>
        /// Get the value of href of the element by locating Xpath value.
        /// </summary>
        /// <param name="xPathValue">Retrieves The name of the attribute by locating Xpath value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating Xpath value.</returns>
        protected String GetHrefAttributeValueByXPath(String xPathValue)
        {
            return GetHrefAttributeValueByLocator(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the value attribute of a web element
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned. </see>
        private String GetValueAttributeByLocator(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("value");
        }

        /// <summary>
        /// Get the value attribute of a web element
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned. </see>
        private String GetDisableAttributeByLocator(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("disabled");
        }

        /// <summary>
        /// Get the value attribute of the element by locating Xpath value.
        /// </summary>
        /// <param name="xPathValue"> Retrieves The name of the attribute by locating Xpath value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating Xpath value.</returns>
        protected String GetValueAttributeByXPath(String xPathValue)
        {
            return GetValueAttributeByLocator(By.XPath(xPathValue));
        }

        /// <summary>
        /// GEt the disabled attribute of the element by locating Xpath value
        /// </summary>
        /// <param name="xPathValue">Return the disabled </param>
        /// <returns></returns>
        protected String GetDisableAttributeByXPath(String xPathValue)
        {
            return GetDisableAttributeByLocator(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the value attribute of the element by locating Id value.
        /// </summary>
        /// <param name="idAttributeValue"> Retrieves The name of the attribute by locating Id value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating Id value.</returns>
        protected String GetValueAttributeById(String idAttributeValue)
        {
            return GetValueAttributeByLocator(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given title of the element by locating Xpath attribute value.
        /// </summary>
        /// <param name="xPathValue"> Retrieves The name of the attribute by locating Xpath attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating Xpath attribute value.</returns>
        protected String GetTitleAttributeValueByXPath(String xPathValue)
        {
            return GetTitleAttributeValueByLocator(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the value of a the given title of the element by locating id attribute value.
        /// </summary>
        /// <param name="idAttributeValue"> Retrieves The name of the attribute by locating id attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating id attribute value.</returns>
        protected String GetTitleAttributeValueById(String idAttributeValue)
        {
            return GetTitleAttributeValueByLocator(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given inner text of the element.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned. </see>
        private String GetInnerTextAttributeValueByLocator(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("innerText");
        }

        /// <summary>
        /// Get the value of a the given inner text of the element by locating Id attribute value.
        /// </summary>
        /// <param name="idAttributeValue"> Retrieves The name of the attribute by locating id attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating id attribute value.</returns>
        protected String GetInnerTextAttributeValueById(String idAttributeValue)
        {
            return GetInnerTextAttributeValueByLocator(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given inner text of the element by locating className attribute value.
        /// </summary>
        /// <param name="classNameAttributeValue"> Retrieves The name of the attribute by locating class attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating id attribute value.</returns>
        protected String GetInnerTextAttributeValueByClassName(String classNameAttributeValue)
        {
            return GetInnerTextAttributeValueByLocator(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given inner text of the element by locating Xpath value.
        /// </summary>
        /// <param name="xPathValue"> Retrieves The name of the attribute by locating Xpath value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating Xpath value.</returns>
        protected String GetInnerTextAttributeValueByXPath(String xPathValue)
        {
            return GetInnerTextAttributeValueByLocator(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the class attribute value of a web element.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned. </see>
        private String GetClassAttributeValue(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("class");
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating id attribute value.
        /// </summary>
        /// <param name="idAttributeValue"> Retrieves The name of the attribute by locating id attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating id attribute value.</returns>
        protected String GetClassAttributeValueById(String idAttributeValue)
        {
            return GetClassAttributeValue(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating class name attribute value.
        /// </summary>
        /// <param name="nameAttributeValue"> Retrieves The name of the attribute by locating class name attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not set by locating class name attribute value.</returns>
        protected String GetClassAttributeValueByClassName(String nameAttributeValue)
        {
            return GetClassAttributeValue(By.ClassName(nameAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating PartialLinkText attribute value.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue"> Retrieves The name of the attribute by locating
        /// PartialLinkText attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not
        /// set by locating PartialLinkText attribute value.</returns>
        protected String GetClassAttributeValueByPartialLinkText(String partialLinkTextAttributeValue)
        {
            return GetClassAttributeValue(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating CssSelector attribute value.
        /// </summary>
        /// <param name="cssSelectorValue"> Retrieves The name of the attribute by locating
        /// CssSelector attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not
        /// set by locating CssSelector attribute value.</returns>
        protected String GetClassAttributeValueByCssSelector(String cssSelectorValue)
        {
            return GetClassAttributeValue(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating XPath attribute value.
        /// </summary>
        /// <param name="xPathValue"> Retrieves The name of the attribute by locating
        /// XPath attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not
        /// set by locating XPath attribute value.</returns>
        protected String GetClassAttributeValueByXPath(String xPathValue)
        {
            return GetClassAttributeValue(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating LinkText attribute value.
        /// </summary>
        /// <param name="linkTextValue"> Retrieves The name of the attribute by locating
        /// LinkText attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not
        /// set by locating LinkText attribute value.</returns>
        protected String GetClassAttributeValueByLinkText(String linkTextValue)
        {
            return GetClassAttributeValue(By.LinkText(linkTextValue));
        }

        /// <summary>
        /// Get the value of a the given class of the element by locating TagName attribute value.
        /// </summary>
        /// <param name="locatorTagNameValue"> Retrieves The name of the attribute by locating
        /// TagName attribute value.</param>
        /// <returns>The attribute's current value or null if the value is not
        /// set by locating TagName attribute value.</returns>
        protected String GetClassAttributeValueByTagName(String locatorTagNameValue)
        {
            return GetClassAttributeValue(By.TagName(locatorTagNameValue));
        }

        /// <summary>
        /// Get the style attribute value of a web element.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The attribute's current value or null if the value is not set.</returns>
        /// <see cref="GetAttribute">Get the value of a the given attribute of the element.
        /// Will return the current value, even if this has been modified after the page has been loaded.
        /// More exactly, this method will return the value of the given attribute, unless that attribute is not present,
        /// in which case the value of the property with the same name is returned. If neither value is set, null is returned. </see>
        private String GetStyleAttributeValue(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).GetAttribute("style");
        }

        /// <summary>
        /// Get the value of a the given id of the element by locating style attribute value.
        /// </summary>
        /// <param name="idAttributeValue"> Retrieves the style of the attribute by locating id attribute value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating Id attribute value.</returns>
        protected String GetStyleAttributeValueById(String idAttributeValue)
        {
            return GetStyleAttributeValue(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the value of a the given xpath of the element by locating style attribute value.
        /// </summary>
        /// <param name="xPathValue"> Retrieves the style of the attribute by locating xpath value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating xpath value.</returns>
        protected String GetStyleAttributeValueByXPath(String xPathValue)
        {
            return GetStyleAttributeValue(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the value of a the given CssSelector of the element by locating style attribute value.
        /// </summary>
        /// <param name="cssSelectorValue"> Retrieves the style of the attribute by locating CssSelector value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating CssSelector value.</returns>
        protected String GetStyleAttributeValueByCssSelector(String cssSelectorValue)
        {
            return GetStyleAttributeValue(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Get the value of a the given PartialLinkText of the element by locating style attribute value.
        /// </summary>
        /// <param name="partialLinkTextValue"> Retrieves the style of the attribute by locating PartialLinkText value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating PartialLinkText value.</returns>
        protected String GetStyleAttributeValueByPartialLinkText(String partialLinkTextValue)
        {
            return GetStyleAttributeValue(By.PartialLinkText(partialLinkTextValue));
        }

        /// <summary>
        /// Get the value of a the given Name of the element by locating style attribute value.
        /// </summary>
        /// <param name="nameValue"> Retrieves the style of the attribute by locating Name value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating Name value.</returns>
        protected String GetStyleAttributeValueByName(String nameValue)
        {
            return GetStyleAttributeValue(By.Name(nameValue));
        }

        /// <summary>
        /// Get the value of a the given Link Text of the element by locating style attribute value.
        /// </summary>
        /// <param name="linkTextValue"> Retrieves the style of the attribute by locating Link Text value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating Link Text value.</returns>
        protected String GetStyleAttributeValueByLinkText(String linkTextValue)
        {
            return GetStyleAttributeValue(By.LinkText(linkTextValue));
        }

        /// <summary>
        /// Get the value of a the given Tag Name of the element by locating style attribute value.
        /// </summary>
        /// <param name="locatorTagNameValue"> Retrieves the style of the attribute by locating Tag Name value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating Tag Name value.</returns>
        protected String GetStyleAttributeValueByTagName(String locatorTagNameValue)
        {
            return GetStyleAttributeValue(By.TagName(locatorTagNameValue));
        }

        /// <summary>
        /// Get the value of a the given Class Name of the element by locating style attribute value.
        /// </summary>
        /// <param name="classNameValue"> Retrieves the style of the attribute by locating Tag Class Name value.</param>
        /// <remarks>This helps to locate the color of a webelement in hexadecimal format.</remarks>
        /// <returns>The attribute's current value or null if the value is not set by locating Tag Class Name value.</returns>
        protected String GetStyleAttributeValueByClassName(String classNameValue)
        {
            return GetStyleAttributeValue(By.ClassName(classNameValue));
        }

        #endregion WebDriver GetAttribute

        #region Comet domain name

        /// <summary>
        /// Get domain name of com
        /// </summary>
        /// <returns>Domain name of commet URl</returns>
        protected string GetCometDomain()
        {
            string getCometDomain = null;
            // Getting the page title
            string url = this.GetCurrentUrl;
            string getDomain = url.Substring(7);
            int indexValue = getDomain.IndexOf('/');
            getCometDomain = getDomain.Substring(0, indexValue);
            return getCometDomain;
        }

        #endregion Comet domain name

        #region WebDriver Element Selected

        /// <summary>
        /// Determine whether or not this element is selected or not.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>True if the element is currently selected or checked, false otherwise.</returns>
        /// <see>Determine whether or not this element is selected or not.
        /// This operation only applies to input elements such as checkboxes,
        /// options in a select and radio buttons.
        ///     <cref>Selected</cref>
        /// </see>
        protected Boolean IsElementSelected(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).Selected;
        }

        /// <summary>
        /// Determine whether or not this element is selected or not by element Id attribute value.
        /// </summary>
        /// <param name="idAttributeValue">Retrieves element Id attribute value.</param>
        /// <returns>True if the element by id attribute value is currently selected or checked, false otherwise.</returns>
        protected Boolean IsElementSelectedById(String idAttributeValue)
        {
            return IsElementSelected(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Determine whether or not this element is selected or not by element xpath value.
        /// </summary>
        /// <param name="xPathValue">Retrieves element xpath value.</param>
        /// <returns>True if the element by Xpath value is currently selected or checked, false otherwise.</returns>
        protected Boolean IsElementSelectedByXPath(String xPathValue)
        {
            return IsElementSelected(By.XPath(xPathValue));
        }

        #endregion WebDriver Element Selected

        #region WebDriver Select Element

        /// <summary>
        /// Select all options that display text matching the argument.
        /// </summary>
        /// <param name="by">>This is HTML element locating mechanism to use.</param>
        /// <param name="value">The visible text to match against.</param>
        private void SelectDropDownValueByText(By by, String value)
        {
            base.WaitForElement(by);
            new SelectElement(WebDriver.FindElement(by)).SelectByText(value);
        }

        /// <summary>
        /// Select all options that display text matching the argument by HTML element Id attribute.
        /// </summary>
        /// <param name="id">This is HTML attribute Id value.</param>
        /// <param name="selectTextValue">The visible text to match against by id attribute of the HTML element.</param>
        protected void SelectDropDownValueThroughTextById(String id, String selectTextValue)
        {
            SelectDropDownValueByText(By.Id(id), selectTextValue);
        }

        /// <summary>
        /// Select all options that display text matching the argument by HTML element CssSelector attribute.
        /// </summary>
        /// <param name="CssSelector">This is HTML attribute CssSelector value.</param>
        /// <param name="selectTextValue">The visible text to match against by id attribute of the HTML element.</param>
        protected void SelectDropDownValueThroughTextByCssSelector(String CssSelector, String selectTextValue)
        {
            SelectDropDownValueByText(By.CssSelector(CssSelector), selectTextValue);
        }

        /// <summary>
        ///  Select all options that display text matching the argument by HTML element name attribute.
        /// </summary>
        /// <param name="nameAttributeValue">This is HTML attribute name value.</param>
        /// <param name="selectTextValue">The visible text to match against by name attribute of the HTML element.</param>
        protected void SelectDropDownValueThroughTextByName(String nameAttributeValue, String selectTextValue)
        {
            SelectDropDownValueByText(By.Name(nameAttributeValue), selectTextValue);
        }

        /// <summary>
        /// Select the option at the given index. This is done by examing the "index"
        /// attribute of an element, and not merely by counting.
        /// </summary>
        /// <param name="by">>This is HTML element locating mechanism to use.</param>
        /// <param name="indexValue">The option at this index will be selected.</param>
        private void SelectDropDownValueByIndex(By by, int indexValue)
        {
            base.WaitForElement(by);
            new SelectElement(WebDriver.FindElement(by)).SelectByIndex(indexValue);
        }

        /// <summary>
        ///  Select the option at the given index by HTML element Id attribute.
        /// </summary>
        /// <param name="idAttributeValue">The option at this index will be selected by id attribute of the HTML element.</param>
        /// <param name="indexValue">The option at this index will be selected.</param>
        protected void SelectDropDownValueThroughIndexById(String idAttributeValue, int indexValue)
        {
            SelectDropDownValueByIndex(By.Id(idAttributeValue), indexValue);
        }

        /// <summary>
        ///  Select the option at the given index by HTML element Id attribute.
        /// </summary>
        /// <param name="idAttributeValue">The option at this index will be selected by id attribute of the HTML element.</param>
        /// <param name="indexValue">The option at this index will be selected.</param>
        protected void SelectDropDownValueThroughIndexByCssSelector(String idAttributeValue, int indexValue)
        {
            SelectDropDownValueByIndex(By.CssSelector(idAttributeValue), indexValue);
        }

        /// <summary>
        /// Select the option from the given list of options attribute and match with expected option value.
        /// This is done by examing the "option"  attribute of an element, and merely by listing.
        /// </summary>
        /// <param name="by">>This is HTML element locating mechanism to use.</param>
        /// <param name="optionName">This is a name of the option.</param>
        /// <see cref="IWebElement">he IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface</see>
        private void SelectDropDownValueByTagNameOption(By by, string optionName)
        {
            base.WaitForElement(by);
            IWebElement select = WebDriver.FindElement(by);
            IList<IWebElement> options = select.FindElements(By.TagName("option"));
            foreach (IWebElement option in options)
            {
                if (option.Text.Equals(optionName))
                {
                    ClickByJavaScriptExecutor(option);
                    Thread.Sleep(500);
                    break;
                }
            }
        }

        /// <summary>
        /// Select the option from the given list of options attribute and match with expected option value.
        /// This is done by examing the "option"  attribute of an element, and merely by listing.
        /// </summary>
        /// <param name="idLocatorValue">>This is HTML element locating mechanism through id.</param>
        /// <param name="optionName">This is a name of the drop down option.</param>
        protected void SelectDropDownOptionById(string idLocatorValue, string optionName)
        {
            SelectDropDownValueByTagNameOption(By.Id(idLocatorValue), optionName);
        }

        /// <summary>
        /// Select the option from the given list of options attribute and match with expected option value.
        /// This is done by examing the "option"  attribute of an element, and merely by listing.
        /// </summary>
        /// <param name="classNameLocatorValue">>This is HTML element locating mechanism through class name.</param>
        /// <param name="optionName">This is a name of the drop down option.</param>
        protected void SelectDropDownOptionByClassName(string classNameLocatorValue, string optionName)
        {
            SelectDropDownValueByTagNameOption(By.ClassName(classNameLocatorValue), optionName);
        }

        /// <summary>
        /// Select the option from the given list of options attribute and match with expected option value.
        /// This is done by examing the "option"  attribute of an element, and merely by listing.
        /// </summary>
        /// <param name="nameLocatorValue">>This is HTML element locating mechanism through name.</param>
        /// <param name="optionName">This is a name of the drop down option.</param>
        protected void SelectDropDownOptionByName(string nameLocatorValue, string optionName)
        {
            SelectDropDownValueByTagNameOption(By.Name(nameLocatorValue), optionName);
        }

        /// <summary>
        /// Select the option from the given list of options attribute and match with expected option value.
        /// This is done by examing the "option"  attribute of an element, and merely by listing.
        /// </summary>
        /// <param name="cssSelectorLocatorValue">>This is HTML element locating mechanism through css selector.</param>
        /// <param name="optionName">This is a name of the drop down option.</param>
        protected void SelectDropDownOptionByCssSelector(string cssSelectorLocatorValue, string optionName)
        {
            SelectDropDownValueByTagNameOption(By.CssSelector(cssSelectorLocatorValue), optionName);
        }

        #endregion WebDriver Select Element

        #region WebDriver Clear

        /// <summary>
        /// If this element is a text entry element, this will clear the value.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <see cref="ClearText">If this element is a text entry element, this will clear the value.
        /// Has no effect on other elements. Text entry elements are INPUT and TEXTAREA elements.
        /// Note that the events fired by this event may not be as you'd expect. In particular,
        /// we don't fire any keyboard or mouse events. If you want to ensure keyboard events are fired,
        /// consider using something like sendKeys(CharSequence...) with the backspace key.
        /// To ensure you get a change event, consider following with a call to sendKeys(CharSequence...) with the tab key.</see>
        private void ClearText(By by)
        {
            base.WaitForElement(by);
            WebDriver.FindElement(by).Clear();
        }

        /// <summary>
        ///  If this element is a text entry element, this will clear the value by element Id attribute value.
        /// </summary>
        /// <param name="idAttributeValue">This is Id attribute value.</param>
        protected void ClearTextById(String idAttributeValue)
        {
            ClearText(By.Id(idAttributeValue));
        }

        /// <summary>
        /// If this element is a text entry element, this will clear the value by element Xpath value.
        /// </summary>
        /// <param name="xPathValue">This is XPath value.</param>
        public void ClearTextByXPath(String xPathValue)
        {
            ClearText(By.XPath(xPathValue));
        }

        /// <summary>
        /// If this element is a text entry element, this will clear the value by element CssSelector value.
        /// </summary>
        /// <param name="cssSelectorValue">This is CssSelector value.</param>
        protected void ClearTextByCssSelector(String cssSelectorValue)
        {
            ClearText(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// If this element is a text entry element, this will clear the value by element class name attribute value.
        /// </summary>
        /// <param name="classNameAttributeValue">This is class name attribute value.</param>
        protected void ClearTextByClassName(String classNameAttributeValue)
        {
            ClearText(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// If this element is a text entry element, this will clear the value by element name attribute value.
        /// </summary>
        /// <param name="nameAttributeValue">This is name attribute value.</param>
        protected void ClearTextByName(String nameAttributeValue)
        {
            ClearText(By.Name(nameAttributeValue));
        }

        #endregion WebDriver Clear

        #region WebDriver Get Text

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <see cref="GetElementText">Get the visible (i.e. not hidden by CSS) innerText of this element, including sub-elements,
        /// without any leading or trailing whitespace.</see>
        /// <returns>The innerText of this element.</returns>
        private String GetElementText(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).Text;
        }

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element by element id attribute value.
        /// </summary>
        /// <param name="idAttributeValue">Retrieves HTML Element Attribute value by Id.</param>
        /// <returns>The innerText of this element by Id attribute value.</returns>
        protected String GetElementTextById(String idAttributeValue)
        {
            return GetElementText(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element by element Xpath value.
        /// </summary>
        /// <param name="xPathValue">Retrieves HTML Element value by XPath.</param>
        /// <returns>The innerText of this element by xpath value.</returns>
        protected String GetElementTextByXPath(String xPathValue)
        {
            return GetElementText(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element by element Class name attribute value.
        /// </summary>
        /// <param name="classNameAttributeValue">Retrieves HTML Element Attribute value by Class Name.</param>
        /// <returns>The innerText of this element by class name attribute value.</returns>
        protected String GetElementTextByClassName(String classNameAttributeValue)
        {
            return GetElementText(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element by element Link Text attribute value.
        /// </summary>
        /// <param name="linkTextAttributeValue">Retrieves HTML Element Attribute value by Link Text.</param>
        /// <returns>The innerText of this element by Link Text attribute value.</returns>
        protected String GetElementTextByLinkText(String linkTextAttributeValue)
        {
            return GetElementText(By.LinkText(linkTextAttributeValue));
        }

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element by element Partial Link Text attribute value.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">Retrieves HTML Element Attribute value by Partial Link Text.</param>
        /// <returns>The innerText of this element by Partial Link Text attribute value.</returns>
        protected String GetElementTextByPartialLinkText(String partialLinkTextAttributeValue)
        {
            return GetElementText(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element by element Css Selector attribute value.
        /// </summary>
        /// <param name="cssSelectorValue">Retrieves HTML Element Attribute value by Css Selector.</param>
        /// <returns>The innerText of this element by Css Selector attribute value.</returns>
        protected String GetElementTextByCssSelector(String cssSelectorValue)
        {
            return GetElementText(By.CssSelector(cssSelectorValue));
        }

        /// <summary>
        /// Get the visible (i.e. not hidden by CSS) innerText of this element by element Tag Name value.
        /// </summary>
        /// <param name="tagNameValue">Retrieves HTML Element value by Tag Name.</param>
        /// <returns>The innerText of this element by Tag Name value.</returns>
        protected String GetElementTextByTagName(String tagNameValue)
        {
            return GetElementText(By.TagName(tagNameValue));
        }

        /// <summary>
        ///  Retrieves specified inner text occurs within this string by attribute value of the element .
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>Retrieves inner text to find in the specified string.</returns>
        private String GetElementInnerText(By by)
        {
            //Get InnerText for the element based on browser
            string getInnerText = string.Empty;
            switch (Browser)
            {
                // This is for Internet Explorer Browser
                case InternetExplorer:
                    getInnerText = WebDriver.FindElement(by).GetAttribute("innerText");
                    break;
                // This is for Chrome Browser
                case Chrome:
                    getInnerText = WebDriver.FindElement(by).GetAttribute("innerText");
                    break;
                // This is for Firefox Browser
                case FireFox:
                    getInnerText = WebDriver.FindElement(by).Text;
                    break;
            }
            return getInnerText;
        }

        /// <summary>
        ///  Retrieves specified inner text occurs within this string by attribute value of the element saved in Iwebelemnt .
        /// </summary>
        /// <param name="by">This is HTML IWebElement.</param>
        /// <returns>Retrieves inner text to find in the specified string.</returns>
        public String GetWebElementInnerText(IWebElement webElement)
        {
            //Get InnerText for the element based on browser
            string getInnerText = string.Empty;
            switch (Browser)
            {
                // This is for Internet Explorer Browser
                case InternetExplorer:
                    getInnerText = webElement.GetAttribute("innerText");
                    break;
                // This is for Chrome Browser
                case Chrome:
                    getInnerText = webElement.GetAttribute("innerText");
                    break;
                // This is for Firefox Browser
                case FireFox:
                    getInnerText = webElement.Text;
                    break;
            }
            return getInnerText;
        }

        /// <summary>
        ///  Retrieves Html inner text occurs within this string by id attribute value of the element .
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="idAttributeValue">Retrieves HTML Element Attribute value by Id.</param>
        /// <returns>Retrieves Html inner text to find in the specified string.</returns>
        protected String GetElementInnerTextById(String idAttributeValue)
        {
            Thread.Sleep(5000);
            return GetElementInnerText(By.Id(idAttributeValue));
        }

        /// <summary>
        ///  Retrieves Html inner text occurs within this string by XPath attribute value of the element .
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="xPathValue">Retrieves HTML Element Attribute value by XPath.</param>
        /// <returns>Retrieves Html inner text to find in the specified string.</returns>
        protected String GetElementInnerTextByXPath(String xPathValue)
        {
            Thread.Sleep(5000);
            return GetElementInnerText(By.XPath(xPathValue));
        }

        /// <summary>
        ///  Retrieves Html inner text occurs within this string by cssSelector attribute value of the element .
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="cssSelectorValue">Retrieves HTML Element Attribute value by cssSelector.</param>
        /// <returns>Retrieves Html inner text to find in the specified string.</returns>
        protected String GetElementInnerTextByCssSelector(String cssSelectorValue)
        {
            Thread.Sleep(5000);
            return GetElementInnerText(By.CssSelector(cssSelectorValue));
        }

        #endregion WebDriver Get Text

        #region WebDriver Window Handle

        /// <summary>
        /// Close the current window, quitting the browser if it's the last window currently open.
        /// </summary>
        protected void CloseBrowserWindow()
        {
            Thread.Sleep(1000);
            WebDriver.Close();
            Thread.Sleep(1000);
        }

        protected int GetCurrentOpenWindowsCount()
        {
            ReadOnlyCollection<string> windowHandles = WebDriver.WindowHandles;
            return windowHandles.Count();
        }

        #endregion WebDriver Window Handle

        #region Webdriver Element Count

        /// <summary>
        /// Returns the number of nodes that match the specified selector.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The number of nodes that match the specified selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        private int GetElementCount(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElements(by).Count;
        }

        /// <summary>
        /// Returns the number of nodes that match the specified xpath.
        /// </summary>
        /// <param name="xPathValue"> The xpath expression to evaluate.</param>
        /// <returns>The number of nodes that match the specified xpath.</returns>
        protected int GetElementCountByXPath(String xPathValue)
        {
            base.WaitForElement(By.XPath(xPathValue));
            return GetElementCount(By.XPath(xPathValue));
        }

        /// <summary>
        /// Returns the number of nodes that match the specified xpath.
        /// </summary>
        /// <param name="xPathValue"> The xpath expression to evaluate.</param>
        /// <returns>The number of nodes that match the specified xpath.</returns>
        protected int GetElementCountByLinkText(String LinkText)
        {
            base.WaitForElement(By.LinkText(LinkText));
            return GetElementCount(By.LinkText(LinkText));
        }

        /// <summary>
        /// Returns the number of nodes that match the specified css selector
        /// </summary>
        /// <param name="cssSelectorValue"> The css selector expression to evaluate.</param>
        /// <returns>the number of nodes that match the specified css selector.</returns>
        protected int GetElementCountByCssSelector(String cssSelectorValue)
        {
            return GetElementCount(By.CssSelector(cssSelectorValue));
        }

        #endregion Webdriver Element Count

        #region WebDriver Navigate

        /// <summary>
        /// Refresh the current page.
        /// </summary>
        /// <see cref="Navigate">An abstraction allowing the driver to access the
        /// browser's history and to navigate to a given URL.</see>
        /// <seealso cref="Refresh">Refresh the current page.</seealso>
        protected void RefreshTheCurrentPage()
        {
            WebDriver.Navigate().Refresh();
        }

        /// <summary>
        /// Move a page forward in the browser's history.
        /// </summary>
        /// <see cref="Navigate">An abstraction allowing the driver to access the
        /// browser's history and to navigate to a given URL.</see>
        /// <seealso cref="Forward">Move a single "item" forward in the browser's history.
        /// Does nothing if we are on the latest page viewed.</seealso>
        protected void MoveBrowserStepForward()
        {
            WebDriver.Navigate().Forward();
        }

        /// <summary>
        /// Move a page back in the browser's history.
        /// </summary>
        ///  <see cref="Navigate">An abstraction allowing the driver to access the
        /// browser's history and to navigate to a given URL.</see>
        /// <seealso cref="Back">Move back a single "item" in the browser's history.</seealso>
        protected void MoveBrowserStepBackward()
        {
            WebDriver.Navigate().Back();
        }

        /// <summary>
        /// An abstraction allowing the driver to access the browser's
        /// history and to navigate to a given URL.
        /// </summary>
        /// <param name="browseUrl">This is Url to navigate in the browser address bar.</param>
        protected void NavigateToBrowseUrl(String browseUrl)
        {
            WebDriver.Navigate().GoToUrl(browseUrl);
        }

        #endregion WebDriver Navigate

        #region WebDriver Actions

        /// <summary>
        /// Perform Click Action at the current mouse location.
        /// </summary>
        /// <param name="webElement">Element to click.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="Click">Clicks at the current mouse location.</seealso>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformClickAction(IWebElement webElement)
        {
            new Actions(WebDriver).Click(webElement).Perform();
        }

        /// <summary>
        ///  Moves the mouse to the middle of the element.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="MoveToElement">Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.</seealso>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformMoveToElementAction(IWebElement webElement)
        {
            new Actions(WebDriver).MoveToElement(webElement).Perform();
        }

        /// <summary>
        ///  Clicks (without releasing) in the middle of the given element.
        /// </summary>
        /// <param name="webElement">Element to move to and click.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <see cref="ClickAndHold">Clicks (without releasing) in the middle of the given element.
        /// This is equivalent to: Actions.moveToElement(onElement).clickAndHold()</see>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformClickAndHoldAction(IWebElement webElement)
        {
            new Actions(WebDriver).ClickAndHold(webElement).Perform();
        }

        /// <summary>
        ///  Performs a double-click at middle of the given element.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="DoubleClick">Performs a double-click at middle of the given element.
        /// Equivalent to: Actions.moveToElement(element).doubleClick()</seealso>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformDoubleClickAction(IWebElement webElement)
        {
            new Actions(WebDriver).DoubleClick(webElement).Perform();
        }

        /// <summary>
        /// Moves the mouse to the middle of the element.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="MoveToElement">Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// Generates a composite action containinig all actions so far, ready to be performed
        ///  (and resets the internal builder state, so subsequent calls to build() will contain fresh sequences).
        /// A convenience method for performing the actions without calling build() first.</seealso>
        /// <see cref="Build">Generates a composite action containinig all actions so far, ready to be performed
        ///  (and resets the internal builder state, so subsequent calls to build() will contain fresh sequences).</see>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformMouseHoverAction(IWebElement webElement)
        {
            new Actions(WebDriver).MoveToElement(webElement).Build().Perform();
        }

        /// <summary>
        /// Clicks in the middle of the given element.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="MoveToElement">Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// Clicks in the middle of the given element. Equivalent to: Actions.moveToElement(onElement).click()</seealso>
        /// <see cref="Click">Clicks at the current mouse location.</see>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformMouseClickAction(IWebElement webElement)
        {
            try
            {
                new Actions(WebDriver).MoveToElement(webElement).Click().Perform();
            }
            catch (Exception)
            {
                throw new GenericException(String.Format
                    ("Element " + "{0}" + " was clicked but application failed to respond and timedout after {1} seconds.",
                    webElement, Convert.ToInt16(ConfigurationManager.AppSettings["WebDriverTimeOutInSeconds"])));
            }
        }

        /// <summary>
        /// Right Clicks in the middle of the given element.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="MoveToElement">Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// Clicks in the middle of the given element. Equivalent to: Actions.moveToElement(onElement).click()</seealso>
        /// <see cref="Click">Clicks at the current mouse location.</see>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformMouseRightClickAction(IWebElement webElement)
        {
            new Actions(WebDriver).MoveToElement(webElement).ContextClick().Build().Perform();
        }

        /// <summary>
        /// Moves the mouse to the middle of the element and Clicks in the middle of the given element.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="MoveToElement">Moves the mouse to the middle of the element.
        /// The element is scrolled into view and its location is calculated using getBoundingClientRect.
        /// Clicks in the middle of the given element. Equivalent to: Actions.moveToElement(onElement).click()</seealso>
        /// <see cref="Click">Clicks at the current mouse location.</see>
        /// <see cref="Build">Generates a composite action containinig all actions so far, ready to be performed
        ///  (and resets the internal builder state, so subsequent calls to build() will contain fresh sequences).</see>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformMoveToElementClickAction(IWebElement webElement)
        {
            new Actions(WebDriver).MoveToElement(webElement).Click().Build().Perform();
        }

        /// <summary>
        /// A convenience method that performs click-and-hold at the location of the source element,
        /// moves to the location of the target element, then releases the mouse.
        /// </summary>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <param name="sourceElementLocation">element to emulate button down at.</param>
        /// <param name="targetElementLocation">element to move to and release the mouse at.</param>
        protected void PerformDragAndDropAction(IWebElement sourceElementLocation,
            IWebElement targetElementLocation)
        {
            new Actions(WebDriver).DragAndDrop(sourceElementLocation, targetElementLocation).Perform();
        }

        /// <summary>
        /// A convenience method that performs click-and-hold at the location of the source element,
        /// moves to the location of the target element based on offset, then releases the mouse.
        /// </summary>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <param name="sourceElementLocation">element to emulate button down at.</param>
        /// <param name="offsetX">horizontal move offset.</param>
        /// <param name="offsetY">vertical move offset.</param>
        protected void PerformDragAndDropToOffset(IWebElement sourceElementLocation, int offsetX, int offsetY)
        {
            new Actions(WebDriver).DragAndDropToOffset(sourceElementLocation, offsetX, offsetY).Perform();
        }

        /// <summary>
        /// Use this Method to Place Cursor to Specified Position.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <param name="xCoordinateValue">This is X Coordinate Value Where Cursor need to Place.</param>
        /// <param name="yCoordinateValue">This is Y Coordinate Value Where Cursor need to Place.</param>
        protected void PlaceCursorPosition(IWebElement webElement, int xCoordinateValue, int yCoordinateValue)
        {
            new Actions(WebDriver).MoveToElement(webElement, xCoordinateValue, yCoordinateValue).Click().Perform();
        }

        /// <summary>
        ///To select the text based on X and Y co-ordinates.
        /// </summary>
        /// <param name="webElement">Element to move.</param>
        /// <param name="xCoordinateStartValue">This is X Coordinate Value Where Cursor need to Place.</param>
        /// <param name="yCoordinateStartValue">This is Y Coordinate Value Where Cursor need to Place.</param>
        /// <param name="offsetX">This is X Coordinate Value Where Cursor need to End.</param>
        /// <param name="offsetY">This is Y Coordinate Value Where Cursor need to End.</param>
        protected void SelectTextUsingCoordinates(IWebElement webElement, int xCoordinateStartValue,
            int yCoordinateStartValue, int offsetX, int offsetY)
        {
            Actions actions = new Actions(WebDriver);
            actions.MoveToElement(webElement, xCoordinateStartValue, yCoordinateStartValue)
                .ClickAndHold()
                .MoveByOffset(offsetX, offsetY)
                .Release()
                .Perform();
        }

        /// <summary>
        /// Releases the depressed left mouse button at the current mouse location.
        /// </summary>
        /// <see cref="Actions">The user-facing API for emulating complex user gestures.
        /// Use this class rather than using the Keyboard or Mouse directly. Implements the builder pattern:
        /// Builds a CompositeAction containing all actions specified by the method calls.</see>
        /// <seealso cref="Release">Releasing an element after clicking and holding it.</seealso>
        /// <see cref="Perform">A convenience method for performing the actions without calling build() first.</see>
        protected void PerformReleaseAction()
        {
            new Actions(WebDriver).Release().Perform();
        }

        /// <summary>
        /// Performs Drag and Drop of a Webelement into another.
        /// </summary>
        /// <param name="destination">Element to be dragged.</param>
        /// <param name="target">Element to be dropped at.</param>
        protected void DragAndDropWebElement(IWebElement destination, IWebElement target)
        {
            new Actions(WebDriver).ClickAndHold(destination).MoveToElement(target).Release(target).Build().Perform();
        }

        /// <summary>
        /// Select multiple elements using Shift key.
        /// </summary>
        /// <param name="keyValue">This is the key name.</param>
        /// <param name="element1">First element to be selected.</param>
        /// <param name="element2">Second element to be selected.</param>
        protected void SelectMultipleElementsUsingShift(String keyValue, IWebElement element1, IWebElement element2)
        {
            new Actions(WebDriver).KeyDown(keyValue).Click(element1).Click(element2).KeyUp(keyValue).Perform();
        }

        /// <summary>
        /// Perform key up.
        /// </summary>
        /// <param name="keyValue">This is a key name to perform up action.</param>
        /// <see cref="KeyUp">This is a key event to perform key up.</see>
        /// <see cref="Actions">Interface representing a single user-interaction action.</see>/>
        /// <see cref="Perform">User-interaction action.</see>/>
        protected void PerformKeyUp(String keyValue)
        {
            new Actions(WebDriver).KeyUp(keyValue).Perform();
        }

        #endregion WebDriver Actions

        #region WebDriver IJavaScriptExecutor

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an click function.
        /// </summary>
        /// <param name="iWebElement">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page will be performed through this interface.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void ClickByJavaScriptExecutor(IWebElement iWebElement)
        {
            try
            {
                ((IJavaScriptExecutor)WebDriver).ExecuteScript("arguments[0].click();", iWebElement);
            }
            catch (Exception)

            {
                throw new GenericException(String.Format
                    ("Element " + "{0}" + " was clicked using java script executor but application failed to respond and timedout after {1} seconds.",
                    iWebElement, Convert.ToInt16(ConfigurationManager.AppSettings["WebDriverTimeOutInSeconds"])));
            }
        }

        /// <summary>
        /// Performs double click on a web element when the browser is 'Chrome' or 'Firefox'.
        /// </summary>
        /// <param name="iWebElemnt">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page will be performed through this interface.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void DoubleClickByJavaScriptExecuter(IWebElement iWebElemnt)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("var evt = document.createEvent('MouseEvents');" +
        "evt.initMouseEvent('dblclick',true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0,null);" +
        "arguments[0].dispatchEvent(evt);", iWebElemnt);
        }

        /// <summary>
        /// Performs double click on a web element when the browser is 'IE'.
        /// </summary>
        /// <param name="iWebElement">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page will be performed through this interface.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void DoubleClickInIeByJavaScriptExecuter(IWebElement iWebElement)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("arguments[0].fireEvent('ondblclick');", iWebElement);
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an fill text
        /// value function by locating element Id.
        /// </summary>
        /// <param name="iWebElementId">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page by locating
        /// Id will be performed through this interface.</param>
        /// <param name="textValue">This Text Vaule.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the
        /// currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void FillTextByIdThroughJavaScriptExecutor
            (IWebElement iWebElementId, String textValue)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript(String.Format("document.getElementById('{0}').value='{1}'", iWebElementId, textValue));
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an fill text value function by locating element class name.
        /// </summary>
        /// <param name="iWebElementName">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page b locating name will be performed through this interface.</param>
        /// <param name="textValue">This is Text Value.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void FillTextByNameThroughJavaScriptExecutor
            (IWebElement iWebElementName, String textValue)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript(String.Format("document.getElementsByName('{0}').value='{1}'", iWebElementName, textValue));
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an fill text value function by locating element class name.
        /// </summary>
        /// <param name="iWebElementClassName">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page b locating class name will be performed through this interface.</param>
        /// <param name="textValue">This is Text Value.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void FillTextByClassNameThroughJavaScriptExecutor
            (IWebElement iWebElementClassName, String textValue)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript(string.Format(
                "document.getElementsByClassName('{0}').value='{1}'",
                iWebElementClassName, textValue));
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an get window title function.
        /// </summary>
        /// <returns>Window Title.</returns>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected string GetWindowTitleByJavaScriptExecutor()
        {
            return (string)((IJavaScriptExecutor)WebDriver).ExecuteScript("return document.title");
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an close browser window function.
        /// </summary>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void CloseBrowserPopUpByJavaScriptExecutor()
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("window.onbeforeunload = function(e){};");
        }

        /// <summary>
        /// Refresh the current selected iframe.
        /// </summary>
        /// <param name="iFrameName">This is the name of the Iframe.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void RefreshIFrameByJavaScriptExecutor
            (String iFrameName)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript(string.Format("document.getElementById('{0}').src = " +
                                                                         "document.getElementById('{0}').src", iFrameName));
        }

        /// <summary>
        /// Scroll Web Element by JavaScript.
        /// </summary>
        /// <param name="webElement">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page will be performed through this interface.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void ScrollElementByJavaScriptExecutor
            (IWebElement webElement)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
        }

        /// <summary>
        /// Hover the mouse on the element.
        /// </summary>
        /// <param name="webElement">Represents an HTML element. Generally,
        /// all interesting operations to do with interacting with a page will be performed through this interface.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void PerformMouseHoverByJavaScriptExecutor(IWebElement webElement)
        {
            const String javaScript = "var evObj = document.createEvent('MouseEvents');" +
                                      "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                                      "arguments[0].dispatchEvent(evObj);";
            IJavaScriptExecutor javaScriptExecutor = WebDriver as IJavaScriptExecutor;
            javaScriptExecutor.ExecuteScript(javaScript, webElement);
        }

        /// <summary>
        /// Get Current Browser Information.
        /// </summary>
        /// <returns>Browser Information.</returns>
        protected String GetCurrentBrowserInformationByJavaScriptExecutor()
        {
            //Get Browser Information
            String getBrowserInfo = (String)((IJavaScriptExecutor)WebDriver).
                ExecuteScript("return navigator.userAgent;");
            return getBrowserInfo;
        }

        /// <summary>
        /// Hover the mouse on the element.
        /// </summary>
        /// <param name="webElement">Element to move to.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void MouseOverByJavaScriptExecutor(IWebElement webElement)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("return $(arguments[0]).mouseover();", webElement);
        }

        /// <summary>
        /// Use this Method to Fill Text to Inner HTML of Text Area Using Javascript.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <param name="textToFill">This is Text to Fill in the Text Area.</param>
        private void FillTexttoInnerHtmlByJavaScriptExecutor(By by, String textToFill)
        {
            base.WaitForElement(by);
            IWebElement iWebElementName = WebDriver.FindElement(by);
            ((IJavaScriptExecutor)WebDriver).ExecuteScript(String.Format("arguments[0].innerHTML = '{0}'", textToFill), iWebElementName);
        }

        /// <summary>
        /// Use this Method to Fill text in Inner HTML Property of Text Area Using Xpath Attribute Value Through Javascript.
        /// </summary>
        /// <param name="xPathAttributeValue">This is Xpath Attribute Value.</param>
        /// <param name="textValue">This is Text Value to Fill.</param>
        protected void FillTextToInnerHtmlByXpathFillTexttoInnerHtmlThroughJavaScriptExecutor
            (String xPathAttributeValue, String textValue)
        {
            FillTexttoInnerHtmlByJavaScriptExecutor(By.XPath(xPathAttributeValue), textValue);
        }

        /// <summary>
        /// Use this Method to Fill text in Inner HTML Property of Text Area Using Xpath Attribute Value Through Javascript.
        /// </summary>
        /// <param name="xPathAttributeValue">This is Xpath Attribute Value.</param>
        /// <param name="textValue">This is Text Value to Fill.</param>
        protected void FillTextToInnerHtmlByCssFillTexttoInnerHtmlThroughJavaScriptExecutor
            (String cssAttributeValue, String textValue)
        {
            FillTexttoInnerHtmlByJavaScriptExecutor(By.CssSelector(cssAttributeValue), textValue);
        }

        #endregion WebDriver IJavaScriptExecutor

        #region WebDriver Keys

        /// <summary>
        /// Press Enter Key.
        /// </summary>
        /// <param name="by">Provides a mechanism by which to find elements within a document.</param>
        /// <see cref="Keys">Representations of pressable keys that are not
        /// text keys for sending to the browser.</see>
        /// <seealso cref="Keys.Enter">Represents the Enter key.</seealso>
        private void PressEnterKey(By by)
        {
            base.WaitForElement(by);
            WebDriver.FindElement(by).SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Press Enter Key By locating element ID attribute value.
        /// </summary>
        /// <param name="idAttributeValue">This is HTML Element ID Attribute value.</param>
        protected void PressEnterKeyById(String idAttributeValue)
        {
            PressEnterKey(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Press Enter Key By locating element class name attribute value.
        /// </summary>
        /// <param name="classNameAttributeValue">This is HTML Element Class Name Attribute value.</param>
        protected void PressEnterKeyByClassName(String classNameAttributeValue)
        {
            PressEnterKey(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Press Enter Key By locating element Xpath attribute value.
        /// </summary>
        /// <param name="xPathAttributeValue">This is HTML Element XPath Attribute value.</param>
        protected void PressEnterKeyByXPath(String xPathAttributeValue)
        {
            PressEnterKey(By.XPath(xPathAttributeValue));
        }

        /// <summary>
        /// Press Enter Key By locating element CssSelector attribute value.
        /// </summary>
        /// <param name="cssSelectorAttributeValue">This is HTML Element CssSelector Attribute value.</param>
        protected void PressEnterKeyByCssSelector(String cssSelectorAttributeValue)
        {
            PressEnterKey(By.CssSelector(cssSelectorAttributeValue));
        }

        /// <summary>
        /// Sends the given keys to the active application.
        /// </summary>
        /// <param name="keyValue">The string of keystrokes to send.</param>
        /// <see cref="SendWait">Provides methods for sending keystrokes to an application.</see>
        protected void PressKey(String keyValue)
        {
            SendKeys.SendWait(keyValue);
        }

        /// <summary>
        /// Press Ctrl+A by locating element.
        /// </summary>
        /// <param name="iWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</param>
        protected void PressCtrlAKey(IWebElement iWebElement)
        {
            // ASCII code 1 for Ctrl-A
            const char character = '\u0001';
            iWebElement.SendKeys(Convert.ToString(character));
        }

        /// <summary>
        /// Press Ctrl+B by locating element.
        /// </summary>
        /// <param name="iWebElement">The IWebElement interface represents an HTML element.
        /// Generally, all interesting operations to do with interacting with a page will be performed through this interface.</param>
        /// <see cref="KeyDown">This is a key event to perform key down.</see>
        /// <see cref="Actions">Interface representing a single user-interaction action.</see>/>
        /// <see cref="SendKeys">This method to simulate typing into an element,
        /// which may set its value.</see>
        /// <see cref="Perform">User-interaction action.</see>/>
        protected void PressCtrlBKey(IWebElement iWebElement)
        {
            // ASCII code 1 for Ctrl-B
            Actions builder = new Actions(WebDriver);
            const char character = '\u0062';
            iWebElement.SendKeys(Convert.ToString(character));
            builder.KeyDown(Keys.Control).SendKeys(Convert.ToString(character)).Perform();
        }

        /// <summary>
        /// Perform key down and then press alternate key.
        /// </summary>
        /// <param name="keyDownName">This is a key event to perform key down.</param>
        /// <param name="pressKeyName">This is a key event to perform key press after key down.</param>
        /// <param name="moveTimes">This is number of times to perform action.</param>
        /// <see cref="Keys">Representations of pressable keys that aren't text. </see>/>
        /// <see cref="Actions">Interface representing a single user-interaction action.</see>/>
        /// <see cref="SendKeys">This method to simulate typing into an element,
        /// which may set its value.</see>
        /// <see cref="Perform">User-interaction action.</see>/>
        protected void PerformKeyDownThenPressKeyToElement(string keyDownName, string pressKeyName, int moveTimes)
        {
            for (int move = 1; move <= moveTimes; move++)
            {
                new Actions(WebDriver).KeyDown(keyDownName).SendKeys(pressKeyName).Perform();
            }
            new Actions(WebDriver).KeyUp(keyDownName).Perform();
        }

        /// <summary>
        /// Perform control key down.
        /// </summary>
        /// <see cref="KeyDown">This is a key event to perform key down.</see>
        /// <see cref="Keys">Representations of pressable keys that aren't text. </see>/>
        /// <see cref="Actions">Interface representing a single user-interaction action.</see>/>
        /// <see cref="SendKeys">This method to simulate typing into an element,
        /// which may set its value.</see>
        /// <see cref="Perform">User-interaction action.</see>/>
        protected void PerformControlKeyDown()
        {
            new Actions(WebDriver).KeyDown(Keys.Control).Perform();
        }

        /// <summary>
        /// Perform control key up.
        /// </summary>
        /// <see cref="KeyUp">This is a key event to perform key up.</see>
        /// <see cref="Keys">Representations of pressable keys that aren't text. </see>/>
        /// <see cref="Actions">Interface representing a single user-interaction action.</see>/>
        /// <see cref="SendKeys">This method to simulate typing into an element,
        /// which may set its value.</see>
        /// <see cref="Perform">User-interaction action.</see>/>
        protected void PerformControlKeyUp()
        {
            new Actions(WebDriver).KeyUp(Keys.Control).Perform();
        }

        #endregion WebDriver Keys

        #region WebDriver Alert

        /// <summary>
        /// Accepts the alert.
        /// </summary>
        /// <see cref="IAlert">Defines the interface through which the user
        /// can manipulate JavaScript alerts.</see>
        protected void AcceptAlert()
        {
            int timeOut = -1;
            //Wait For Element
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeOut));
                IAlert alert = wait.Until(drv => drv.SwitchTo().Alert());
                alert.Accept();
            }
            //Exception Handling
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Dismisses the alert.
        /// </summary>
        /// <see cref="IAlert">Defines the interface through which the user
        /// can manipulate JavaScript alerts.</see>
        protected void DismissAlert()
        {
            int timeOut = -1;
            //Wait For Element
            if (timeOut == -1)
            {
                timeOut = this._waitTimeOut;
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeOut));
                IAlert alert = wait.Until(drv => drv.SwitchTo().Alert());
                alert.Dismiss();
            }
            //Exception Handling
            catch (Exception)
            {
                throw;
            }
        }

        #endregion WebDriver Alert

        #region WebDriver Manage

        /// <summary>
        /// Delete all the cookies for the current domain.
        /// </summary>
        protected void DeleteAllBrowserCookies()
        {
            WebDriver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        ///  Delete a cookie from the browser's "cookie". The domain of the cookie will be ignored.
        /// </summary>
        /// <param name="cookieName">This is name of cookie.</param>
        protected void DeleteBrowserCookie(Cookie cookieName)
        {
            WebDriver.Manage().Cookies.DeleteCookie(cookieName);
        }

        /// <summary>
        /// Maximizes the current window if it is not already maximized.
        /// </summary>
        protected void MaximizeWindow()
        {
            WebDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Sets the amount of time to wait for a page load to complete before throwing an error.
        /// </summary>
        /// <param name="timeInSeconds">The amount of time to wait.</param>
        protected void SetPageLoadWaitTime(int timeInSeconds)
        {
            WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeInSeconds);
        }

        /// <summary>
        /// Specifies the amount of time the driver should wait when searching for an element if it is not immediately present.
        /// </summary>
        /// <param name="timeInSeconds">The amount of time to wait.</param>
        /// /// <exception cref="NoSuchElementException">When searching for a single element, the driver should poll the page
        /// until the element has been found, or this timeout expires.</exception>
        protected void SetImplicitWaitTime(int timeInSeconds)
        {
            WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeInSeconds);
        }

        /// <summary>
        /// Set the size of the current window. This will change the outer window dimension,
        /// not just the view port, synonymous to window.resizeTo() in JS.
        /// </summary>
        /// <param name="width">The target width size.</param>
        /// <param name="height">The target height size.</param>
        /// <see cref="https://msdn.microsoft.com/en-us/library/system.drawing.size(v=vs.110).aspx"></see>
        /// <remarks>Stores an ordered pair of integers, which specify a Height and Width.</remarks>
        protected void SetWindowSize(int width, int height)
        {
            WebDriver.Manage().Window.Size = new Size(width, height);
        }

        /// <summary>
        /// Set the position of the current window. This is relative to the upper
        /// left corner of the screen, synonymous to window.moveTo() in JS.
        /// </summary>
        /// <param name="coordinateX">The target x coordinate position of the window.</param>
        /// <param name="coordinateY">The target y coordinate position of the window.</param>
        /// <see cref="https://msdn.microsoft.com/en-us/library/system.drawing.point%28v=vs.110%29.aspx"></see>
        /// <remarks>Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.</remarks>
        protected void SetWindowPosition(int coordinateX, int coordinateY)
        {
            WebDriver.Manage().Window.Position = new Point(coordinateX, coordinateY);
        }

        #endregion WebDriver Manage

        #region WebDriverSystemClock

        /// <summary>
        /// Get the computes a point of time in the future.
        /// </summary>
        /// <param name="durationInMillis">The point in the future, in milliseconds
        /// relative to the current time.</param>
        /// <returns>A timestamp representing a point in the future.</returns>
        /// <see cref="SystemClock">org.openqa.selenium.support.ui.SystemClock</see>/>
        protected DateTime GetLaterByTime(long durationInMillis)
        {
            SystemClock systemClock = new SystemClock();
            return systemClock.LaterBy(new TimeSpan(durationInMillis));
        }

        /// <summary>
        /// Tests if a point in time occurs before the current time.
        /// </summary>
        /// <param name="endInMillis">The timestamnp to check.</param>
        /// <returns>Whether the given timestamp represents a point in time before the current time.</returns>
        /// <see cref="SystemClock">org.openqa.selenium.support.ui.SystemClock</see>/>
        protected Boolean IsNowBeforeTime(long endInMillis)
        {
            SystemClock systemClock = new SystemClock();
            return systemClock.IsNowBefore(new DateTime(endInMillis));
        }

        #endregion WebDriverSystemClock

        #region Cross Browser

        /// <summary>
        /// Get the top position for an DOM-element based on the selector.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>Left position of the DOM-element based on the selector.</returns>
        private Double GetElementPositionTop(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).Location.X;
        }

        /// <summary>
        /// Get the top position for an DOM-element based on the id selector.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id attribute value.</param>
        /// <returns>Top position of the DOM-element based on the id selector.</returns>
        protected Double GetElementPositionTopById(String idAttributeValue)
        {
            return GetElementPositionTop(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the top position for an DOM-element based on the xpath selector.
        /// </summary>
        /// <param name="XPathValue">This is the xpath value.</param>
        /// <returns>Top position of the DOM-element based on the xpath selector.</returns>
        protected Double GetElementPositionTopByXPath(String XPathValue)
        {
            return GetElementPositionTop(By.XPath(XPathValue));
        }

        /// <summary>
        /// Get the left position for an DOM-element based on the selector.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>Left position of the DOM-element based on the selector.</returns>
        private Double GetElementPositionLeft(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).Location.Y;
        }

        /// <summary>
        /// Get the left position for an DOM-element based on the id selector.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id attribute value.</param>
        /// <returns>>Left position of the DOM-element based on the id selector.</returns>
        protected Double GetElementPositionLeftById(String idAttributeValue)
        {
            return GetElementPositionLeft(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the left position for an DOM-element based on the xpath selector.
        /// </summary>
        /// <param name="XPathValue">This is the xpath value.</param>
        /// <returns>>Left position of the DOM-element based on the xpath selector.</returns>
        protected Double GetElementPositionLeftByXPath(String XPathValue)
        {
            return GetElementPositionLeft(By.XPath(XPathValue));
        }

        /// <summary>
        /// Get the height for an DOM-element based on the selector.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>Height of the DOM-element based on the selector.</returns>
        private Double GetElementHeight(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).Size.Height;
        }

        /// <summary>
        /// Get the width for an DOM-element based on the id selector.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id attribute value.</param>
        /// <returns>Height of the DOM-element based on the id selector.</returns>
        protected Double GetElementHeightById(String idAttributeValue)
        {
            return GetElementHeight(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the width for an DOM-element based on the xpath selector.
        /// </summary>
        /// <param name="xPathValue">This is the xpath value.</param>
        /// <returns>Height of the DOM-element based on the xpath selector.</returns>
        protected Double GetElementHeightByXPath(String xPathValue)
        {
            return GetElementHeight(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the width for an DOM-element based on the selector.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>Width of the DOM-element based on the selector.</returns>
        private Double GetElementWidth(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElement(by).Size.Width;
        }

        /// <summary>
        /// Get the width for an DOM-element based on the id selector.
        /// </summary>
        /// <param name="idAttributeValue">This is the Id attribute value.</param>
        /// <returns>Width of the DOM-element based on the id selector.</returns>
        protected Double GetElementWidthById(String idAttributeValue)
        {
            return GetElementWidth(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Get the width for an DOM-element based on the xpath selector.
        /// </summary>
        /// <param name="xPathValue">This is the xpath value.</param>
        /// <returns>Width of the DOM-element based on the xpath selector.</returns>
        protected Double GetElementWidthByXPath(String xPathValue)
        {
            return GetElementWidth(By.XPath(xPathValue));
        }

        /// <summary>
        /// Get the size of the current window. This will return the outer window dimension, not just the view port.
        /// </summary>
        /// <returns>Size of the current window in dimesion width and height.</returns>
        protected Size GetCurrentOpenWindowSize()
        {
            return WebDriver.Manage().Window.Size;
        }

        #endregion Cross Browser

        #region WebDriver FindElements

        /// <summary>
        /// Find all elements within the current context using the given mechanism.
        /// </summary>
        /// <param name="by">This is HTML element locating mechanism to use.</param>
        /// <returns>The number of webelements that match the specified selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        private ICollection<IWebElement> GetWebElementsCollection(By by)
        {
            base.WaitForElement(by);
            return WebDriver.FindElements(by);
        }

        /// <summary>
        /// Find all elements within the current context that match the specified XPath selector.
        /// </summary>
        /// <param name="xPathValue">Locates elements via XPath.</param>
        /// <returns>The number of webelements that match the specified XPath selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        /// <see cref="by">This is HTML element locating mechanism to use.</see>
        protected ICollection<IWebElement> GetWebElementsCollectionByXPath(String xPathValue)
        {
            return GetWebElementsCollection(By.XPath(xPathValue));
        }

        /// <summary>
        /// Find all elements within the current context that match the specified Id selector.
        /// </summary>
        /// <param name="idAttributeValue">Locates elements by the value of the "Id" attribute.</param>
        /// <returns>The number of webelements that match the specified Id selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        /// <see cref="by">This is HTML element locating mechanism to use.</see>
        protected ICollection<IWebElement> GetWebElementsCollectionById(String idAttributeValue)
        {
            return GetWebElementsCollection(By.Id(idAttributeValue));
        }

        /// <summary>
        /// Find all elements within the current context that match the specified Name selector.
        /// </summary>
        /// <param name="nameAttributeValue">Locates elements by the value of the "Name" attribute.</param>
        /// <returns>The number of webelements that match the specified Name selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        /// <see cref="by">This is HTML element locating mechanism to use.</see>
        protected ICollection<IWebElement> GetWebElementsCollectionByName(String nameAttributeValue)
        {
            return GetWebElementsCollection(By.Name(nameAttributeValue));
        }

        /// <summary>
        /// Find all elements within the current context that match the specified ClassName selector.
        /// </summary>
        /// <param name="classNameAttributeValue">Locates elements by the value of the "ClassName" attribute.</param>
        /// <returns>The number of webelements that match the specified ClassName selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        /// <see cref="by">This is HTML element locating mechanism to use.</see>
        protected ICollection<IWebElement> GetWebElementsCollectionByClassName(String classNameAttributeValue)
        {
            return GetWebElementsCollection(By.ClassName(classNameAttributeValue));
        }

        /// <summary>
        /// Find all elements within the current context that match the specified LinkText selector.
        /// </summary>
        /// <param name="linkTextAttributeValue">Locates elements by the value of the "LinkText" attribute.</param>
        /// <returns>The number of webelements that match the specified LinkText selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        /// <see cref="by">This is HTML element locating mechanism to use.</see>
        protected ICollection<IWebElement> GetWebElementsCollectionByLinkText(String linkTextAttributeValue)
        {
            return GetWebElementsCollection(By.LinkText(linkTextAttributeValue));
        }

        /// <summary>
        /// Find all elements within the current context that match the specified PartialLinkText selector.
        /// </summary>
        /// <param name="partialLinkTextAttributeValue">Locates elements by the value of the "PartialLinkText" attribute.</param>
        /// <returns>The number of webelements that match the specified PartialLinkText selector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        /// <see cref="by">This is HTML element locating mechanism to use.</see>
        protected ICollection<IWebElement> GetWebElementsCollectionByPartialLinkText(String partialLinkTextAttributeValue)
        {
            return GetWebElementsCollection(By.PartialLinkText(partialLinkTextAttributeValue));
        }

        /// <summary>
        /// Find all elements within the current context that match the specified CssSelector.
        /// </summary>
        /// <param name="cssSelectorAttributeValue">Locates elements by the value of the "CssSelector" attribute.</param>
        /// <returns>The number of webelements that match the specified CssSelector.</returns>
        /// <see cref="FindElements">Finds all IWebElements within the current context using the given mechanism.</see>
        /// <see cref="by">This is HTML element locating mechanism to use.</see>
        protected ICollection<IWebElement> GetWebElementsCollectionByCssSelector(String cssSelectorAttributeValue)
        {
            return GetWebElementsCollection(By.CssSelector(cssSelectorAttributeValue));
        }

        #endregion WebDriver FindElements

        #region WebDriver Wait

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an click function.
        /// </summary>
        /// <param name="timeoutSecs">Represents an time to wait for the ajax calls to complete.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        ///

        protected void WaitForAjaxToComplete(int waitTimeOutDuration = -1)
        {
            var stopWatch = new Stopwatch();
            if (waitTimeOutDuration == -1)
            {
                waitTimeOutDuration = this._waitTimeOut;
            }
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < waitTimeOutDuration)
                {
                    var ajaxIsComplete = (bool)(WebDriver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                    if (ajaxIsComplete)
                    {
                        break;
                    }
                }
            }
            //Exception Handling
            catch (Exception ex)
            {
                stopWatch.Stop();
                throw ex;
            }
            stopWatch.Stop();
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an click function.
        /// </summary>
        /// <param name="timeoutSecs">Represents an time to wait for the document load to complete.</param>
        /// <see cref="ExecuteScript">Executes JavaScript in the context of the currently selected frame or window.
        /// The script fragment provided will be executed as the body of an anonymous function.</see>
        /// <seealso cref="IJavaScriptExecutor">Indicates that a driver can execute JavaScript, providing
        /// access to the mechanism to do so.</seealso>
        protected void WaitForDocumentLoadToComplete(int waitTimeOutDuration = -1)
        {
            var stopWatch = new Stopwatch();
            if (waitTimeOutDuration == -1)
            {
                waitTimeOutDuration = this._waitTimeOut;
            }
            try
            {
                while (stopWatch.Elapsed.TotalSeconds < waitTimeOutDuration)
                {
                    var documentLoadIsComplete = (bool)(WebDriver as IJavaScriptExecutor).ExecuteScript("return document.readyState == 'complete'");
                    if (documentLoadIsComplete)
                    {
                        break;
                    }
                }
            }
            //Exception Handling
            catch (Exception ex)
            {
                stopWatch.Stop();
                throw ex;
            }
            stopWatch.Stop();
        }

        protected IWebElement WaitTillElementFound(By IWebElement, int waitTimeOutDuration = -1)
        {
            IWebElement myDynamicElement = null;
            var stopWatch = new Stopwatch();
            if (waitTimeOutDuration == -1)
            {
                waitTimeOutDuration = this._waitTimeOut;
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTimeOutDuration));
                myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(IWebElement));
            }
            //Exception Handling
            catch (Exception ex)
            {
                stopWatch.Stop();
                throw ex;
            }
            stopWatch.Stop();
            return myDynamicElement;
        }

        protected IWebElement FindElementTill(By by, int waitTimeOutDuration = -1)
        {
            IWebElement myDynamicElement = null;
            var stopWatch = new Stopwatch();
            if (waitTimeOutDuration == -1)
            {
                waitTimeOutDuration = this._waitTimeOut;
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTimeOutDuration));
                myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(by));
            }
            //Exception Handling
            catch (Exception ex)
            {
                stopWatch.Stop();
                throw ex;
            }
            stopWatch.Stop();
            return myDynamicElement;
        }

        #endregion WebDriver Wait
    }
}