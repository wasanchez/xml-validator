using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XML.Validator.Exceptions
{
    internal class UnwellFormedXmlException : Exception
    {
        public UnwellFormedXmlException()
        {
        }

        public UnwellFormedXmlException(string? message) : base(message)
        {
        }

        public UnwellFormedXmlException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnwellFormedXmlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
