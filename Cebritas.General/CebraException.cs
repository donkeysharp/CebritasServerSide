using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.General {
    public class CebraException : Exception {
        public int Status { get; set; }
        public CebraException(int status, string message)
            : base(message) {
            this.Status = status;
        }
        public CebraException(string message)
            : base(message) {
            this.Status = Constants.HTTP_BAD_REQUEST;
        }
    }
}