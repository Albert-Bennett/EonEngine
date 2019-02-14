using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EonEngineTool.Lib
{
    /// <summary>
    /// Used to help preform processes.
    /// </summary>
    public static class ProcessHelper
    {
        /// <summary>
        /// Used to start a process.
        /// </summary>
        /// <param name="processFilepath">The filepath of the .exe file.</param>
        /// <param name="hidden">Wheather or not the process should be hidden.</param>
        public static void StartProcess(string processFilepath, bool hidden)
        {
            ProcessStartInfo start = new ProcessStartInfo(processFilepath);

            string arguments = "";
            start.Arguments = arguments;

            if (hidden)
                start.WindowStyle = ProcessWindowStyle.Hidden;
            else
                start.WindowStyle = ProcessWindowStyle.Normal;

            using (Process process = Process.Start(start))
            {
                process.WaitForExit();

                int exitCode = process.ExitCode;
            }
        }

        /// <summary>
        /// Used to start a process inside of a container control.
        /// </summary>
        /// <param name="processFilepath">The filepath for the process.</param>
        /// <param name="containerHandle">The handle for the container.</param>
        public static void StartProcessInsideOf(string processFilepath, IntPtr containerHandle)
        {
            ProcessStartInfo start = new ProcessStartInfo(processFilepath);

            string arguments = "";
            start.Arguments = arguments;

            using (Process process = Process.Start(start))
            {
                process.WaitForInputIdle();
                SetParent(process.MainWindowHandle, containerHandle);

                process.WaitForExit();
                int exitCode = process.ExitCode;
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
