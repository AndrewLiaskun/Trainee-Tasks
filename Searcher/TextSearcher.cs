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
    public class TextSearcher
    {
        private const RegexOptions DefaultOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
        private readonly Regex _htmlLink = new Regex(@"\b((http|ftp|https):\/\/|www\.)([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?", DefaultOptions);
        private readonly Regex _mailLink = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", DefaultOptions);
        private readonly Regex _filePath = new Regex(@"^(([A-Z]:)?[\.]?[\\{1,2}/]?.*[\\{1,2}/])*(.+)\.(.+)", DefaultOptions);

        private HttpWebResponse _response;

        public TextSearcher(string path)
        {
            Initialize(path);
        }

        public event EventHandler<MatchEventArgs> MatchFound;

        private void Initialize(string path)
        {
            Stream stream = null;

            if (IsURL(path))
                stream = GetResponseStream(path);
            else if (IsFile(path))
                stream = GetFileStream(path);
            else
                throw new NotSupportedException("");

            Task.Run(() => AnalyzeData(stream));
        }

        private Stream GetResponseStream(string path) => GetResponse(path).GetResponseStream();

        private HttpWebResponse GetResponse(string path)
        {
            var request = (HttpWebRequest)WebRequest.Create(path);
            request.Credentials = CredentialCache.DefaultCredentials;

            // Ignore Certificate validation failures (aka untrusted certificate + certificate chains)
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            _response = (HttpWebResponse)request.GetResponse();
            return _response;
        }

        private async void AnalyzeData(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    FindValues(await reader.ReadLineAsync());
                }
            }
            stream.Close();
            _response?.Close();
        }

        private bool IsFile(string path) => _filePath.IsMatch(path);

        private bool IsURL(string path) => _htmlLink.IsMatch(path) || _mailLink.IsMatch(path);

        private Stream GetFileStream(string path) => File.OpenRead(path);

        private void RaiseMatchFound(MatchType match, string value) => MatchFound?.Invoke(this, new MatchEventArgs(match, value));

        private void FindValues(string line)
        {

            foreach (Match match in _mailLink.Matches(line))
                RaiseMatchFound(MatchType.Email, match.Groups[0].Value);

            foreach (Match match in _htmlLink.Matches(line))
                RaiseMatchFound(MatchType.Link, match.Groups[0].Value);
        }
    }
}