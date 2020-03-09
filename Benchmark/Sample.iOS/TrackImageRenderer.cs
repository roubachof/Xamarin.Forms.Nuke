using System;
using System.ComponentModel;
using System.Threading.Tasks;

//using FFImageLoading.FormsHandler;
//using FFImageLoading.FormsHandler.Platform;

using Foundation;

using Sample.iOS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Image), typeof(TrackImageRenderer))]
// [assembly: ExportRenderer(typeof(CachedImage), typeof(TrackCachedImageRenderer))]

namespace Sample.iOS
{
    public class TrackImageRenderer : ImageRenderer
    {
        protected override async Task TrySetImage(Image previous = null)
        {
            await base.TrySetImage(previous);

            if (Control.Image != null)
            {
                // NSData data = Control.Image.AsJPEG(1);
                // Console.WriteLine($"Image set with scale {Control.Image.CurrentScale}, size {Control.Image.Size}, bytes {data.Length}");
            }
        }
    }

    //public class TrackCachedImageRenderer : CachedImageRenderer
    //{
    //    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        base.OnElementPropertyChanged(sender, e);
    //        if (e.PropertyName == "Renderer")
    //        {
    //            if (Control.Image != null)
    //            {
    //                NSData data = Control.Image.AsJPEG(1);
    //                Console.WriteLine($"Image set with scale {Control.Image.CurrentScale}, size {Control.Image.Size}, bytes {data.Length}");
    //            }
    //        }
    //    }
    //}
}