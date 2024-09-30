using System;
using System.Threading;
using System.Windows.Forms;

namespace HMS
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{A9A4C56D-B43C-4D58-97D5-53AC0831321E}");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new demo());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("An instance of the application is already running.");
            }
        }
    }
}
