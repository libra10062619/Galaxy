﻿using System;
namespace Galaxy.Infrastructure
{
    /// <summary>
    /// Disposable.
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        protected bool _disposed;
        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableObject"/> class.
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    Disposing();
                    _disposed = true;
                }
            }
        }

        protected abstract void Disposing();
    }
}
