using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace EPS.Automation
{

    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        /// <summary>
        /// Generating a screen capture when using RemoteWebDriver.
        /// </summary>
        /// <param name="remoteAdress">Gets the remote address to which the request channel sends messages.</param>
        /// <param name="capabilities">Object containing the desired capabilities of the browser.</param>
        public ScreenShotRemoteWebDriver(Uri remoteAddress, DriverOptions options, TimeSpan commandTimeout)
        : base(remoteAddress, options)
        {
        }

        /// <summary>
        /// Gets a <see cref="Screenshot"/> object representing the image of the page on the screen.
        /// </summary>
        /// <returns> screenshot<see cref="Screenshot"/> object containing the image.</returns>
        public new Screenshot GetScreenshot()
        {
            // Get the screenshot as base64.
            Response screenshotResponse = Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();
            return new Screenshot(base64);
        }
    }
}