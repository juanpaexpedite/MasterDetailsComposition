   public class CompositionTimer
    {
        private Timer threadTimer;

        public delegate void Event();
        public event Event Tick;

        public TimeSpan Interval;

        public bool IsEnabled { get; private set; }

        public CompositionTimer(TimeSpan span)
        {
            Interval = span;
        }

        public void Start()
        {
            if (!IsEnabled)
            {
                threadTimer = new Timer(threadCallback, null, (int)Interval.TotalMilliseconds, Timeout.Infinite);
                IsEnabled = true;
            }
        }

        public void Stop()
        {
            if (IsEnabled)
            {
                threadTimer.Dispose();
                IsEnabled = false;
            }
        }

        Stopwatch stopwatch;
        private void threadCallback(object state)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();

            Tick?.Invoke();

            var newvalue = Math.Max(0, Interval.TotalMilliseconds - stopwatch.ElapsedMilliseconds);

            threadTimer.Change((int)newvalue, Timeout.Infinite);

        }
    }
