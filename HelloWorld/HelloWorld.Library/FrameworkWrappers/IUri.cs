using System;

namespace HelloWorld.Library.FrameworkWrappers
{
    /// <summary>
    ///     Wraps the System.Uri class
    /// </summary>
    public interface IUri
    {
        /// <summary>
        ///     Creates a Uri based on the specified Uri string
        /// </summary>
        /// <param name="uriString">The Uri string</param>
        /// <returns>A DateTime object containing the date and time of Now</returns>
        Uri GetUri(string uriString);
    }
}
