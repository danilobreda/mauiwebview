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
            {"access_token":"b67a99c2d08a4837b1ed9a0935667357bredasf50c2bed2e3849fcad8caa46393a3284","expires_in":1440,"created_date":"2023-02-28T10:07:08.4864043Z","expire_date":"2023-03-01T10:07:08.4864043Z"}
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

            public void Logout()
            {
                Application.Current.Quit();
            }

            public void PrintPdf(string pdfBase64WithoutMimeType)
            {

            }

            public void PrintHtml(string htmlDecoded)
            {

            }

            public void DownloadFile(string url, string nomeArquivo)
            {

            }

            public void SendWhatsapp(string url)
            {

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