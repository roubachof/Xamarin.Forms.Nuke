using System;
using System.Timers;

namespace ImageSourceHandlers.Forms.Sample.Profiler
{
	public class MemoryProfiler : IDisposable
    {
        public static MemoryProfiler Instance;

		readonly string _name;
		readonly Timer _timer = new Timer ();

		long _peakMemory;
        long _lowestMemory;
        long _checkpointMemory;

        Action _clearCache;

		public MemoryProfiler (string name, Action clearCache)
		{
			_name = name;
            _clearCache = clearCache;
            _timer.Interval = 3000;
			_timer.Elapsed += OnElapsed;
			_timer.Start ();
		}

        public string Name => _name;

        public void ClearCache()
        {
            _clearCache?.Invoke();
        }

        public void Reset()
        {
            _checkpointMemory = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64;
        }

		void OnElapsed (object sender, ElapsedEventArgs e)
		{
            //var runtime = Java.Lang.Runtime.GetRuntime ();
            //long usedMemory = runtime.TotalMemory () - runtime.FreeMemory ();

            var usedMemory = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64;
            if (usedMemory > _peakMemory)
                _peakMemory = usedMemory;

            if (_checkpointMemory == 0)
                _checkpointMemory = usedMemory;

            if (_lowestMemory == 0)
                _lowestMemory = usedMemory;

            if (usedMemory < _lowestMemory)
                _lowestMemory = usedMemory;

            Console.WriteLine(
                "Sample|{0} Memory, Used: {1}, Peak: {2}, Lowest: {3}, MaxConsumed: {4}, Checkpoint: {5}",
                _name,
                usedMemory,
                _peakMemory,
                _lowestMemory,
                _peakMemory - _lowestMemory,
                usedMemory - _checkpointMemory);

            // Console.WriteLine(APlatformPerformance.Instance.GetMemoryInfo());
        }

		public void Dispose ()
		{
			_timer.Stop ();
		}
	}
}