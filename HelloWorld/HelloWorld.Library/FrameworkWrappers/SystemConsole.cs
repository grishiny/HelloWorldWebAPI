using System;

namespace HelloWorld.Library.FrameworkWrappers
{
    /// <summary>
    ///     Class for wrapping System.Console
    /// </summary>
    public class SystemConsole : IConsole
    {
        /// <summary>
        ///     Writes to the Console
        /// </summary>
        /// <param name="message">Message to write</param>
        public void Write(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        ///     Writes a line to the Console
        /// </summary>
        /// <param name="message">Message to write</param>
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        ///     Writes to the Console.Error (standard error)
        /// </summary>
        /// <param name="message">Message to write</param>
        public void ErrorWrite(string message)
        {
            Console.Error.Write(message);
        }

        /// <summary>
        ///     Writes a line to the Console.Error (standard error)
        /// </summary>
        /// <param name="message">Message to write</param>
        public void ErrorWriteLine(string message)
        {
            Console.Error.WriteLine(message);
        }
    }
}
