using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EventGridPubSub.Publisher
{
    /// <summary>
    /// See https://docs.microsoft.com/en-us/azure/event-grid/security-authentication
    /// </summary>
    public class TokenGenerator
    {
        private const char Resource = 'r';
        private const char Expiration = 'e';
        private const char Signature = 's';

        public string BuildSharedAccessSignature(string resource, DateTime expirationUtc, string key)
        {
            string encodedResource = HttpUtility.UrlEncode(resource);
            var encodedExpirationUtc = HttpUtility.UrlEncode(expirationUtc.ToString("o"));

            string unsignedSas = $"{Resource}={encodedResource}&{Expiration}={encodedExpirationUtc}";
            using (var hmac = new HMACSHA256(Convert.FromBase64String(key)))
            {
                string signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(unsignedSas)));
                string encodedSignature = HttpUtility.UrlEncode(signature);
                string signedSas = $"{unsignedSas}&{Signature}={encodedSignature}";

                return signedSas;
            }
        }
    }
}