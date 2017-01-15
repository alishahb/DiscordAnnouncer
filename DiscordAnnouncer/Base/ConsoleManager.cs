using System.Runtime.InteropServices;
using Alisha;

namespace Alisha.DiscordAnnouncer.Base
{
    public class ConsoleManager
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public ConsoleManager()
        {
            AllocConsole();
        }
    }
}