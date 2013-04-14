using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cebritas.General.Cryptography {
    public enum HashSumType {
        MD5,
        SHA1,
        SHA256,
        SHA512
    }

    public static class HashSumUtil {
        public static string GetHashSum(string item, HashSumType type) {
            HashAlgorithm hash = CreateHashService(type);
            byte[] result = hash.ComputeHash(Encoding.ASCII.GetBytes(item));

            return BitConverter.ToString(result).ToLower().Replace("-", "");
        }

        private static HashAlgorithm CreateHashService(HashSumType type) {
            switch (type) {
                case HashSumType.MD5:
                    return new MD5CryptoServiceProvider();
                case HashSumType.SHA1:
                    return new SHA1CryptoServiceProvider();
                case HashSumType.SHA256:
                    return new SHA256CryptoServiceProvider();
                case HashSumType.SHA512:
                    return new SHA512CryptoServiceProvider();
            }
            return new MD5CryptoServiceProvider();
        }
    }
}