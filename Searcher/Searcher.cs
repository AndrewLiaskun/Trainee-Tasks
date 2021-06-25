// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

namespace Searcher
{
    public class Searcher
    {
        public void GetUrl(string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            //request.Method = "HEAD";
            //request.AllowAutoRedirect = false;
            request.Credentials = CredentialCache.DefaultCredentials;

            // Ignore Certificate validation failures (aka untrusted certificate + certificate chains)
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string responseFromServer = reader.ReadToEnd();
        }
    }
}