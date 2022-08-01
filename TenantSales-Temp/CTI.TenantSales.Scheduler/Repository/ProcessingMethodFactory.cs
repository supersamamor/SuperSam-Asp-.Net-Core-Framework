using CTI.TenantSales.Core.Constants;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace CTI.TenantSales.Scheduler.Repository
{
    public class ProcessingMethodFactory : IDisposable
    {
        public IProcessingMethod? ProcessingMethod { get; set; }
        public ProcessingMethodFactory(string salesType)
        {
            ProcessingMethod = salesType.ToUpper() switch
            {
                SalesType.SALESTYPE_DAILY => new ProcessMethodDaily(),
                SalesType.SALESTYPE_HOURLY => new ProcessMethodHourly(),
                _ => throw new Exception("Invalid file type (eg. S for Daily and H for Hourly)."),
            };
        }

        bool disposed = false;
        // Instantiate a SafeHandle instance.
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.               
                ProcessingMethod = null;
            }
            disposed = true;
        }
    }
}
