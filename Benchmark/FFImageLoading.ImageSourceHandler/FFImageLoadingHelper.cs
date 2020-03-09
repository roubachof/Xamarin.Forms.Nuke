using System;
using System.Threading;
using System.Threading.Tasks;

using Foundation;

using UIKit;

using Xamarin.Forms;

namespace FFImageLoading
{
    [Preserve]
    internal static class FFImageLoadingHelper
    {
        public static void Init()
        {
        }

        public static async Task<UIImage> LoadViaFFImageLoading(ImageSource source, CancellationToken token)
        {
            try
            {
                switch (source)
                {
                    case UriImageSource uriSource:
                        var urlString = uriSource.Uri.OriginalString;
                        if (string.IsNullOrEmpty(urlString))
                        {
                            return null;
                        }

                        FormsHandler.Debug("Loading `{0}` as a web URL", urlString);
                        return await LoadImageAsync(urlString);

                    case FileImageSource fileSource:
                        var fileName = fileSource.File;
                        if (string.IsNullOrEmpty(fileName))
                        {
                            return null;
                        }

                        FormsHandler.Debug("Loading `{0}` as a file", fileName);
                        return await LoadFileAsync(fileName);

                    case StreamImageSource streamImageSource:
                        FormsHandler.Debug("Loading Image as a stream");
                        return await LoadStreamAsync(streamImageSource);

                    default:
                        FormsHandler.Warn("Unhandled image source type {0}", source.GetType().Name);
                        break;
                }
            }
            catch (Exception exception)
            {
                //Since developers can't catch this themselves, I think we should log it and silently fail
                FormsHandler.Warn("Unexpected exception in FFImageLoading image source handler: {0}", exception);
            }

            return default(UIImage);
        }

        private static Task<UIImage> LoadImageAsync(string urlString)
        {
            return ImageService.Instance.LoadUrl(urlString).AsUIImageAsync();
        }

        private static Task<UIImage> LoadFileAsync(string filePath)
        {
            return ImageService.Instance.LoadFile(filePath).AsUIImageAsync();
        }

        private static Task<UIImage> LoadStreamAsync(StreamImageSource streamSource)
        {
            return ImageService.Instance.LoadStream(token => ((IStreamImageSource)streamSource).GetStreamAsync(token))
                .AsUIImageAsync();
        }
    }
}