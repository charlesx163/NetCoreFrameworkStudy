using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DisposeEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public class MyResource : IDisposable
        {
            private IntPtr handle;
            private Component component = new Component();
            private bool disposed = false;
            public MyResource(IntPtr handle)
            {
                this.handle = handle;
            }
            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    component.Dispose();
                }
                CloseHandle(handle);
                handle = IntPtr.Zero;
                disposed = true;
            }
            [DllImport("Kernel32")]
            private extern static Boolean CloseHandle(IntPtr handle);

            ~MyResource()
            {
                Dispose(false);
            }
        }
    }
}
