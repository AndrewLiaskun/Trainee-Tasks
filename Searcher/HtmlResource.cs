// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Searcher
{
    public class HtmlResource : IResourceHolder
    {
        private HttpWebResponse _response;
        private bool disposedValue;

        public HtmlResource(string path)
        {
            _response = GetResponse(path);
        }

        ~HtmlResource()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        public Stream GetResourceStream() => _response.GetResponseStream();

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _response.Close();
                }

                disposedValue = true;
            }
        }

        private HttpWebResponse GetResponse(string path)
        {
            var request = (HttpWebRequest)WebRequest.Create(path);
            request.Credentials = CredentialCache.DefaultCredentials;

            // Ignore Certificate validation failures (aka untrusted certificate + certificate chains)
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            return (HttpWebResponse)request.GetResponse();
        }
    }
}