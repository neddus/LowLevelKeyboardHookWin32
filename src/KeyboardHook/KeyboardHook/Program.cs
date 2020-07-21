using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KeyboardHook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
                Application.Exit();
            };

            AppDomain.CurrentDomain.ProcessExit += (object sender, EventArgs e) =>
            {
                Console.WriteLine("Cleaning up..");
            };
            using var hook = new KeyboardHook();
            hook.Run();
            Application.Run();
        }
    }
}
