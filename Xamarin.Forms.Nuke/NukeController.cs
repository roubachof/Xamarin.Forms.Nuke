
using Xamarin.Nuke;

namespace Xamarin.Forms.Nuke
{
    public static class NukeController
    {
        public static void ClearCache()
        {
            DataLoader.Shared.RemoveAllCachedResponses();
            ImageCache.Shared.RemoveAll();
        }
    }
}