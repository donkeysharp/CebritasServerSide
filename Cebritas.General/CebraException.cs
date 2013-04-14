using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.General {
    public class CebraException : Exception {
        public CebraException(string message)
            : base(message) {
        }
    }
}