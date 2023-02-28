namespace HybridWebView
{
    public static class HybridWebViewConfiguration
    {
        public static string protocol => useHttps ? "https" : "http";

        //public static string url = "vendasimples.com.br";
        //public static bool useHttps = true;
        
        public static string url = "192.168.0.103:8080/#/loginapp";
        //public static string url = "localhost:8080/#/loginapp";
        public static bool useHttps = false;
    }
}
