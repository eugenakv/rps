using System;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissors
{
    public class HashGenerator : IDisposable
    {
        private bool disposed = false;
        private RandomNumberGenerator generator;

        public HashGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                generator?.Dispose();
            }

            disposed = true;
        }

        public (string Key, string Hash) ComputeHash(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace", nameof(message));
            }

            var keyBytes = new byte[16];
            generator.GetBytes(keyBytes);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.Default.GetBytes(message));
                return (keyBytes.ToHex(), hashBytes.ToHex());
            }
        }
    }
}