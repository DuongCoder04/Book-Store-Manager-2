using System;

namespace WindowsFormsApp.DataService
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MyDbContext Context;

        public UnitOfWork()
        {
            Context = new MyDbContext();
        }

        private IBillRepository _billRepository;
        public IBillRepository Bill => _billRepository ?? (_billRepository = new BillRepository(Context));

        private IBillDetailRepository _billDetailRepository;
        public IBillDetailRepository BillDetail => _billDetailRepository ?? (_billDetailRepository = new BillDetailRepository(Context));

        public bool SaveChanges()
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //Log Exception Handling message if needed
                return false;
            }
        }

        #region IDisposable Support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // Dispose managed state (managed objects)
                Context.Dispose();
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
