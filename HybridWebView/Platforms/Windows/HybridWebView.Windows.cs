﻿using Microsoft.Web.WebView2.Core;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

namespace HybridWebView
{
    partial class HybridWebView
    {
        /// <summary>
        /// Gets the application's base URI. Defaults to <c>https://0.0.0.0/</c>
        /// </summary>
        /// 
        private static readonly string AppOrigin = $"{HybridWebViewConfiguration.protocol}://{HybridWebViewConfiguration.url}/";

        private static readonly Uri AppOriginUri = new(AppOrigin);

        private CoreWebView2Environment _coreWebView2Environment;

        async partial void InitializeHybridWebView()
        {
            var wv2 = (Microsoft.UI.Xaml.Controls.WebView2)Handler.PlatformView;
            wv2.WebMessageReceived += Wv2_WebMessageReceived;

           _coreWebView2Environment = await CoreWebView2Environment.CreateAsync();

            await wv2.EnsureCoreWebView2Async();

            wv2.CoreWebView2.Settings.IsWebMessageEnabled = true;
            //wv2.CoreWebView2.AddWebResourceRequestedFilter($"{AppOrigin}*", CoreWebView2WebResourceContext.All);
            //wv2.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;

            wv2.Source = new Uri(AppOrigin);
        }

        /*private async void CoreWebView2_WebResourceRequested(CoreWebView2 sender, CoreWebView2WebResourceRequestedEventArgs args)
        {
            // Get a deferral object so that WebView2 knows there's some async stuff going on. We call Complete() at the end of this method.
            using var deferral = args.GetDeferral();

            if (new Uri(args.Request.Uri) is Uri uri && AppOriginUri.IsBaseOf(uri))
            {
                var relativePath = AppOriginUri.MakeRelativeUri(uri).ToString().Replace('/', '\\');

                string contentType;
                if (string.IsNullOrEmpty(relativePath))
                {
                    relativePath = MainFile;
                    contentType = "text/html";
                }
                else
                {
                    var requestExtension = Path.GetExtension(relativePath);
                    contentType = requestExtension switch
                    {
                        ".htm" or ".html" => "text/html",
                        ".js" => "application/javascript",
                        ".css" => "text/css",
                        _ => "text/plain",
                    };
                }

                var assetPath = Path.Combine(HybridAssetRoot, relativePath);

                using var contentStream = await GetAssetStreamAsync(assetPath);
                if (contentStream is null)
                {
                    var notFoundContent = "Resource not found (404)";
                    args.Response = _coreWebView2Environment.CreateWebResourceResponse(
                        Content: null,
                        StatusCode: 404,
                        ReasonPhrase: "Not Found",
                        Headers: GetHeaderString("text/plain", notFoundContent.Length)
                    );
                }
                else
                {
                    args.Response = _coreWebView2Environment.CreateWebResourceResponse(
                        Content: await CopyContentToRandomAccessStreamAsync(contentStream),
                        StatusCode: 200,
                        ReasonPhrase: "OK",
                        Headers: GetHeaderString(contentType, (int)contentStream.Length)
                    );
                }
            }

            // Notify WebView2 that the deferred (async) operation is complete and we set a response.
            deferral.Complete();

            async Task<IRandomAccessStream> CopyContentToRandomAccessStreamAsync(Stream content)
            {
                using var memStream = new MemoryStream();
                await content.CopyToAsync(memStream);
                var randomAccessStream = new InMemoryRandomAccessStream();
                await randomAccessStream.WriteAsync(memStream.GetWindowsRuntimeBuffer());
                return randomAccessStream;
            }
        }

        private protected static string GetHeaderString(string contentType, int contentLength) =>
$@"Content-Type: {contentType}
Content-Length: {contentLength}";*/

        private void Wv2_WebMessageReceived(Microsoft.UI.Xaml.Controls.WebView2 sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            OnMessageReceived(args.TryGetWebMessageAsString());
        }
    }
}
