using System.Threading;
using System.Threading.Tasks;

using Foundation;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Nuke;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportImageSourceHandler(
//    typeof(StreamImageSource), typeof(Xamarin.Nuke.ImageSourceHandler))]
[assembly: ExportImageSourceHandler(
    typeof(FileImageSource), typeof(ImageSourceHandler))]
[assembly: ExportImageSourceHandler(
    typeof(UriImageSource), typeof(ImageSourceHandler))]

namespace Xamarin.Forms.Nuke
{
    [Preserve (AllMembers = true)]
    public class ImageSourceHandler : IImageSourceHandler
    {
        public Task<UIImage> LoadImageAsync(
            ImageSource imageSource,
            CancellationToken cancellationToken = new CancellationToken(),
            float scale = 1)
        {
            return NukeHelper.LoadViaNuke(
                imageSource, cancellationToken);
        }
    }
}