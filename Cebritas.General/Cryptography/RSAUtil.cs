using System;
using System.Security.Cryptography;
using System.Text;

namespace Cebritas.General.Cryptography {
    public class RSAUtil {
        public string XmlPublicKey { get; set; }
        public string XmlPrivateKey { get; set; }

        private RSACryptoServiceProvider rsa;

        public RSAUtil() {
            this.rsa = new RSACryptoServiceProvider();
        }

        #region "Extra Encryption"
        public string EncryptTextToString(string text) {
            return GetBase64String(EncryptText(text));
        }

        public string DecryptTextToString(string text) {
            return GetEncodingString(DecryptText(text));
        }
        #endregion "Extra Encryption"

        #region "Core Encryption"
        public byte[] EncryptText(string text) {
            if (XmlPublicKey.Equals(string.Empty)) {
                throw new CebraException(Messages.PUBLIC_KEY_MUST_BE_ESTABLISHED);
            }
            try {
                byte[] textBytes = GetEncodingBytes(text);
                rsa.FromXmlString(XmlPublicKey);
                return rsa.Encrypt(textBytes, false);
            } catch (Exception) {
                throw new CebraException(Messages.ERROR_ENCRYPTING_TEXT);
            }
        }

        public byte[] DecryptText(string text) {
            if (XmlPrivateKey.Equals(string.Empty)) {
                throw new CebraException(Messages.PUBLIC_KEY_MUST_BE_ESTABLISHED);
            }
            try {
                byte[] textBytes = GetBase64Bytes(text);
                rsa.FromXmlString(XmlPrivateKey);
                return rsa.Decrypt(textBytes, false);
            } catch (Exception ex) {
                throw new CebraException(ex.Message);
            }
        }
        #endregion "Core Encryption"

        #region "Key Generation"
        public string GeneratePublicKey() {
            return rsa.ToXmlString(false);
        }

        public string GeneratePrivateKey() {
            return rsa.ToXmlString(true);
        }
        #endregion "Key Generation"

        #region "Util"
        private byte[] GetBase64Bytes(string item) {
            return Convert.FromBase64String(item);
        }

        private byte[] GetEncodingBytes(string item) {
            return Encoding.UTF8.GetBytes(item);
        }

        private string GetBase64String(byte[] bytes) {
            return Convert.ToBase64String(bytes);
        }

        private string GetEncodingString(byte[] bytes) {
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion "Util"
    }
}