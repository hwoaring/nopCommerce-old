using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;

namespace Nop.Core.Helpers
{
    public class WeixinHelper
    {
        public static long OpenIdToLong(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
                return 0L;

            var md5 = new MD5CryptoServiceProvider();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(openId));
            md5.Clear();

            var sb = new StringBuilder();
            
            foreach (var d in data)
            {
                sb.Append(d.ToString("D3"));
            }

            var str_result = sb.ToString();
            var result = 0L;

            result += Convert.ToInt64(str_result.Substring(0, 18));
            result += Convert.ToInt64(str_result.Substring(18, 18));
            result += Convert.ToInt64(str_result.Substring(36));
            return result;
        }
    }
}
