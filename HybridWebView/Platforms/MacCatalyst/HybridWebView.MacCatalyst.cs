using Foundation;
using WebKit;

namespace HybridWebView
{
    partial class HybridWebView
    {

        internal static string AppOrigin = $"{HybridWebViewConfiguration.protocol}://{HybridWebViewConfiguration.url}/";
        internal static Uri AppOriginUri = new(AppOrigin);

        partial void InitializeHybridWebView()
        {
            var wv = (WKWebView)Handler.PlatformView;

            using var nsUrl = new NSUrl(AppOrigin);
            using var request = new NSUrlRequest(nsUrl);
            wv.LoadRequest(request);
        }
    }
}
