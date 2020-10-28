using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Foundation;

using UIKit;

using Xamarin.Forms.Platform.iOS;
using Xamarin.Nuke;

namespace Xamarin.Forms.Nuke
{
    [Preserve]
    internal static class NukeHelper
    {
        private static readonly Dictionary<float, string> ScaleToDensitySuffix = new Dictionary<float, string>
            {
                { 1, string.Empty }, 
                { 2, "@2x" }, 
                { 3, "@3x" },
                { 4, "@4x" },
                { 5, "@5x" },
                { 6, "@6x" },
            };

        private static readonly FileImageSourceHandler DefaultFileImageSourceHandler = new FileImageSourceHandler();

        public static void Preserve()
        {
        }

        public static Task<UIImage> LoadViaNuke(ImageSource source, CancellationToken token, float scale)
        {
            try
            {
                switch (source)
                {
                    case UriImageSource uriSource:
                        return HandleUriSource(uriSource, token, scale);

                    case FileImageSource fileSource:
                        return FormsHandler.DisableFileImageSourceHandling
                            ? DefaultFileImageSourceHandler.LoadImageAsync(fileSource, token, scale)
                            : HandleFileSourceAsync(fileSource, token, scale);

                    default:
                        FormsHandler.Warn($"Unhandled image source type {source.GetType().Name}");
                        break;
                }
            }
            catch (Exception exception)
            {
                FormsHandler.Warn($"Unexpected exception in Nuke image source handler: {exception}");
            }

            return Task.FromResult(default(UIImage));
        }

        private static Task<UIImage> HandleUriSource(UriImageSource uriSource, CancellationToken token, float scale)
        {
            var urlString = uriSource.Uri?.OriginalString;
            if (string.IsNullOrEmpty(urlString))
            {
                FormsHandler.Debug(() => "A null or empty url has been specified for the UriImageSource, returning...");
                return null;
            }

            FormsHandler.Debug(() => $"Loading \"{urlString}\" as a web URL");
            return LoadImageAsync(new NSUrl(urlString));
        }

        private static Task<UIImage> HandleFileSourceAsync(FileImageSource fileSource, CancellationToken token, float scale)
        {
            var fileName = fileSource.File;
            if (string.IsNullOrEmpty(fileName))
            {
                FormsHandler.Debug(() => "A null or empty filename has been specified for the FileImageSource, returning...");
                return null;
            }

            string name = Path.GetFileNameWithoutExtension(fileName);
            string nameWithSuffix = $"{name}{ScaleToDensitySuffix[scale]}";
            string filenameWithDensity = fileName.Replace(name, nameWithSuffix);

            NSUrl fileUrl;
            if (File.Exists(filenameWithDensity))
            {
                FormsHandler.Debug(() => $"Loading \"{filenameWithDensity}\" as a file URI");
                fileUrl = NSUrl.FromFilename(filenameWithDensity);;
            }
            else if (File.Exists(fileName))
            {
                FormsHandler.Debug(() => $"Loading \"{fileName}\" as a file URI");
                fileUrl = NSUrl.FromFilename(fileName);;
            }
            else
            {
                FormsHandler.Debug(() => $"Couldn't retrieve the image URI: loading \"{fileName}\" from Bundle");
                return Task.FromResult(UIImage.FromBundle(fileName));
            }

            return LoadImageAsync(fileUrl);
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
                            FormsHandler.Debug(() => $"Fail to load image: {url.AbsoluteString}, innerError: {errorMessage}");
                        }

                        tcs.SetResult(image);
                    });

            return tcs.Task;
        }
    }
}