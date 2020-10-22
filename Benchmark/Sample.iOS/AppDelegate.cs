using System;

using Foundation;

using ImageSourceHandlers.Forms.Sample.Profiler;

using UIKit;

namespace Sample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Uncomment for XF only test
            //MemoryProfiler.Instance = new MemoryProfiler("Xamarin.Forms", null);

            // Uncomment for Nuke test
            MemoryProfiler.Instance = new MemoryProfiler("Nuke", Xamarin.Forms.Nuke.NukeController.ClearCache);
            Xamarin.Forms.Nuke.FormsHandler.Init(true, disableFileImageSourceHandling: false);

            // Uncomment for FFIL test
            //MemoryProfiler.Instance = new MemoryProfiler("FFImageLoading", FFImageLoading.FFImageLoadingController.ClearCache);
            //FFImageLoading.FormsHandler.Init(false);

            LoadApplication(new ImageSourceHandlers.Forms.Sample.Views.App());

            Console.WriteLine($"Scale: {UIScreen.MainScreen.Scale}");

            return base.FinishedLaunching(app, options);
        }
    }
}
