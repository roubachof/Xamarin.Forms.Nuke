
namespace FFImageLoading
{
    public static class FFImageLoadingController
    {
        public static void ClearCache()
        {
            ImageService.Instance.InvalidateDiskCacheAsync();
            ImageService.Instance.InvalidateMemoryCache();
        }
    }
}