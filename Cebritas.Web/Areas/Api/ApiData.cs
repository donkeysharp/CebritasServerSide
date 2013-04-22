using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cebritas.Web.Areas.Api {
    public class ApiData {
        /// <summary>
        /// RSA Private key
        /// </summary>
        public string PrivateKey { get; set; }
        /// <summary>
        /// RSA Public key
        /// </summary>
        public string PublicKey { get; set; }

        private static ApiData context = new ApiData();

        public static ApiData GetInstance() {
            return context;
        }

        private ApiData() {
            PrivateKey = "<RSAKeyValue><Modulus>nE0NbiN/Qsg2DVHej0DY0AiDM+VWNhvyhExon6PU8h7uIc1c5WiwkuZV6JyxwpKl138mfIE9xXNN7hOQzIskyzxiAHhmOBrHISifaXwopaC2QWVCB4dV4MjrE0lLdlDnNgQDG4CSt8gYyw3UFM5LbR4vIP+hp79Jy3ZW9RjGNMk=</Modulus><Exponent>AQAB</Exponent><P>zP+j08wOozlZHOmAi3unIZyTZj9LqvvT9Mlivbq/aZsQjzYaz56IaEpJWldbX+N7E7o0kmOM3UD99LTAE76/Rw==</P><Q>wy/cqyKA8DRqP6SEpYR8vIYH+WXhrDHj/Wf3DiiyczUk1XhMxmTRFC5HPVPnEjmVw2CflUpqnUQflJAZjakTbw==</Q><DP>vmASBpkUZuTVKxJ2PBLDbWV5RZU2cj2X41Y6irQpGqvUvwqh73nsd921LV6/Dte07ucX93LX2ImIzn4lerDD9Q==</DP><DQ>a7ZP6kjiKqxiLbjWUpjoVQkKAYFNpj7p9/+VgMTIpXcgWoVGqP0dvCtFuPxCOfZ5RRZfOn2UlDDx1IQo9dnmFQ==</DQ><InverseQ>Dbf2Lu+gIR826cteNJEVbRdclKz8OkHZEMnJ5w2wyLIA4VPf05DTKEPCDcBFuhphsASBWOIfEfwkze/ooafUkQ==</InverseQ><D>hdvbl6rg75HF8OxfnfIcfTX9H7HWfqq6rSE/LRFDa0SgDuTxHSvmpTiM9JVWC9xKGd+0V0bcX0DbyfyJsxOrouhNq5o8J2IbYCBH+MwBWfgJOVNvPNGnYypajwhe0AmYNZk/EoLc3BYKmGbfmSEHN1JMiW8dLaYHhykUq980Pu0=</D></RSAKeyValue>";
            PublicKey = "<RSAKeyValue><Modulus>nE0NbiN/Qsg2DVHej0DY0AiDM+VWNhvyhExon6PU8h7uIc1c5WiwkuZV6JyxwpKl138mfIE9xXNN7hOQzIskyzxiAHhmOBrHISifaXwopaC2QWVCB4dV4MjrE0lLdlDnNgQDG4CSt8gYyw3UFM5LbR4vIP+hp79Jy3ZW9RjGNMk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        }
    }
}