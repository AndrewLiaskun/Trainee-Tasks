// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Searcher
{
    public class TextSearcher
    {
        private const RegexOptions DefaultOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;

        private static readonly Regex _htmlMatcher = new Regex(@"\b((http|ftp|https):\/\/|www\.)([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?", DefaultOptions);
        private static readonly Regex _mailMatcher = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", DefaultOptions);
        private static readonly Regex _fileMatcher = new Regex(@"^(([A-Z]:)?[\.]?[\\{1,2}/]?.*[\\{1,2}/])*(.+)\.(.+)", DefaultOptions);

        private IResourceHolder _resource;

        private TextSearcher(string dataSource, bool isFile) => _resource = isFile ? _resource = new FileResource(dataSource) : new HtmlResource(dataSource);

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

        private static bool IsFile(string path) => _fileMatcher.IsMatch(path);

        private static bool IsURL(string path) => _htmlMatcher.IsMatch(path) || _mailMatcher.IsMatch(path);

        private async Task DoAnalyzing()
        {
            using (var dataSource = _resource.GetResourceStream())
            using (var reader = new StreamReader(dataSource))
            {
                while (!reader.EndOfStream)
                {
                    FindValues(await reader.ReadLineAsync());
                }
            }
        }

        private void RaiseMatchFound(MatchType match, string value) => MatchFound?.Invoke(this, new MatchEventArgs(match, value));

        private void FindValues(string line)
        {

            foreach (Match match in _mailMatcher.Matches(line))
                RaiseMatchFound(MatchType.Email, match.Groups[0].Value);

            foreach (Match match in _htmlMatcher.Matches(line))
                RaiseMatchFound(MatchType.Link, match.Groups[0].Value);
        }
    }
}