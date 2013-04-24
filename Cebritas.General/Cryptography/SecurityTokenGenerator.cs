using System;

namespace Cebritas.General.Cryptography {
    public static class SecurityTokenGenerator {
        public static string GenerateAuthenticationToken(string email) {
            string token = email + "&" + DateTime.Now.ToString("F") + "&" + Guid.NewGuid();
            return HashSumUtil.GetHashSum(token, HashSumType.SHA1);
        }

        public static string GenerateAccessToken(string email) {
            string token = email + "&" + DateTime.Now.ToString("F") + "&" + Guid.NewGuid();
            return HashSumUtil.GetHashSum(token, HashSumType.MD5);
        }

        public static string GenerateRandomActivationCode() {
            string token = DateTime.Now.ToString("F") + "&" + Guid.NewGuid();
            return HashSumUtil.GetHashSum(token, HashSumType.MD5);
        }

        public static string GenerateGuid() {
            return Guid.NewGuid().ToString("N");
        }
    }
}