// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Searcher
{
    public class TextSearcher : IResourceHolder
    {
        private const RegexOptions DefaultOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;

        private static readonly Regex _htmlLink = new Regex(@"\b((http|ftp|https):\/\/|www\.)([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?", DefaultOptions);
        private static readonly Regex _mailLink = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", DefaultOptions);
        private static readonly Regex _fileMatcher = new Regex(@"^(([A-Z]:)?[\.]?[\\{1,2}/]?.*[\\{1,2}/])*(.+)\.(.+)", DefaultOptions);

        private string _dataSource;
        private bool _isFile;

        public TextSearcher()
        {
        }

        private TextSearcher(string dataSource, bool isFile)
        {
            _dataSource = dataSource;
            _isFile = isFile;
        }

        public event EventHandler<MatchEventArgs> MatchFound;

        public static TextSearcher CreateSearcher(string dataSource)
        {
            if (IsURL(dataSource))
                return new TextSearcher(dataSource, false);

            if (IsFile(dataSource))
                return new TextSearcher(dataSource, true);

            throw new NotSupportedException("Something wrong with your path. Try again! ");
        }

        public void Analyze() => Task.Run(async () => await DoAnalyzing());

        public Stream GetResourceStream(IDisposable disposable)
        {
            if (_isFile)
                return File.OpenRead(_dataSource);

            return ((HttpWebResponse)disposable).GetResponseStream();
        }

        public void Dispose()
        {
            this.Dispose();
        }

        private static bool IsFile(string path) => _fileMatcher.IsMatch(path);

        private static bool IsURL(string path) => _htmlLink.IsMatch(path) || _mailLink.IsMatch(path);

        private HttpWebResponse GetResponse(string path)
        {
            var request = (HttpWebRequest)WebRequest.Create(path);
            request.Credentials = CredentialCache.DefaultCredentials;

            // Ignore Certificate validation failures (aka untrusted certificate + certificate chains)
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            return (HttpWebResponse)request.GetResponse();
        }

        private async Task DoAnalyzing()
        {
            using (var dataSource = GetDataSource())
            using (var stream = GetResourceStream(dataSource))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    FindValues(await reader.ReadLineAsync());
                }
            }
        }

        private IDisposable GetDataSource()
        {
            if (_isFile)
                return null;

            return GetResponse(_dataSource);
        }

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