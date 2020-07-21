using System;
using System.Collections.Generic;
using System.Text;
using PInvoke;

namespace KeyboardHook
{
    public class KeyboardHook : IDisposable
    {
        private bool _disposedValue;
        private User32.SafeHookHandle _hookHandle;

        private int Callback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return User32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

            Console.WriteLine($"{wParam} {lParam}");
            return User32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
        public void Run()
        {
            _hookHandle = User32.SetWindowsHookEx(User32.WindowsHookType.WH_KEYBOARD_LL, Callback, IntPtr.Zero, 0);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                NativeMethods.UnhookWindowsHookEx(_hookHandle.DangerousGetHandle());
                _hookHandle.Close();
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
