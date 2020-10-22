using System;

namespace Xamarin.Forms.Nuke
{
    public static class FormsHandler
    {
        private const string WarningSeverity = "WNG|";
        private const string DebugSeverity = "DBG|";

        private const string LogTag = "|Nuke|";

        /// <summary>
        /// A flag indicating if Debug logging is enabled.
        /// </summary>
        public static bool IsDebugEnabled { get; private set; }

        /// <summary>
        /// If set to true, the processing of FileImageSource will be delegated to default Xamarin.Forms handlers.
        /// </summary>
        public static bool DisableFileImageSourceHandling { get; private set; } = false;

        /// <summary>
        /// Initialize the Nuke library.
        /// </summary>
        /// <param name="debug">A flag indicating if Debug logging is enabled</param>
        /// <param name="disableFileImageSourceHandling">If set to true, the processing of FileImageSource will be delegated to the default Xamarin.Forms file image handler.</param>
        public static void Init(bool debug = false, bool disableFileImageSourceHandling = false)
        {
            IsDebugEnabled = debug;
            DisableFileImageSourceHandling = disableFileImageSourceHandling;
            NukeHelper.Preserve();

            Console.WriteLine(GetLogPrefix(DebugSeverity) + $"Initializing Xamarin.Forms.Nuke with {{ debug: {debug}, disableFileImageSourceHandling: {disableFileImageSourceHandling} }}");
        }

        internal static void Warn(string message)
        {
            Console.WriteLine(GetLogPrefix(WarningSeverity) + message);
        }

        internal static void Debug(Func<string> messageFunc)
        {
            if (IsDebugEnabled)
            {
                Console.WriteLine(GetLogPrefix(DebugSeverity) + messageFunc());
            }
        }

        private static string GetLogPrefix(string logSeverity)
        {
            return DateTime.Now.ToString("MM-dd H:mm:ss.fff") + LogTag + logSeverity;
        }
    }
}
