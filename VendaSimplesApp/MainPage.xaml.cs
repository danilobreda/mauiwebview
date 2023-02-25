using System.Text;
using HybridWebView;

namespace VendaSimplesApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            webView.JSInvokeTarget = new JSInvokeTarget(this);

            BindingContext = this;
        }

        private async void LoginApp()
        {
            var json = """
            {"access_token":"1950b61e555746dca385bea37f7e65bfbredas269542aa80bf4321b6ae29587df6fd7a","expires_in":1440,"created_date":"2023-02-25T03:19:58.7488814Z","expire_date":"2023-02-26T03:19:58.7488814Z"}
            """;

            var base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(json));
            _ = await MainThread.InvokeOnMainThreadAsync(async () => await webView.InvokeJsMethodAsync("loginApp", base64));
        }

        private sealed class JSInvokeTarget
        {
            private MainPage _mainPage;

            public JSInvokeTarget(MainPage mainPage)
            {
                _mainPage = mainPage;
            }

            public void StartLoadingMaui()
            {
                _mainPage.LoginApp();
            }
        }

        private void OnInvokeJSMethod(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await MainThread.InvokeOnMainThreadAsync(async () => await webView.InvokeJsMethodAsync("toggleSidebar"));
            });
        }
    }
}