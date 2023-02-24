using Android.Webkit;
using Java.Interop;
using Microsoft.Maui.Platform;
using AWebView = Android.Webkit.WebView;

namespace HybridWebView
{
    partial class HybridWebView
    {
        /// <summary>
        /// Gets the application's base URI. Defaults to <c>https://0.0.0.0/</c>
        /// </summary>
        internal static readonly string AppOrigin = $"{HybridWebViewConfiguration.protocol}://{HybridWebViewConfiguration.url}/";

        internal static readonly Uri AppOriginUri = new(AppOrigin);

        private HybridWebViewJavaScriptInterface _javaScriptInterface;

        async partial void InitializeHybridWebView()
        {
            var awv = (AWebView)Handler.PlatformView;
            awv.Settings.JavaScriptEnabled = true;

            _javaScriptInterface = new HybridWebViewJavaScriptInterface(this);
            awv.AddJavascriptInterface(_javaScriptInterface, "hybridWebViewHost");

            awv.LoadUrl(AppOrigin);
        }

        private sealed class HybridWebViewJavaScriptInterface : Java.Lang.Object
        {
            private readonly HybridWebView _hybridWebView;

            public HybridWebViewJavaScriptInterface(HybridWebView hybridWebView)
            {
                _hybridWebView = hybridWebView;
            }

            [JavascriptInterface]
            [Export("sendMessage")]
            public void SendMessage(string message)
            {
                _hybridWebView.OnMessageReceived(message);
            }
        }
    }
}
