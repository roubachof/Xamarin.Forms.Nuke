using System.Threading;
using System.Threading.Tasks;

using Foundation;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportImageSourceHandler(
    typeof(FileImageSource), typeof(FFImageLoading.ImageSourceHandler))]
[assembly: ExportImageSourceHandler(
    typeof(StreamImageSource), typeof(FFImageLoading.ImageSourceHandler))]
[assembly: ExportImageSourceHandler(
    typeof(UriImageSource), typeof(FFImageLoading.ImageSourceHandler))]

namespace FFImageLoading
{
    [Preserve (AllMembers = true)]
    public class ImageSourceHandler : IImageSourceHandler
    {
        public Task<UIImage> LoadImageAsync(
            ImageSource imageSource,
            CancellationToken cancellationToken = new CancellationToken(),
            float scale = 1)
        {
            return FFImageLoadingHelper.LoadViaFFImageLoading(
                imageSource, cancellationToken);
        }
    }
}