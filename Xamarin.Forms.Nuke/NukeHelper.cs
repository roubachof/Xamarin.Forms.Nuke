using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Foundation;

using UIKit;

using Xamarin.Nuke;

namespace Xamarin.Forms.Nuke
{
    [Preserve]
    internal static class NukeHelper
    {
        public static void Init()
        {
        }

        public static void ClearCache()
        {
        }

        public static async Task<UIImage> LoadViaNuke(ImageSource source, CancellationToken token)
        {
            try
            {
                switch (source)
                {
                    case UriImageSource uriSource:
                        var urlString = uriSource.Uri.OriginalString;
                        if (String.IsNullOrEmpty(urlString))
                        {
                            return null;
                        }

                        FormsHandler.Debug("Loading `{0}` as a web URL", urlString);
                        return await LoadImageAsync(new NSUrl(urlString));

                    case FileImageSource fileSource:
                        var fileName = fileSource.File;
                        if (string.IsNullOrEmpty(fileName))
                        {
                            return null;
                        }

                        FormsHandler.Debug("Loading `{0}` as a file", fileName);
                        NSUrl fileUrl = null;
                        if (File.Exists(fileName))
                        {
                            fileUrl = NSUrl.FromFilename(fileName);;
                        }
                        else
                        {
                            string name = Path.GetFileNameWithoutExtension(fileName);
                            string extension = Path.GetExtension(fileName);
                            FormsHandler.Debug($"Loading as bundle resource name: {name} with type: {extension}");
                            fileUrl = NSBundle.MainBundle.GetUrlForResource(name, extension);
                            FormsHandler.Debug($"Bundle resource path: {fileUrl.AbsoluteString}");
                        }

                        return await LoadImageAsync(fileUrl);

                    //case StreamImageSource streamImageSource:
                    //    FormsHandler.Debug("Loading Image as a stream");
                    //    return await LoadStreamAsync(streamImageSource);

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

        private static Task<UIImage> LoadImageAsync(NSUrl url)
        {
            var tcs = new TaskCompletionSource<UIImage>();

            ImagePipeline.Shared.LoadImageWithUrl(
                url,
                (image, errorMessage) =>
                    {
                        if (image == null)
                        {
                            FormsHandler.Debug("Fail to load image: {0}, innerError: {1}", url.AbsoluteString, errorMessage);
                        }

                        tcs.SetResult(image);
                    });

            return tcs.Task;
        }
    }
}