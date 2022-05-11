using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DisposeEx
{
    public class MyClass : IDisposable
    {
        private IntPtr NativeResource { get; set; } = Marshal.AllocHGlobal(100);
        public Random ManagedResource { get; set; } = new Random();
        private bool disposed;
        ~MyClass()
        {
            Dispose(false);
        }
        public void Close()
        {
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                return;
            if (disposing)
            {
                if (ManagedResource != null)
                {
                    ManagedResource = null;
                }
            }
            if (NativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(NativeResource);
                NativeResource = IntPtr.Zero;
            }
            disposed = true;
        }
    }
}
