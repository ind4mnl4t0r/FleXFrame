using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Helpers
{
    public class TokenStorage
    {
        private static string? _token;

        public static void SaveToken(string token)
        {
            _token = token;
        }

        public static string? GetToken()
        {
            return _token;
        }

        public static void ClearToken()
        {
            _token = null;
        }
    }
}
