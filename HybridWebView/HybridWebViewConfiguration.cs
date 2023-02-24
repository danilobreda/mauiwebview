using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridWebView
{
    public static class HybridWebViewConfiguration
    {
        public static string protocol => useHttps ? "https" : "http";

        public static string url = "vendasimples.com.br";
        public static string urlCookie = ".vendasimples.com.br";
        public static bool useHttps = true;
        public static string port = "";

        //public static string url = "localhost:8080";
        //public static string urlCookie = "localhost";
        //public static bool useHttps = false;
        //public static string port = @"""80,8080""";
    }
}
