using System;
using System.Diagnostics;

namespace algorithms.TreeSearching
{
    public class SearchCancellationToken
    {
        public bool Cancelled { get { return _isCancelled(); } }

        private Func<bool> _isCancelled;

        private Stopwatch _timer;

        public SearchCancellationToken(Func<bool> stopWhen)
        {
            _isCancelled = stopWhen;
        }

        public SearchCancellationToken(int milliseconds)
        {
            _timer = Stopwatch.StartNew();
            _isCancelled = () => _timer.ElapsedMilliseconds >= milliseconds;
        }
    }
}