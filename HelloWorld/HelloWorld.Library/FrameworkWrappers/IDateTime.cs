using System;

namespace HelloWorld.Library.FrameworkWrappers
{
    /// <summary>
    ///     Wraps the DateTime structure
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        ///     Gets the DateTime as of Now
        /// </summary>
        /// <returns>A DateTime object containing the date and time of Now</returns>
        DateTime Now();
    }
}
