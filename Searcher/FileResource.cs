// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.IO;

namespace Searcher
{
    public class FileResource : IResourceHolder
    {
        private FileInfo _file;

        public FileResource(string path)
        {
            _file = new FileInfo(path);
        }

        public Stream GetResourceStream() => _file.OpenRead();

        #region IDisposable Support

        private bool _isDisposed = false; // To detect redundant calls

        ~FileResource()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _file = null;
                }

                _isDisposed = true;
            }
        }

        #endregion IDisposable Support
    }
}