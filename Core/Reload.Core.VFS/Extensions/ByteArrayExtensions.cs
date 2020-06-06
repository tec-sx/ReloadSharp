using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Reload.Core.VFS.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string Md5Hash(this byte[] data)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(data);

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
