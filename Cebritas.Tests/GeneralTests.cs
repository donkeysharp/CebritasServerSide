using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cebritas.General;
using Cebritas.General.Cryptography;
using Cebritas.General.Networking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cebritas.Tests {
    [TestClass]
    public class GeneralTests {
        [TestMethod]
        public void Md5Test() {
            string md5Sum = HashSumUtil.GetHashSum("none", HashSumType.MD5);
            Assert.AreEqual<string>("334c4a4c42fdb79d7ebc3e73b517e6f8", md5Sum);
        }

        [TestMethod]
        public void SHA1Test() {
            string sha1Sum = HashSumUtil.GetHashSum("none", HashSumType.SHA1);
            Assert.AreEqual<string>("71f8e7976e4cbc4561c9d62fb283e7f788202acb", sha1Sum);
        }

        [TestMethod]
        public void SHA256Test() {
            string sha256Sum = HashSumUtil.GetHashSum("none", HashSumType.SHA256);
            Assert.AreEqual<string>("140bedbf9c3f6d56a9846d2ba7088798683f4da0c248231336e6a05679e4fdfe", sha256Sum);
        }

        [TestMethod]
        public void SHA512Test() {
            string sha512Sum = HashSumUtil.GetHashSum("none", HashSumType.SHA512);
            Assert.AreEqual<string>("ab93a9e95d70edb06025511cea4e2b8047fb7e1deaf7244fc0d3edf5e7cb57d8fb7b951bdeb3c6b552714878749eb19b9103e64a83635e8885c7d3e1d0fc5649", sha512Sum);
        }

        [TestMethod]
        public void SendEmail() {
            string mail = "serguimant@hotmail.com";
            string password = "xxxxxxx";
            EmailUtil x = new EmailUtil("smtp.live.com", 587, mail, password);
            x.AddRecipient("danieloo_123@hotmail.com");
            x.SetBody("test message");
            bool testPassed = false;
            try {
                x.SendMessage("test");
                testPassed = true;
            } catch (Exception) {
                testPassed = false;
            }

            Assert.AreEqual<bool>(true, testPassed);
        }

        [TestMethod]
        public void RSAEncryptTest() {
            try {
                RSAUtil rsa = new RSAUtil();
                rsa.XmlPublicKey = "<RSAKeyValue><Modulus>nE0NbiN/Qsg2DVHej0DY0AiDM+VWNhvyhExon6PU8h7uIc1c5WiwkuZV6JyxwpKl138mfIE9xXNN7hOQzIskyzxiAHhmOBrHISifaXwopaC2QWVCB4dV4MjrE0lLdlDnNgQDG4CSt8gYyw3UFM5LbR4vIP+hp79Jy3ZW9RjGNMk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
                rsa.XmlPrivateKey = "<RSAKeyValue><Modulus>nE0NbiN/Qsg2DVHej0DY0AiDM+VWNhvyhExon6PU8h7uIc1c5WiwkuZV6JyxwpKl138mfIE9xXNN7hOQzIskyzxiAHhmOBrHISifaXwopaC2QWVCB4dV4MjrE0lLdlDnNgQDG4CSt8gYyw3UFM5LbR4vIP+hp79Jy3ZW9RjGNMk=</Modulus><Exponent>AQAB</Exponent><P>zP+j08wOozlZHOmAi3unIZyTZj9LqvvT9Mlivbq/aZsQjzYaz56IaEpJWldbX+N7E7o0kmOM3UD99LTAE76/Rw==</P><Q>wy/cqyKA8DRqP6SEpYR8vIYH+WXhrDHj/Wf3DiiyczUk1XhMxmTRFC5HPVPnEjmVw2CflUpqnUQflJAZjakTbw==</Q><DP>vmASBpkUZuTVKxJ2PBLDbWV5RZU2cj2X41Y6irQpGqvUvwqh73nsd921LV6/Dte07ucX93LX2ImIzn4lerDD9Q==</DP><DQ>a7ZP6kjiKqxiLbjWUpjoVQkKAYFNpj7p9/+VgMTIpXcgWoVGqP0dvCtFuPxCOfZ5RRZfOn2UlDDx1IQo9dnmFQ==</DQ><InverseQ>Dbf2Lu+gIR826cteNJEVbRdclKz8OkHZEMnJ5w2wyLIA4VPf05DTKEPCDcBFuhphsASBWOIfEfwkze/ooafUkQ==</InverseQ><D>hdvbl6rg75HF8OxfnfIcfTX9H7HWfqq6rSE/LRFDa0SgDuTxHSvmpTiM9JVWC9xKGd+0V0bcX0DbyfyJsxOrouhNq5o8J2IbYCBH+MwBWfgJOVNvPNGnYypajwhe0AmYNZk/EoLc3BYKmGbfmSEHN1JMiW8dLaYHhykUq980Pu0=</D></RSAKeyValue>";

                string encrypted = rsa.EncryptTextToString("hola");
                string decrypted = rsa.DecryptTextToString(encrypted);
                Assert.AreEqual<string>("hola", decrypted);
            } catch (Exception e) {
                Assert.Fail(e.Message);
            }
        }
    }
}